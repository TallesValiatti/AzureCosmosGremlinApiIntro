namespace AzureCosmosGremlinApiIntro.Services.Interfaces
{
    public interface IGraphRepository
    {
        Task<bool> CustomerExistsAsync(string email);
        Task<IEnumerable<string>> GetAllPhoneNumbersAsync(string email);
        Task<IEnumerable<string>> GetAllDevicesAsync(string email);
        Task<string> GetBankAccountAsync(string email);
        Task<string> GetCityAsync(string email);
    }
}