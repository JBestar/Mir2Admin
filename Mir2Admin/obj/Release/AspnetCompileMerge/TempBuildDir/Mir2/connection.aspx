<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="connection.aspx.cs" Inherits="Mir2Admin.Mir2.connection" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div style="height: 10px;"></div>
    <script>
        function myLoop() {
            setTimeout(function () {

                window.location.reload();
                myLoop();

            }, 60000)
        }
        myLoop();
    </script>
    <table class="table-content">
        <tr>
            <td >
                <asp:DropDownList ID="DropDownEmployee" runat="server" Width="240" Height="25" Visible="False"></asp:DropDownList>                
                <!--
                <a></a>
                <asp:Label Runat="server" ID="lbl_server" Width="40" Font-Bold="True" Font-Size="Medium" Text="서버" />            
                <asp:DropDownList ID="DropDownServerName" runat="server" Width="150" Height="25" Visible="True"></asp:DropDownList>
                <asp:DropDownList ID="DropDownServerNo" runat="server" Width="50" Height="25" Visible="True"></asp:DropDownList>                
                <a></a>                
                -->
                <asp:Label Runat="server" ID="Label1" Width="110" Height="25" Font-Bold="True" Font-Size="Medium" Text="아이디 || 캐릭" />                            
                <asp:TextBox ID="txt_search" runat="server" Width="200" />                
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="보기" Visible="False" />
            </td>
            
        </tr>
    </table>
    <asp:GridView ID="GridConnection" runat="server" AllowPaging="true" AutoGenerateColumns="false"
        OnPageIndexChanging="PageChang" Width="100%" Height="100" PageSize="20"
        OnRowDataBound="GridConnection_RowDataBound" OnRowCommand="GridConnection_RowCommand">

        <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
        <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
        <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
        <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

        <Columns>
            <asp:TemplateField HeaderText="번호">
                <ItemTemplate>
                    <div style="text-align: center; width: 30px;">
                        <asp:Label runat="server" ID="lbl_index" Width="30"
                            Text='<%# DataBinder.Eval(Container, "DataItem.lbl_index") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="오토">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_mb_uid") %>
                        <br>
                        <%# DataBinder.Eval(Container, "DataItem.lbl_mb_vip") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="서버이름">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_game_server") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="캐릭이름">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_game_name") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="오토상태">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_auto_running") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="캐릭정보">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_game_level") %>
                        <br>
                        <%# DataBinder.Eval(Container, "DataItem.lbl_game_locale") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="게임머니">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_game_money1") %>
                        <br>
                        <%# DataBinder.Eval(Container, "DataItem.lbl_game_money2") %>
                        
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="시작시간">
                <ItemTemplate>
                    <div style="text-align: left; margin-left: 2px;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_time_begin") %>
                        <br>
                        <%# DataBinder.Eval(Container, "DataItem.lbl_time_last") %>
                        
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="제품정보">
                <ItemTemplate>
                    <div style="text-align: center;">
                        <%# DataBinder.Eval(Container, "DataItem.lbl_app_title")%>
                        <br>
                        <%# DataBinder.Eval(Container, "DataItem.lbl_app_version")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="아이피">
                <ItemTemplate>
                    <div style="text-align: center;">
                        <asp:Label runat="server" ID="lbl_ipaddress"
                            Text='<%# DataBinder.Eval(Container, "DataItem.lbl_pub_addr") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <table style="width: 100%">
        <tr>
            <td style="width: 30%; text-align: left">
                <asp:Label ID="Lbl_ToTal" runat="server" Text="실행중인 오토가 없습니다.." Font-Bold="True" Font-Size="Medium"></asp:Label>
            </td>
            <td style="width: 30%; text-align: right">
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Excel출력" Visible="False" />
            </td>
        </tr>
    </table>


</asp:Content>
