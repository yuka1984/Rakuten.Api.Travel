using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rakuten.Api.Travel.Test
{
    public class SimpleHotelSearchTest
    {
        [Fact]
        public async Task ClassRequestTest()
        {
            var client = new TravelClient(TestConfig.ApplicatonId);
            client.AffiliateId = TestConfig.AffiliateId;

            var result = await client.SearchAsync("japan", "akita", "tazawa");

            if (result.PagingInfo.PageCount > 1)
            {
                for (int i = 2; i <= result.PagingInfo.Page; i++)
                {
                    result = await client.SearchAsync("japan", "akita", "tazawa", pageSize:i);
                }
            }
        }

        [Fact]
        public async Task ClassRequestTest_Exception()
        {
            var client = new TravelClient(TestConfig.ApplicatonId);
            client.AffiliateId = TestConfig.AffiliateId;

            try
            {
                var result = await client.SearchAsync("japan", "akita", "test");
            }
            catch (Exception e)
            {
                Assert.Equal(e.Message, "not_found");
            }
        }

        [Fact]
        public async Task HotelNoRequestTest()
        {
            var client = new TravelClient(TestConfig.ApplicatonId);
            client.AffiliateId = TestConfig.AffiliateId;

            var result = await client.SearchAsync(new[] {"19486", "74732"});
        }

        [Fact]
        public async Task PointRequestTest()
        {
            var client = new TravelClient(TestConfig.ApplicatonId);
            client.AffiliateId = TestConfig.AffiliateId;

            var result = await client.SearchAsync(35.626457, 139.74073, 3);
        }


    }
}
