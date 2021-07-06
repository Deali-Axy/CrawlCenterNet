namespace CrawlCenter.Contrib.WebMessages {
    public class Message {
        public string Tag { get; set; }
        public string Content { get; set; }
    }

    public static class MessageTags {
        public const string Debug = "secondary";
        public const string Info = "info";
        public const string Success = "success";
        public const string Warning = "warning";
        public const string Error = "danger";
    }
}