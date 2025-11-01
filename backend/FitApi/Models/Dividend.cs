using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class Dividend : Entity
{
    [Required]
    public required DateOnly PeriodStart { get; set; }

    [Required]
    public required DateOnly PeriodEnd { get; set; }

    [Required]
    public required DateOnly ExDividendDate { get; set; }

    [Required]
    public required DateOnly PaymentDate { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public required double AmountPerShare { get; set; }

    // relations
    public int CompanyId { get; set; }

    public required Company Company { get; set; }
}
