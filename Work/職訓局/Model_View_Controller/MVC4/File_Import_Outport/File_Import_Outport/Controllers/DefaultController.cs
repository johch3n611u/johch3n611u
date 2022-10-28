using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace File_Import_Outport.Controllers
{
    public class DefaultController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlDataAdapter da;
        DataSet ds;

        // GET: Default
        public ActionResult Index()
        {
            da = new SqlDataAdapter("select * from 學生",Conn);
            ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            StreamWriter wr = new StreamWriter(Server.MapPath("~\\FileOutput\\output.csv"),false,System.Text.Encoding.Default);
            string data = "";
            foreach (DataColumn colume in dt.Columns)
            {
                data += colume.ColumnName + ",";
            }
            data += "\n";
            wr.Write(data);
            data = "";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn colume in dt.Columns)
                {
                    data += row[colume].ToString().Trim() + ",";
                }
                data += "\n";
                wr.Write(data);
                data = "";

            }

            wr.Dispose();
            wr.Close();

            return View();
        }


        public ActionResult UpLoad()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpLoad(HttpPostedFileBase upfile)
        {
            if(upfile!=null)
            {
                if(Path.GetExtension(upfile.FileName)==".csv")
                {
                    da = new SqlDataAdapter("select * from 學生2", Conn);
                    ds = new DataSet();
                    da.Fill(ds);

                    StreamReader sr = new StreamReader(upfile.InputStream,System.Text.Encoding.Default);

                    sr.ReadLine();
                    string data = "";
                    string[] arrData;
                    while (!sr.EndOfStream)
                    {
                        data=sr.ReadLine();
                        arrData = data.Split(',');
                        DataRow dr = ds.Tables[0].NewRow();
                        int k = ds.Tables[0].Columns.Count;
                        for (int i=0;i<k;i++)
                        {
                            if(i==(k-1))
                            {
                                if(arrData[i]!="")
                                 dr[ds.Tables[0].Columns[i]]= Convert.ToDateTime(arrData[i]);

                            }
                            else
                                dr[ds.Tables[0].Columns[i]] = arrData[i];

                        }
                        ds.Tables[0].Rows.Add(dr);
                    }

                    SqlCommandBuilder sc = new SqlCommandBuilder(da);
                    da.Update(ds);

                   
                }
            }



            return View();
        }

    }
}