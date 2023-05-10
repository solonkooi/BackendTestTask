using System.ComponentModel.DataAnnotations;

namespace BackendTestTask.Business.Models;

public class CreateNodeRequest
{
    [Required]
    public string TreeName { get; set; }
    [Required]
    public long ParentNodeId { get; set; }
    [Required]
    public string NodeName { get; set; }
}