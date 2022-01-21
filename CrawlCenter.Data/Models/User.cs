using System;

namespace CrawlCenter.Data.Models {
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User : EntityBase {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 密码（MD5加密）
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime? DateTimeJoined { get; set; }
    }
}