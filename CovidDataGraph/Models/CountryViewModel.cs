namespace CovidDataGraph.Models
{
    public class CountryViewModel
    {
        public CountryViewModel(string location, decimal populationDensity)
        {
            this.Name = location;
            this.PopulationDensity = populationDensity;
        }
        public string Name { get; private set; }
        public decimal PopulationDensity { get; private set; }
    }
}
