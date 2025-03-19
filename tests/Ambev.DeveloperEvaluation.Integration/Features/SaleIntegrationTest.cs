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
                CustomerId = Guid.Parse("9b2e90d7-9820-4577-9b6a-6d755170a3ca"),
                TotalAmount = 92.50M,
                TotalAmountWithDiscount = 74.00M,
                Canceled = false,
                SubsidiaryId = Guid.Parse("9c8319d7-04a1-44e9-a0c5-a68e1ee186f5"),
                Products = new[]
                {
                    new CreateSaleProductsRequest
                    {
                        ProductId = Guid.Parse("b07a13d5-8b60-4dc5-adc4-a4edb3b4a15c"),
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
                Id = Guid.Parse("f8334a46-f7c0-42ec-a870-1ec31d2d7411"),
                UpdatedAt = DateTime.Now,
                CustomerId = Guid.Parse("9b2e90d7-9820-4577-9b6a-6d755170a3ca"),
                TotalAmount = 37M,
                TotalAmountWithDiscount = 37M,
                Canceled = false,
                SubsidiaryId = Guid.Parse("9c8319d7-04a1-44e9-a0c5-a68e1ee186f5"),
                Products = new[]
                {
                    new UpdateSaleProductsRequest
                    {
                        ProductId = Guid.Parse("b07a13d5-8b60-4dc5-adc4-a4edb3b4a15c"),
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
            HttpResponseMessage response = await _httpClient.GetAsync($"api/sales/{Guid.Parse("f8334a46-f7c0-42ec-a870-1ec31d2d7411")}", new CancellationToken(false));
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
