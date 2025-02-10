using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using System.Text.Json;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.Integration.Features
{
    public class SaleIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;
        public SaleIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            string baseAddress = _httpClient.BaseAddress.ToString();
            _httpClient.BaseAddress = new Uri($"{baseAddress.Remove(baseAddress.Length - 1)}:5119");
        }

        [Fact]
        public async Task CreateSale_ReturnsSuccess_Async()
        {
            CreateSaleRequest request = new()
            {
                CreatedAt = DateTime.Now,
                CustomerId = Guid.Parse("1f272660-cd6b-41ca-a6f5-d5c3a1426815"),
                TotalAmount = 92.50M,
                TotalAmountWithDiscount = 74.00M,
                Canceled = false,
                SubsidiaryId = Guid.Parse("285ad3df-1849-4436-8880-1362863aa4d0"),
                Products = new[]
                {
                    new CreateSaleProductsRequest
                    {
                        ProductId = Guid.Parse("9c4614d5-e4f8-4ccd-9904-b9741d9484d7"),
                        Quantity = 10,
                        Price = 9.25M,
                        TotalAmount = 92.50M,
                        TotalAmountWithDiscount = 74.00M,
                        Discount = 20
                    }
                }
            };
            var content = serializeRequest(request);

            HttpResponseMessage response  = await _httpClient.PostAsync("api/sales", content, new CancellationToken(false));
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_ReturnsSuccess_Async()
        {
            UpdateSaleRequest request = new()
            {
                Id = Guid.Parse("25f7a2e8-9618-48cf-a541-342b9dbde89d"),
                UpdatedAt = DateTime.Now,
                CustomerId = Guid.Parse("1f272660-cd6b-41ca-a6f5-d5c3a1426815"),
                TotalAmount = 37M,
                TotalAmountWithDiscount = 37M,
                Canceled = false,
                SubsidiaryId = Guid.Parse("285ad3df-1849-4436-8880-1362863aa4d0"),
                Products = new[]
                {
                    new UpdateSaleProductsRequest
                    {
                        ProductId = Guid.Parse("9c4614d5-e4f8-4ccd-9904-b9741d9484d7"),
                        Quantity = 4,
                        Price = 9.25M,
                        TotalAmount = 37M,
                        TotalAmountWithDiscount = 37M,
                        Discount = 0
                    }
                }
            };
            var content = serializeRequest(request);

            HttpResponseMessage response = await _httpClient.PutAsync("api/sales", content, new CancellationToken(false));
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Get_ReturnsSuccess_Async()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/sales/{Guid.Parse("25f7a2e8-9618-48cf-a541-342b9dbde89d")}", new CancellationToken(false));
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsSuccess_Async()
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/sales/{Guid.Parse("05da2600-b8c9-4bb0-8596-a40db740149c")}", new CancellationToken(false));
            Assert.True(response.IsSuccessStatusCode);
        }

        private StringContent serializeRequest(object request)
        {
            string requestSerialized = JsonSerializer.Serialize(request);
            var content = new StringContent(requestSerialized, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
