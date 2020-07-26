using CovidDataGraph.Models;
using FluentAssertions;
using NUnit.Framework;
using RandomTestValues;

using System.Collections.Generic;
using System.Linq;

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
                {"USA", RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
                {RandomValue.String(), RandomValue.Object<Country>()},
            };

            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            Assert.That(actual.Count(), Is.LessThanOrEqualTo(10), $"Should return 10 countries at most, but returned {countries.Count}");
        }

        [Test]
        public void OrderedByPopulationDensityDescending()
        {
            // Arrange
            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Location = "USA", Population_Density = 2 }},
                {RandomValue.String(), new Country {Population_Density = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Population_Density = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Population_Density = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Population_Density = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Population_Density = RandomValue.Decimal()}},
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
                {RandomValue.String(), new Country { Population_Density = 1.5m }},
                {RandomValue.String(), new Country { Population_Density = 15m }},
                {RandomValue.String(), new Country { Population_Density = 1.8m }},
                {RandomValue.String(), new Country { Population_Density = 3m }},
                {RandomValue.String(), new Country { Population_Density = STD_DEV_GREATER_THAN_USA }},
                {RandomValue.String(), new Country { Population_Density = 1m }},
                {RandomValue.String(), new Country { Population_Density = 14m }},
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
                {RandomValue.String(), new Country { Population_Density = 1.5m }},
                {RandomValue.String(), new Country { Population_Density = 15m }},
                {RandomValue.String(), new Country { Population_Density = 1.8m }},
                {RandomValue.String(), new Country { Population_Density = 3m }},
                {RandomValue.String(), new Country { Population_Density = STD_DEV_GREATER_THAN_USA }},
                {RandomValue.String(), new Country { Population_Density = 1m }},
                {RandomValue.String(), new Country { Population_Density = 14m }},
                {RandomValue.String(), new Country { Population_Density = OUTSIDE_STD_DEV_OF_USA}},
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
                {RandomValue.String(), new Country { Population_Density = 1.5m }},
                {RandomValue.String(), new Country { Population_Density = OUTSIDE_STD_DEV_OF_USA }},
                {RandomValue.String(), new Country { Population_Density = 1.8m }},
                {RandomValue.String(), new Country { Population_Density = 3m }},
                {RandomValue.String(), new Country { Population_Density = 56m }},
                {RandomValue.String(), new Country { Population_Density = 1m }},
                {RandomValue.String(), new Country { Population_Density = 14m }},
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
                {RandomValue.String(), new Country { Population_Density = 1.5m }},
                {RandomValue.String(), new Country { Population_Density = 15m }},
                {RandomValue.String(), new Country { Population_Density = 1.8m }},
                {RandomValue.String(), new Country { Population_Density = 3m }},
                {RandomValue.String(), new Country { Population_Density = 56m }},
                {RandomValue.String(), new Country { Population_Density = 1m }},
                {RandomValue.String(), new Country { Population_Density = 14m }},
                {RandomValue.String(), new Country { Population_Density = STD_DEV_LOWER_THAN_USA }},
            };

            // Act
            var actual = countries.GetTopByPopDensity();

            // Assert
            actual.Should().Contain(x => x.PopulationDensity == STD_DEV_LOWER_THAN_USA);
        }
    }
}