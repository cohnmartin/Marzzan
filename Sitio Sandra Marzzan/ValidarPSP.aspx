<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidarPSP.aspx.cs" Inherits="ValidarPSP" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=3.1.9.807, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ShowComprobante(id) {
            window.setTimeout(function () {
                var oWnd = radopen('ReportViewer.aspx?Id=' + id, 'RadWindow2');
            }, 150);
        }

        function PaginaInicio() {
            window.location.href = "inicio.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Web20" VisibleTitlebar="true"
        Title="Atención">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="870" Title="Comprobante Pedido"
                Height="600" Modal="true" Overlay="false" NavigateUrl="ReportViewer.aspx" VisibleTitlebar="true" OnClientClose="PaginaInicio"
                VisibleStatusbar="false" ShowContentDuringLoad="false" Skin="WebBlue">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div>
        Impresión Comprobante Pago con Tarjeta
    </div>
    </form>
</body>
</html>
