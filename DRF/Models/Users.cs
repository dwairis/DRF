﻿public class Users
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int? OrganizationID { get; set; }
    public string Email { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}