<%@ Page Language="C#" MasterPageFile="~/Include/AppStaff.Master" AutoEventWireup="true" CodeBehind="agencyEdit.aspx.cs" Inherits="Mir2Admin.Staff.agencyEdit" %>

<asp:content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder" >
<script src="../Contents/scripts/jscolor.js"></script>
<script  src="../Contents/scripts/calendar.js"></script>
<div onclick="bShow=true" id="calendar" onchange="selectHidden('calendar')" style="z-index:+999;position:absolute;visibility:hidden;"></div>

<table class="adm table-edit">
        <tr>
            <td class="td-edit1">                
            </td>
        </tr>
		<tr>
		<td>
            <asp:TextBox ID="hide_mb_uid" runat="server" Visible="false" />
			<table class="normal_box" >
			<colgroup>
                <col style="width:200px;"/>
				<col style="width:200px;"/>
				<col style="width:500px;"/>
				<col style="width:200px;"/>
			</colgroup>
            <tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td style="text-align:left">분류:</td>
				<td style="text-align:left">
                    <asp:DropDownList ID="DropDownEmployee" runat="server" Width="166" Height="22" />
                </td>
				<td style="text-align:left"></td>
			</tr>
            <tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td style="text-align:left">총판아이디:</td>
				<td style="text-align:left">
                    <asp:TextBox ID="txt_mb_uid" runat="server" Width="160" style="IME-MODE:disabled" />(영문, 숫자만 가능)
                </td>
				<td style="text-align:left"></td>
			</tr>
			<tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td style="text-align:left">비밀번호:</td>
				<td style="text-align:left">
                    <asp:TextBox ID="txt_mb_pwd" runat="server" Width="160" style="IME-MODE:disabled" />
                </td>
				<td style="text-align:left"></td>
			</tr>
			<tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td style="text-align:left">총판닉네임:</td>
				<td style="text-align:left">
                    <asp:TextBox ID="txt_mb_nickname" runat="server" Width="160" />
                </td>
				<td style="text-align:left"></td>
			</tr>
			<tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td style="text-align:left">총판색깔:</td>
				<td style="text-align:left">
                    <asp:TextBox ID="txt_mb_color" runat="server" Width="160" 
                    class="jscolor" Text="FFFFFF" style="ime-mode:disabled;"></asp:TextBox>

	                <script>
	                    function setTextColor(picker) {
	                        document.getElementsByTagName('body')[0].style.color = '#' + picker.toString()
	                    }
	                </script>
                </td>
				<td style="text-align:left"></td>
			</tr>
            <!--
			<tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td style="text-align:left">오토풀권한:</td>
				<td style="text-align:left">
                    <div><asp:CheckBox ID="ChkLuckyGold" runat="server" Text="럭키골드존" /></div>
                    <div><asp:CheckBox ID="ChkLuckyPal" runat="server" Text="럭키팔팔" /></div>
                    <div><asp:CheckBox ID="ChkCross" runat="server" Text="조합베팅" /></div>
                    <div><asp:CheckBox ID="ChkMpowerball" runat="server" Text="멀티파워볼" /></div>
                    <div><asp:CheckBox ID="ChkParadais" runat="server" Text="파라다이스" /></div>
                    <div><asp:CheckBox ID="ChkYPowerball" runat="server" Text="Y파워볼" /></div>
                </td>
				<td style="text-align:left"></td>
			</tr>
            -->
			<tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td colspan="2" style="text-align:left"><hr/></td>
				<td style="text-align:left"></td>
			</tr>

			<tr class="tr-edit ht center">
				<td style="text-align:left"></td>
				<td colspan="2" style="text-align:left">
                    <asp:Button ID="BtnSaveCompany" runat="server" Text="저 장" Width="100" onclick="BtnSaveCompany_Click" />
                    <asp:Button ID="BtnCancelCompany" runat="server" Text="취 소" Width="100" onclick="BtnCancelCompany_Click" />
				</td>
				<td colspan="2" style="text-align:left">
				</td>
				<td style="text-align:left"></td>
			</tr>

			</table>
		</td>
	</tr>
</table>

</asp:content>