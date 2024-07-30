
[Dapper.Contrib.Extensions.Table("dbo.LookupsCategory")]
public class LookupsCategory
{
    [Dapper.Contrib.Extensions.ExplicitKey]
    public int Id { get; set; }  
    public string CategoryName { get; set; }  
    public int? CreatedBy { get; set; }  
    public DateTime? CreatedAt { get; set; }  
}