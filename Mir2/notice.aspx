<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="notice.aspx.cs" Inherits="Mir2Admin.Mir2.notice" %>

<asp:content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder" >
<div style="height:6px;"></div>
<table style="width:100%">
    <tr>
        <td style="width:500px; margin:10px auto;">

            <asp:TextBox ID="notice_contents" runat="server" Height="480px" TextMode="MultiLine" Width="640px"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="공지내용업데이트" onclick="OnClickUpdateNotice" />
           
        </td>
        <td>

        </td>
    </tr>
</table>

</asp:content>