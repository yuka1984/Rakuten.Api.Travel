using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rakuten.Api.Travel
{
    public static class SimpleHotelSearchExtentions
    {
        internal static SimpleHotelSearchResult Convert(this SimpleHotelSearchResultWithApi model)
        {
            var result = new SimpleHotelSearchResult();
            result.PagingInfo = model.PagingInfo;
            result.Hotels = model.Hotels.Select(x =>
            {
                var resultHotel = new Hotel();
                foreach (var hotel in x)
                {
                    if (hotel.HotelBasicInfo != null)
                    {
                        resultHotel.HotelBasicInfo = hotel.HotelBasicInfo;
                    }
                    if (hotel.HotelDetailInfo != null)
                    {
                        resultHotel.HotelDetailInfo = hotel.HotelDetailInfo;
                    }
                    if (hotel.HotelFacilitiesInfo != null)
                    {
                        resultHotel.HotelFacilitiesInfo = hotel.HotelFacilitiesInfo;
                    }
                    if (hotel.HotelOtherInfo != null)
                    {
                        resultHotel.HotelOtherInfo = hotel.HotelOtherInfo;
                    }
                    if (hotel.HotelPolicyInfo != null)
                    {
                        resultHotel.HotelPolicyInfo = hotel.HotelPolicyInfo;
                    }
                    if (hotel.HotelRatingInfo != null)
                    {
                        resultHotel.HotelRatingInfo = hotel.HotelRatingInfo;
                    }
                }
                return resultHotel;
            }).ToArray();

            return result;
        }
    }

    internal class SimpleHotelSearchResultWithApi
    {
        public Paginginfo PagingInfo { get; set; }
        public Hotel[][] Hotels { get; set; }
    }

    public class SimpleHotelSearchResult
    {
        [JsonProperty(PropertyName = "pagingInfo")]
        public Paginginfo PagingInfo { get; set; }
        [JsonProperty(PropertyName = "hotels")]
        public Hotel[] Hotels { get; set; }
    }

    public class Paginginfo
    {
        [JsonProperty(PropertyName = "recordCount")]
        public int RecordCount { get; set; }
        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }
        [JsonProperty(PropertyName = "first")]
        public int First { get; set; }
        [JsonProperty(PropertyName = "last")]
        public int Last { get; set; }
    }

    public class Hotel
    {
        public Hotelbasicinfo HotelBasicInfo { get; set; }
        public Hotelratinginfo HotelRatingInfo { get; set; }
        public Hoteldetailinfo HotelDetailInfo { get; set; }
        public Hotelfacilitiesinfo HotelFacilitiesInfo { get; set; }
        public Hotelpolicyinfo HotelPolicyInfo { get; set; }
        public Hotelotherinfo HotelOtherInfo { get; set; }
    }

    public class Hotelbasicinfo
    {
        public int HotelNo { get; set; }
        public string HotelName { get; set; }
        public string HotelInformationUrl { get; set; }
        public string PlanListUrl { get; set; }
        public string DpPlanListUrl { get; set; }
        public string ReviewUrl { get; set; }
        public string HotelKanaName { get; set; }
        public string HotelSpecial { get; set; }
        public int? HotelMinCharge { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string PostalCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Access { get; set; }
        public string ParkingInformation { get; set; }
        public string NearestStation { get; set; }
        public string HotelImageUrl { get; set; }
        public string HotelThumbnailUrl { get; set; }
        public string RoomImageUrl { get; set; }
        public string RoomThumbnailUrl { get; set; }
        public string HotelMapImageUrl { get; set; }
        public int? ReviewCount { get; set; }
        public float? ReviewAverage { get; set; }
        public string UserReview { get; set; }
    }

    public class Hotelratinginfo
    {
        public float? ServiceAverage { get; set; }
        public float? LocationAverage { get; set; }
        public float? RoomAverage { get; set; }
        public float? EquipmentAverage { get; set; }
        public float? BathAverage { get; set; }
        public float? MealAverage { get; set; }
    }

    public class Hoteldetailinfo
    {
        public string ReserveTelephoneNo { get; set; }
        public string MiddleClassCode { get; set; }
        public string SmallClassCode { get; set; }
        public string AreaName { get; set; }
        public string HotelClassCode { get; set; }
        public string CheckinTime { get; set; }
        public string CheckoutTime { get; set; }
        public string LastCheckinTime { get; set; }
    }

    public class Hotelfacilitiesinfo
    {
        public int HotelRoomNum { get; set; }
        public Roomfacility[] RoomFacilities { get; set; }
        public Hotelfacility[] HotelFacilities { get; set; }
        public Aboutmealplace[] AboutMealPlace { get; set; }
        public Aboutbath[] AboutBath { get; set; }
        public string AboutLeisure { get; set; }
        public Handicappedfacility[] HandicappedFacilities { get; set; }
        public string LinguisticLevel { get; set; }
    }

    public class Roomfacility
    {
        public string Item { get; set; }
    }

    public class Hotelfacility
    {
        public string Item { get; set; }
    }

    public class Aboutmealplace
    {
        public string BreakfastPlace { get; set; }
        public string DinnerPlace { get; set; }
    }

    public class Aboutbath
    {
        public string BathType { get; set; }
        public string BathQuality { get; set; }
        public string BathBenefits { get; set; }
    }

    public class Handicappedfacility
    {
        public string Item { get; set; }
    }

    public class Hotelpolicyinfo
    {
        public string Note { get; set; }
        public string CancelPolicy { get; set; }
        public Availablecreditcard[] AvailableCreditCard { get; set; }
        public object AboutCreditCardNote { get; set; }
        public object AboutPointAdd { get; set; }
        public object AboutMileageAdd { get; set; }
    }

    public class Availablecreditcard
    {
        public string Card { get; set; }
    }

    public class Hotelotherinfo
    {
        public string Privilege { get; set; }
        public string OtherInformation { get; set; }
    }
}
