using System.ComponentModel.DataAnnotations;

namespace BackendTestTask.Business.Models;

public class GetRangeJurnalRequest
{
    [Required]
    public int Skip { get; set; }
    [Required] 
    public int Take { get; set; }
}