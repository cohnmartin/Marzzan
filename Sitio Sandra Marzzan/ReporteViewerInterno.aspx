<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteViewerInterno.aspx.cs" Inherits="ReporteViewerInterno" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=3.1.9.807, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>


<body >
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="content" >
        <center id="center">
            <telerik:ReportViewer ID="ReportViewer1" runat="server"  style="border:1px solid #ccc;" 
			width="99%" Height="800px"  ProgressText="Generando Reporte..." 
                ShowParametersButton="False" ShowPrintButton="False" ShowRefreshButton="False" 
                ShowZoomSelect="False" ShowExportGroup="false"  >
               <Resources ExportButtonText="Exportar"  
                   ExportSelectFormatText="Seleccione Formato Exportación" LabelOf="de" 
                   ProcessingReportMessage="Generando Reporte..."  />
            </telerik:ReportViewer>
        </center>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
       
    </telerik:RadAjaxManager>
    <script type="text/javascript">
        function ExportarRTF() {
            ReportViewer1.ExportReport("RTF");
            $find("<%=RadAjaxManager1.ClientID%>").ajaxRequest("Imprimir");
        }
    </script>   
    </form>
</body>
</html>
