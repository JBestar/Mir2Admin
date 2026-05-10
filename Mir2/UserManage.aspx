<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="Mir2Admin.Mir2.UserManage" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div style="height: 6px;"></div>
    <div style="z-index: +999; position: absolute; visibility: hidden;"></div>
    <table>
        <tr>
            <th style="border-bottom: double; background-color: #FFF; color: #00115d">
                <asp:DropDownList ID="DropBlackEmployee" runat="server" Width="220" Height="25" Visible="True"></asp:DropDownList>

                <asp:Label ID="Lbl_ToTal" runat="server" Text="블랙리스트" Font-Bold="True" Font-Size="Large"></asp:Label>
            </th>
            <th></th>
            <th style="border-bottom: double; background-color: #FFF; color: #00115d">
                <asp:DropDownList ID="DropUserEmployee" runat="server" Width="220" Height="25" Visible="True"></asp:DropDownList>
                <asp:Label ID="Lbl_Account" runat="server" Text="아군리스트" Font-Bold="True" Font-Size="Large"></asp:Label>
            </th>
        </tr>
        <colgroup>
            <col style="width: 600px;" />
            <col style="width: 50px;" />
            <col style="width: 600px;" />

        </colgroup>
        <tr>
            <td style="text-align: left; vertical-align: top;">
                <div style="height: 10px;"></div>
                <div style="text-align: left;">
                    <asp:Label runat="server" ID="lbl_server" Width="50" Font-Bold="True" Font-Size="Medium" Text="서버" />
                    <asp:DropDownList ID="DropBlackServerName" runat="server" Width="135" Height="25" Visible="True"></asp:DropDownList>
                    <asp:DropDownList ID="DropBlackServerNo" runat="server" Width="60" Height="25" Visible="True"></asp:DropDownList>
                    <asp:Button ID="BtnBlackView" runat="server" OnClick="BtnBlackView_Click" Text="보기" Visible="True" Width="70" />
                </div>
                <div style="height: 3px;"></div>
                <div style="text-align: left;">

                    <asp:Label runat="server" ID="Label1" Width="50" Height="25" Font-Bold="True" Font-Size="Medium" Text="유저" />
                    <asp:TextBox ID="txt_black" runat="server" Width="200" />
                    <asp:Button ID="BtnBlackAdd" runat="server" OnClick="BtnBlackAdd_Click" Text="추가" Visible="True" Width="70" />
                    <asp:Button ID="BtnBlackFind" runat="server" OnClick="BtnBlackFind_Click" Text="검색" Visible="True" Width="70" />
                    <asp:Button ID="BtnBlackAllDelete" runat="server" OnClick="BtnBlackDelete_Click"
                        OnClientClick="return confirm( '전부 삭제하시겠습니까?')" Text="전체삭제" Visible="True" Width="80" />

                </div>
                <div style="height: 5px;"></div>
                <div>
                    <asp:GridView ID="GridBlackList" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                        OnPageIndexChanging="BlackListChange" Width="100%" Height="30" PageSize="20"
                        OnRowDataBound="GridBlackList_RowDataBound" OnRowCommand="GridBlackList_RowCommand">

                        <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
                        <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
                        <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
                        <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

                        <Columns>
                            <asp:TemplateField HeaderText="번호" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style="text-align: center; margin-left: 5px; width: 30px;">
                                        <%# DataBinder.Eval(Container, "DataItem.lbl_black_index") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="서버">
                                <ItemTemplate>
                                    <div style="text-align: center; margin-left: 5px; width: 100px;">
                                        <%# DataBinder.Eval(Container, "DataItem.lbl_black_server") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="유저">
                                <ItemTemplate>
                                    <div style="text-align: center; margin-left: 5px; width: 100px;">
                                        <%# DataBinder.Eval(Container, "DataItem.lbl_black_name") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div style="text-align: center; margin-left: 5px; width: 130px;">
                                        <asp:Button ID="BtnBlackActive" runat="server" Text="활성" Width="60"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_black_no") %>'
                                            CommandName="cmdBlackActive" />
                                        <asp:Button ID="BtnBlackDelete" runat="server" Text="삭제" Width="60"
                                            OnClientClick="return confirm( '삭제하시겠습니까?')"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_black_no") %>'
                                            CommandName="cmdBlackDelete" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            <td style="text-align: left; vertical-align: top;"></td>
            <td style="text-align: left; vertical-align: top;">
                <div style="height: 10px;"></div>
                <div style="text-align: left;">
                    <asp:Label runat="server" ID="Label2" Width="50" Font-Bold="True" Font-Size="Medium" Text="서버" />
                    <asp:DropDownList ID="DropUserServerName" runat="server" Width="135" Height="25" Visible="True"></asp:DropDownList>
                    <asp:DropDownList ID="DropUserServerNo" runat="server" Width="60" Height="25" Visible="True"></asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" OnClick="BtnUserView_Click" Text="보기" Visible="True" Width="70" />
                </div>
                <div style="height: 3px;"></div>
                <div style="text-align: left;">
                    <asp:Label runat="server" ID="Label3" Width="50" Height="25" Font-Bold="True" Font-Size="Medium" Text="유저" />
                    <asp:TextBox ID="txt_user" runat="server" Width="200" />
                    <asp:Button ID="BtnUserAdd" runat="server" OnClick="BtnUserAdd_Click" Text="추가" Visible="True" Width="70" />
                    <asp:Button ID="BtnUserFind" runat="server" OnClick="BtnUserFind_Click" Text="검색" Visible="True" Width="70" />
                    <asp:Button ID="BtnUserAllDelete" runat="server" OnClick="BtnUserDelete_Click" 
                        OnClientClick="return confirm( '전부 삭제하시겠습니까?')" Text="전체삭제" Visible="True" Width="80" />

                </div>
                <div>
                    <asp:GridView ID="GridUserList" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                        OnPageIndexChanging="UserListChange" Width="100%" Height="30" PageSize="20"
                        OnRowDataBound="GridUserList_RowDataBound" OnRowCommand="GridUserList_RowCommand">

                        <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
                        <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
                        <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
                        <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

                        <Columns>
                            <asp:TemplateField HeaderText="번호" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div style="text-align: center; margin-left: 5px; width: 30px;">
                                        <%# DataBinder.Eval(Container, "DataItem.lbl_user_index") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="서버">
                                <ItemTemplate>
                                    <div style="text-align: left; margin-left: 5px; width: 100px;">
                                        <%# DataBinder.Eval(Container, "DataItem.lbl_user_server") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="유저">
                                <ItemTemplate>
                                    <div style="text-align: left; margin-left: 5px; width: 100px;">
                                        <%# DataBinder.Eval(Container, "DataItem.lbl_user_name") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div style="text-align: center; margin-left: 5px; width: 130px;">
                                        <asp:Button ID="BtnUserActive" runat="server" Text="활성"  Width="60"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_user_no") %>'
                                            CommandName="cmdUserActive" />
                                        <asp:Button ID="BtnUserDelete" runat="server" Text="삭제"  Width="60"
                                            OnClientClick="return confirm( '삭제하시겠습니까?')"
                                            CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_user_no") %>'
                                            CommandName="cmdUserDelete" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>
