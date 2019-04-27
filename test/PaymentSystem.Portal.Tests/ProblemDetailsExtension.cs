using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace PaymentSystem.Portal.Tests
{
    public static class ProblemDetailsExtension
    {
        public static async Task<ProblemDetails> ProblemDetails(this HttpResponseMessage message)
        {
            return JsonConvert.DeserializeObject<ProblemDetails>(await message.Content.ReadAsStringAsync());
        }
    }
}