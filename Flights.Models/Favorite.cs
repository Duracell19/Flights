﻿using System.Collections.Generic;

namespace Flights.Models
{
    public class Favorite
    {
        public List<string> CitiesFrom { get; set; }
        public List<string> CitiesTo { get; set; }
        public string CityFrom { get; set; }
        public string CityTo { get; set; }
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
        public List<string> IataFrom { get; set; }
        public List<string> IataTo { get; set; }
    }
}
