using System.ComponentModel.DataAnnotations;

namespace FIT.FitApi;

public abstract class EntityDto
{
    [Required]
    public int Id { get; set; }
}
