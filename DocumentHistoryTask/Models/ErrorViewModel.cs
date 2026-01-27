using Microsoft.Extensions.Hosting;

namespace DocumentHistoryTask.Models

{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public string? ErrorMessage { get; set; } = "Не задано";

        public Exception? ParentException { get; set; }

        public bool ShowRequestId =>  !string.IsNullOrEmpty(RequestId);
    }
}
