using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;

namespace BackendTestTask.Core;

[Dapper.Contrib.Extensions.Table("journals")]
public class Journal
{

    [Key]
    public long id { get; set; }
    public string text { get; set; }
    public long event_id { get; set; }
    public DateTime created_date_utc { get; set; }
}