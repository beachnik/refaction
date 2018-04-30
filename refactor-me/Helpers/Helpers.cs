using System.Data.SqlClient;
using System.Web;

namespace refactor_me.Models
{
    public class Helpers
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        public static SqlConnection NewConnection()
        {
            string connstr = "";
            
            //A bit of a dirty hack to let me set up some unit testing
            if (HttpContext.Current != null)
            {
                connstr = ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));   
            } else
            {
                string unitTestFileLocation = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                unitTestFileLocation.Substring(0, unitTestFileLocation.IndexOf(@"\bin\"));
                connstr = ConnectionString.Replace("{DataDirectory}", unitTestFileLocation + @"\App_Data");
            }
            return new SqlConnection(connstr);
        }
    }
}