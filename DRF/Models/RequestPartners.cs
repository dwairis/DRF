
[Dapper.Contrib.Extensions.Table("dbo.RequestPartners")]

public class RequestPartners
{

    public int RequestId { get; set; }
    public int PartnerId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    [Dapper.Contrib.Extensions.Key]
    public int Id { get; set; }
}