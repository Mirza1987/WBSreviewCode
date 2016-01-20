using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitArchive.Models
{
    public class ErrorTable
    {
        [Key]
        public int ErrorId { get; set; }
        public string AppMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public string ErrorLevel { get; set; }
        public DateTime ErrorTime { get; set; }

        public ErrorTable()
        {
            ErrorTime = DateTime.Now;
        }
    }
}