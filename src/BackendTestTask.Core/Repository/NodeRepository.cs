using BackendTestTask.Core.Interfaces;
using Dapper;

namespace BackendTestTask.Core.Repository;

public class NodeRepository : BaseRepository<Node>, INodeRepository
{
    public NodeRepository(DapperContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<Node>> GetTreeNodesByNameAsync(string treeName)
    {
        var query = @$"WITH RECURSIVE NodeSql (id, parent_id, name, tree_name) AS
                        (SELECT ""id"", ""parent_id"", ""name"", ""tree_name""
                        FROM ""nodes"" 
                        WHERE ""id"" = (SELECT ""id"" FROM ""nodes"" tn WHERE tn.""tree_name"" = '{treeName}' Order By tn.""id"" LIMIT 1) 
                        UNION ALL
                        SELECT t.""id"", t.""parent_id"", t.""name"", t.""tree_name""
                        FROM NodeSql INNER JOIN ""nodes"" t
                        ON NodeSql.id = t.""parent_id"")
                        SELECT * FROM NodeSql";

        using var db = _context.CreateConnection();
        var nodes = await db.QueryAsync<Node>(query);
        
        return nodes;
    }

    public async Task<IEnumerable<Node>> GetTreeNodesByNodeIdAsync(long nodeId)
    {
        var query = @$"WITH RECURSIVE NodeSql (id, parent_id, name, tree_name) AS
                        (SELECT ""id"", ""parent_id"", ""name"", ""tree_name""
                        FROM ""nodes"" 
                        WHERE ""id"" = {nodeId}
                        UNION ALL
                        SELECT t.""id"", t.""parent_id"", t.""name"", t.""tree_name""
                        FROM NodeSql INNER JOIN ""nodes"" t
                        ON NodeSql.Id = t.""parent_id"")
                        SELECT * FROM NodeSql";

        using var db = _context.CreateConnection();
        var nodes = await db.QueryAsync<Node>(query);

        return nodes;
    }
}