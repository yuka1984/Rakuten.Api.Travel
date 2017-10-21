using System;
using Newtonsoft.Json;

namespace Rakuten.Api.Travel
{
    public class ErrorResult
    {
        public int HttpStatus { get; set; }

        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
    }

    public class ApiException : Exception
    {
        public ApiException(ErrorResult errorResult) : base($"{errorResult.Error}")
        {
            base.Source = $"status:{errorResult.HttpStatus} {errorResult.Error} : {errorResult.ErrorDescription}";
        }
    }
}