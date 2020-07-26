using System.Collections.Generic;
using CovidDataGraph.Models;
using NUnit.Framework;
using FluentAssertions;

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
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            Assert.That(actual, Has.Exactly(10).Items, $"Should return 10 countries, but returned {countries.Count}");
        }
        [Test]
        public void OrderedByDiabetesPrevalenceDescending()
        {
            // Arrange
            var countries = new Dictionary<string, Country>
            {
                {"USA", new Country { Location = "USA", Diabetes_Prevalence = 6.8m }},
                {"A", new Country { Location = "A", Diabetes_Prevalence = 6.8m }},
                {"B", new Country { Location = "B", Diabetes_Prevalence = 62.8m }},
                {"C", new Country { Location = "C", Diabetes_Prevalence = 36.8m }},
                {"D", new Country { Location = "D", Diabetes_Prevalence = 5.3m }},
                {"E", new Country { Location = "E", Diabetes_Prevalence = 1.2m }},
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
                {"A", new Country { Diabetes_Prevalence = STD_DEV_GREATER_THAN_USA}},
                {"B", new Country { Diabetes_Prevalence = 87.3m}},
                {"C", new Country { Diabetes_Prevalence = 23m}},
                {"D", new Country { Diabetes_Prevalence = 21m}},
                {"E", new Country { Diabetes_Prevalence = 1m}},
                {"F", new Country { Diabetes_Prevalence = 3m}},
                {"G", new Country { Diabetes_Prevalence = 5m}},
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
                {"A", new Country { Diabetes_Prevalence = STD_DEV_GREATER_THAN_USA}},
                {"B", new Country { Diabetes_Prevalence = 87.3m}},
                {"C", new Country { Diabetes_Prevalence = 23m}},
                {"D", new Country { Diabetes_Prevalence = 21m}},
                {"E", new Country { Diabetes_Prevalence = OUTSIDE_STD_DEV_OF_USA}},
                {"F", new Country { Diabetes_Prevalence = 3m}},
                {"G", new Country { Diabetes_Prevalence = 5m}},
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
                {"A", new Country { Diabetes_Prevalence = 5.6m }},
                {"B", new Country { Diabetes_Prevalence = STD_DEV_LOWER_THAN_USA }},
                {"C", new Country { Diabetes_Prevalence = 1m }},
                {"D", new Country { Diabetes_Prevalence = 1m }},
                {"E", new Country { Diabetes_Prevalence = 2m }},
                {"F", new Country { Diabetes_Prevalence = 78m }},
                {"G", new Country { Diabetes_Prevalence = 1m }},
                {"H", new Country { Diabetes_Prevalence = 51m }},
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
                {"A", new Country { Diabetes_Prevalence = 5.6m }},
                {"B", new Country { Diabetes_Prevalence = 56.3m }},
                {"C", new Country { Diabetes_Prevalence = 1m }},
                {"D", new Country { Diabetes_Prevalence = 1m }},
                {"E", new Country { Diabetes_Prevalence = 2m }},
                {"F", new Country { Diabetes_Prevalence = 78m }},
                {"G", new Country { Diabetes_Prevalence = 1m }},
                {"H", new Country { Diabetes_Prevalence = 51m }},
                {"I", new Country { Diabetes_Prevalence = OUTSIDE_STD_DEV_OF_USA }},
            };

            // Act
            var actual = countries.GetTopByDiabetesPrevalence();

            // Assert
            actual.Should().NotContain(x => x.DiabetesPrevalence == OUTSIDE_STD_DEV_OF_USA);
        }
    }
}
