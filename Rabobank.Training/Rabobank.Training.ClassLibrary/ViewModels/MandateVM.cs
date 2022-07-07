namespace Rabobank.Training.ClassLibrary.ViewModels

{
    /// <summary>
    /// Mandate View Model class to be used by client side and Maps to Mandate Entity
    /// </summary>
    public class MandateVM
    {
        public string? Name { get; set; }
        public decimal Allocation { get; set; }
        public decimal Value { get; set; }
    }
}
