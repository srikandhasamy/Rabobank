using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabobank.Training.ClassLibrary.BusinessLayer;
using Rabobank.Training.ClassLibrary.ViewModels;

namespace Rabobank.Training.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioProcessor? _portfolioProcessor = null;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        /// <SUMMARY>
        /// Cunstructor Will Initialize Portfolioprocessor service 
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public PortfolioController(IPortfolioProcessor processor, IConfiguration config, ILogger<PortfolioController> logger)
        {
            _portfolioProcessor = processor;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// It read XML data and process to return portfolio position 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PositionVM[] GetPosition()
        {
            PositionVM[] positions;
            try
            {
                var fundsFilePath = _config["FundsOfMandatesFile"];
                var portfolioViewModel = _portfolioProcessor!.GetUpdatedPortfolio(fundsFilePath);

                if (portfolioViewModel == null)
                {
                    throw new ArgumentException("Portfolio returned a null argument");
                }
                if (portfolioViewModel.Positions == null || portfolioViewModel.Positions.Count == 0)
                {
                    throw new ArgumentException("Portfolio Positions returned a null argument.");
                }

                positions = portfolioViewModel.Positions.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occered while retrieving Positions from Portfolio");
                throw;
            }

            return positions;
        }

    }
}
