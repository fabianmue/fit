using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class DividendChangeDto
{
    [Required]
    public DateOnly PeriodStart { get; set; }

    [Required]
    public DateOnly PeriodEnd { get; set; }

    [Required]
    public DateOnly ExDividendDate { get; set; }

    [Required]
    public DateOnly PaymentDate { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double AmountPerShare { get; set; }
}
