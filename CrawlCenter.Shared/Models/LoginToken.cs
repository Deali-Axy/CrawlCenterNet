namespace CrawlCenter.Shared.Models;

public class LoginToken {
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}