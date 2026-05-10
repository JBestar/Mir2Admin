<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="password.aspx.cs" Inherits="Mir2Admin.Staff.password" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <table class="adm table-edit">
        <tr>
            <td class="td-edit1"></td>
        </tr>
        <tr>
            <td>
                <table class="normal_box" >
                    <colgroup>
                        <col style="width:200px;"/>
                        <col style="width:200px;"/>
                        <col style="width:500px;"/>
                        <col style="width:200px;"/>              
                    </colgroup>
                    <tr class="tr-edit ht center">
                        <td style="text-align:left"></td>
                        <td style="text-align:left">이전비밀번호:</td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txt_password_0" runat="server" Width="160" TextMode="Password" />
                        </td>
                        <td style="text-align:left"></td>
                    </tr>
                    <tr class="tr-edit ht center">
                        <td style="text-align:left"></td>
                        <td style="text-align:left">새비밀번호:</td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txt_password_1" runat="server" Width="160" TextMode="Password" />
                        </td>
                        <td style="text-align:left"></td>
                    </tr>
                    <tr class="tr-edit ht center">
                        <td style="text-align:left"></td>
                        <td style="text-align:left">비밀번호확인:</td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txt_password_2" runat="server" Width="160" TextMode="Password" />
                        </td>
                        <td style="text-align:left"></td>
                    </tr>

                    <tr class="tr-edit ht center">
                        <td style="text-align:left"></td>
                        <td colspan="2" style="text-align:left">
                            <hr>
                        </td>
                        <td style="text-align:left"></td>
                    </tr>

                    <tr class="tr-edit ht center">
                        <td style="text-align:left"></td>
                        <td colspan="2" style="text-align:left">
                            <asp:Button ID="BtnSavePassword" runat="server" Text="저 장" Width="100" OnClick="BtnSavePassword_Click" />
                            <asp:Button ID="BtnCancelPassword" runat="server" Text="취 소" Width="100" OnClick="BtnCancelPassword_Click" />
                        </td>
                        <td colspan="2" style="text-align:left"></td>
                        <td style="text-align:left"></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
