using System;
using Xunit;
using WeatherApi.Services;

namespace WeatherApi.Tests.Services {
    public class WindWarningTests {

        [Fact]
        public void NoWarningWhenWindSpeedIsUnder50() {
            var windSpeed = 49.9;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);
            
            Assert.Null(warning);
        }

        [Fact]
        public void YellowWarningWhenWindSpeedIs50() {
            var windSpeed = 50.0;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);

            Assert.Equal("yellow", warning.ToLower());
        }

        [Fact]
        public void YellowWarningWhenWindSpeedIs59() {
            var windSpeed = 59.9;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);

            Assert.Equal("yellow", warning.ToLower());
        }        

        [Fact]
        public void AmberWarningWhenWindSpeedIs60() {
            var windSpeed = 60.0;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);

            Assert.Equal("amber", warning.ToLower());
        }

        [Fact]
        public void AmberWarningWhenWindSpeedIs69() {
            var windSpeed = 69.9;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);

            Assert.Equal("amber", warning.ToLower());
        }         

        [Fact]
        public void RedWarningWhenWindSpeedIs70() {
            var windSpeed = 70.0;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);

            Assert.Equal("red", warning.ToLower());
        }

        [Fact]
        public void RedWarningWhenWindSpeedIs80() {
            var windSpeed = 80.0;

            var warning = WindWarning.GetWarningLevel((float) windSpeed);

            Assert.Equal("red", warning.ToLower());
        }                        
    }
}