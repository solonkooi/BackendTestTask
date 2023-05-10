using BackendTestTask.Business.Models;

namespace BackendTestTask.Business.Interfaces;

public interface INodeService
{
    Task<NodeModel> GetTreeByNameAsync(string treeName);
    Task CreateNodeAsync(CreateNodeRequest model);
    Task DeleteNodeAsync(string treeName, long nodeId);
    Task RenameNodeAsync(RenameNodeRequest model);
}