<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracija.aspx.cs" Inherits="Report.Administracija" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/administracija.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row justify-content-center">
				<div class="col col-3 text-center">
					 <label>Djelatnik</label>
						  <asp:HyperLink 
								NavigateUrl="~/ManageDjelatnik.aspx" 
								runat="server" 
								ID="BtnManageDjelatnik"
								CssClass="btn btn-primary"
								Text="Nastavi"/>
				</div>
				<div class="col col-3 text-center">
					 <label>Tim</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/ManageTim.aspx" 
								runat="server" 
								ID="BtnEditTim"
								CssClass="btn btn-primary"
								Text="Nastavi"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <label>Projekt</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/ManageProjekt.aspx" 
								runat="server" 
								ID="BtnEditProjekt"
								CssClass="btn btn-primary"
								Text="Nastavi"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <label>Klijent</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/ManageKlijent" 
								runat="server" 
								ID="BtnEditKlijent"
								CssClass="btn btn-primary"
								Text="Nastavi"/>
					 </div>
				</div>
		  </div>
	 </div>

</asp:Content>
