
[Dapper.Contrib.Extensions.Table("dbo.RequestStatus")]

public class RequestStatus
{
    [Dapper.Contrib.Extensions.ExplicitKey]
    public int? RequestId { get; set; }
    public string Status { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Notes { get; set; }
}