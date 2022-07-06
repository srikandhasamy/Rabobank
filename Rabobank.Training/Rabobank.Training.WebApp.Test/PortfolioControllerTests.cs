using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Rabobank.Training.WebApp.Controllers;
using Rabobank.Training.ClassLibrary.ViewModels;
using Rabobank.Training.ClassLibrary.BusinessLayer;


namespace Rabobank.Training.WebApp.Test
{
    [TestClass]
    public class PortfolioControllerTests
    {
        private readonly Mock<IPortfolioProcessor> portfolioProcessor = new();
        private readonly Mock<ILogger<PortfolioController>> logger = new();

        //Test Should Return Correct PortfolioObject Back 
        [TestMethod]
        public void Get_ShouldReturnCorrectPortfolioObjectBack_RunSuccessfully()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            // Arrange
            var dummyPortfolio = new PortfolioVM
            {
                Positions = new List<PositionVM> {

                     new PositionVM { Code="NL0000287100", Name="Henekens", Value=12345 },
                     new PositionVM { Code="NL000029332", Name="Optimix", Value=23456 },
                     new PositionVM { Code="NL0000440584", Name="DP Global", Value=34567 },
                     new PositionVM { Code="NL0000440588", Name="Rabobank core", Value=45678 },
                     new PositionVM { Code="inc005", Name="Morgan Stanley", Value=56789 },
                     new PositionVM { Code="inc005", Name="Morgan Stanley", Value=56789 }
                    }
            };


            portfolioProcessor.Setup(p => p.GetUpdatedPortfolio(It.IsAny<string>())).Returns(dummyPortfolio);

            var sut = new PortfolioController(portfolioProcessor.Object, config, logger.Object);

            // Act
            var httpResult = sut.Get();

            // Assert
            httpResult.Should().HaveCount(6, "Because we passed 6 Positions in dummy return object");

        }

        //Test Should Throw ArgumentException If Portfolio Returned Is Null

        [TestMethod]
        public void Get_ShouldThrowArgumentException_IfPortfolioReturnedIsNull()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            // Arrange 
            PortfolioVM? portfolio = null;

            portfolioProcessor.Setup(p => p.GetUpdatedPortfolio(It.IsAny<string>())).Returns(portfolio!);

            var sut = new PortfolioController(portfolioProcessor.Object, config, logger.Object);
            
            // Act
            Func<PositionVM[]> func = () => sut.Get();

            // Assert
            func.Should().Throw<ArgumentException>("Because GetPortfolio returns null here and client code mus throw an Argument Exception").WithMessage("Necessary Portfolio is not available to display");

        }


        //Test Should Throw Exception If PortfolioObject Does Not HavePositions
        [TestMethod]
        public void Get_ShouldThrowException_IfPortfolioObjectDoesNotHavePositions()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json").Build();

            // Arrange 
            var portfolio = new PortfolioVM
            {
                Positions = null

            };

            portfolioProcessor.Setup(p => p.GetUpdatedPortfolio(It.IsAny<string>())).Returns(portfolio);

            var sut = new PortfolioController(portfolioProcessor.Object, config, logger.Object); 
            
            // Act
            Func<PositionVM[]> func = () => sut.Get();

            // Assert
            func.Should().Throw<Exception>("Because GetPortfolio returns no Positions in Portfolio and client code must throw an exception").WithMessage("No Valid Positions found under portfolio.");

        }

    }
}