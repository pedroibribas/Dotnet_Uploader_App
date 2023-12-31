namespace Uploader.Data.Models.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FilePath { get; set; }
}