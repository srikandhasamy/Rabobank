using Rabobank.Training.ClassLibrary.ViewModels;

namespace Rabobank.Training.ClassLibrary.BusinessLayer
{
    public interface IFundsProcessor
    {
        List<FundOfMandates> GetFundOfMandates(string fileName);
        PortfolioVM GetPortfolio();
        PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fundOfmandates);
    }
}
