<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionDerivaciones.aspx.cs" Inherits="GestionDerivaciones" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Derivaciones Sandra Marzzan</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <link href="MySkin/ToolTip.MySkin.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 90%;
        }
    </style>

    <script type="text/javascript">
        var oWnd;
        function OnClientItemsRequesting(sender, eventArgs) {
           
            var row = $find("RadGrid1").get_masterTableView().get_selectedItems()[0];
            var prov = $find("RadGrid1").get_masterTableView().getCellByColumnUniqueName(row, "Provincia").innerText;
            var context = eventArgs.get_context();
            
            context["Prov"] = prov;

        }
        function openWinAsig() {

            if ($find("RadGrid1").get_masterTableView().get_selectedItems().length > 0) {
                var row = $find("RadGrid1").get_masterTableView().get_selectedItems()[0];
                var prov = $find("RadGrid1").get_masterTableView().getCellByColumnUniqueName(row, "Provincia").innerText;
                document.getElementById("lblTitulo").innerText = document.getElementById("lblTitulo").innerText + prov;
              
                $find("toolTipAsignacion").show();
            }
            else {

                $find("tooltipAsignar").show();
            }
            
//            if ($find("RadGrid1").get_masterTableView().get_selectedItems().length > 0) {
//                var a = $find("RadGrid1").get_masterTableView().get_selectedItems()[0].getDataKeyValue("IdUsurioAltaTemprana")
//                var row = $find("RadGrid1").get_masterTableView().get_selectedItems()[0];
//                var provincia = $find("RadGrid1").get_masterTableView().getCellByColumnUniqueName(row, "Provincia").innerText;

//                oWnd = radopen("BusquedaLider.aspx?IdUsuario=" + a + "&Prov=" + provincia, "RadWindow1");
//            }
//            else {

//                $find("tooltipAsignar").show();
//            }
        }
        
        function OnClientClose(oWnd){
            //get the transferred arguments
                if (oWnd.argument) {
                    var cityName = oWnd.argument.selCoor;
                    oWnd.BrowserWindow.location.reload();
                //var seldate = arg.selDate;
                //$get("order").innerHTML = "You chose to fly to <strong>" + cityName + "</strong> on <strong>" + seldate + "</strong>";
            }
        }
    </script>

</head>
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="true" VisibleStatusbar="false"
        ReloadOnShow="false" runat="server" Skin="WebBlue">
        <Windows>
            <telerik:RadWindow runat="server" ID="RadWindow1" Behaviors="Close" Width="400" Height="240" Modal="true"
            OnClientClose="OnClientClose" NavigateUrl="BusquedaLider.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>   
        
    <telerik:RadToolTip ID="tooltipAsignar" runat="server" ShowEvent="FromCode" TargetControlID="RadGrid1"
        ShowCallout="true" Sticky="false" AutoCloseDelay="4000" Animation="Resize" Text="Debe Seleccionar un elemento del listado para poder asignarle el líder o sub líder." 
        Skin="Web20" Position="TopCenter"
        IsClientID="true">
    </telerik:RadToolTip>
    
    
    
                                                    
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                        <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"  Visible="false" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="63px" 
                    Text="El listado que se muestra a continuación posee los usuario que se han postulado
                        para ser revendedores, debe seleccionar cada uno de ellos y derivarlos a un líder o sub líder
                        para comenzar con la gestion correspondiente" 
                    Width="606px"></asp:Label>
                    
                
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="PageContent">
                                
                    <table class="style1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upGrila" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" Skin="WebBlue" DataSourceID="LinqDataSource1">
                                            <MasterTableView AutoGenerateColumns="False" DataSourceID="LinqDataSource1" DataKeyNames="IdUsurioAltaTemprana,Provincia"
                                                ClientDataKeyNames="IdUsurioAltaTemprana,Provincia">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="IdUsurioAltaTemprana" HeaderText="" ReadOnly="True"
                                                        Visible="false" SortExpression="IdUsurioAltaTemprana" UniqueName="IdUsurioAltaTemprana">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre Completo"
                                                        ReadOnly="True" SortExpression="NombreCompleto" UniqueName="NombreCompleto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FechaAlta" DataType="System.DateTime" HeaderText="Fecha Alta"
                                                        ReadOnly="True" SortExpression="FechaAlta" UniqueName="FechaAlta" DataFormatString="{0:dd/MM/yyyy}">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="objProvincia.Descripcion" HeaderText="Provincia"
                                                        ReadOnly="True" SortExpression="Provincia" UniqueName="Provincia">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Departamento" HeaderText="Departamento" ReadOnly="True"
                                                        SortExpression="Departamento" UniqueName="Departamento">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Direccion" HeaderText="Direccion" ReadOnly="True"
                                                        SortExpression="Direccion" UniqueName="Direccion">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="QuienPresento" HeaderText="Quien lo Presento"
                                                        ReadOnly="True" SortExpression="QuienPresento" UniqueName="QuienPresento">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comentario" HeaderText="Comentario realizado"
                                                        ReadOnly="True" SortExpression="Comentario" UniqueName="Comentario">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:25px; background:url('Imagenes/sprite_webBlue.gif') 0 -300px repeat-x;border:1px solid gray" >
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 40%" align="right">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="Imagenes/RealizarAlta.gif" />
                                        </td>
                                        <td style="width: 50%" align="left">
                                            <asp:Button ID="btnDerivar" runat="server" BackColor="Transparent" Style="cursor: hand;"
                                                BorderStyle="None" Height="18px" Font-Bold="true" Font-Names="Verdana" Font-Size="0.8em"
                                                ForeColor="black" Text="Realizar Derivación" Width="115px" OnClientClick="openWinAsig(); return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                
                
                  
                
                
                </div>
            </td>
        </tr>
    </table>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext" 
        Select="new (IdUsurioAltaTemprana, NombreCompleto, QuienPresento, FechaAlta, Comentario, Direccion, Departamento, objProvincia, Provincia)" 
        TableName="UsuariosAltaTempranas" Where="LiderDerivado == Null">
        
    </asp:LinqDataSource>
   
   
    <asp:UpdatePanel ID="upToolTip" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
     <telerik:RadToolTip runat="server" ID="toolTipAsignacion" Skin="MySkin"
     Sticky="true"    
     ManualClose="true" 
     Position="TopCenter"
     Width="400px"
     Height="280px"
     Animation="Fade"    
     RelativeTo="Element"
     ShowEvent="FromCode"
     EnableEmbeddedSkins="false"
     TargetControlID="btnDerivar" > 
    
       <table cellpadding="0" cellspacing="0" style="background-color:White;width:100%;height:100%">
            <tr>
                <td align="center">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="44px" 
                    Text="Líderes o Sub Líderes de la provincia " 
                    Width="363px"></asp:Label></td>
            </tr>
            <tr>
                <td align="center">
                    <telerik:RadComboBox ID="cboCoord" runat="server" Width="250px" Height="150px"
                        EmptyMessage="Escriba el nombre de un líder" 
                        EnableLoadOnDemand="True" ShowMoreResultsBox="false"  
                        EnableVirtualScrolling="false"  Skin="WebBlue"  ZIndex="100000"
                        OnClientItemsRequesting="OnClientItemsRequesting"
                        onitemsrequested="cboCoord_ItemsRequested">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label2" runat="server"  Font-Names="Tahomas" 
                    Font-Size="16px" ForeColor="#0066CC" Height="58px" 
                    Text="Recuerde que al momento de realizar la asignación el líder o sub líder
                    será informado vía mail de dicha acción" 
                    Width="312px"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="28%" border="0" cellpadding="0" cellspacing="0" >
                        <tr style="height:25px; >
                            <td style="vertical-align:middle" align="left" >
                                <asp:Button ID="btnAsignar" runat="server" SkinID="btnBasic"
                                    Text="Asignar" onclick="btnAsignar_Click"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
     
          
    </telerik:RadToolTip>
    
    <telerik:RadToolTip ID="ToolTipOk" runat="server" ShowEvent="FromCode" 
        TargetControlID="RadGrid1"
        ShowCallout="false" Sticky="false" Animation="Resize"
        Title="Asignación Correcta"
        Width="400"
        Text="La asignacion ha si realizado correctamente, y el líder o sub líder ha sido informado." 
        Skin="Web20" Position="Center" ManualClose="true"
        IsClientID="true">
    </telerik:RadToolTip>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
