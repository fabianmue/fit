using System.Net;
using Xunit;

namespace FIT.FitApi.Test;

public class CompaniesControllerTests : IDisposable
{
    private readonly FitApiWebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    public CompaniesControllerTests()
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
    public async Task GetAll_ReturnsNone_WhenNoneCreated()
    {
        // Act
        var getAllResponse = await _client.GetAsync("/api/companies");

        // Assert
        getAllResponse.EnsureSuccessStatusCode();
        var read = await getAllResponse.Content.ReadFromJsonAsync<List<CompanyDto>>();
        Assert.NotNull(read);
    }

    [Fact]
    public async Task PostAndGet_CreatesAndReturns()
    {
        // Arrange
        var toCreate = new CompanyChangeDto
        {
            Name = "Name",
            ReportingMultiplier = 1000,
            ReportingCurrency = "CHF",
            ShareCurrency = "CHF",
            DividendCurrency = "CHF",
        };

        // Act - create
        var postResponse = await _client.PostAsJsonAsync("/api/companies", toCreate);

        // Assert - create
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var created = await postResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        AssertEqual(toCreate, created);

        // Act - get by id
        var getResponse = await _client.GetAsync($"/api/companies/{created.Id}");

        // Assert - get by id
        getResponse.EnsureSuccessStatusCode();
        var read = await getResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(read);
        Assert.Equal(created.Id, read.Id);
        AssertEqual(toCreate, read);
    }

    [Fact]
    public async Task PostAndGetAll_CreatesAndReturns()
    {
        // Arrange
        var toCreate = new CompanyChangeDto
        {
            Name = "Name",
            ReportingMultiplier = 1000,
            ReportingCurrency = "CHF",
            ShareCurrency = "CHF",
            DividendCurrency = "CHF",
        };

        // Act - create
        var postResponse = await _client.PostAsJsonAsync("/api/companies", toCreate);

        // Assert - create
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var created = await postResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        AssertEqual(toCreate, created);

        // Act - get all
        var getAllResponse = await _client.GetAsync($"/api/companies");

        // Assert - get all
        getAllResponse.EnsureSuccessStatusCode();
        var read = await getAllResponse.Content.ReadFromJsonAsync<List<CompanyDto>>();
        Assert.NotNull(read);
        var readMatch = read.SingleOrDefault(d => d.Id == created.Id);
        Assert.NotNull(readMatch);
        AssertEqual(toCreate, readMatch);
    }

    [Fact]
    public async Task PostAndPut_CreatesAndUpdates()
    {
        // Arrange
        var toCreate = new CompanyChangeDto
        {
            Name = "Name",
            ReportingMultiplier = 1000,
            ReportingCurrency = "CHF",
            ShareCurrency = "CHF",
            DividendCurrency = "CHF",
        };

        // Act - create
        var postResponse = await _client.PostAsJsonAsync("/api/companies", toCreate);

        // Assert - create
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var created = await postResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        AssertEqual(toCreate, created);

        // Act - update
        var toUpdate = new CompanyChangeDto
        {
            Name = "Name Updated",
            ReportingMultiplier = 1000000,
            ReportingCurrency = "USD",
            ShareCurrency = "USD",
            DividendCurrency = "USD",
        };
        var putResponse = await _client.PutAsJsonAsync($"/api/companies/{created.Id}", toUpdate);

        // Assert - update
        putResponse.EnsureSuccessStatusCode();
        var updated = await putResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated.Id);
        AssertEqual(toUpdate, updated);
    }

    [Fact]
    public async Task PostAndDelete_CreatesAndDeletes()
    {
        // Arrange
        var toCreate = new CompanyChangeDto
        {
            Name = "Name",
            ReportingMultiplier = 1000,
            ReportingCurrency = "CHF",
            ShareCurrency = "CHF",
            DividendCurrency = "CHF",
        };

        // Act - create
        var postResponse = await _client.PostAsJsonAsync("/api/companies", toCreate);

        // Assert - create
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var created = await postResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        AssertEqual(toCreate, created);

        // Act - delete
        var deleteResponse = await _client.DeleteAsync($"/api/companies/{created.Id}");

        // Assert - delete
        deleteResponse.EnsureSuccessStatusCode();
    }

    private static void AssertEqual(CompanyChangeDto expected, CompanyDto actual)
    {
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.ReportingMultiplier, actual.ReportingMultiplier);
        Assert.Equal(expected.ReportingCurrency, actual.ReportingCurrency);
        Assert.Equal(expected.ShareCurrency, actual.ShareCurrency);
        Assert.Equal(expected.DividendCurrency, actual.DividendCurrency);
    }
}
