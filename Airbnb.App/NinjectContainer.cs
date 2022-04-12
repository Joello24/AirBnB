using System;
using System.Data.SqlClient;
using Airbnb.Core;
using Airbnb.Core.Enums;
using Airbnb.CORE.Repositories;
using Airbnb.DAL;
using Airbnb.DAL.Database;
using Airbnb.UI;
using Ninject;

namespace Airbnb.App
{
    public static class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }
        private const string tester = "Server=localhost;Database=AirbnbNewYork;User Id=sa;Password=Fhlipoiop122!";
        private static SqlConnection conn;
        
        public static void Configure(LoggingMode logMode, string guestFile, string hostFile,string reservationFile, string logfile, ApplicationMode appMode)
        {
            Setup();
            Kernel = new StandardKernel();
            
            if (logMode == LoggingMode.Console)
            {
                Kernel.Bind<ILogger>().To<ConsoleLogger>();
            }
            else if(logMode == LoggingMode.File)
            {
              Kernel.Bind<ILogger>().To<FileLogger>().WithConstructorArgument("filePath", logfile);  
            }
            else
            {
                Kernel.Bind<ILogger>().To<NullLogger>();            
            }

            if (appMode == ApplicationMode.Database)
            {
                Kernel.Bind<IHostRepo>().To<HostDatabaseRepository>();
                Kernel.Bind<IGuestRepo>().To<GuestDatabaseRepository>().WithConstructorArgument(conn);
                Kernel.Bind<IReservationRepo>().To<ReservationDatabaseRepository>().WithConstructorArgument(conn);
            }
            else
            {
                Kernel.Bind<IHostRepo>().To<HostFileRepository>().WithConstructorArgument(hostFile);
                Kernel.Bind<IGuestRepo>().To<GuestFileRepository>().WithConstructorArgument(guestFile);
                Kernel.Bind<IReservationRepo>().To<ReservationFileRepository>()
                    .WithConstructorArgument(reservationFile);
            }

            Kernel.Bind<IListingRepo>().To<ListingRepo>();
            Kernel.Bind<Controller>().To<Controller>();
        }

        private static void Setup()
        {
            conn = new SqlConnection(tester);
            try
            {
                Console.WriteLine("TESTING CONNECTION TO DATABASE");
                conn.Open();
                Console.WriteLine("Connected to database");
                conn.Close();
            }catch (Exception e)
            {
                Console.WriteLine("Error connecting to database");
                Console.WriteLine(e.Message);
            }
        }
    }
}
