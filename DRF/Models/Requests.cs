
[Dapper.Contrib.Extensions.Table("dbo.Requests")]

public class Requests
{
    [Dapper.Contrib.Extensions.Key]

    public int Id { get; set; }
    public int ThirdPartyOrganization { get; set; }
    public string ProgramTitle { get; set; }
    public string BriefOnProgram { get; set; }
    public int TotalTarget { get; set; }
    public int TargetRequest { get; set; }
    public DateTime? ProjectStartDate { get; set; }
    public DateTime? ProjectEndDate { get; set; }
    public string Criteria { get; set; }
    public DateTime? ReferralDeliveryDL { get; set; }
    public int? ReferralTotal { get; set; }
    public int CounterPart { get; set; }
    public string ContactPerson { get; set; }
    public string CurrentStatus { get; set; }
    public string DataFileUrl { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HiredSelfEmployed { get; set; }
}