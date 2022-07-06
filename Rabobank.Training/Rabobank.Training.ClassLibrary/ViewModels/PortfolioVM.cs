namespace Rabobank.Training.ClassLibrary.ViewModels
{
    /// <summary>
    /// View Model class to be used by Front End and 
    /// Contains list of Positions which are currently static based on the given requirement.
    /// </summary>
    public class PortfolioVM
    {
        public List<PositionVM>? Positions { get; set; }
    }
}
