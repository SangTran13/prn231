using BusinessObject;
using Newtonsoft.Json;
using Shared;
using System.Text;

namespace eStoreClient.Untils
{
    public class ApiHandler
    {
        // API Request and Response Handling
        public static async Task<ApiResponse<T>> DeserializeApiResponse<T>(string apiUrl, HttpMethod method, object value = null)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;

                try
                {
                    var request = new HttpRequestMessage(method, apiUrl);

                    // If the method is not GET, we serialize the body.
                    if (value != null && method != HttpMethod.Get)
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    }

                    // Send the request
                    response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"API request failed with status code: {response.StatusCode}");
                    }

                    var content = await response.Content.ReadAsStringAsync();

                    // Deserialize the response
                    return JsonConvert.DeserializeObject<ApiResponse<T>>(content);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error in API call: {ex.Message}");
                }
            }
        }

        // Check if email already exists in the database
        public static async Task<bool> CheckEmailExists(string email)
        {
            try
            {
                var apiResponse = await DeserializeApiResponse<List<Member>>("https://localhost:7237/api/members", HttpMethod.Get);

                return apiResponse != null && apiResponse.Data.Any(m => m.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking email: {ex.Message}", ex);
            }
        }
    }
}
