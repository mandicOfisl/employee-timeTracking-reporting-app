<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewReport.aspx.cs" Inherits="Report.NewReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col text-center">
					 <asp:Button 
								Text="Izvještaj za klijenta" 
								runat="server"
								ID="BtnReportKlijent"
								OnClick="BtnReportKlijent_Click"
								CssClass="btn btn-outline-secondary"/>
				</div>
				<div class="col text-center">
					 <asp:Button 
								Text="Izvještaj djelatnika tima" 
								runat="server"
								ID="BtnReportTim"
								OnClick="BtnReportTim_Click"
								CssClass="btn btn-outline-success"/>
				</div>
		  </div>
	 </div>

</asp:Content>
