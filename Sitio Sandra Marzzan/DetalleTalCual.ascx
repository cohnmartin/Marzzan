<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetalleTalCual.ascx.cs" Inherits="DetalleTalCual" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


    <table width="100%">
    <tr>
        <td>50 mg</td>
        <td>100 mg</td>
        <td>Plum</td>
    </tr>
 <tr>
        <td>
              <telerik:RadNumericTextBox ID="RadNumericTextBoxsdfds1" runat="server" MaxLength="3" 
                    MaxValue="999" MinValue="0" ShowSpinButtons="True" Skin="Vista" Value="20" 
                    Width="40px">
                    <NumberFormat AllowRounding="True" DecimalDigits="0" />
                </telerik:RadNumericTextBox>
        </td>
        <td>
                <telerik:RadNumericTextBox ID="RadNumericTdfffffdextBox1" runat="server" MaxLength="3" 
                    MaxValue="999" MinValue="0" ShowSpinButtons="True" Skin="Vista" Value="0" 
                    Width="40px">
                    <NumberFormat AllowRounding="True" DecimalDigits="0" />
                </telerik:RadNumericTextBox>                                    
        </td>
        <td>
                <telerik:RadNumericTextBox ID="RadvfvvNumerdfdicTextBox2" runat="server" MaxLength="3" 
                    MaxValue="999" MinValue="0" ShowSpinButtons="True" Skin="Vista" Value="0" 
                    Width="40px">
                    <NumberFormat AllowRounding="True" DecimalDigits="0" />
                </telerik:RadNumericTextBox>                                    
        </td>
    </tr>                  
                     
</table>

