using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HeadCount.Controllers
{
    public class DataClass
    {
        public static void Connect()
        {
            ILoggerFactory loggerFactory = new LoggerFactory()
  .AddConsole()
  .AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Start read data from satabase!");

            Task.Run(
                () =>
                {
                    try
                    {
                        while (!Startup.ReadDataCancelToken.IsCancellationRequested)
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
                                    Startup.Num = (int)reader[0];
                                }

                                // Call Close when done reading.
                                reader.Dispose();
                            }
                            Task.Delay(1000, Startup.ReadDataCancelToken.Token);

                        }

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.ToString());
                    }

                }, Startup.ReadDataCancelToken.Token);
        }
    }
}