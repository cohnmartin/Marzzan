using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Data.Objects;
using System.ComponentModel;
using System.Linq.Expressions;

/// <summary>
/// Summary description for HelperWeb
/// </summary>
public class HelperWeb
{
    public static string ToCapitalize(string inputString)
    {

        System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

        System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

        return textInfo.ToTitleCase(inputString.ToLower());

    }

    #region Metodos par la Exportacion a excel

    private static List<string> _datosReporte = new List<string>();

    private static void gvLegajos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        /*Create header row above generated header row*/
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //create row    
            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            int TotalColumnas = ((System.Web.UI.WebControls.GridView)(sender)).Columns.Count;
            int colSpan = TotalColumnas - 2 > 6 ? 6 : TotalColumnas - 1;

            #region Columna de Logo
            Image logo = new Image();
            int segmentos = HttpContext.Current.Request.Url.Segments.Count();
            string ruta = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.IndexOf(HttpContext.Current.Request.Url.Segments[segmentos - 1].ToString()) - 1);
            logo.ImageUrl = ruta + "/imagenes/LogoReportesPDF.png";
            logo.Width = Unit.Pixel(142);
            logo.Height = Unit.Pixel(92);

            TableCell left = new TableHeaderCell();
            left.ColumnSpan = 1;
            left.RowSpan = 4;
            left.Controls.Add(logo);
            left.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
            left.Style.Add(HtmlTextWriterStyle.PaddingTop, "10px");
            left.Width = Convert.ToInt32(18 * decimal.Parse("8,46"));
            left.Style.Add("white-space", "nowrap");
            row.Cells.Add(left);
            #endregion

            #region  Columna de TITULO
            left = new TableHeaderCell();
            left.ColumnSpan = colSpan;
            left.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            left.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#C5BE97");
            left.Style.Add(HtmlTextWriterStyle.Color, "Black");
            left.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            left.Style.Add(HtmlTextWriterStyle.FontFamily, "Calibri");
            left.Style.Add(HtmlTextWriterStyle.FontSize, "11");
            left.Style.Add("white-space", "nowrap");
            left.Text = _datosReporte[0];

            row.Cells.Add(left);

            //Add the new row to the gridview as the master header row
            //A table is the only Control (index[0]) in a GridView
            ((Table)(sender as GridView).Controls[0]).Rows.AddAt(0, row);
            #endregion

            #region  Columna de FECHA
            row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            //spanned cell that will span the columns I don't want to give the additional header 
            left = new TableHeaderCell();
            left.ColumnSpan = colSpan;
            left.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            left.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#C5BE97");
            left.Style.Add(HtmlTextWriterStyle.Color, "Black");
            left.Style.Add(HtmlTextWriterStyle.FontFamily, "Calibri");
            left.Style.Add(HtmlTextWriterStyle.FontSize, "11");
            left.Style.Add(HtmlTextWriterStyle.FontWeight, "Normal");
            left.Style.Add("white-space", "nowrap");
            left.Text = _datosReporte[1];
            row.Cells.Add(left);

            //Add the new row to the gridview as the master header row
            //A table is the only Control (index[0]) in a GridView
            ((Table)(sender as GridView).Controls[0]).Rows.AddAt(1, row);
            #endregion

            #region  Columna Descripcion 1
            row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            //spanned cell that will span the columns I don't want to give the additional header 
            left = new TableHeaderCell();
            left.ColumnSpan = colSpan;
            left.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            left.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#C5BE97");
            left.Style.Add(HtmlTextWriterStyle.Color, "Black");
            left.Style.Add(HtmlTextWriterStyle.FontFamily, "Calibri");
            left.Style.Add(HtmlTextWriterStyle.FontSize, "11");
            left.Style.Add(HtmlTextWriterStyle.FontWeight, "Normal");
            left.Style.Add("white-space", "nowrap");
            left.Text = _datosReporte[2];

            row.Cells.Add(left);

            ((Table)(sender as GridView).Controls[0]).Rows.AddAt(2, row);
            #endregion

            #region  Columna de Descripcion 2

            row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            //spanned cell that will span the columns I don't want to give the additional header 
            left = new TableHeaderCell();
            left.ColumnSpan = colSpan;
            left.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            left.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#C5BE97");
            left.Style.Add(HtmlTextWriterStyle.Color, "Black");
            left.Style.Add(HtmlTextWriterStyle.FontFamily, "Calibri");
            left.Style.Add(HtmlTextWriterStyle.FontSize, "11");
            left.Style.Add(HtmlTextWriterStyle.FontWeight, "Normal");
            left.Style.Add("white-space", "nowrap");
            left.Text = _datosReporte.Count > 3 ? _datosReporte[3] : "";
            row.Cells.Add(left);

            ((Table)(sender as GridView).Controls[0]).Rows.AddAt(3, row);

            #endregion

            #region Columna de Separacion en Blanco

            row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            left = new TableHeaderCell();
            left.ColumnSpan = TotalColumnas;
            left.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            left.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
            left.Style.Add("white-space", "nowrap");
            left.Text = "&nbsp;";
            row.Cells.Add(left);

            ((Table)(sender as GridView).Controls[0]).Rows.AddAt(4, row);

            #endregion


            foreach (TableCell item in e.Row.Cells)
            {
                item.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#C5BE97");
                item.Style.Add(HtmlTextWriterStyle.Color, "Black");
                item.Style.Add(HtmlTextWriterStyle.FontFamily, "Calibri");
                item.Style.Add(HtmlTextWriterStyle.FontSize, "11");
                item.Style.Add(HtmlTextWriterStyle.FontWeight, "Normal");
            }


        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add(HtmlTextWriterStyle.Color, "Black");
            e.Row.Height = Unit.Pixel(18);
        }

    }


    public static GridView GenerarExportExcel(object datosExportar, Dictionary<string, string> alias, List<string> camposExcluir, List<string> DatosReporte)
    {
        _datosReporte = DatosReporte;
        GridView gv = new GridView();
        gv.RowCreated += new GridViewRowEventHandler(gvLegajos_RowCreated);
        gv.AutoGenerateColumns = false;

        foreach (System.Reflection.PropertyInfo item in (datosExportar as IList)[0].GetType().GetProperties())
        {
            if (!camposExcluir.Contains(item.Name))
            {
                string alia = alias.ContainsKey(item.Name) ? alias[item.Name] : item.Name;
                BoundField boundField = new BoundField();
                boundField.DataField = item.Name;
                boundField.HeaderText = alia;
                boundField.NullDisplayText = "";
                boundField.ItemStyle.Wrap = false;
                boundField.HeaderStyle.Wrap = false;
                gv.Columns.Add(boundField);
            }

        }

        gv.DataSource = datosExportar;
        gv.DataBind();

        return gv;

    }

    #endregion
}
