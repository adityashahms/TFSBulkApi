using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace TFSBulkApi
{
    public static class ApiHelper
    {
        private static readonly string BaseUrl = "https://dev.azure.com/{organization}/{project}/_apis/wit/classificationnodes";
        private static readonly string ApiVersion = "7.0";
        private static readonly string Organization = "adityaShahTest"; // Replace with your organization
        private static readonly string Project = "TestingAPI"; // Replace with your project
        private static readonly string PatToken = "BbRiv7dSdYnX9fe8GMk2xBrUZDnGlFa3Gmi8sYQayJr0Mp3IO5MNJQQJ99AKACAAAAAAArohAAASAZDOvf1M"; // Replace with your Personal Access Token (PAT)

        public static async Task<string> DeleteClassificationNodeAsync(ClassificationNode node)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{PatToken}")));

                // Construct the full URL for the classification node endpoint
                var url = $"{BaseUrl}/{node.StructureGroup}/{node.Path}?api-version={ApiVersion}"
                    .Replace("{organization}", Organization)
                    .Replace("{project}", Project);

                // Send PUT request (Azure DevOps uses PUT for create/update)
                // var response = await client.PutAsync(url, content);
                var response = await client.DeleteAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create or update node. Status: {response.StatusCode}. Details: {errorMessage}");
                }
            }
        }

        public static async Task<string> CreateOrUpdateClassificationNodeAsync(ClassificationNode node)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{PatToken}")));

                // Construct the full URL for the classification node endpoint
                var url = $"{BaseUrl}/{node.StructureGroup}?api-version={ApiVersion}"
                    .Replace("{organization}", Organization)
                    .Replace("{project}", Project);

                // Create or update node data
                var nodeData = new
                {
                    name = node.Name
                };

                var jsonContent = JsonConvert.SerializeObject(nodeData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send PUT request (Azure DevOps uses PUT for create/update)
                // var response = await client.PutAsync(url, content);
                var response = await client.PostAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create or update node. Status: {response.StatusCode}. Details: {errorMessage}");
                }
            }
        }

        public static async Task<string> RenameClassificationNodeAsync(ClassificationNode node)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{PatToken}")));

                // Construct the full URL for the classification node endpoint
                var url = $"{BaseUrl}/{node.StructureGroup}/{node.Path}?api-version={ApiVersion}"
                    .Replace("{organization}", Organization)
                    .Replace("{project}", Project);

                // Create or update node data
                var nodeData = new
                {
                    name = node.Name
                };

                var jsonContent = JsonConvert.SerializeObject(nodeData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send PUT request (Azure DevOps uses PUT for create/update)
                // var response = await client.PutAsync(url, content);
                var response = await client.PatchAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create or update node. Status: {response.StatusCode}. Details: {errorMessage}");
                }
            }
        }
    }
}
