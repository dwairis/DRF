

[Dapper.Contrib.Extensions.Table("dbo.Lookups")]
public class Lookups
{
    [Dapper.Contrib.Extensions.Key]
    //[Dapper.Contrib.Extensions.ExplicitKey]
    public int Id { get; set; }
    public string Value { get; set; }
    public int CategoryID { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}