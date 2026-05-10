<%@ Page Title="" Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="history.aspx.cs" Inherits="Mir2Admin.Update.history" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div style="height: 6px;"></div>
    <asp:GridView ID="GridUpdate" runat="server" AllowPaging="true" AutoGenerateColumns="false"
        OnPageIndexChanging="PageChang" Width="100%" Height="100" PageSize="20"
        OnRowDataBound="GridUpdate_RowDataBound" OnRowCommand="GridUpdate_RowCommand">

        <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
        <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
        <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
        <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

        <Columns>
            <asp:TemplateField HeaderText="번호" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <div style="text-align: center;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_index") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="버전">
                <ItemTemplate>
                    <div style="text-align: center;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_version") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="업뎃일짜">
                <ItemTemplate>
                    <div style="text-align: center;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_date") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <div style="text-align: center;">                        
                        <asp:Button ID="BtnDelete" runat="server" Text="삭제"
                            OnClientClick="return confirm( '버전을 삭제하시겠습니까?')"
                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_version") %>'
                            CommandName="cmdUpdateDelete" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="관리자">
                <ItemTemplate>
                    <div style="text-align: center;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_author") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>



