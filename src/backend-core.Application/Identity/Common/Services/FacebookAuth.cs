using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using backend_core.Application.Common.Exceptions;
using backend_core.Application.Identity.Common.Interfaces;
using backend_core.Domain.Models;
using Microsoft.Extensions.Options;

namespace backend_core.Application.Identity.Common.Services
{
    public class FacebookAuth : IFacebookAuth
    {
        private const string TokenValidatorUrl = "https://graph.facebook.com/v20.0/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/v20.0/me?access_token={0}&fields=id%2Cname%2Cemail%2Cpicture%2Cfirst_name%2Clast_name&format=json";

        private readonly FacebookSettings _facebookSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuth(IOptions<FacebookSettings> facebookOptions, IHttpClientFactory httpClientFactory)
        {
            _facebookSettings = facebookOptions.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FacebookTokenValidation> ValidateFacebookAccessToken(string accessToken)
        {
            var formattedUrl = string.Format(TokenValidatorUrl, accessToken, _facebookSettings.AppId, _facebookSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            var responseResult = result.EnsureSuccessStatusCode();
            if (!responseResult.IsSuccessStatusCode)
            {
                throw new BadRequestException("Something went wrong!");
            }
            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<FacebookTokenValidation>(responseAsString)!;
        }

        public async Task<FacebookUserInfoResult> GetFacebookUserInfo(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            var responseResult = result.EnsureSuccessStatusCode();
            if (!responseResult.IsSuccessStatusCode)
            {
                throw new BadRequestException("Something went wrong!");
            }
            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<FacebookUserInfoResult>(responseAsString)!;
        }

        
    }
}