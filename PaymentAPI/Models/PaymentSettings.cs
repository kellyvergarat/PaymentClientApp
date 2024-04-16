namespace PaymentAPI.Models
{
    //Good practice for design patterns : dependency injection
    public interface IPaymentSettings
    {
        string Server { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }

    public class PaymentSettings : IPaymentSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }

    }
}
