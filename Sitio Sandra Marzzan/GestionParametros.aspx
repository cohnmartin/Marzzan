<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionParametros.aspx.cs"
    Inherits="GestionParametros" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Parametros Sandra Marzzan</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>



<body style="background-image: url(Imagenes/repetido.jpg); margin-top: 1px; background-repeat: repeat-x;
    background-color: White;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="FuncionesComunes.js" />
        </Scripts>
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                    </div>
                </div>
                <div class="CabeceraContent">
                    <table class="style1">
                        <tr>
                            <td align="center">
                                <table cellpadding="0" cellspacing="5" style="width: 80%">
                                    <tr>
                                        <td align="center" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                                                ForeColor="Gray" Text="Gestión de Parametros" Width="378px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticUpdates="true" AutoGenerateEditColumn="True"
                                            DataSourceID="LinqDataSource1" GridLines="None" Skin="Vista">
                                            <MasterTableView AutoGenerateColumns="False" DataSourceID="LinqDataSource1" AllowAutomaticUpdates="true"
                                                AllowAutomaticInserts="true" DataKeyNames="IdParametro" CommandItemDisplay="Bottom" 
                                                EditMode="PopUp" >
                                                <CommandItemTemplate>
                                                    <div style="padding: 5px 5px;">
                                                        <asp:LinkButton ID="btnInsert" runat="server" CommandName="InitInsert" >
                                                                <img style="padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/AddRecord.gif" />Nuevo Parametro</asp:LinkButton>
                                                    </div>
                                                </CommandItemTemplate>
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="IdParametro" DataType="System.Int64" Display="False"
                                                        HeaderText="IdParametro" ReadOnly="True" SortExpression="IdParametro" UniqueName="IdParametro"
                                                        Visible="False">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Tipo" HeaderText="Nombre Parametro" SortExpression="Tipo"
                                                        UniqueName="Tipo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Valor" HeaderText="Valor" SortExpression="Valor"
                                                        UniqueName="Valor">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Contexto" HeaderText="Contexto" ReadOnly="True"
                                                        SortExpression="Contexto" UniqueName="Contexto" Visible="False"  >
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings CaptionFormatString="Gestion Parametro">
                                                    <EditColumn ButtonType="ImageButton" EditText="Editar" >
                                                    </EditColumn>
                                                    <PopUpSettings ScrollBars="Auto" Modal="true" Width="50%"  />
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                       
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
        EnableUpdate="True" EnableDelete="true" EnableInsert="true" TableName="Parametros"
        Where="Contexto == @Contexto" oninserting="LinqDataSource1_Inserting" >
        <WhereParameters>
            <asp:Parameter DefaultValue="WEB" Name="Contexto" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="50">
        <ProgressTemplate>
            <div id="divBloq1">
            </div>
            <div class="processMessageTooltipGral">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                    <tr>
                        <td align="center">
                            <img alt="a" src="Imagenes/waiting.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div id="divTituloCarga" style="font-weight: bold; font-family: Tahoma; font-size: 12px;
                                color: Gray; vertical-align: middle">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
