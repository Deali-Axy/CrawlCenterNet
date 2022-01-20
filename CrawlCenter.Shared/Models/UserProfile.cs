namespace CrawlCenter.Shared.Models; 

/// <summary>
/// 用户信息
/// </summary>
public class UserProfile {
    /// <summary>
    /// ID、识别符
    /// </summary>
    public string Identity { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 加入时间
    /// </summary>
    public DateTime? DateTimeJoined { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Description { get; set; }
}