<%@ Page Title="" Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="update.aspx.cs" Inherits="Mir2Admin.Update.update" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div style="height: 6px;"></div>
    <div style="z-index: +999; position: absolute; visibility: hidden;"></div>

    <table class="adm table-edit">
        <tr>
            <td class="td-edit1"></td>
        </tr>
        <tr>
            <td>

                <table class="normal_box">
                    <colgroup>
                        <col style="width: 200px;" />
                        <col style="width: 200px;" />
                        <col style="width: 500px;" />
                        <col style="width: 200px;" />
                    </colgroup>
                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td style="text-align: left; width: 80px;">업로드파일:</td>
                        <td style="text-align: left">
                            <asp:FileUpload ID="UploadCtrl" runat="server" AllowMultiple="false"></asp:FileUpload>
                            (zip파일)               
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td style="text-align: left; width: 100px;">업로드버전:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_version" runat="server" Width="240" />
                            <asp:Label ID="txt_version_last" runat="server" Width="200" />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>

                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td colspan="2" style="text-align: left">
                            <hr />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>

                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:Button ID="Btn_update" runat="server" Text="업로드" Width="100" OnClick="OnClickUpdate" />
                            <asp:Button ID="Btn_cancel" runat="server" Text="취 소" Width="100" OnClick="OnClickCancel" />
                        </td>
                        <td colspan="2" style="text-align: left"></td>
                        <td style="text-align: left"></td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>

</asp:Content>
