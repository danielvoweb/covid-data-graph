using System.Collections.Generic;
using System.Linq;
using CovidDataGraph.Models;

namespace CovidDataGraph
{
    public static class BusinessLogic
    {
        private const string USA = "USA";
        public static IEnumerable<CountryViewModel> GetTopByPopDensity(this IDictionary<string, Country> countries)
        {
            var countryViewModels = countries.Select(x => new CountryViewModel(
                x.Value.Location, x.Value.Population_Density, x.Value.Diabetes_Prevalence));
            var stdDev = countryViewModels.Select(x => x.PopulationDensity).StdDev();
            var us_PopulationDensity = countries.First(x => x.Key == USA).Value.Population_Density;

            return countryViewModels
                .Where(x => x.PopulationDensity <= us_PopulationDensity + stdDev)
                .Where(x => x.PopulationDensity >= us_PopulationDensity - stdDev)
                .OrderByDescending(x => x.PopulationDensity)
                .Take(10);
        }

        public static IEnumerable<CountryViewModel> GetTopByDiabetesPrevalence(this IDictionary<string, Country> countries)
        {
            var countryViewModels = countries.Select(x => new CountryViewModel(
                    x.Value.Location, x.Value.Population_Density, x.Value.Diabetes_Prevalence));
            var stdDev = countryViewModels.Select(x => x.DiabetesPrevalence).StdDev();
            var us_DiabetesPrevalence = countries.First(x => x.Key == USA).Value.Diabetes_Prevalence;

            return countryViewModels
                .Where(x => x.DiabetesPrevalence <= us_DiabetesPrevalence + stdDev)
                .Where(x => x.DiabetesPrevalence >= us_DiabetesPrevalence - stdDev)
                .OrderByDescending(x => x.DiabetesPrevalence)
                .Take(10);
        }
    }
}
