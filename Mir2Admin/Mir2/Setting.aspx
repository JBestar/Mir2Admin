<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Mir2Admin.Mir2.Setting" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div style="height: 6px;"></div>
    <div style="z-index: +999; position: absolute; visibility: hidden;"></div>
    <table>
        <tr>
            <th style="font-size: 20px; border-bottom: double; background-color: #FFF; color: #00115d">
                <asp:Label ID="Lbl_ToTal" runat="server" Text="관리자리스트" Font-Bold="True" Font-Size="Large"></asp:Label>
                
            </th>
            <th></th>
            <th style="font-size: 20px; border-bottom: double; background-color: #FFF; color: #00115d">

                <asp:Label ID="Lbl_Account" runat="server" Text="구매리스트" Font-Bold="True" Font-Size="Large"></asp:Label>
                
            </th>
        </tr>
       <colgroup>
            <col style="width: 300px;" />
            <col style="width: 40px;" />
            <col style="width: 950px;" />

        </colgroup>
        <tr>
            <td style="text-align: left; vertical-align: top;">
                <asp:GridView ID="GridAgentList" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                    OnPageIndexChanging="AgentListChange" Width="100%" Height="30" PageSize="20"
                    OnRowDataBound="GridAgentList_RowDataBound" OnRowCommand="GridAgentList_RowCommand">

                    <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
                    <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
                    <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
                    <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

                    <Columns>
                        <asp:TemplateField HeaderText="서버번호" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style="text-align: center; margin-left: 5px; width: 30px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_agent_serverno") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="네임">
                            <ItemTemplate>
                                <div style="text-align: center; margin-left: 5px; width: 100px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_agent_name") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="상태값">
                            <ItemTemplate>
                                <div style="text-align: center; margin-left: 5px; width: 30px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_agent_active") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <div style="text-align: center;">                        
                                    <asp:Button ID="BtnDelete" runat="server" Text="삭제" Width="60"
                                        OnClientClick="return confirm( '삭제하시겠습니까?')"
                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.lbl_agent_index") %>'
                                        CommandName="cmdUpdateDelete" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </td>
            <td style="text-align: left; vertical-align: top;"></td>
            <td style="text-align: left; vertical-align: top;">


                <asp:GridView ID="GridBuyerList" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                    OnPageIndexChanging="BuyerListChange" Width="100%" Height="30" PageSize="20"
                    OnRowCommand="GridBuyerList_RowCommand">

                    <HeaderStyle CssClass="normal_box" HorizontalAlign="Center"></HeaderStyle>
                    <PagerStyle CssClass="normal_box" HorizontalAlign="Center"></PagerStyle>
                    <RowStyle CssClass="normal_box" Height="20px" BackColor="#FFFFFF" HorizontalAlign="Left"></RowStyle>
                    <AlternatingRowStyle CssClass="normal_box" Height="20px" BackColor="#DEDEDE" HorizontalAlign="Left"></AlternatingRowStyle>

                    <Columns>
                        <asp:TemplateField HeaderText="서버번호" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div style="text-align: center; margin-left: 5px; width: 30px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_serverno") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="뱅커">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 60px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_banker") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="구매자">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 60px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_name") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="아이템아이디">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 60px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_itemid") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="판매아이디">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 120px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_saleid") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="강화차수">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 50px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_length") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="등록날자">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 120px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_date") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="등록상태">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 50px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_regst") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="구매상태">
                            <ItemTemplate>
                                <div style="text-align: left; margin-left: 5px; width: 50px;">
                                    <%# DataBinder.Eval(Container, "DataItem.lbl_buyer_buyst") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>

