using System.Collections.Generic;

namespace CovidDataGraph.Models
{
    public class Country
    {
        public string Location { get; set; }
        public decimal Population { get; set; }
        public decimal Population_Density { get; set; } = 0;
        public decimal Diabetes_Prevalence { get; set; }
        public IEnumerable<Data> Data { get; set; }
    }
}
