using System;
using Airbnb.Core;
using Airbnb.CORE.Repositories;
using Airbnb.DAL;
using Airbnb.UI;
using Ninject;

namespace Airbnb.App
{
    public static class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }
        
        public static void Configure(LoggingMode logMode, string guestFile, string hostFile,string reservationFile, string logfile)
        {
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
            
            Kernel.Bind<IHostRepo>().To<HostFileRepository>().WithConstructorArgument(hostFile);
            Kernel.Bind<IGuestRepo>().To<GuestFileRepository>().WithConstructorArgument(guestFile);
            Kernel.Bind<IReservationRepo>().To<ReservationFileRepository>().WithConstructorArgument(reservationFile);
            
            Kernel.Bind<Controller>().To<Controller>();
        }
    }
}
