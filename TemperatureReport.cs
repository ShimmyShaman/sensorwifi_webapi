using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi
{
    [Table("TMP_REPORTS")]
    public class TemperatureReport
    {
        [Key]
        public int Id {get; set;}

        public DateTime Time {get; set;}

        public int TempTimesTen {get;set;}
    }
}