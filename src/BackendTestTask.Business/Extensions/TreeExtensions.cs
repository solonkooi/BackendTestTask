using BackendTestTask.Business.Models;

namespace BackendTestTask.Business.Extensions;

public static class TreeExtensions
{
    public static NodeModel BuildTree(this List<NodeModel> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        return nodes[0].BuildTree(nodes);
    }

    private static NodeModel BuildTree(this NodeModel root, List<NodeModel> nodes)
    {
        if (nodes.Count == 0)
        {
            return root;
        }

        var children = root.FetchChildren(nodes).ToList();
        root.Children?.AddRange(children);
        root.RemoveChildren(nodes);

        for (var i = 0; i < children.Count; i++)
        {
            children[i] = children[i].BuildTree(nodes);
            if (nodes.Count == 0)
            {
                break;
            }
        }

        return root;
    }

    public static IEnumerable<NodeModel> FetchChildren(this NodeModel root, IEnumerable<NodeModel> nodes)
    {
        return root.Id == 0
            ? nodes.Where(n => n.ParentId == null)
            : nodes.Where(n => n.ParentId == root.Id);
    }

    public static void RemoveChildren(this NodeModel root, List<NodeModel> nodes)
    {
        if (root.Children == null) return;

        foreach (var node in root.Children)
        {
            nodes.Remove(node);
        }
    }
}