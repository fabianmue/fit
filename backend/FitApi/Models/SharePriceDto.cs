using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class SharePriceDto : EntityDto
{
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public double Price { get; set; }
}
