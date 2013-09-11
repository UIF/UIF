using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace UrbanImpactCommon
{
   public class DatabaseUtility
   {

       /////public static string UIF_Server_connectionString = "Data Source=10.10.1.10;Initial Catalog=UIF_PerformingArts;User Id=pa;Password=performarts4!;";
       //////public static string UIF_Server_Athletics_connectionString = "Data Source=UIF-Server;Initial Catalog=UIF_Athletics;User Id=athle;Password=Ath4let1C!;";


       public static string UIF_Server_connectionString = "Data Source=PGH187DEVPIT03;Initial Catalog=UIF;User Id=gjanco;Password=password123%;";
       public static string UIF_Server_Athletics_connectionString = "Data Source=UIF-Server;Initial Catalog=UIF_Athletics;User Id=athle;Password=Ath4let1C!;";


        //public static string UIF_Server_connectionString = "Data Source=Ryan-PC;Initial Catalog=UIF_PerformingArts;User Id=pa;Password=performarts4!;";
        //public static string UIF_Server_Athletics_connectionString = "Data Source=Ryan-PC;Initial Catalog=UIF_Athletics;User Id=athle;Password=Ath4let1C!;";
        
       /////public static string UIF_Server_connectionString = "Data Source=Ryan-PC;Initial Catalog=UIF_PerformingArts_newerwork;User Id=pa;Password=performarts;";
       //public static SqlConnection con = new SqlConnection(UIF_Server_connectionString);
   }
}
