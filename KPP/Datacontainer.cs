using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPP
{
    class Datacontainer
    {
        public static string connectionString;
        public static string connectsource;
        public static string anvandarnamn;
        public static string personnummer;
        public static string losen;
        public static string FilKod;
        public static string PostID;
        public static string RapportDatum;
        public static string outputstring;
      //  public static string saljare;
        public static SqlConnection cnn;
        public static SqlCommand command, command2;
        public static int Index;
    }
}
