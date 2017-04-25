using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HeadCount.Controllers
{
    public class HomeController : Controller
    {
      static  ILoggerFactory loggerFactory = new LoggerFactory()
.AddConsole()
.AddDebug();
        ILogger logger = loggerFactory.CreateLogger<HomeController>();

        public IActionResult Index()
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
                    logger.LogDebug("People Count:" + num);
                    // Call Close when done reading.
                    reader.Dispose();
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
            }
           
            ViewData["Title"] = "人数统计";
            ViewData["Num"] = num;
   
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
