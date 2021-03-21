using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureReportController : ControllerBase
    {
        private readonly ILogger<TemperatureReportController> _logger;

        private readonly TemperatureReportContext dbContext;  
  
        public TemperatureReportController(ILogger<TemperatureReportController> logger, TemperatureReportContext dbContext)
        {
            this.dbContext = dbContext;  
            _logger = logger;
        }

        /// <summary>
        /// Gets all Temperature reports.
        /// </summary>
        /// <returns>The set of all temperature reports.</returns>
        [HttpGet]
        public IEnumerable<TemperatureReport> Get()
        {
            _logger.LogDebug("Get Called. {0} results", dbContext.Reports.Count());
            return dbContext.Reports;
        }

        /// <summary>
        /// Adds a new temperature report to the underlying persistence.
        /// </summary>
        /// <param name="tempTimesTen">The temperature multiplied by 10.</param>
        /// <returns><c>true</c> in all cases.</returns>
        [HttpPost]
        public bool Post(int tempTimesTen)
        {
            dbContext.AddReport(tempTimesTen);
            _logger.LogDebug("Post req: added {0}", tempTimesTen);
            // Console.WriteLine("Post req: added {0}", tempTimesTen);
            // Console.WriteLine("dbcontext={0}", dbContext.ContextId);

            return true;
        }

        /// <summary>
        /// Clears all previously posted temperature reports.
        /// </summary>
        /// <returns><c>true</c> in all cases.</returns>
        [HttpDelete]
        public bool Clear() {
            dbContext.ClearReports();
            return true;
        }
    }
}
