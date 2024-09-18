using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using aws_secrets_manager.Utility;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace aws_secrets_manager.Services
{
    public static class AwsSecretsManagerService
    {
        private static readonly IAmazonSecretsManager _secretsManager = new AmazonSecretsManagerClient();
        private static readonly string secretName = Environment.GetEnvironmentVariable("AWS_SECRET_NAME");
        private static readonly string region = Environment.GetEnvironmentVariable("AWS_REGION");

        public static async Task<Dictionary<string, string>> GetSecretAsDictionaryAsync()
        {
            if (string.IsNullOrEmpty(secretName) || string.IsNullOrEmpty(secretName))
            {
                throw new InvalidOperationException("Secret name or region is not set in environment variables.");
            }

            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            try
            {
                var response = await _secretsManager.GetSecretValueAsync(request);

                if (response.SecretString != null)
                {
                    var secretData = JsonDocument.Parse(response.SecretString);
                    return Helper.ParseJsonToDictionary(secretData.RootElement);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            return null;
        }
    }
}
