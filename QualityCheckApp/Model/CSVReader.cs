using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace QualityCheckApp.Model
{
    public class CSVReader:DataReader
    {
        public override void ParseData()
        {
            
        }
        /*
        public override DataTable ReadFile1(string fullPath, bool headerRow)
        {

            string path = fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1);
            string filename = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
            DataSet ds = new DataSet();

            try
            {
                if (File.Exists(fullPath))
                {
                    string connectionStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}" + ";Extended Properties=\"Text;HDR={1};FMT=Delimited\\\"", path, headerRow ? "Yes" : "No");
                    string SQL = string.Format("SELECT * FROM {0}", filename);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, connectionStr);
                    adapter.Fill(ds, "TextFile");
                    ds.Tables[0].TableName = "Table1";                   
                }
                foreach (DataColumn col in ds.Tables["Table1"].Columns)
                {
                    col.ColumnName = col.ColumnName.Replace(" ", "_");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        */
        public override  DataTable ReadFile(string fullPath, bool headerRow)
        {
            DataTable dt = new DataTable("Data");
            try
            {
                using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" +
                Path.GetDirectoryName(fullPath) + "\";Extended Properties='text;HDR=yes;FMT=Delimited(,)';"))
                {
                    //Execute select query
                    using (OleDbCommand cmd = new OleDbCommand(string.Format("select *from [{0}]", new FileInfo(fullPath).Name), cn))
                    {
                        cn.Open();
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
    }
}
