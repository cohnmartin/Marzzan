<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using CommonMarzzan;

public class Upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        try
        {
            HttpPostedFile postedFile = context.Request.Files["Filedata"];

            string savepath = "";
            string tempPath = context.Request.QueryString["folder"];

            //tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
            //savepath = context.Server.MapPath(tempPath);

            savepath = context.Server.MapPath(context.Request.QueryString["folder"]);

            string filename = postedFile.FileName;
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);


            postedFile.SaveAs(savepath + @"\" + filename);

            // Load the Image
            string ancho = ""; string alto = "";

            if (!(filename.Contains(".xls") || filename.Contains(".xlsx") || filename.Contains(".doc") || filename.Contains(".docx")
                || filename.Contains(".pdf")))
            {
                using (System.Drawing.Image objImage = System.Drawing.Image.FromFile(savepath + @"\" + filename))
                {
                    // Display its Height and Width
                    ancho = objImage.Width.ToString();
                    alto = objImage.Height.ToString();
                }

                context.Response.Write(tempPath + "/" + filename + "|" + ancho + "|" + alto);
                context.Response.StatusCode = 200;
            }
            /// Logica aplicable solamente a los archivos de despacho
            else if (filename.Contains(".xls") && context.Request.QueryString["folder"].Contains("Despacho"))
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savepath + @"\" + filename + ";Extended Properties=Excel 8.0");
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Hoja1$]", connection);
                OleDbDataReader dr;
                try
                {

                    connection.Open();
                    dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch
                {

                    connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savepath + @"\" + filename + ";Extended Properties=Excel 8.0");
                    command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);
                    connection.Open();
                    dr = command.ExecuteReader(CommandBehavior.CloseConnection);

                }


                DataTable excelData = new DataTable("ExcelData");
                excelData.Load(dr);



                /// Validacion del archivo seleccionado
                string[] campos = new string[] { "CODIGO", "CONSULTOR", "NRODOC", "CCRR", "DIRECCION", "LOCALIDAD", "CODPOS", "PROVINCIA", "TELEFONO", "BULTOS", "TAMAÑO", "TOTAL_PEDIDO", "SALDOCTACTE", "CONTROL", "TOTAL_A_PAGAR", "VAL_DECLA", "DETPAGAR", "MENSAJE", "OBSERVACIONES", "COMPROBANTE", "COORDINADOR", "TELEFONOLIDER", "SCVTRN_COD", "ID", "SPVTCO_COD", "PESOESTIMADO", "ROTULO" };

                foreach (string nombrecampo in campos)
                {
                    if (excelData.Columns.IndexOf(nombrecampo) < 0)
                    {
                        context.Response.Write("La columna de datos: " + nombrecampo + " no existe en el documento o la misma a cambiado de nombre imposible procesar el archivo. Por favor tome contacto con el administrador.");
                        context.Response.StatusCode = 200;
                        return;

                    }
                }

                /// Validación de existencia de información en la base de datos.
                List<string> nrosGuias = (from DataRow row in excelData.Rows
                                          select row["COMPROBANTE"].ToString()).ToList<string>();


                Marzzan_InfolegacyDataContext dc = new Marzzan_InfolegacyDataContext();
                List<string> ExisteNroGuia = dc.DetalleGuias.Where(d => nrosGuias.Contains(d.Comprobante))
                                            .Select(w => w.Comprobante).ToList();

                if (ExisteNroGuia.Count > 0)
                {
                    string nros = string.Join("-", ExisteNroGuia.ToArray());

                    context.Response.Write("El archivo posee uno o mas nros de guia que ya han sido procesado en otro despacho." + nros);
                    context.Response.StatusCode = 200;
                    return;
                }


                context.Response.Write("a Generar|" + excelData.Rows[0]["Fecha"] + "|" + excelData.Rows[0]["Transporte"] + "|" + excelData.Rows.Count.ToString());
                context.Response.StatusCode = 200;

            }
            else
            {
                context.Response.Write(tempPath + "/" + filename + "|" + ancho + "|" + alto);
                context.Response.StatusCode = 200;
            }


        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}