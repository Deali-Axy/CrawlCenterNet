using System;

namespace CrawlCenter.Web.ViewModels {
    public class ErrorViewModel {
        public int? StatusCode { get; set; }
        public string RequestId { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }

        public bool ShowStatusCode => StatusCode != null;
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public bool ShowPath => !string.IsNullOrEmpty(Path);

        public bool ShowQueryString => !string.IsNullOrEmpty(QueryString);
    }
}