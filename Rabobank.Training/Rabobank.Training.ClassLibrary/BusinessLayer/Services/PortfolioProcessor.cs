using Rabobank.Training.ClassLibrary.ViewModels;

namespace Rabobank.Training.ClassLibrary.BusinessLayer
{
    /// <summary>
    /// Get updated Portfolio by calling FundProcessor for all data manupulation based on the filename.
    /// </summary>
    public class PortfolioProcessor : IPortfolioProcessor
    {
        public PortfolioProcessor(IFundsProcessor fundsProcessor)
        {
            FundsProcessor = fundsProcessor;
        }

        public IFundsProcessor FundsProcessor { get; set; }

        public PortfolioVM GetUpdatedPortfolio(string fileName)
        {
            PortfolioVM? portfolioVM = null;
            List<FundOfMandates>? mandates = null;
            
            portfolioVM = FundsProcessor.GetPortfolio();
            mandates = FundsProcessor.ReadFundOfMandatesFile(fileName);

            portfolioVM.Positions!.ForEach(position =>
            {
                mandates.ForEach(fundofmandate =>
                {
                    position = FundsProcessor.GetCalculatedMandates(position, fundofmandate);
                });
            });

            return portfolioVM;
        }
    }

}
