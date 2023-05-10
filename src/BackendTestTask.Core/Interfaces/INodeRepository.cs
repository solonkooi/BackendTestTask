namespace BackendTestTask.Core.Interfaces;

public interface INodeRepository: IBaseRepository<Node>
{
    Task<IEnumerable<Node>> GetTreeNodesByNameAsync(string treeName);
    Task<IEnumerable<Node>> GetTreeNodesByNodeIdAsync(long nodeId);
}