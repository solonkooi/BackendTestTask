namespace BackendTestTask.Business.Models;

public class NodeModel
{
    public long Id { get; set; }
    public string TreeName { get; set; }
    public string Name { get; set; }
    public long? ParentId { get; set; }
    public List<NodeModel>? Children { get; set; }
}