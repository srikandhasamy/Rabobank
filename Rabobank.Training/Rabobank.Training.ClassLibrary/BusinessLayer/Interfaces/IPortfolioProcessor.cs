using Rabobank.Training.ClassLibrary.ViewModels;

namespace Rabobank.Training.ClassLibrary.BusinessLayer
{
    public interface IPortfolioProcessor
    {
        PortfolioVM GetUpdatedPortfolio(string fileName);
    }
}
