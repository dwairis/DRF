
[Dapper.Contrib.Extensions.Table("dbo.UploadedData")]
public class UploadedData
{
    [Dapper.Contrib.Extensions.Key]
    public int Id { get; set; }
    public int? RequestId { get; set; }
    public string FileUrl { get; set; }
    public string CaseNumber { get; set; }
    public string IndividualId { get; set; }
    public int RoundNumber { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}