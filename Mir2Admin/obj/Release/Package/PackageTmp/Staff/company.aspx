<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="company.aspx.cs" Inherits="Mir2Admin.Staff.company" %>

<asp:content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder" >


<div style="height:6px;"></div>

<table class="table-content">
    <tr>
        <td >
            <asp:Label Runat="server" ID="lbl_banner" Width="120" Text="본사명 || 아이디" />
            <asp:TextBox ID="txt_search" runat="server" Width="100" />
            <asp:Button ID="BtnSearch" runat="server" Text="본사검색" />
        </td>
        <td class="td-content-right">
            <asp:Button ID="BtnRegCompany" runat="server" Text="본사등록" onclick="BtnRegCompany_Click" />
        </td>
    </tr>
</table>
<asp:GridView ID="GridCompany" runat="server" AllowPaging="true" AutoGenerateColumns="false"
    onpageindexchanging="PageChang" Width="100%" Height="100" PageSize="20"
    OnRowDataBound="GridCompany_RowDataBound" OnRowCommand="GridCompany_RowCommand" >

    <HeaderStyle CssClass="normal_box" HorizontalAlign="Justify" VerticalAlign="Middle"></HeaderStyle>
    <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
    <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
    <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

    <Columns>
        <asp:TemplateField HeaderText="번호">
            <ItemTemplate>
                <div class="div-column-no">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_index") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="아이디">
            <ItemTemplate>
                <div class="div-column-id">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_mb_uid") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="본사명">
            <ItemTemplate>
                <div class="div-column-company">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_nickname") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="등록일">
            <ItemTemplate>
                <div class="div-column-date">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_time_join") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="접속일">
            <ItemTemplate>
                <div class="div-column-date">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_time_last") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <div class="div-column-stop">
                    <asp:Button ID="BtnActive" runat="server" Text="차단" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateActive" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="" >
            <ItemTemplate>
                <div class="div-column-modify">
                    <asp:Button ID="BtnChange" runat="server" Text="수정" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateChange" />
                    <asp:Button ID="BtnDelete" runat="server" Text="삭제" 
                        OnClientClick="return confirm( '본사를 삭제하시겠습니까?')"
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateDelete" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

</asp:content>
