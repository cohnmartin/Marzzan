<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Styles.css" type="text/css" rel="stylesheet" />
    <style type="text/css">

        .style2
        {
            vertical-align:top;
        }
        .txtFormat
        {
        	filter: progid:dximagetransform.microsoft.gradient(gradienttype=0,startcolorstr=#dcdcdc,endcolorstr=#ffffff); 
	        background-color: transparent;
        	border-style:none;
       	}
    </style>
        
</head>
<body style="background-image: url(Imagenes/fondo.jpg); margin-top:2px ">
    <form id="form1" runat="server" >
 <script type="text/javascript">		
                var gridId = "DataGrid1";
				/* <![CDATA[ */									
				function crearnodo()
				{
                    var treeView = $find('RadTreeView2');
                    
                    var node = new Telerik.Web.UI.RadTreeNode();
                    node.set_text("Test");
                    treeView.get_nodes().add(node);
                    //The html you would like to use as a template
                    var html = "<table border='1'><tr><td>One</td><td>Two</td></tr></table>";
//                    var html = " <table><tr><td> <span id='RadTreeView2_i0_xx'>212 fem</span></asp:Label></td><td><div id='RadTreeView2_i0_RadNumericTexvtBox1_wrapper' class='radInput_Vista' style='display:inline;zoom:1;width:40px;'><table cellpadding='0' cellspacing='0' style='border-width:0;border-collapse:collapse;width:100%;'><tr><td class='inputCell' style='width:100%;white-space:nowrap;'><input type='text' value='0' maxlength='3' id='RadTreeView2_i0_RadNumericTexvtBox1_text' class='radEnabledCss_Vista' style='width:100%;' /><input style='visibility:hidden;float:right;margin:-18px 0 0 0;width:1px;height:1px;overflow:hidden;border:0;padding:0;' id='RadTreeView2_i0_RadNumericTexvtBox1' value='0' type='text' /><input style='visibility:hidden;float:right;margin:-18px 0 0 0;width:1px;height:1px;overflow:hidden;border:0;padding:0;' id='RadTreeView2_i0_RadNumericTexvtBox1_Value' name='RadTreeView2$i0$RadNumericTexvtBox1' value='0' type='text' /></td><td class='spinImgCell'><a class='spinbutton up' href='javascript: void(0)' id='RadTreeView2_i0_RadNumericTexvtBox1_SpinUpButton'><span>Spin Up</span></a><a class='spinbutton down' href='javascript: void(0)' id='RadTreeView2_i0_RadNumericTexvtBox1_SpinDownButton'><span>Spin Down</span></a></td></tr></table><input id='RadTreeView2_i0_RadNumericTexvtBox1_ClientState' name='RadTreeView2_i0_RadNumericTexvtBox1_ClientState' type='hidden' /></div></td></tr></table> "; 
//								
//								
                    //Get the DOM element which represents the node's contents
                    var contentElement = node.get_contentElement();
                    //Update its html with the template
                    contentElement.innerHTML = html;				
				
				}
				
				function isMouseOverGrid(target)
				{
					parentNode = target;
					while (parentNode != null)
					{					
						if (parentNode.id == gridId)
						{
							return parentNode;
						}
						parentNode = parentNode.parentNode;
					}
		            
					return null;
				}
		        
				function onNodeDragging(sender, args)
				{
					var target = args.get_htmlElement();	
					
					if(!target) return;
					
					if (target.tagName == "INPUT")
					{		
						target.style.cursor = "hand";
					}
					
					if (target.tagName == "DIV")
					{		
						target.style.cursor = "hand";
					}
					
					var grid = isMouseOverGrid(target)
					if (grid)
					{
						grid.style.cursor = "hand";
					}
				}
		        
				function dropOnHtmlElement(args)
				{					
				    //if(droppedOnInput(args))
				    //    return;
				        
				    if(droppedOnGrid(args))
				        return;					
				}
				
				function droppedOnGrid(args)
				{
				    var target = args.get_htmlElement();
				    
				    while(target)
				    {
				        if(target.id == gridId)
				        {
				            args.set_htmlElement(target);
				            return;				            				            				            
				        }
				        
				        target = target.parentNode;
				    }
				    args.set_cancel(true);
				}
				
				function onNodeDropping(sender, args)
				{			
					dropOnHtmlElement(args);
				}
		/* ]]> */
        </script> 
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
		<table  cellpadding="0" cellspacing="0">
            <tr>
                <td class="style2" colspan="2" align="center">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                  <div class="HeaderDer_panelContainer">
                                  </div>                            
                            </td>
                            <td>
                                  <div class="Header_panelContainer">

                                  </div>                                        
                            </td>
                            <td>
                                  <div class="HeaderIzq_panelContainer">

                                  </div>                            
                            </td>
                        </tr>
                    </table>                      
                 </td>
            </tr>
            <tr>
                <td class="style2">
                   <telerik:RadPanelBar runat="server" ID="RadPanelBar1" Skin="Vista"  
                        Height="480px" Width="380px" ExpandMode="FullExpandedItem" BackColor="#DDF1FA">
                    <CollapseAnimation Type="None" Duration="100"></CollapseAnimation>
			            <Items>
				            <telerik:RadPanelItem Text="TAL CUAL - MASCULINOS" Selected="true" Expanded="true" Value="grilla1" >
					            <Items >
						            <telerik:RadPanelItem>
							            <ItemTemplate >

							                <telerik:RadGrid ID="RadGrid1" runat="server" 
                                                GridLines="None" Skin="Vista" AutoGenerateColumns="False">
                                                <MasterTableView>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="col0" DataType="System.String" 
                                                            HeaderText="Codigo" ReadOnly="True" SortExpression="IdMaestroBase" 
                                                            UniqueName="IdMaestroBase"  >
                                                           <ItemStyle Width="30px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="col1" DataType="System.String" 
                                                            HeaderText="Nombre" ReadOnly="True" SortExpression="IdMaestroBase" 
                                                            UniqueName="IdMaestrsdoBase" >
                                                            <ItemStyle Width="230px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="50" UniqueName="TemplateColumn"   >
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="txtFormat" ID="ddsw" Text="" runat="server" Width="20px" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>                                                        
                                                        <telerik:GridTemplateColumn HeaderText="100" UniqueName="TemplateColumn"   >
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="txtFormat" ID="dds" Text="" runat="server" Width="20px" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                                                                                                                                                                                                                     
                                                        <telerik:GridTemplateColumn HeaderText="Plum" UniqueName="TemplateColumn"   >
                                                            <ItemTemplate>
                                                                
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" MaxLength="3" 
                                                                    MaxValue="999" MinValue="0" ShowSpinButtons="True" Skin="Vista" Value="0" 
                                                                    Width="40px">
                                                                    <NumberFormat AllowRounding="True" DecimalDigits="0" />
                                                                </telerik:RadNumericTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                                                                                                                                                                                                                                                
                                                    </Columns>

                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                            </telerik:RadGrid>

							            </ItemTemplate>
						            </telerik:RadPanelItem>
					            </Items>
				            </telerik:RadPanelItem>			

				            <telerik:RadPanelItem Text="TAL CUAL - FEMENINOS" Value="arbol">
					            <Items>
						            <telerik:RadPanelItem>
							            <ItemTemplate>
                                            <telerik:RadTreeView ID="RadTreeView1" runat="server" EnableDragAndDrop="True" OnNodeDrop="RadTreeView1_HandleDrop"
                                               OnClientNodeDropping="onNodeDropping" 
                                               OnClientNodeDragging="onNodeDragging" 
                                               OnDataBound="RadTreeView1_DataBound" Skin="Outlook">
                                            
                                            </telerik:RadTreeView>								            
							            </ItemTemplate>
						            </telerik:RadPanelItem>
					            </Items>
				            </telerik:RadPanelItem>	
				            <telerik:RadPanelItem Text="AROMATIZADORES - LA CADA DEL AROMA">
					            <Items>
						            <telerik:RadPanelItem>
							            <ItemTemplate>
                                            <br />
                                            <asp:Button ID="btncopia" runat="server" Text="crear" OnClientClick="crearnodo();return false;" />								            
							            </ItemTemplate>
						            </telerik:RadPanelItem>
					            </Items>
				            </telerik:RadPanelItem>	
				            <telerik:RadPanelItem Text="SANDRA MARZZAN">
					            <Items>
						            <telerik:RadPanelItem>
							            <ItemTemplate>
								            
							            </ItemTemplate>
						            </telerik:RadPanelItem>
					            </Items>
				            </telerik:RadPanelItem>					            				            
				            <telerik:RadPanelItem Text="ACCESORIOS">
					            <Items>
						            <telerik:RadPanelItem>
							            <ItemTemplate>
								            
							            </ItemTemplate>
						            </telerik:RadPanelItem>
					            </Items>
				            </telerik:RadPanelItem>					            
			            </Items>

                        <ExpandAnimation Type="None" Duration="100"></ExpandAnimation>
		            </telerik:RadPanelBar>				            			                          
                </td>
                <td valign="bottom" align="center">
		            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="LoadingPanel1">
		            <div class="panelContainer">
			            <div class="contentContainer">
				            <div class="panelContent">
					            <asp:Panel id="Panel1" runat="server" style="font: normal 12px Arial, Verdana, Sans-serif; color: #a6a896;">
                                   <asp:DataGrid runat="server" ID="DataGrid1" GridLines="none" Width="220px">
                                     </asp:DataGrid>      
                                     <telerik:RadTreeView ID="RadTreeView2" runat="server" Skin="Vista" >
                                            <Nodes>
                                                <telerik:RadTreeNode Text="Bakerloo">
                                                    <NodeTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="xx" Text="212 fem" runat="server" ></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="RadNumericTexvtBox1" runat="server" MaxLength="3" 
                                                                        MaxValue="999" MinValue="0" ShowSpinButtons="True" Skin="Vista" Value="0" 
                                                                        Width="40px">
                                                                        <NumberFormat AllowRounding="True" DecimalDigits="0" />
                                                                    </telerik:RadNumericTextBox>
                                                                </td>
                                                             
                                                            </tr>
                                                        </table>

                                                    </NodeTemplate>
                                                </telerik:RadTreeNode>  
                                            </Nodes>                                              
                                            </telerik:RadTreeView>	                                     
                                     <asp:Button ID="dd" runat="server" Text="dfsf" />                             
					            </asp:Panel>						
				            </div>
			            </div>
		            </div>
		            </telerik:RadAjaxPanel>
		            <telerik:RadAjaxLoadingPanel  id="LoadingPanel1" height="75px" width="600px" Runat="server" Transparency="5">
			            <asp:Image style="margin-left: 110px; margin-top: 140px;" id="Image1" runat="server" ImageUrl="~/Imagenes/LoadingProgressBar.gif" BorderWidth="0px" AlternateText="Loading"></asp:Image>
		            </telerik:RadAjaxLoadingPanel>
		        </td>
            </tr>
        </table>
		                
    </form>
</body>
</html>
