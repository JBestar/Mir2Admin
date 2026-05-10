<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuUpdate.ascx.cs" Inherits="Mir2Admin.Include.MenuUpdate" %>

<table class ="table-menu">
	<tr>
        <td bgcolor="<% Response.Write(strBgHistory); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="history.aspx">버전이력</a>
           </div>
		</td>
    </tr>
	<tr>
        <td bgcolor="<% Response.Write(strBgUpdate); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="update.aspx">버전관리</a>
            </div>
		</td>
    </tr>
    <tr>
        <td bgcolor="<% Response.Write(strBgDown); %>" class="td-menu">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif" class ="menu-bgcolor" > 
            <a class="btn btn-menu" href="download.aspx">최신버전</a>
            </div>
		</td>
    </tr>
</table>