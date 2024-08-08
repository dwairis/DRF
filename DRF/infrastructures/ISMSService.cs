using DAGCrypto;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SMSService.Models;
using System.Net.Http.Headers;

namespace DRF.infrastructures
{
    public interface ISMSService
    {
        Task<string> GetToken();
        Task<ResponseResult> Send(SendSMSRequest request);
        Task<ResponseResult> SendBatch(SendSMSBatchRequest request);
        Task<SuccessResponse<GetSMSResponse>> Get(long id);
        Task<ResponseResult> SendOTP(SendOTPRequest request);
        Task<ResponseResult> VerifyOTP(VerifyOTPRequest request);
        Task<ResponseResult> SendEmailOTP(SendEmailOTPRequest data);
        Task<ResponseResult> VerifyEmailOTP(VerifyEmailOTPRequest data);
    }
    public class SMSService : ISMSService
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly SMSServiceSettings serviceSettings;
        private readonly IDAGCrypto crypto;
        private readonly string tokenFilePath;
        public SMSService(IWebHostEnvironment hostingEnvironment, IOptions<SMSServiceSettings> serviceSettings, IDAGCrypto crypto)
        {
            this.hostingEnvironment = hostingEnvironment;
            tokenFilePath = Path.Combine(hostingEnvironment.ContentRootPath, "sms_service_token.text");
            this.serviceSettings = serviceSettings.Value;

            if (!System.IO.File.Exists(tokenFilePath))
            {
                System.IO.File.Create(tokenFilePath);
            }

            this.crypto = crypto;
        }

        public async Task<SuccessResponse<GetSMSResponse>> Get(long id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{serviceSettings.Url}/SMS/Get/" + id);
            string token = "Bearer " + await GetToken();
            request.Headers.Add("Authorization", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<SuccessResponse<GetSMSResponse>>();
            }
            return null;

        }

        private async Task<string> IssueNewToken()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/token");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            request.Content = new StringContent(JsonConvert.SerializeObject(new TokenGenerationRequest() { ApplicationID = serviceSettings.ApplicationID, SecretCode = serviceSettings.SecretCode }), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rsp = await response.Content.ReadAsAsync<TokenResponse>();
                if (rsp != null)
                {
                    System.IO.File.WriteAllText(this.tokenFilePath, crypto.Encrypt(rsp.AccessToken));
                    return rsp.AccessToken;
                }
                throw new Exception("Unable to generate new token");
            }
            else
            {
                throw new Exception($"Unable to generate new token {response.StatusCode}; {serviceSettings.Url}/token");
            }
        }
        public async Task<string> GetToken()
        {

            string currentToken = System.IO.File.ReadAllText(this.tokenFilePath) ?? "";

            if (!string.IsNullOrEmpty(currentToken))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{serviceSettings.Url}/WhoIm");
                var t = crypto.Decrypt(currentToken);
                request.Headers.Add("Authorization", "Bearer " + t);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                var response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return t;
                }
                else
                {

                    return await IssueNewToken();

                }
            }
            return await IssueNewToken();



        }

        public async Task<ResponseResult> Send(SendSMSRequest data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/SMS/Send");
            string token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResponseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
            }
            else
            {
                var rsp = await response.Content.ReadAsAsync<JsonErrorResponseMessage>();
                return new ResponseResult { StatusCode = response.StatusCode, Message = rsp.Message };
            }
        }

        public async Task<ResponseResult> SendBatch(SendSMSBatchRequest data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/SMS/SendBatch");
            string token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResponseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
            }
            else
            {
                var rsp = await response.Content.ReadAsAsync<JsonErrorResponseMessage>();
                return new ResponseResult { StatusCode = response.StatusCode, Message = rsp.Message };
            }
        }

        public async Task<ResponseResult> SendOTP(SendOTPRequest data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/OTP/Send");
            string token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResponseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
            }
            else
            {
                var rsp = await response.Content.ReadAsAsync<JsonErrorResponseMessage>();
                return new ResponseResult { StatusCode = response.StatusCode, Message = rsp.Message };
            }

        }

        public async Task<ResponseResult> VerifyOTP(VerifyOTPRequest data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/OTP/Verify");
            string token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResponseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
            }
            else
            {
                var rsp = await response.Content.ReadAsAsync<JsonErrorResponseMessage>();
                return new ResponseResult { StatusCode = response.StatusCode, Message = rsp.Message };
            }

        }
        public async Task<ResponseResult> SendEmailOTP(SendEmailOTPRequest data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/OTP/SendEmail");
            string token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResponseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
            }
            else
            {
                var rsp = await response.Content.ReadAsAsync<JsonErrorResponseMessage>();
                return new ResponseResult { StatusCode = response.StatusCode, Message = rsp.Message };
            }

        }

        public async Task<ResponseResult> VerifyEmailOTP(VerifyEmailOTPRequest data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{serviceSettings.Url}/OTP/VerifyEmail");
            string token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            request.Content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResponseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
            }
            else
            {
                var rsp = await response.Content.ReadAsAsync<JsonErrorResponseMessage>();
                return new ResponseResult { StatusCode = response.StatusCode, Message = rsp.Message };
            }

        }
    }
}
