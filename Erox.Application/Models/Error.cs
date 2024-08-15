using Erox.Application.Enums;
using Microsoft.AspNetCore.Mvc.Versioning;


namespace Erox.Application.Models
{
    public class Error
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}
