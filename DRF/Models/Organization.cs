

[Dapper.Contrib.Extensions.Table("dbo.Organization")]

public class Organization
{
    [Dapper.Contrib.Extensions.Key]

    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsThirdParty { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Type { get; set; }
    public int CreatedBy { get; set; }
}