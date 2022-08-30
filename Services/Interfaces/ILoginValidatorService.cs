using AzureCosmosGremlinApiIntro.Models;

namespace AzureCosmosGremlinApiIntro.Services.Interfaces
{
    public interface ISignInValidatorService
    {
        Task<bool> IsValidAsync(SignInData data);
    }
}