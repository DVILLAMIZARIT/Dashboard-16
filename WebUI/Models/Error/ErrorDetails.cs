using System;

namespace WebUI.Models.Error
{
    public class ErrorDetails
    {
        public Int32 Code { get; set; }

        public Exception Exception { get; set; }
    }
}