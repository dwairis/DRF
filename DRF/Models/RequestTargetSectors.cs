
[Dapper.Contrib.Extensions.Table("dbo.RequestTargetSectors")]

public class RequestTargetSectors
{
    [Dapper.Contrib.Extensions.ExplicitKey]
    public int? RequestId { get; set; }
    public int? TargetSectorsID { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}