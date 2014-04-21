<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" %>



<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=3.1.9.807, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
    <div id="content" >
        <center id="center">
            <telerik:ReportViewer ID="ReportViewer1" runat="server"  style="border:1px solid #ccc;" 
			width="99%" Height="800px"  ProgressText="Generando Reporte..." 
                ShowParametersButton="False" ShowPrintButton="True" ShowRefreshButton="False" 
                ShowZoomSelect="False"   >
               <Resources ExportButtonText="Exportar" 
                   ExportSelectFormatText="Seleccione Formato Exportación" LabelOf="de" 
                   ProcessingReportMessage="Generando Reporte..."  />
                   
            </telerik:ReportViewer>
        </center>
    </div>
    </form>
</body>
</html>
