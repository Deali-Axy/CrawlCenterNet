namespace CrawlCenter.Shared.DTO.User; 

public class UserCreateDto {
    /// <summary>
    /// 用户名
    /// </summary>
    public string Name { get; set; }
        
    /// <summary>
    /// 密码（未加密）
    /// </summary>
    public string Password { get; set; }
        
    /// <summary>
    /// 手机号码
    /// </summary>
    public string? Phone { get; set; }
}