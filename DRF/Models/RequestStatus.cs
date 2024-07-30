
[Dapper.Contrib.Extensions.Table("dbo.RequestStatus")]

public class RequestStatus
{

    public int? RequestId { get; set; }
    public string Status { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Notes { get; set; }

    [Dapper.Contrib.Extensions.Key]
    public int Id { get; set; }
}