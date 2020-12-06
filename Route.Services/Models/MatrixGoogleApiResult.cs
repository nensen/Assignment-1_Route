using Route.Models;
using System.Collections.Generic;

namespace Route.Services.Models
{
    public class MatrixGoogleApiResult
    {
        public string Status { get; set; }

        public List<MatrixGoogleApiRow> Rows { get; set; }
    }

    public class MatrixGoogleApiRow
    {
        public List<MatrixGoogleApiElement> Elements { get; set; }
    }

    public class MatrixGoogleApiElement
    {
        public TextValueWrapper Distance { get; set; }

        public TextValueWrapper Duration { get; set; }

        public string Status { get; set; }
    }
}