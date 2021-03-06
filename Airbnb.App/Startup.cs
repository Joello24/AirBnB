using Airbnb.Core;
using Ninject;
using Airbnb.Core;
using Airbnb.Core.Enums;
using Airbnb.DAL.Database;
using Airbnb.UI;

namespace Airbnb.App;

public static class Startup
{
  internal static void Run()
  {
    const string dataDir = "../Data/";
    const string guestFile = dataDir + "guests.csv";
    const string hostFile = dataDir + "hosts.csv";
    const string logFile = dataDir + "log.error.csv";
    const string reservationsDir = dataDir + "reservations/";

    View.DisplayHeader("Welcome to Air-BnB!");
    
    LoggingMode logMode = (LoggingMode)View.GetLoggingMode() switch
    {
      LoggingMode.None => LoggingMode.None,
      LoggingMode.Console => LoggingMode.Console,
      LoggingMode.File => LoggingMode.File,
      _ => throw new ArgumentOutOfRangeException()
    };

    NinjectContainer.Configure(logMode, guestFile,hostFile,reservationsDir, logFile, ApplicationMode.Database);
    var controller = NinjectContainer.Kernel.Get<Controller>();
    
    controller.Run();
  }
}
