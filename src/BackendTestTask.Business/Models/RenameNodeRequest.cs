using System.ComponentModel.DataAnnotations;

namespace BackendTestTask.Business.Models;

public class RenameNodeRequest
{
    [Required]
    public string TreeName { get; set; } 
    [Required]
    public long NodeId { get; set; }
    [Required]
    public string NewNodeName { get; set; }
}