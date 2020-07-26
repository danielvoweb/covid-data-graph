namespace CovidDataGraph.Models
{
    public class CountryViewModel
    {
        public CountryViewModel(string location, decimal populationDensity, decimal diabetesPrevalence)
        {
            this.Name = location;
            this.PopulationDensity = populationDensity;
            this.DiabetesPrevalence = diabetesPrevalence;
        }
        public string Name { get; private set; }
        public decimal PopulationDensity { get; private set; }
        public decimal DiabetesPrevalence { get; private set; }
    }
}
