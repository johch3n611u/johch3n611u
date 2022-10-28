using NLog;
using System.Text;
using System.Web.Mvc;

namespace AutofacDemo.Controllers
{
    public class HomeController : Controller
    {
        ILog logger;

        public HomeController(ILog inLogger)
        {
            logger = inLogger;
        }

        public ActionResult Index()
        {
            logger.Write($"{System.DateTime.Now} 進入 Home/Index \r\n");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }

    public interface ILog
    {
        void Write(string message);
    }

    #region Basic LOG

    public class TextWriterLog : ILog
    {
        public void Write(string message)
        {
            System.IO.File.AppendAllText(@"C:\log\logFile.txt", message, Encoding.UTF8);
        }
    }

    #endregion

    #region NLOG

    public class TextWriterNLog : ILog
    {
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void SortLogger(string message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Trace:
                    logger.Trace(message);
                    break;
                case LogType.Debug:
                    logger.Debug(message);
                    break;
                case LogType.Info:
                    logger.Info(message);
                    break;
                case LogType.Warn:
                    logger.Warn(message);
                    break;
                case LogType.Error:
                    logger.Error(message);
                    break;
                default:
                    break;
            }
        }

        public void Write(string strMessage)
        {
            SortLogger(strMessage, LogType.Trace);
        }

        public enum LogType
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error
        }

        public class LogInfoModel
        {
            public LogType Type { get; set; }
            public string Exception { get; set; }
        }
    }

    #endregion

}