<%@ Control  Language="C#" AutoEventWireup="true"  CodeFile="ProductoRegalo.ascx.cs" Inherits="ProductoRegalo" %>
            <table width="100%" style="background-color:White">
                <tr>
                    <td  style="cursor:hand;background-image: url('Imagenes/TituloSecundarioResumen.jpg'); background-repeat: repeat-y;">
                         <table width="100%">
                            <tr>
                                <td align="left" style="width:100%">
                                     <asp:Label ForeColor="White" Font-Bold="true" Font-Size="12px" Font-Names="Sans-Serif" 
                                     ID="Label1" runat="server" Text="Seleccione su Regalo"></asp:Label>            
                                </td>
                            </tr>
                         </table>
                         
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                         <asp:ListBox ID="listProdRegalo" runat="server" Height="122px" Width="100%"
                          style="border:none" BackColor="#F4FAFF" SkinID="Web20">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" >
                        <asp:Button SkinID="btnBasic" ID="btnSolicitar" runat="server" Text="Solicitar" 
                          OnClientClick="CloseActiveToolTip();"  onclick="btnSolicitar_Click" UseSubmitBehavior="false" />    
                    </td>
                </tr>
            </table>
            

            