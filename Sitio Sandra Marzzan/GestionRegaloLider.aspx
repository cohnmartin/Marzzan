<%@ Page Language="C#" Theme="SkinMarzzan" AutoEventWireup="true" CodeFile="GestionRegaloLider.aspx.cs" Inherits="GestionRegaloLider" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Regalo de tu Líder</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 292px;
        }
    </style>
  
</head>
<script type="text/javascript">
    function ControlarCarga() {
        var tooltipo = $find('<%= tooltipCarga.ClientID %>');
        if ($find('<%= cboPromociones.ClientID %>').get_value() == "") {

            tooltipo.set_targetControlID('<%= cboPromociones.ClientID %>');
            tooltipo.set_text("Debe Seleccionar el regalo que desea configurar");
            tooltipo.show();
            return false;

        }
        else if ($find('<%= cboDefiniciones.ClientID %>').get_value() == "") {

            tooltipo.set_targetControlID('<%= cboDefiniciones.ClientID %>');
            tooltipo.set_text("Debe Seleccionar el tipo de definición para agregar la configuración");
            tooltipo.show();
            return false;

        }


        return true;



    }
</script>    
<body style="background-image: url(Imagenes/repetido.jpg); margin-top:1px; background-repeat:repeat-x;background-color:White;"  >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadToolTip runat="server" ID="tooltipCarga" Skin="WebBlue" Sticky="false"
                ShowCallout="true" AutoCloseDelay="4000" Width="220px" Animation="None" Position="BottomCenter"
                ManualClose="true" RelativeTo="Element" ShowEvent="FromCode" Title="Atención!!">
    </telerik:RadToolTip>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50" >
        <ProgressTemplate>
            <div class="progressBackgroundFilterBlue">
            </div>
            <div style="position: absolute; top: 45%; left: 38%; padding: 5px; width: 24%; z-index: 1001;
                background-color: Transparent; vertical-align: middle; text-align: center;">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 62px;">
                    <tr>
                        <td align="center">
                            <img alt="a" src="Imagenes/waiting.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbltitulopaciente1" runat="server" Font-Bold="True" Font-Names="Thomas"
                                Font-Size="12px" ForeColor="Black" Height="21px" Style="vertical-align: middle"
                                Text="">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="Header_panelContainerSimple">
                    <div class="CabeceraInicial">
                        <asp:Button ID="btnVolver" runat="server" Text="" SkinID="btnVolver" OnClick="btnVolver_Click"   Visible="false" />
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
                                                ForeColor="Gray" Text="Gestión de Regalo de tu Líder" Width="378px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinqDataSource ID="LinqDataSourcePromo" runat="server" ContextTypeName="CommonMarzzan.Marzzan_InfolegacyDataContext"
                                    OrderBy="Descripcion"
                                    TableName="Productos" Where=" Tipo == @Tipo ">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="R" Name="Tipo" Type="Char" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                                <asp:UpdatePanel ID="upSelectedPromo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" class="style1">
                                            <tr>
                                                <td style="width: 30%; text-align: right">
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones" Text="Tipo Regalos:" runat="server"></asp:Label>
                                                </td>
                                                <td style="height: 25px">
                                                    <telerik:RadComboBox ID="cboPromociones" runat="server" Width="496px" EmptyMessage="Seleccione un Regalo"
                                                        Skin="WebBlue" AllowCustomText="true" MarkFirstMatch="true" DataSourceID="LinqDataSourcePromo" 
                                                        DataTextField="Descripcion" DataValueField="IdProducto" AutoPostBack="True" OnSelectedIndexChanged="cboPromociones_SelectedIndexChanged">
                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upAgregarDefinicion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="1" cellspacing="2" class="style1">
                                            <tr>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones3" Text="Unidades Negocio:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboUndNeg" runat="server" Skin="Vista" Width="200px" OnItemsRequested="cboUndNeg_ItemsRequested"
                                                        OnClientSelectedIndexChanging="LoadLineas" OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones4" Text="Lineas:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboLineas" runat="server" Skin="Vista" Width="200px" OnItemsRequested="cboLineas_ItemsRequested"
                                                        OnClientSelectedIndexChanging="LoadFragancias" OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones6" Text="Fragancias:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboFragancias" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboFragancias_ItemsRequested" OnClientSelectedIndexChanging="LoadPresentaciones"
                                                        OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                            </tr>
                                            <tr>
                                               
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones7" Text="Presentaciones:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboPresentaciones" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboPresentaciones_ItemsRequested" OnClientSelectedIndexChanging="LoadDefiniciones"
                                                        OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                                <td>
                                                    <asp:Label SkinID="lblBlue" ID="lblPromociones8" Text="Definiciones:" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cboDefiniciones" runat="server" Skin="Vista" Width="200px"
                                                        OnItemsRequested="cboDefiniciones_ItemsRequested" OnClientItemsRequested="ItemsLoaded" />
                                                </td>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnAgregarDefinicion" runat="server" SkinID="btnAgregar" Text="Agregar"
                                                         OnClientClick="return ControlarCarga();" OnClick="btnAgregarDefinicion_Click" />
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="upGrilla" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <telerik:RadGrid ID="grillaComponentes" runat="server" AllowPaging="False" Width="90%"
                                            Font-Size="11px" Font-Names="Segoe UI,tahoma,verdana,sans-serif" 
                                            GridLines="None" Skin="Vista" 
                                            OnItemDataBound="grillaComponentes_ItemDataBound"
                                            OnItemCreated="grillaComponentes_ItemCreated">
                                            <MasterTableView GroupLoadMode="Client" 
                                                AutoGenerateColumns="False"
                                                GroupHeaderItemStyle-HorizontalAlign="Left" 
                                                GroupsDefaultExpanded="false"
                                                ShowGroupFooter="false"
                                                DataKeyNames="Idcomposicion"
                                                NoDetailRecordsText="No hay definiciones realizadas" 
                                                NoMasterRecordsText="No hay definiciones realizadas">
                                                <GroupByExpressions>
                                                    <telerik:GridGroupByExpression>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="Grupo" HeaderText="Grupo" />
                                                        </SelectFields>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldName="Cantidad" HeaderText="Cantidad" />
                                                        </SelectFields>
                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="Grupo"  />
                                                        </GroupByFields>
                                                    </telerik:GridGroupByExpression>
                                                </GroupByExpressions>
                                                <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                    Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                                                    Resizable="False" Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="Producto">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProd"><%# Eval("objProductoHijo.DescripcionCompleta")%></asp:Label>
                                                            <asp:Label runat="server" ID="Label1"><%# Eval("objPresentacion.Descripcion")%></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Eliminar Grupo Completo:
                                                            <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif"  />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="Template1" HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="btnEliminar" ImageUrl="~/Imagenes/Delete.gif" OnClick="btnEliminar_Click" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings>
                                                
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnGrabar" runat="server" SkinID="btnBasic" Text="Grabar" OnClick="btnGrabar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        
                        <tr style="display:none">
                            <td>
                                <table cellpadding="1" cellspacing="2" class="style1">
                                    <tr>
                                        <td style="width:20%;height:250px">
                                            <table cellpadding="0" cellspacing="0" border="1" style="height:100%;width:100%">
                                                <tr>
                                                    <td valign="top" style="height: 325px;width:100%">
                                                    <div style="overflow:auto;height:100%">
                                                        <telerik:RadTreeView ID="RadTreeProductos" runat="server" DataFieldID="IdProducto"
                                                            DataFieldParentID="Padre" DataTextField="Descripcion" 
                                                            DataValueField="IdProducto" Skin="Vista">
                                                            <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                                                            <ExpandAnimation Duration="100"></ExpandAnimation>
                                                        </telerik:RadTreeView>
                                                    </div>    
                                                    </td>
                                                </tr>
                                                <tr style="height:25px" valign="middle" >
                                                    <td align="left" style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                                            <asp:LinkButton ID="btnAbrirCerrar" runat="server"  
                                                            style="cursor:hand; " 
                                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                                                                <img style="cursor:hand;padding-left: 5px;padding-right: 5px;border:0px;vertical-align:middle;" alt="" src="Imagenes/AddRecord.gif" />Abrir/Cerrar
                                                            </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width:20%;height:250px">
                                            <table cellpadding="0" cellspacing="0" border="1" style="height:100%;width:100%">
                                                <tr>
                                                    <td valign="top" style="height: 325px;width:100%">
                                                    <div style="overflow:auto;height:100%">
                                                        <telerik:RadTreeView  ID="RadTreeDefinicion" runat="server" Skin="Vista" >
                                                            <Nodes>
                                                                <telerik:RadTreeNode runat="server" Text="Definición" Expanded="true">
                                                                    <Nodes>
                                                                        <telerik:RadTreeNode runat="server" Text="Regalo">
                                                                        </telerik:RadTreeNode>
                                                                    </Nodes>
                                                                </telerik:RadTreeNode>
                                                            </Nodes>
                                                            <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                                                            <ExpandAnimation Duration="100"></ExpandAnimation>
                                                        </telerik:RadTreeView>
                                                    </div>
                                                    </td>
                                                </tr>
                                                <tr  style="height:25px" valign="middle" >
                                                    <td align="left"  style="height: 25px; background: url('imagenes/sprite_webBlue.gif') 0  -300px repeat-x">
                                                        <asp:LinkButton ID="btnRequerido" runat="server"  style="cursor:hand; " 
                                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                                                             <img style="padding-left: 5px; padding-right: 5px; border: 0px; vertical-align: middle;"
                                                                alt="" src="Imagenes/AddRecord.gif" />Requerido
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                
                
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

  <script type="text/javascript">
      

      function LoadLineas(combo, eventarqs) {
          var DefinicionesCombo = $find("cboDefiniciones");
          var LineaCombo = $find("cboLineas");
          var FraganciasCombo = $find("cboFragancias");
          var PresentacionesCombo = $find("cboPresentaciones");
          var item = eventarqs.get_item();
          
          LineaCombo.set_text("Cargando....");
          FraganciasCombo.clearSelection();
          PresentacionesCombo.clearSelection();
          DefinicionesCombo.clearSelection();


          if (item.get_index() >= 0) {
              LineaCombo.requestItems(item.get_value(), false);
          }
          else {
              LineaCombo.set_text(" ");
              LineaCombo.clearItems();

              FraganciasCombo.set_text(" ");
              FraganciasCombo.clearItems();

              PresentacionesCombo.set_text(" ");
              PresentacionesCombo.clearItems();

              DefinicionesCombo.set_text(" ");
              DefinicionesCombo.clearItems();

          }

      }
     
      function LoadFragancias(combo, eventarqs) {
          var DefinicionesCombo = $find("cboDefiniciones");
          var FraganciasCombo = $find("cboFragancias");
          var PresentacionesCombo = $find("cboPresentaciones");
          var item = eventarqs.get_item();

          if (item.get_index() >= 0) {
              FraganciasCombo.set_text("Cargando....");
              PresentacionesCombo.clearSelection();
              DefinicionesCombo.clearSelection();
              FraganciasCombo.requestItems(item.get_value(), false);
          }
          else {

              FraganciasCombo.set_text(" ");
              FraganciasCombo.clearItems();

              PresentacionesCombo.set_text(" ");
              PresentacionesCombo.clearItems();

              DefinicionesCombo.set_text(" ");
              DefinicionesCombo.clearItems();

          }

      }
      function LoadPresentaciones(combo, eventarqs) {
          var LineaCombo = $find("cboLineas");
          var DefinicionesCombo = $find("cboDefiniciones");
          var PresentacionesCombo = $find("cboPresentaciones");
          var item = eventarqs.get_item();

          if (item.get_index() >= 0) {
              PresentacionesCombo.set_text("Cargando....");
              DefinicionesCombo.clearSelection();
              PresentacionesCombo.requestItems(item.get_value() + "|" + LineaCombo.get_value(), false);
          }
          else {

              PresentacionesCombo.set_text(" ");
              PresentacionesCombo.clearItems();

              DefinicionesCombo.set_text(" ");
              DefinicionesCombo.clearItems();

          }


      }

      function LoadDefiniciones(combo, eventarqs) {
          var DefinicionesCombo = $find("cboDefiniciones");
          var item = eventarqs.get_item();

          if (item.get_index() >= 0) {
              DefinicionesCombo.set_text("Cargando....");
              DefinicionesCombo.requestItems(item.get_value(), false);
          }
          else {

              DefinicionesCombo.set_text(" ");
              DefinicionesCombo.clearItems();

          }

      }
      
      
      function ItemsLoaded(combo, eventarqs) {
          if (combo.get_items().get_count() > 0) {
              combo.set_text("")
              combo.showDropDown();
          }
          else {
              var DefinicionesCombo = $find("cboDefiniciones");
              DefinicionesCombo.set_text("Cargando....");
              DefinicionesCombo.requestItems("", false);
           }

       }

      

      
</script>
