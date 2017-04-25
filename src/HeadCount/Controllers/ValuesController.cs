using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeadCount.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static ILoggerFactory loggerFactory = new LoggerFactory()
.AddConsole()
.AddDebug();
        ILogger logger = loggerFactory.CreateLogger<HomeController>();
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            int num = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(Startup.ConnecrString))
                {
                    var queryString = "SELECT [CurrentNum] FROM[GV_VMS].[dbo].[V_GetCurrentNUM];";
                    SqlCommand command =
                        new SqlCommand(queryString, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        num = (int)reader[0];
                    }

                    // Call Close when done reading.
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }

            return "{\"num\":" + num + "}";
        }

    }
}
