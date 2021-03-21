using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using webapi;
using System;
using System.Linq;

namespace webapi.Models
{
    public class TemperatureReportContext : DbContext
    {
        private readonly object threadLock = new object();

        public TemperatureReportContext(DbContextOptions<TemperatureReportContext> options)
            : base(options)
        {
            // Empty
        }

        /// <summary>
        /// The Reports.
        /// </summary>
        public DbSet<TemperatureReport> Reports { get; set; }

        /// <summary>
        /// Add a temperature report to the underlying persistence.
        /// </summary>
        /// <param name="tempTimesTen">The temperature multiplied by 10.</param>
        public void AddReport(int tempTimesTen)
        {
            lock(threadLock)
            {
                Reports.Add(new TemperatureReport() { Time = DateTime.Now, TempTimesTen = tempTimesTen});
                SaveChanges();
                Console.WriteLine("count now  :{0}", Reports.Count());
            }
        }

        /// <summary>
        /// Clears the underlying persistence table of all temperature reports.
        /// </summary>
        public void ClearReports()
        {
            lock(threadLock)
            {
                Database.ExecuteSqlRaw("delete from [dbo].[TMP_REPORTS];");
            }
        }
    }
}