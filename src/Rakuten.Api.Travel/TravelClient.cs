using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace Rakuten.Api.Travel
{
    public class TravelClient
    {
        public static string SimpleHotelSearchEndPoint { get; set; } = "https://app.rakuten.co.jp/services/api/Travel/SimpleHotelSearch/20170426";
        public static string HotelDetailSearchEndPoint { get; set; } = "https://app.rakuten.co.jp/services/api/Travel/HotelDetailSearch/20170426";
        public static string VacantHotelSearchEndPoint { get; set; } = "https://app.rakuten.co.jp/services/api/Travel/VacantHotelSearch/20170426";

        private readonly HttpClient _client;
        /// <summary>
        /// アプリケーションID
        /// </summary>
        public string ApplicatonId { get; private set; }

        /// <summary>
        /// アフリエイトID
        /// </summary>
        public string AffiliateId { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="applicationId">ApplicationId</param>
        public TravelClient(string applicationId)
        {
            this.ApplicatonId = applicationId;
            _client = new HttpClient();
        }


        public async Task<SimpleHotelSearchResult> SearchAsync(string largeClassCode, string middleClassCode = null, string smallClassCode = null, string detailClassCode = null, int pageSize = 1)
        {
            var builder = new UriBuilder(SimpleHotelSearchEndPoint);
            var paramCollection = GetDefaultParamCollection();
            paramCollection.Add("page", pageSize.ToString());
            paramCollection.Add("largeClassCode",largeClassCode);
            if (!string.IsNullOrEmpty(middleClassCode))
            {
                paramCollection.Add("middleClassCode", middleClassCode);
            }
            if (!string.IsNullOrEmpty(smallClassCode))
            {
                paramCollection.Add("smallClassCode", smallClassCode);
            }
            if (!string.IsNullOrEmpty(detailClassCode))
            {
                paramCollection.Add("detailClassCode", detailClassCode);
            }
            builder.Query = ToQueryString(paramCollection);

            var response = await _client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SimpleHotelSearchResultWithApi>(json);
                return result.Convert();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ErrorResult>(json);
                result.HttpStatus = (int)response.StatusCode;
                throw new ApiException(result);
            }
        }

        public async Task<SimpleHotelSearchResult> SearchAsync(string[] hotelNo, int pageSize = 1)
        {
            var builder = new UriBuilder(SimpleHotelSearchEndPoint);
            var paramCollection = GetDefaultParamCollection();
            paramCollection.Add("page", pageSize.ToString());
            paramCollection.Add("hotelNo", string.Join(",", hotelNo));
            builder.Query = ToQueryString(paramCollection);

            var response = await _client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SimpleHotelSearchResultWithApi>(json);
                return result.Convert();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ErrorResult>(json);
                result.HttpStatus = (int)response.StatusCode;
                throw new ApiException(result);
            }
        }

        public Task<SimpleHotelSearchResult> SearchAsync(string hotelNo, int pageSize = 1)
        {
            return SearchAsync(new[] {hotelNo}, pageSize);
        }

        public async Task<SimpleHotelSearchResult> SearchAsync(double latitude, double longitude, double searchRadius, int pageSize = 1)
        {
            if (searchRadius <= 0 || searchRadius > 3)
            {
                throw new ArgumentException($"{nameof(searchRadius)}は0.1から3.0まで");
            }
            var builder = new UriBuilder(SimpleHotelSearchEndPoint);
            var paramCollection = GetDefaultParamCollection();
            paramCollection.Add("allReturnFlag", "1");
            paramCollection.Add("datumType", "1");
            paramCollection.Add("latitude", latitude.ToString());
            paramCollection.Add("longitude", longitude.ToString());
            paramCollection.Add("searchRadius", Math.Round(searchRadius, 1).ToString());

            builder.Query = ToQueryString(paramCollection);
            var response = await _client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SimpleHotelSearchResultWithApi>(json);
                return result.Convert();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ErrorResult>(json);
                result.HttpStatus = (int)response.StatusCode;
                throw new ApiException(result);
            }
        }

        private NameValueCollection GetDefaultParamCollection()
        {
            var paramCollection = new NameValueCollection();
            paramCollection.Add("applicationId", ApplicatonId);
            paramCollection.Add("format", "json");
            paramCollection.Add("formatVersion", "2");
            paramCollection.Add("responseType", "large");

            return paramCollection;
        }


        private static string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "" + string.Join("&", array);
        }

        
    }
}
