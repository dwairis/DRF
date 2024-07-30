
[Dapper.Contrib.Extensions.Table("dbo.RequestTargetSectors")]

public class RequestTargetSectors
{

    public int RequestId { get; set; }
    public int? TargetSectorsID { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    [Dapper.Contrib.Extensions.Key]
    public int Id { get; set; }
}