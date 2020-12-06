using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.Business;
using Route.DAL;
using Route.Helpers;
using Route.Models;
using Route.Models.Data;
using Route.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService routeService;
        private readonly IRouteCalculator routeCalculator;
        private readonly RouteAppContext context;
        private readonly IMapper mapper;

        public RouteController(
            IRouteService routeService,
            IRouteCalculator routeCalculator,
            RouteAppContext context,
            IMapper mapper)
        {
            this.routeService = routeService;
            this.routeCalculator = routeCalculator;
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult New(int? routeId)
        {
            if (routeId != null)
            {
                var route = context.RouteInfos.SingleOrDefault(r => r.Id == routeId);

                if (route == null)
                {
                    throw new Exception($"Route with id: {routeId} not found.");
                }

                return View(mapper.Map<RouteViewModel>(route));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(RouteViewModel routeViewModel)
        {
            // If model is empty display validation Error
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Get route info from API
            var routeResult = await routeService.GetRouteInfoFromGoogle(
                routeViewModel.InitialAddress,
                routeViewModel.DestinationAddress);

            if (!routeResult.Success)
            {
                ViewBag.ErrorMessage = routeResult.ErrorMessage;
                return View();
            }

            // Get coordinates based on address
            var initialAddressCoordResult = await routeService.GetCoordinateFromAddress(routeViewModel.InitialAddress);
            if (!initialAddressCoordResult.Success)
            {
                ViewBag.ErrorMessage = initialAddressCoordResult.ErrorMessage;
                return View();
            }

            var destinationAddressCoordResult = await routeService.GetCoordinateFromAddress(routeViewModel.DestinationAddress);
            if (!destinationAddressCoordResult.Success)
            {
                ViewBag.ErrorMessage = destinationAddressCoordResult.ErrorMessage;
                return View();
            }

            // Get Fuel consumption
            double routeFuelConsumption = routeCalculator.GetRouteFuelConsumption(routeViewModel.FuelConsumptionPer100, routeResult.TotalDistance.Value);

            // Save route info to db
            var routeToSave = new RouteInfo()
            {
                InitialAddress = routeViewModel.InitialAddress,
                InitialAddressCoords = Formater.GetLatLngCoords(initialAddressCoordResult),
                DestinationAddress = routeViewModel.DestinationAddress,
                DestinationAddressCoords = Formater.GetLatLngCoords(destinationAddressCoordResult),
                FuelConsumptionPer100 = routeViewModel.FuelConsumptionPer100,
                RouteFuelConsumption = routeFuelConsumption.ToString(),
                TotalDistance = routeResult.TotalDistance.Text,
                TotalDuration = routeResult.TotalDuration.Text
            };

            context.RouteInfos.Add(routeToSave);
            await context.SaveChangesAsync();

            // Go to route detail
            return RedirectToAction(actionName: "Detail", controllerName: "Route", routeValues: new { routeId = routeToSave.Id });
        }

        [HttpGet]
        public IActionResult Detail(int routeId)
        {
            var route = context.RouteInfos.SingleOrDefault(r => r.Id == routeId);

            if (route == null)
            {
                throw new Exception($"Route with id: {routeId} not found.");
            }

            var routeVM = mapper.Map<RouteViewModel>(route);

            return View(routeVM);
        }

        public IActionResult GetList()
        {
            var allRoutes = context.RouteInfos.ToList();

            var allRoutesVm = mapper.Map<List<RouteViewModel>>(allRoutes);

            return View(viewName: "RouteList", model: allRoutesVm);
        }
    }
}