using FluentAssertions;
using Rabobank.Training.ClassLibrary.ViewModels;
using Rabobank.Training.ClassLibrary.BusinessLayer;

namespace Rabobank.Training.ClassLibrary.Tests
{
    [TestClass]
    public class FundProcessorTests
    {

        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithValidFile.xml")]

        //Test Should Read Valid XML File Correctly
        public void ReadFundOfMandates_WithValidFile_HandleRequest()
        {
            // Arrange
            int counOfMandatesIntestFile = 4;
            string filePath = "FundOfMandatesDataWithValidFile.xml";

            IFundsProcessor fileProcessor = new FundProcessor();

            // Act
            var funds = fileProcessor.ReadFundOfMandatesFile(filePath);


            // Assert
            Assert.IsNotNull(funds);
            Assert.AreEqual(funds.Count, counOfMandatesIntestFile); //Assuming we already know count of mandates inside the test file.
            Assert.IsInstanceOfType(funds, typeof(List<FundOfMandates>));

            funds.Should().HaveCount(counOfMandatesIntestFile, "we have passed 4 mandates to XML");
            funds.Should().NotBeNull();
            funds.Should().BeAssignableTo(typeof(List<FundOfMandates>));

        }


        //Test Should Throw Error When Funds Mandates Are Not Available In File
        [TestMethod]
        [DeploymentItem("TestData//FundOfMandatesDataWithBlankFundsOfMandates.xml")]
        public void ReadFundOfMandates_WithBlankFile_ThrowError()
        {
            // Arrange            
            string filePath = "FundOfMandatesDataWithBlankFundsOfMandates.xml";

            IFundsProcessor fileProcessor = new FundProcessor();

            // Act
            var sut = FluentActions.Invoking(() => fileProcessor.ReadFundOfMandatesFile(filePath));

            // Assert
            sut.Should().Throw<Exception>().WithMessage("Unable to Read blank FundOfMandatesFile. Please check the file.");

        }

         
        //Test should Return StaticList Of Portfolios
        [TestMethod]
        public void GetPortfolios_ShouldReturnStaticList()
        {
            // Arrage
            var portfolio =MockData.GetMockPortfolioVMList();

            IFundsProcessor fundProcessor = new FundProcessor();

            // Act
            var sut = fundProcessor.GetPortfolio();

            // Assert
            sut.Should().NotBeNull().And.BeAssignableTo(typeof(PortfolioVM)).And.BeEquivalentTo(portfolio);

        } 

        //Test Should Add Liquidity Mandate As Additional Mandatein PositionVM
        [TestMethod]
        public void GetCalculatedMandates_ShouldAddLiquidityMandate_AsAdditionalMandateinPositionVM()
        {
            // Arrage

            var inputPosition = MockData.GetMockPositionVM();
            var outputPosition = MockData.GetMockPositionVM();
            outputPosition.Mandates=MockData.GetMockMandateVMList();
            var fundOfMandates =  MockData.GetMockFundOfMandates();

            IFundsProcessor fundsProcessor = new FundProcessor();

            // Act
            var outputPos = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);

            // Assert
            outputPos.Should().NotBeNull().And.BeOfType<PositionVM>().And.BeEquivalentTo(outputPosition);
            outputPos.Mandates.Should().HaveCount(5, "Because Liquidity should get added since we pass Liquidity allocation more than 0");
             
        }



        //Test Should Not AddLiquidity Mandate As Additional Mandatein PositionVM
        [TestMethod]
        public void GetCalculatedMandates_ShouldNotAddLiquidityMandate_AsAdditionalMandateinPositionVM()
        {
            //Arrange

            var inputPosition =MockData.GetMockPositionVM();
            var outputPosition = MockData.GetMockPositionVM();
            outputPosition.Mandates = MockData.GetMockMandateVMList();
            var fundOfMandates = MockData.GetMockFundOfMandates();

            IFundsProcessor fundsProcessor = new FundProcessor();

            // Act
            var sut = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);

            // Assert
            sut.Should().NotBeNull().And.BeOfType<PositionVM>().And.BeEquivalentTo(outputPosition);
             
        }



        //Test Should Not Make Any Changes To PositionVM Since InstrumentCode DoNo tMatch
        [TestMethod]
        public void GetCalculatedMandates_ShouldNotMakeAnyChangesToPositionVM_SinceInstrumentCodeDoNotMatch()
        {
            //Arrange

            var inputPosition = MockData.GetMockPositionVM();
            var outputPosition = MockData.GetMockPositionVM();
            var fundOfMandates = MockData.GetMockFundOfMandates();
            fundOfMandates.InstrumentCode = "Pos2";

            IFundsProcessor fundsProcessor = new FundProcessor();

            // Act
            var sut = fundsProcessor.GetCalculatedMandates(inputPosition, fundOfMandates);

            // Assert
            sut.Should().NotBeNull().And.BeOfType<PositionVM>().And.BeEquivalentTo(outputPosition);

        }

    }
}