using System.Collections.Generic;
using CovidDataGraph.Models;
using NUnit.Framework;
using FluentAssertions;

namespace CovidDataGraph.Tests
{
    public class PopulationDensityCases
    {
        [Test]
        public void ReturnsTenCountries()
        {
            // Arrange
            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country()},
                {"A", new Country()},
                {"B", new Country()},
                {"C", new Country()},
                {"D", new Country()},
                {"E", new Country()},
                {"F", new Country()},
                {"G", new Country()},
                {"H", new Country()},
                {"I", new Country()},
                {"J", new Country()},
                {"K", new Country()},
                {"L", new Country()},
                {"M", new Country()},
                {"N", new Country()},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            Assert.That(actual, Has.Exactly(10).Items, $"Should return 10 countries, but returned {countries.Count}");
        }

        [Test]
        public void OrderedByPopulationDensityDescending()
        {
            // Arrange
            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Location = "USA", Population_Density = 2 }},
                {"A", new Country { Location = "A", Population_Density = 1}},
                {"B", new Country { Location = "B", Population_Density = 7}},
                {"C", new Country { Location = "C", Population_Density = 3}},
                {"D", new Country { Location = "D", Population_Density = 1}},
                {"E", new Country { Location = "E", Population_Density = 2}},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            Assert.That(actual, Is.Ordered.Descending.By("PopulationDensity"), "Should return countries ordered by population density");
        }

        [Test]
        public void IncludesSimilarCountriesWithinStdDevGreater()
        {
            // Arrange
            const decimal STD_DEV_GREATER_THAN_USA = 56m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Population_Density = 38m }},
                {"A", new Country { Population_Density = 1.5m }},
                {"B", new Country { Population_Density = 15m }},
                {"C", new Country { Population_Density = 1.8m }},
                {"D", new Country { Population_Density = 3m }},
                {"E", new Country { Population_Density = STD_DEV_GREATER_THAN_USA }},
                {"F", new Country { Population_Density = 1m }},
                {"G", new Country { Population_Density = 14m }},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            actual.Should().Contain(x => x.PopulationDensity == STD_DEV_GREATER_THAN_USA);
        }

        [Test]
        public void ExcludesCountriesOutsideStdDevGreater()
        {
            // Arrange
            const decimal STD_DEV_GREATER_THAN_USA = 56m;
            const decimal OUTSIDE_STD_DEV_OF_USA = 100m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Population_Density = 38m }},
                {"A", new Country { Population_Density = 1.5m }},
                {"B", new Country { Population_Density = 15m }},
                {"C", new Country { Population_Density = 1.8m }},
                {"D", new Country { Population_Density = 3m }},
                {"E", new Country { Population_Density = STD_DEV_GREATER_THAN_USA }},
                {"F", new Country { Population_Density = 1m }},
                {"G", new Country { Population_Density = 14m }},
                {"H", new Country { Population_Density = OUTSIDE_STD_DEV_OF_USA}},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            actual.Should().NotContain(x => x.PopulationDensity == OUTSIDE_STD_DEV_OF_USA);
        }

        [Test]
        public void ExcludesCountriesOutsideStdDevLower()
        {
            // Arrange
            const decimal OUTSIDE_STD_DEV_OF_USA = 15m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Population_Density = 38m }},
                {"A", new Country { Population_Density = 1.5m }},
                {"B", new Country { Population_Density = OUTSIDE_STD_DEV_OF_USA }},
                {"C", new Country { Population_Density = 1.8m }},
                {"D", new Country { Population_Density = 3m }},
                {"E", new Country { Population_Density = 56m }},
                {"F", new Country { Population_Density = 1m }},
                {"G", new Country { Population_Density = 14m }},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            actual.Should().NotContain(x => x.PopulationDensity == OUTSIDE_STD_DEV_OF_USA);
        }

        [Test]
        public void IncludeSimilarCountriesWithinStdDevLower()
        {
            // Arrange
            const decimal STD_DEV_LOWER_THAN_USA = 20.5m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Population_Density = 38m }},
                {"A", new Country { Population_Density = 1.5m }},
                {"B", new Country { Population_Density = 15m }},
                {"C", new Country { Population_Density = 1.8m }},
                {"D", new Country { Population_Density = 3m }},
                {"E", new Country { Population_Density = 56m }},
                {"F", new Country { Population_Density = 1m }},
                {"G", new Country { Population_Density = 14m }},
                {"H", new Country { Population_Density = STD_DEV_LOWER_THAN_USA }},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            actual.Should().Contain(x => x.PopulationDensity == STD_DEV_LOWER_THAN_USA);
        }
    }
}