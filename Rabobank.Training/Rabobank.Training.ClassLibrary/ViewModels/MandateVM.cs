namespace Rabobank.Training.ClassLibrary.ViewModels
    
{
    /// <summary>
    /// View Model class to be used by Front End and Maps to Mandate Entity
    /// </summary>
    public class MandateVM
    {
        public string? Name { get; set; }
        public decimal Allocation { get; set; }
        public decimal Value { get; set; }
    }
}
