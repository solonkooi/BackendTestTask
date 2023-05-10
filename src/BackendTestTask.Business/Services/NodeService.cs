using AutoMapper;
using BackendTestTask.Business.Exceptions;
using BackendTestTask.Business.Extensions;
using BackendTestTask.Business.Interfaces;
using BackendTestTask.Business.Models;
using BackendTestTask.Core;
using BackendTestTask.Core.Interfaces;

namespace BackendTestTask.Business.Services;

public class NodeService : INodeService
{
    private readonly INodeRepository _nodeRepository;
    private readonly IMapper _mapper;

    public NodeService(INodeRepository nodeRepository, IMapper mapper)
    {
        _nodeRepository = nodeRepository;
        _mapper = mapper;
    }

    public async Task<NodeModel> GetTreeByNameAsync(string treeName)
    {
        var nodes = await _nodeRepository.GetTreeNodesByNameAsync(treeName);
        if (!nodes.Any())
          throw new SecureException($"Node does not exist with trees name: {treeName}");
        
        var tree = _mapper.Map<List<NodeModel>>(nodes);
        return tree.BuildTree();
    }

    public async Task CreateNodeAsync(CreateNodeRequest model)
    {
        if (!await IsExistNodeByTreeNameAndParentNodeIdAsync(model.TreeName, model.ParentNodeId))
        {
            throw new SecureException($"Node does not exist with parent node id: {model.ParentNodeId}");
        }

        if (await IsExistNodeByTreeNameAndNodeNameAsync(model.TreeName, model.NodeName))
        {
            throw new SecureException($"Node exist with node name: {model.NodeName}");
        }

        var newNode = new Node { name = model.NodeName, tree_name = model.TreeName, parent_id = model.ParentNodeId };
        await _nodeRepository.CreateAsync(newNode);
    }

    public async Task DeleteNodeAsync(string treeName, long nodeId)
    {
        var node = await _nodeRepository.GetByIdAsync(nodeId);
        if (node == null)
        {
            throw new SecureException($"Node does not exist with node id: {nodeId}");
        }

        node.ChildNodes = await _nodeRepository.GetTreeNodesByNodeIdAsync(node.id);

        if (node.ChildNodes?.Count() > 0)
        {
            throw new SecureException($"You have to delete all children nodes first for node id {node.id}");
        }

        await _nodeRepository.DeleteAsync(node.id);
    }

    public async Task RenameNodeAsync(RenameNodeRequest model)
    {
        if (await IsExistNodeByTreeNameAndNodeNameAsync(model.TreeName, model.NewNodeName))
        {
            throw new SecureException($"Node exist with node name: {model.NewNodeName}");
        }

        var node = await _nodeRepository.GetByIdAsync(model.NodeId);
        if (node == null)
        {
            throw new SecureException($"Node does not exist with node id: {model.NodeId}");
        }

        node.name = model.NewNodeName;
        await _nodeRepository.UpdateAsync(node);
    }
    
    private async Task<bool> IsExistNodeByTreeNameAndParentNodeIdAsync(string treeName, long parentNodeId)
    {
        var nodes = await _nodeRepository.GetAll();
        var parentNode = nodes.FirstOrDefault(x => x.tree_name == treeName && x.parent_id == parentNodeId);

        return parentNode != null;
    }
    
    private async Task<bool> IsExistNodeByTreeNameAndNodeNameAsync(string treeName, string nodeName)
    {
        var nodes = await _nodeRepository.GetAll();
        var parentNode =  nodes.FirstOrDefault(x => x.tree_name == treeName && x.name == nodeName);

        return parentNode != null;
    }
}