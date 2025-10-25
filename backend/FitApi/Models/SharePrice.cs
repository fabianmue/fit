using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public class SharePrice : Entity
{
    [Required]
    public required DateOnly Date { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public required double Price { get; set; }

    // relations
    public int CompanyId { get; set; }

    public required Company Company { get; set; }
}
