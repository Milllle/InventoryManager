using System.Net.Http.Json;
using InventoryManager.Client.Models;

namespace InventoryManager.Client.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Product>>("api/product");

        public async Task<List<Product>> GetLowStockAsync() =>
            await _http.GetFromJsonAsync<List<Product>>("api/product/lowstock");

        public async Task CreateAsync(Product product) =>
            await _http.PostAsJsonAsync("api/product", product);

        public async Task DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/product/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateAsync(Product product)
        {
            var response = await _http.PutAsJsonAsync($"api/product/{product.Id}", product);
            response.EnsureSuccessStatusCode();
        }


    }
}
