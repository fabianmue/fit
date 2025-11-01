using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class SharePriceChangeDto
{
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }
}
