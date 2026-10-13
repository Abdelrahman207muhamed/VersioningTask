using System.Net;
using System.Text.Json;
using Xunit;

namespace VersioningDemo.IntegrationTests;

public class ProductsVersioningTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsVersioningTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<JsonDocument> GetJsonAsync(string url)
    {
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(json);
    }

    // ---------- V1 ----------

    [Fact]
    public async Task V1_GetById_ReturnsNumericPrice_AndNoCategoryName()
    {
        using var doc = await GetJsonAsync("/api/v1/products/1");
        var root = doc.RootElement;

        Assert.Equal("Laptop", root.GetProperty("name").GetString());
        Assert.Equal(JsonValueKind.Number, root.GetProperty("price").ValueKind);
        Assert.False(root.TryGetProperty("categoryName", out _));
    }

    [Fact]
    public async Task V1_GetAll_ReturnsArray_WithoutCategoryName()
    {
        using var doc = await GetJsonAsync("/api/v1/products");
        var root = doc.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(2, root.GetArrayLength());
        foreach (var item in root.EnumerateArray())
            Assert.False(item.TryGetProperty("categoryName", out _));
    }

    // ---------- V2 ----------

    [Fact]
    public async Task V2_GetById_ReturnsCategoryName()
    {
        using var doc = await GetJsonAsync("/api/v2/products/1");
        var root = doc.RootElement;

        Assert.Equal("Laptop", root.GetProperty("name").GetString());
        Assert.Equal(JsonValueKind.Number, root.GetProperty("price").ValueKind);
        Assert.Equal("Electronics", root.GetProperty("categoryName").GetString());
    }

    [Fact]
    public async Task V2_GetAll_ReturnsArray_WithCategoryName()
    {
        using var doc = await GetJsonAsync("/api/v2/products");
        var root = doc.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(2, root.GetArrayLength());
        foreach (var item in root.EnumerateArray())
            Assert.True(item.TryGetProperty("categoryName", out _));
    }

    // ---------- Not found ----------

    [Theory]
    [InlineData("/api/v1/products/999")]
    [InlineData("/api/v2/products/999")]
    public async Task GetById_NonExistingProduct_Returns404(string url)
    {
        var response = await _client.GetAsync(url);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // ---------- Versioning rules ----------

    [Fact]
    public async Task UnsupportedVersion_V3_DoesNotSucceed()
    {
        var response = await _client.GetAsync("/api/v3/products");
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task NoVersion_DoesNotSucceed()
    {
        var response = await _client.GetAsync("/api/products");
        Assert.False(response.IsSuccessStatusCode);
    }
}