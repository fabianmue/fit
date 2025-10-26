using System.Net;
using Xunit;

namespace FIT.FitApi.Test;

public class ReportingsControllerTests : IDisposable
{
    private readonly FitApiWebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;

    private readonly CompanyChangeDtoFaker _companyFaker = new();

    private readonly ReportingChangeDtoFaker _reportingFaker = new();

    public ReportingsControllerTests()
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

        // Arrange - reporting
        var reportingToCreate = _reportingFaker.Generate();

        // Act
        var postReportingResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/reportings",
            reportingToCreate
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, postReportingResponse.StatusCode);
        var createdReporting =
            await postReportingResponse.Content.ReadFromJsonAsync<ReportingDto>();
        Assert.NotNull(createdReporting);
        Assert.NotEqual(0, createdReporting.Id);
        AssertEqual(reportingToCreate, createdReporting);
    }

    [Fact]
    public async Task Post_WithoutExistingCompany_ReturnsNotFound()
    {
        // Arrange
        var reportingToCreate = _reportingFaker.Generate();

        // Act
        var postResponse = await _client.PostAsJsonAsync(
            $"/api/companies/1/reportings",
            reportingToCreate
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

        // Arrange - reporting
        var reportingToCreate = _reportingFaker.Generate();
        var postReportingResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/reportings",
            reportingToCreate
        );
        Assert.Equal(HttpStatusCode.OK, postReportingResponse.StatusCode);
        var createdReporting =
            await postReportingResponse.Content.ReadFromJsonAsync<ReportingDto>();
        Assert.NotNull(createdReporting);

        // Act
        var reportingToUpdate = _reportingFaker.Generate();
        var putReportingResponse = await _client.PutAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/reportings/{createdReporting.Id}",
            reportingToUpdate
        );

        // Assert
        Assert.Equal(HttpStatusCode.OK, putReportingResponse.StatusCode);
        var updatedReporting = await putReportingResponse.Content.ReadFromJsonAsync<ReportingDto>();
        Assert.NotNull(updatedReporting);
        AssertEqual(reportingToUpdate, updatedReporting);
    }

    [Fact]
    public async Task Put_WithoutExistingCompany_ReturnsNotFound()
    {
        // Arrange
        var toUpdate = _reportingFaker.Generate();

        // Act
        var putResponse = await _client.PutAsJsonAsync($"/api/companies/1/reportings/1", toUpdate);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
    }

    [Fact]
    public async Task Put_WithoutExistingReporting_ReturnsNotFound()
    {
        // Arrange - company
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Arrange - reporting
        var reportingToUpdate = _reportingFaker.Generate();

        // Act
        var putResponse = await _client.PutAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/reportings/1",
            reportingToUpdate
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

        // Arrange - reporting
        var reportingToCreate = _reportingFaker.Generate();
        var postReportingResponse = await _client.PostAsJsonAsync(
            $"/api/companies/{createdCompany.Id}/reportings",
            reportingToCreate
        );
        Assert.Equal(HttpStatusCode.OK, postReportingResponse.StatusCode);
        var createdReporting =
            await postReportingResponse.Content.ReadFromJsonAsync<ReportingDto>();
        Assert.NotNull(createdReporting);

        // Act
        var deleteResponse = await _client.DeleteAsync(
            $"/api/companies/{createdCompany.Id}/reportings/{createdReporting.Id}"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithoutExistingCompany_ReturnsNotFound()
    {
        // Act
        var putResponse = await _client.DeleteAsync($"/api/companies/1/reportings/1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithoutExistingReporting_ReturnsNotFound()
    {
        // Arrange
        var companyToCreate = _companyFaker.Generate();
        var postCompanyResponse = await _client.PostAsJsonAsync("/api/companies", companyToCreate);
        Assert.Equal(HttpStatusCode.Created, postCompanyResponse.StatusCode);
        var createdCompany = await postCompanyResponse.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(createdCompany);

        // Act
        var putResponse = await _client.DeleteAsync(
            $"/api/companies/{createdCompany.Id}/reportings/1"
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
    }

    private static void AssertEqual(ReportingChangeDto expected, ReportingDto actual)
    {
        Assert.Equal(expected.PeriodStart, actual.PeriodStart);
        Assert.Equal(expected.PeriodEnd, actual.PeriodEnd);
        Assert.Equal(expected.Comment, actual.Comment);
        Assert.Equal(expected.Revenue, actual.Revenue);
        Assert.Equal(expected.Earnings, actual.Earnings);
        Assert.Equal(expected.EarningsPerShare, actual.EarningsPerShare);
        Assert.Equal(expected.TotalAssets, actual.TotalAssets);
        Assert.Equal(expected.TotalLiabilities, actual.TotalLiabilities);
    }
}
