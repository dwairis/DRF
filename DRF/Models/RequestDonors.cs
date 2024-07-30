

[Dapper.Contrib.Extensions.Table("dbo.RequestDonors")]

public class RequestDonors
{
    
    public int RequestId { get; set; }
    public int DonorId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    [Dapper.Contrib.Extensions.Key]
    public int Id { get; set; }
}