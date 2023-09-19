using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            if (Datacontainer.connectsource == "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;")
            {
                //  Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],[Personal number],[Familyname],[First Name] FROM[Klingen].[dbo].[Patients] WHERE[Personal number] IN(SELECT[Personal number] FROM[Klingen].[dbo].[Patients] GROUP BY[Personal number] HAVING COUNT(*) > 1)";
                Sql = "WITH CombinedInvoice AS (SELECT* FROM [Klingen].dbo.[Internal Invoice Amnion] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Blood] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Chorion] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Counselling] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice DNA] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Fibroblast] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice FISH] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Foetus] UNION ALL SELECT* FROM [Klingen].dbo.[Internal Invoice Tumor])";
                Sql += "SELECT '2258' AS FilKod,'' AS PostID, FORMAT(GETDATE(), 'yyyy-MM-dd') AS RapportDatum,'2258' AS Saljare,'51001' AS SjukhusKod,REPLACE(C.[Personal number], '-', '') AS Pnr,LEFT(C.[Personal number], 2) AS Sekel,FORMAT(C.[Arrived date], 'yyyy-MM-dd') AS Handelsedatum,C.CustomerID, '' AS VDTID,'' AS VRDTyp,'L25' as Kostnadskod,1 AS Antal,FORMAT(C.Price, '0') AS Kostnad,'' AS Filler1,FORMAT(C.[Care Date], 'yyyy-MM-dd') AS Bestallningsdatum,'' AS Analyskod,'' AS Filler2,C.Type AS Analystext FROM CombinedInvoice C;";
//--Extract and transform the data

            }
            else
            {
                Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],[Personal number],[Familyname],[First Name] FROM[Klingen_test].[dbo].[Patients] WHERE[Personal number] IN(SELECT[Personal number] FROM[Klingen_test].[dbo].[Patients] GROUP BY[Personal number] HAVING COUNT(*) > 1)";

            }
            Datacontainer.command = new SqlCommand(Sql, Datacontainer.cnn);
            Datacontainer.command.CommandType = CommandType.Text;
            SqlDataReader reader = Datacontainer.command.ExecuteReader();
         //   int radnummer;
         //   radnummer = 4;
            while (reader.Read())
            {

                Datacontainer.Index = (int)reader.GetValue(0);
                Datacontainer.FilKod = (String)reader.GetValue(2);
                //Datacontainer.Familyname = (String)reader.GetValue(3);
               
            }

            reader.Close();
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
