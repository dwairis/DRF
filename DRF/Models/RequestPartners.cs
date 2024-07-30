
[Dapper.Contrib.Extensions.Table("dbo.RequestPartners")]

public class RequestPartners
{
    [Dapper.Contrib.Extensions.ExplicitKey]
    public int? RequestId { get; set; }
    public int? PartnerId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}