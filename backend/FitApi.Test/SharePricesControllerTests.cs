using System.Net;
using Xunit;

namespace FIT.FitApi.Test;

public class SharePricesControllerTests : IDisposable
{
    private readonly FitApiWebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    private readonly CompanyChangeDtoFaker _companyFaker = new();

    private readonly SharePriceChangeDtoFaker _sharePriceFaker = new();

    public SharePricesControllerTests()
    {
        _factory = new FitApiWebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Fact]
    public async Task Post_Creates()
    {
        // Arrange - company
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Arrange - share price
        var sharePriceToCreate = _sharePriceFaker.Generate();

        // Act
        var postSharePriceResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/shareprices",
            sharePriceToCreate
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, postSharePriceResponse.StatusCode);
        var createdSharePrice =
            await postSharePriceResponse.Content.ReadFromJsonAsync<SharePriceDto>();
        Assert.NotNull(createdSharePrice);
        Assert.NotEqual(0, createdSharePrice.Id);
        AssertEqual(sharePriceToCreate, createdSharePrice);
    }

    [Fact]
    public async Task Post_WithoutExistingCompany_ReturnsNotFound()
    {
        // Arrange
        var sharePriceToCreate = _sharePriceFaker.Generate();

        // Act
        var postResponse = await _client.PostAsJsonAsync(
            $"/api/companies/1/shareprices",
            sharePriceToCreate
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, postResponse.StatusCode);
    }

    [Fact]
    public async Task PostAndUpdate_CreatesAndUpdates()
    {
        // Arrange - company
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Arrange - share price
        var sharePriceToCreate = _sharePriceFaker.Generate();
        var postSharePriceResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/shareprices",
            sharePriceToCreate
        );
        Assert.Equal(HttpStatusCode.OK, postSharePriceResponse.StatusCode);
        var createdSharePrice =
            await postSharePriceResponse.Content.ReadFromJsonAsync<SharePriceDto>();
        Assert.NotNull(createdSharePrice);

        // Act
        var sharePriceToUpdate = _sharePriceFaker.Generate();
        var putSharePriceResponse = await _client.PutAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/shareprices/{createdSharePrice.Id}",
            sharePriceToUpdate
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, putSharePriceResponse.StatusCode);
        var updatedSharePrice =
            await putSharePriceResponse.Content.ReadFromJsonAsync<SharePriceDto>();
        Assert.NotNull(updatedSharePrice);
        AssertEqual(sharePriceToUpdate, updatedSharePrice);
    }

    [Fact]
    public async Task Put_WithoutExistingCompany_ReturnsNotFound()
    {
        // Arrange
        var toUpdate = _sharePriceFaker.Generate();

        // Act
        var putResponse = await _client.PutAsJsonAsync($"/api/companies/1/shareprices/1", toUpdate);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
    }

    [Fact]
    public async Task Put_WithoutExistingSharePrice_ReturnsNotFound()
    {
        // Arrange - company
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Arrange - share price
        var sharePriceToUpdate = _sharePriceFaker.Generate();

        // Act
        var putResponse = await _client.PutAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/shareprices/1",
            sharePriceToUpdate
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
    }

    [Fact]
    public async Task PostAndDelete_CreatesAndDeletes()
    {
        // Arrange - company
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Arrange - share price
        var sharePriceToCreate = _sharePriceFaker.Generate();
        var postSharePriceResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/shareprices",
            sharePriceToCreate
        );
        Assert.Equal(HttpStatusCode.OK, postSharePriceResponse.StatusCode);
        var createdSharePrice =
            await postSharePriceResponse.Content.ReadFromJsonAsync<SharePriceDto>();
        Assert.NotNull(createdSharePrice);

        // Act
        var deleteResponse = await _client.DeleteAsync(
            $"/api/companies/{createdCompany.Id}/shareprices/{createdSharePrice.Id}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithoutExistingCompany_ReturnsNotFound()
    {
        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/companies/1/shareprices/1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithoutExistingSharePrice_ReturnsNotFound()
    {
        // Arrange
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Act
        var deleteResponse = await _client.DeleteAsync(
            $"/api/companies/{createdCompany.Id}/shareprices/1"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }

    private static void AssertEqual(SharePriceChangeDto expected, SharePriceDto actual)
    {
        Assert.Equal(expected.Date, actual.Date);
        Assert.Equal(expected.Price, actual.Price);
    }
}
