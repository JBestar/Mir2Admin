<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuLm2.ascx.cs" Inherits="Mir2Admin.Include.MenuLm2" %>


<table class ="table-menu">
       <% if (curEmployee.emp_level >= 10)
    { %>
	<tr>
        <td bgcolor="<% Response.Write(strBgNotice); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="notice.aspx">공지사항</a>
           </div>
		</td>
    </tr>
    <% }%>
	<tr>
        <td bgcolor="<% Response.Write(strBgMember); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="member.aspx">회원관리</a>
            </div>
		</td>
    </tr>
    
	<tr>
        <td bgcolor="<% Response.Write(strBgConnect); %>" class="td-menu">
			<div style="margin-left:20px; width:120px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="connection.aspx">실시간접속</a>
           </div>
		</td>
    </tr>
     <% if (curEmployee.emp_level >= 12)
    { %>
    <tr>
        <td bgcolor="<% Response.Write(strBgBlack); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="UserManage.aspx">PK관리</a>
           </div>
		</td>
    </tr>
    <tr>
        <td bgcolor="<% Response.Write(strBgSetting); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="Setting.aspx">거래설정</a>
           </div>
		</td>
    </tr>
    <% }%>
</table>