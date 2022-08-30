#nullable disable
namespace AzureCosmosGremlinApiIntro.Models
{
    public class SignInData
    {
        public string Email { get; set; }
        public string City { get; set; }
        public string DeviceUdid { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
    }
}