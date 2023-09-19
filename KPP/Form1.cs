using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Datacontainer.connectsource = "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Sql;
            StreamWriter sw = new StreamWriter("KPP_PRODUKTIONSDATABAS.txt");
            if (Datacontainer.connectsource == "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;")
            {
                //  Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],[Personal number],[Familyname],[First Name] FROM[Klingen].[dbo].[Patients] WHERE[Personal number] IN(SELECT[Personal number] FROM[Klingen].[dbo].[Patients] GROUP BY[Personal number] HAVING COUNT(*) > 1)";
                Sql = "WITH CombinedInvoice AS (SELECT* FROM [Klingen].dbo.[Internal Invoice Amnion] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Blood] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Chorion] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Counselling] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice DNA] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Fibroblast] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice FISH] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Foetus] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Tumor])";
                Sql += "SELECT '2258' AS FilKod,'' AS PostID, FORMAT(GETDATE(), 'yyyy-MM-dd') AS RapportDatum,'2258' AS Saljare,'51001' AS SjukhusKod,REPLACE(C.[Personal number], '-', '') AS Pnr,LEFT(C.[Personal number], 2) AS Sekel,FORMAT(C.[Arrived date], 'yyyy-MM-dd') AS Handelsedatum,C.CustomerID, '' AS VDTID,'' AS VRDTyp,'L25' as Kostnadskod,1 AS Antal,FORMAT(C.Price, '0') AS Kostnad,'' AS Filler1,FORMAT(C.[Care Date], 'yyyy-MM-dd') AS Bestallningsdatum,'' AS Analyskod,'' AS Filler2,C.Type AS Analystext FROM CombinedInvoice C;";
//--Extract and transform the data

            }
            else
            {
                Sql = "WITH CombinedInvoice AS (SELECT* FROM [Klingen_test].dbo.[Internal Invoice Amnion] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice Blood] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice Chorion] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice Counselling] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice DNA] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice Fibroblast] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice FISH] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice Foetus] UNION ALL SELECT* FROM [Klingen_test].dbo.[Internal Invoice Tumor])";
                Sql += "SELECT '2258' AS FilKod,'' AS PostID, FORMAT(GETDATE(), 'yyyy-MM-dd') AS RapportDatum,'2258' AS Saljare,'51001' AS SjukhusKod,REPLACE(C.[Personal number], '-', '') AS Pnr,LEFT(C.[Personal number], 2) AS Sekel,FORMAT(C.[Arrived date], 'yyyy-MM-dd') AS Handelsedatum,C.CustomerID, '' AS VDTID,'' AS VRDTyp,'L25' as Kostnadskod,1 AS Antal,FORMAT(C.Price, '0') AS Kostnad,'' AS Filler1,FORMAT(C.[Care Date], 'yyyy-MM-dd') AS Bestallningsdatum,'' AS Analyskod,'' AS Filler2,C.Type AS Analystext FROM CombinedInvoice C;";
                //--Extract and transform the data
            }
            Datacontainer.command = new SqlCommand(Sql, Datacontainer.cnn);
            Datacontainer.command.CommandType = CommandType.Text;
            SqlDataReader reader = Datacontainer.command.ExecuteReader();
         //   int radnummer;
         //   radnummer = 4;
            while (reader.Read())
            {

            //    Datacontainer.Index = (int)reader.GetValue(0);
                Datacontainer.FilKod = (String)reader.GetValue(0);
                Datacontainer.outputstring = Datacontainer.FilKod;
                Datacontainer.outputstring += ",";
                Datacontainer.PostID = (String)reader.GetValue(1);
                Datacontainer.outputstring += Datacontainer.PostID;
                Datacontainer.outputstring += ",";
                Datacontainer.RapportDatum = (String)reader.GetValue(2);
                Datacontainer.outputstring += Datacontainer.RapportDatum;
                Datacontainer.outputstring += ",";
              //  Datacontainer.saljare = (String)reader.GetValue(3);
                Datacontainer.outputstring += (String)reader.GetValue(3);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(4);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(5);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(6);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(7);
                Datacontainer.outputstring += ",";
                if (reader.GetValue(8) !=  DBNull.Value)
                {
                    Datacontainer.outputstring += (String)reader.GetValue(8);
                    Datacontainer.outputstring += ",";
                }
                else
                {
                    Datacontainer.outputstring += ",";
                }
                Datacontainer.outputstring += (String)reader.GetValue(9);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(10);
                Datacontainer.outputstring += ",";
                if (reader.GetValue(11) != DBNull.Value)
                {
                    Datacontainer.outputstring += (String)reader.GetValue(11);
                    Datacontainer.outputstring += ",";
                }
                else
                {
                    Datacontainer.outputstring += ",";
                }
                if (reader.GetValue(12) != DBNull.Value)
                {
                    Datacontainer.outputstring += (int)reader.GetValue(12);
                    Datacontainer.outputstring += ",";
                }
                else
                {
                    Datacontainer.outputstring += ",";
                }
                Datacontainer.outputstring += (String)reader.GetValue(13);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(14);
                Datacontainer.outputstring += ",";
                if (reader.GetValue(15) != DBNull.Value)
                {
                    Datacontainer.outputstring += (String)reader.GetValue(15);
                    Datacontainer.outputstring += ",";
                }
                else
                {
                    Datacontainer.outputstring += ",";
                }
                Datacontainer.outputstring += (String)reader.GetValue(16);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(17);
                Datacontainer.outputstring += ",";
                Datacontainer.outputstring += (String)reader.GetValue(18);
              //  Datacontainer.outputstring += ",";
                sw.WriteLine(Datacontainer.outputstring);
               
            }

            reader.Close();
            sw.Close();
            FileInfo file = new FileInfo("KPP_PRODUKTIONSDATABAS.txt");
            if (Datacontainer.connectsource == "Data Source=Klingen-test-su-db,62468; Initial Catalog = Klingen_Test;")
            {
                file.MoveTo("KPP_TESTDATABAS.txt");
            }
          
                string message = "Utläsning klart !";
            string title = "";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                button2.Enabled = true;
            }
            else
            {
                // Do something
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Datacontainer.connectsource = "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Datacontainer.connectsource = "Data Source=Klingen-test-su-db,62468; Initial Catalog = Klingen_Test;";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Datacontainer.anvandarnamn = textBox1.Text;
            Datacontainer.losen = textBox2.Text;
            Datacontainer.connectionString = @Datacontainer.connectsource + "User ID=" + textBox1.Text + ";Password=" + textBox2.Text + "";
            Datacontainer.cnn = new SqlConnection(Datacontainer.connectionString);
            Datacontainer.cnn.Open();
            string message = "Connection Open  !";
            string title = "";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                button2.Enabled = true;
            }
            else
            {
                // Do something
            }


        }
    }
}
