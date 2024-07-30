

[Dapper.Contrib.Extensions.Table("dbo.RequestDonors")]

public class RequestDonors
{
    [Dapper.Contrib.Extensions.ExplicitKey]
    public int? RequestId { get; set; }
    public int? DonorId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}