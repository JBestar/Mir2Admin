<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="memberMulti.aspx.cs" Inherits="Mir2Admin.Mir2.memberMulti" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <link rel="stylesheet" href="../Contents/styles/jquery-ui.css" type="text/css" />

    <script src="../Contents/scripts/jquery.js"></script>
    <script src="../Contents/scripts/jquery-ui.js"></script>
    <script src="../Contents/scripts/datepicker-ko.js"></script>
        <script>
        $(function () {
            $("#<%= txt_until_date.ClientID %>").datepicker(
                {
                    dateFormat: 'yy-mm-dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'
                });

        });
    </script>
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
                        <td style="text-align: left">매장카테고리:</td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="DropDownEmployee" runat="server" Width="244" Height="22" />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td style="text-align: left">회원닉네임:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_mb_nickname" runat="server" Width="240" />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr class="col1 ht center" style="margin-top: 20px;">
                        <td style="text-align: left"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:CheckBox ID="Chk_mb_vip" runat="server" Text=" VIP회원" /></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td style="text-align: left">허가기간:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_until_date"
                                runat="server"
                                Width="80px"
                                AutoPostBack="False"
                                TabIndex="1"
                                placeholder="yyyy-mm-dd"
                                autocomplete="off"
                                MaxLength="10"></asp:TextBox>
                            (일까지)
                        </td>
                    </tr>
                    
                    <tr class="col1 ht center">
                        <td style="text-align: left"></td>
                        <td style="text-align: left">생성개수:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_mk_cnt" runat="server" Width="140" Style="ime-mode: disabled" />
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
                        <td style="text-align: left">
                            <asp:Button ID="BtnAutoMember" runat="server" Text="자동생성" Width="100" OnClick="BtnAutoMember_Click" />
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="BtnSaveMember" runat="server" Text="저 장" Width="100" OnClick="BtnSaveMember_Click" />
                            <asp:Button ID="BtnCancelMember"  runat="server" Text="취 소" Width="100"  OnClick="BtnCancelMember_Click" />
                        </td>
                        <td colspan="2" style="text-align: left"></td>
                        <td style="text-align: left"></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
    <div style="margin-top: 20px; margin-left: 10%; width: 600px;">
        <asp:GridView ID="GridMember" runat="server" AllowPaging="true" AutoGenerateColumns="false"
            OnPageIndexChanging="PageChang" Width="500px" Height="100" PageSize="20">

            <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
            <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
            <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
            <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

            <Columns>
                <asp:TemplateField HeaderText="아이디">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <%# DataBinder.Eval(Container, "DataItem.lbl_mb_uid") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="비번">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <%# DataBinder.Eval(Container, "DataItem.lbl_mb_pwd") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>


</asp:Content>
