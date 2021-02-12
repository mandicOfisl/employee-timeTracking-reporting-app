<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewReport.aspx.cs" Inherits="Report.NewReport" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col text-center">
					 <asp:HyperLink 
						  NavigateUrl="~/KlijentReport.aspx" 
						  runat="server"
						  CssClass="btn btn-outline-secondary"
						  Text="Izvještaj za klijenta" meta:resourcekey="HyperLinkResource1"/>
				</div>
				<div class="col text-center">
					 <asp:HyperLink 
						  NavigateUrl="~/TimReport.aspx" 
						  runat="server"
						  CssClass="btn btn-outline-success"
						  Text="Izvještaj djelatnika tima" meta:resourcekey="HyperLinkResource2"/>
				</div>
		  </div>
	 </div>

</asp:Content>
