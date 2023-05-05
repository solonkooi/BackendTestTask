using Dapper.Contrib.Extensions;

namespace BackendTestTask.Core;

[Dapper.Contrib.Extensions.Table("nodes")]
public class Node
{
    [Key]
    public long id { get; set; }
    public string name { get; set; }
    public string tree_name { get; set; }
    public long parent_id { get; set; }
    
    [Write(false)]
    [Computed]
    public IEnumerable<Node>? ChildNodes { get; set; }
}