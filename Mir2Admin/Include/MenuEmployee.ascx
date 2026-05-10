<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuEmployee.ascx.cs" Inherits="Mir2Admin.Include.MenuEmployee" %>

<table class="table-menu">
    <% if (curEmployee.emp_level >= 10)
    { %>
	<tr>
        <td class="td-menu" bgcolor="<% Response.Write(strBgCompany); %>" >
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif"> 
            <a class="btn btn-menu" href="company.aspx">본사</a>
            </div>
		</td>
    </tr>
    <% }%>
    <% if (curEmployee.emp_level >= 9)
    { %>
	<tr>
        <td class="td-menu" bgcolor="<% Response.Write(strBgAgency); %>" >
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif"> 
            <a class="btn btn-menu" href="agency.aspx">총판</a>
            </div>
		</td>
    </tr>
    <% }%>
    <% if (curEmployee.emp_level >= 8)
    { %>
	<tr>
        <td class="td-menu" bgcolor="<% Response.Write(strBgEmployee); %>">
			<div style="margin-left:20px; width:110px; font-size: 12pt; color:#506EA5; font-weight:bold;">
			
			<img src="../Contents/images/icon_bullet.gif"> 
            <a  class="btn btn-menu" href="employee.aspx">매장</a>
            </div>
		</td>
    </tr>
    <% }%>
</table>
<script>
    function OnClick_Company()
    {
        document.getElementById("id-company").style.backgroundColor= Response.Write(strBgCompany);
    }

</script>
