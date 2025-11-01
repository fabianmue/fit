using System.Net;
using Xunit;

namespace FIT.FitApi.Test;

public class DividendsControllerTests : IDisposable
{
    private readonly FitApiWebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    private readonly CompanyChangeDtoFaker _companyFaker = new();

    private readonly DividendChangeDtoFaker _dividendFaker = new();

    public DividendsControllerTests()
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

        // Arrange - dividend
        var dividendToCreate = _dividendFaker.Generate();

        // Act
        var postDividendResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/dividends",
            dividendToCreate
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, postDividendResponse.StatusCode);
        var createdDividend = await postDividendResponse.Content.ReadFromJsonAsync<DividendDto>();
        Assert.NotNull(createdDividend);
        Assert.NotEqual(0, createdDividend.Id);
        AssertEqual(dividendToCreate, createdDividend);
    }

    [Fact]
    public async Task Post_WithoutExistingCompany_ReturnsNotFound()
    {
        // Arrange
        var dividendToCreate = _dividendFaker.Generate();

        // Act
        var postResponse = await _client.PostAsJsonAsync(
            $"/api/companies/1/dividends",
            dividendToCreate
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

        // Arrange - dividend
        var dividendToCreate = _dividendFaker.Generate();
        var postDividendResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/dividends",
            dividendToCreate
        );
        Assert.Equal(HttpStatusCode.OK, postDividendResponse.StatusCode);
        var createdDividend = await postDividendResponse.Content.ReadFromJsonAsync<DividendDto>();
        Assert.NotNull(createdDividend);

        // Act
        var dividendToUpdate = _dividendFaker.Generate();
        var putDividendResponse = await _client.PutAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/dividends/{createdDividend.Id}",
            dividendToUpdate
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, putDividendResponse.StatusCode);
        var updatedDividend = await putDividendResponse.Content.ReadFromJsonAsync<DividendDto>();
        Assert.NotNull(updatedDividend);
        AssertEqual(dividendToUpdate, updatedDividend);
    }

    [Fact]
    public async Task Put_WithoutExistingCompany_ReturnsNotFound()
    {
        // Arrange
        var toUpdate = _dividendFaker.Generate();

        // Act
        var putResponse = await _client.PutAsJsonAsync($"/api/companies/1/dividends/1", toUpdate);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
    }

    [Fact]
    public async Task Put_WithoutExistingDividend_ReturnsNotFound()
    {
        // Arrange - company
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Arrange - dividend
        var dividendToUpdate = _dividendFaker.Generate();

        // Act
        var putResponse = await _client.PutAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/dividends/1",
            dividendToUpdate
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

        // Arrange - dividend
        var dividendToCreate = _dividendFaker.Generate();
        var postDividendResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/dividends",
            dividendToCreate
        );
        Assert.Equal(HttpStatusCode.OK, postDividendResponse.StatusCode);
        var createdDividend = await postDividendResponse.Content.ReadFromJsonAsync<DividendDto>();
        Assert.NotNull(createdDividend);

        // Act
        var deleteResponse = await _client.DeleteAsync(
            $"/api/companies/{createdCompany.Id}/dividends/{createdDividend.Id}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithoutExistingCompany_ReturnsNotFound()
    {
        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/companies/1/dividends/1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithoutExistingDividend_ReturnsNotFound()
    {
        // Arrange
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Act
        var deleteResponse = await _client.DeleteAsync(
            $"/api/companies/{createdCompany.Id}/dividends/1"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }

    private static void AssertEqual(DividendChangeDto expected, DividendDto actual)
    {
        Assert.Equal(expected.PeriodStart, actual.PeriodStart);
        Assert.Equal(expected.PeriodEnd, actual.PeriodEnd);
        Assert.Equal(expected.PayoutDate, actual.PayoutDate);
        Assert.Equal(expected.AmountPerShare, actual.AmountPerShare);
    }
}
