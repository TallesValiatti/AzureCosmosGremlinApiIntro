using AzureCosmosGremlinApiIntro.Models;
using AzureCosmosGremlinApiIntro.Services.Interfaces;

namespace AzureCosmosGremlinApiIntro.Services
{
    public class SignInValidatorService : ISignInValidatorService
    {
        private readonly IGraphRepository _graphRepository;

        public SignInValidatorService(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }

        public async Task<bool> IsValidAsync(SignInData data)
        {
            if(!await _graphRepository.CustomerExistsAsync(data.Email))
                return false;

            var city = (await _graphRepository.GetCityAsync(data.Email)).ToLower();
            if(!city.Equals(data.City.ToLower()))
                return false;

            var bankAccountNumber = (await _graphRepository.GetBankAccountAsync(data.Email)).ToLower();
            if(!bankAccountNumber.Equals(data.BankAccountNumber.ToLower()))
                return false;

            var devices = await _graphRepository.GetAllDevicesAsync(data.Email);
            if(!devices.Any(x => x.Equals(data.DeviceUdid)))
                return false;

            var phoneNumbers = await _graphRepository.GetAllPhoneNumbersAsync(data.Email);
            if(!phoneNumbers.Any(x => x.ToLower().Equals(data.PhoneNumber.ToLower())))
                return false;

            return true;
        }
    }
}