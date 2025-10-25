using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class CompanyDto : EntityDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int ReportingMultiplier { get; set; }

    [Required]
    public string ReportingCurrency { get; set; }

    [Required]
    public string ShareCurrency { get; set; }

    [Required]
    public string DividendCurrency { get; set; }
}
