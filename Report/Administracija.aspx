<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracija.aspx.cs" Inherits="Report.Administracija" meta:resourcekey="PageResource1" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/administracija.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row justify-content-center">
				<div class="col col-3 text-center">
					 <asp:Label 
						  Text="Djelatnik" 
						  runat="server"
						  ID="lblDjelatnik" 
						  Font-Bold="true"
						  Font-Size="X-Large"						  
						  meta:resourcekey="lblDjelatnikResource1"/>
						  <asp:HyperLink 
								NavigateUrl="~/ManageDjelatnik.aspx" 
								runat="server" 
								ID="BtnManageDjelatnik"
								CssClass="btn btn-primary"
								Text="Nastavi" meta:resourcekey="BtnManageDjelatnikResource1"/>
				</div>
				<div class="col col-3 text-center">
					 <asp:Label 
						  Text="Tim" 
						  runat="server"
						  ID="lblTim" 
						  Font-Bold="true"
						  Font-Size="X-Large"
						  meta:resourcekey="lblTimResource1"/>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/ManageTim.aspx" 
								runat="server" 
								ID="BtnEditTim"
								CssClass="btn btn-primary"
								Text="Nastavi" meta:resourcekey="BtnEditTimResource1"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <asp:Label 
						  Text="Projekt" 
						  runat="server"
						  ID="lblProjekt" 
						  Font-Bold="true"
						  Font-Size="X-Large"
						  meta:resourcekey="lblProjektResource1"/>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/ManageProjekt.aspx" 
								runat="server" 
								ID="BtnEditProjekt"
								CssClass="btn btn-primary"
								Text="Nastavi" meta:resourcekey="BtnEditProjektResource1"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <asp:Label 
						  Text="Klijent" 
						  runat="server"
						  ID="lblKlijent" 
						  Font-Bold="true"
						  Font-Size="X-Large"
						  meta:resourcekey="lblKlijentResource1"/>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/ManageKlijent" 
								runat="server" 
								ID="BtnEditKlijent"
								CssClass="btn btn-primary"
								Text="Nastavi" meta:resourcekey="BtnEditKlijentResource1"/>
					 </div>
				</div>
		  </div>
	 </div>

</asp:Content>
