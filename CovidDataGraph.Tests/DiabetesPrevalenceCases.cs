using CovidDataGraph.Models;
using FluentAssertions;
using NUnit.Framework;
using RandomTestValues;

using System.Collections.Generic;
using System.Linq;

namespace CovidDataGraph.Tests
{
    public class DiabetesPrevalenceCases
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
        public void OrderedByDiabetesPrevalenceDescending()
        {
            // Arrange
            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country {Diabetes_Prevalence = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Diabetes_Prevalence = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Diabetes_Prevalence = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Diabetes_Prevalence = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Diabetes_Prevalence = RandomValue.Decimal()}},
                {RandomValue.String(), new Country {Diabetes_Prevalence = RandomValue.Decimal()}},
            };

            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            Assert.That(actual, Is.Ordered.Descending.By("DiabetesPrevalence"), "Should return countries order by diabetes prevalence");
        }

        [Test]
        public void IncludesSimilarCountrieWithinStdDevGreater()
        {
            // Arrange
            const decimal STD_DEV_GREATER_THAN_USA = 86m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Diabetes_Prevalence = 66m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = STD_DEV_GREATER_THAN_USA}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 87.3m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 23m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 21m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 3m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 5m}},
            };
            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            actual.Should().Contain(x => x.DiabetesPrevalence == STD_DEV_GREATER_THAN_USA);
        }

        [Test]
        public void ExcludesCountriesOutsideStdDevLower()
        {
            // Arrange
            const decimal STD_DEV_GREATER_THAN_USA = 86m;
            const decimal OUTSIDE_STD_DEV_OF_USA = 1m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Diabetes_Prevalence = 66m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = STD_DEV_GREATER_THAN_USA}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 87.3m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 23m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 21m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = OUTSIDE_STD_DEV_OF_USA}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 3m}},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 5m}},
            };
            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            actual.Should().NotContain(x => x.DiabetesPrevalence == OUTSIDE_STD_DEV_OF_USA);
        }

        [Test]
        public void IncludesSimilarCountriesWithinStdDevLower()
        {
            // Arrange
            const decimal STD_DEV_LOWER_THAN_USA = 56.3m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Diabetes_Prevalence = 58m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 5.6m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = STD_DEV_LOWER_THAN_USA }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 2m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 78m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 51m }},
            };

            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            actual.Should().Contain(x => x.DiabetesPrevalence == STD_DEV_LOWER_THAN_USA);
        }

        [Test]
        public void ExcludeCountriesOutsideStdDevHigher()
        {
            // Arrange
            const decimal OUTSIDE_STD_DEV_OF_USA = 150m;

            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Diabetes_Prevalence = 58m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 5.6m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 56.3m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 2m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 78m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 1m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = 51m }},
                {RandomValue.String(), new Country { Diabetes_Prevalence = OUTSIDE_STD_DEV_OF_USA }},
            };

            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            actual.Should().NotContain(x => x.DiabetesPrevalence == OUTSIDE_STD_DEV_OF_USA);
        }
    }
}
