
[Dapper.Contrib.Extensions.Table("dbo.RequestUpdates")]

public class RequestUpdates
{
    [Dapper.Contrib.Extensions.ExplicitKey]
    pub
    public int? RequestId { get; set; }
    public string Update { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}