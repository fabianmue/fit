using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class DividendDto : EntityDto
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
    public double AmountPerShare { get; set; }
}
