using FluentAssertions;
using Rabobank.Training.ClassLibrary.BusinessLayer;

namespace Rabobank.Training.ClassLibrary.Tests
{
    [TestClass]
    public class FundProcessorTests
    {
        //Test Should Read Valid XML File Correctly
        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithValidFile.xml")]
        public void ReadFundOfMandates_WithValidFile_HandleRequest()
        {
            // Arrange
            int expectedCountMandatesFile = 4;
            string filePath = "FundOfMandatesDataWithValidFile.xml";

            IFundsProcessor fileProcessor = new FundProcessor();

            // Act
            var funds = fileProcessor.ReadFundOfMandatesFile(filePath);


            // Assert
            Assert.AreEqual(funds.Count, expectedCountMandatesFile);

        }


        //Test Should Throw Error When Funds Mandates Are Not Available In File
        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithEmptyFundsOfMandates.xml")]
        public void ReadFundOfMandates_WithEmptyFundsOfMandates_ThrowError()
        {
            // Arrange            
            string filePath = "FundOfMandatesDataWithEmptyFundsOfMandates.xml";

            IFundsProcessor fileProcessor = new FundProcessor();

            // Act
            var sut = FluentActions.Invoking(() => fileProcessor.ReadFundOfMandatesFile(filePath));

            // Assert
            sut.Should().Throw<Exception>().WithMessage("Invalid FundOfMandates file. Please check the file.");

        }


        //Test should Return StaticList Of Portfolios
        [TestMethod]
        public void GetPortfolios_ShouldReturnStaticList()
        {
            // Arrage
            var portfolio = MockData.GetMockPortfolioVMList();

            IFundsProcessor fundProcessor = new FundProcessor();

            // Act
            var sut = fundProcessor.GetPortfolio();

            // Assert
            sut.Should().BeEquivalentTo(portfolio);

        }

        //Test Should Add Liquidity Mandate As Additional Mandatein PositionVM
        [TestMethod]
        public void GetCalculatedMandates_ShouldAddLiquidityMandate_AsAdditionalMandateinPositionVM()
        {
            // Arrage

            var inputPosition = MockData.GetMockPositionVM();
            var outputPosition = MockData.GetMockPositionVM();
            outputPosition.Mandates = MockData.GetMockMandateVMList();
            var fundOfMandates = MockData.GetMockFundOfMandates();

            IFundsProcessor fundsProcessor = new FundProcessor();

            // Act
            var outputPos = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);

            // Assert
            outputPos.Should().BeEquivalentTo(outputPosition);
            outputPos.Mandates.Should().BeEquivalentTo(outputPosition.Mandates);

        }



        //Test Should Not AddLiquidity Mandate As Additional Mandatein PositionVM
        [TestMethod]
        public void GetCalculatedMandates_ShouldNotAddLiquidityMandate_AsAdditionalMandateinPositionVM()
        {
            //Arrange

            var inputPosition = MockData.GetMockPositionVM();
            var outputPosition = MockData.GetMockPositionVM();
            outputPosition.Mandates = MockData.GetMockMandateVMList();
            var fundOfMandates = MockData.GetMockFundOfMandates();

            IFundsProcessor fundsProcessor = new FundProcessor();

            // Act
            var sut = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);

            // Assert
            sut.Should().BeEquivalentTo(outputPosition);

        }



        //Test Should Not Make Any Changes To PositionVM Since InstrumentCode DoNo tMatch
        [TestMethod]
        public void GetCalculatedMandates_ShouldNotMakeAnyChangesToPositionVM_SinceInstrumentCodeDoNotMatch()
        {
            //Arrange

            var inputPosition = MockData.GetMockPositionVM();
            var outputPosition = MockData.GetMockPositionVM();
            var fundOfMandates = MockData.GetMockFundOfMandates();
            fundOfMandates.InstrumentCode = "Test2";

            IFundsProcessor fundsProcessor = new FundProcessor();

            // Act
            var sut = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);

            // Assert
            sut.Should().BeEquivalentTo(outputPosition);

        }

    }
}