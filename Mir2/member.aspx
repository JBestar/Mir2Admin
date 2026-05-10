<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="member.aspx.cs" Inherits="Mir2Admin.Mir2.member" %>

<asp:content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder" >
<div style="height:6px;"></div>
<table  class="table-content">
    <tr>
        <td style="width:600px;">
            <asp:DropDownList ID="DropDownEmployee" runat="server" Width="240" Height="25"></asp:DropDownList>
            <asp:TextBox ID="txt_search" runat="server" Width="200" />
            <asp:Button ID="BtnSearch" runat="server" Text="회원검색" onclick="BtnSearch_Click" />
            <asp:TextBox ID="hide_mb_emp_fid" runat="server" Visible="false" />
        </td>
        <td style="text-align:right">
        <% if (curEmployee.emp_level >= 7)
        { %>        
            <asp:Button ID="BtnRegistorMulti" runat="server" Text="멀티등록" onclick="BtnRegistorMulti_Click" />
        <%} %>
        <% if (curEmployee.emp_level >= 7)
        { %>     
            <asp:Button ID="BtnRegistorMember" runat="server" Text="회원등록" onclick="BtnRegistorMember_Click" />
         <%} %>
        </td>
    </tr>
</table>
<asp:GridView ID="GridMember" runat="server" AllowPaging="true" AutoGenerateColumns="false"
    onpageindexchanging="PageChang" Width="100%" Height="100" PageSize="20"
    OnRowDataBound="GridMember_RowDataBound" OnRowCommand="GridMember_RowCommand"
    >

    <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
    <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
    <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
    <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

    <Columns>
        <asp:TemplateField HeaderText="번호">
            <ItemTemplate>
                <div style="text-align:center;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_index") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <div style="text-align:center; width:auto; min-width:100px;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_employee") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="아이디:비번">
            <ItemTemplate>
                <div style="text-align:center;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_mb_uid") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="닉네임">
            <ItemTemplate>
                <div style="text-align:center; margin-left:5px;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_nickname") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="등록일">
            <ItemTemplate>
                <div style="text-align:center;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_time_join") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="접속일">
            <ItemTemplate>
                <div style="text-align:center;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_time_last") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="허용일짜">
            <ItemTemplate>
                <div style="text-align:center;">
                    <%# DataBinder.Eval(Container, "DataItem.lbl_time_limit") %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <div style="text-align:center;">
                    <asp:Button ID="BtnActive" runat="server" Text="차단" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateActive" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <div style="text-align:center;">
                    <asp:Button ID="BtnChange" runat="server" Text="수정" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateChange" />
                    <asp:Button ID="BtnDelete" runat="server" Text="삭제" 
                        OnClientClick="return confirm( '회원정보를 삭제하시겠습니까?')"
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateDelete" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <div style="text-align:left; margin-left:10px; width:280px;">
                    <asp:Button ID="Btn1Week" runat="server" Text="1주연장" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdate1Week" />
                    <asp:Button ID="Btn2Week" runat="server" Text="2주연장" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdate2Week" />
                    <asp:Button ID="BtnMonth" runat="server" Text="1달연장" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_mb_fid") %>'
                        CommandName="cmdUpdateMonth" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:content>