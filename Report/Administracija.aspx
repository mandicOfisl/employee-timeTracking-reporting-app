﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracija.aspx.cs" Inherits="Report.Administracija" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/administracija.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row justify-content-center">
				<div class="col col-3 text-center">
					 <label>Djelatnik</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnEditDjelatnik"
								CssClass="btn btn-success"
								Text="Uredi"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnViewDjelatnik"
								CssClass="btn btn-primary"
								Text="Pregledaj"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnAddDjelatnik"
								CssClass="btn btn-warning"
								Text="Dodaj"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <label>Tim</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnEditTim"
								CssClass="btn btn-success"
								Text="Uredi"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnViewtim"
								CssClass="btn btn-primary"
								Text="Pregledaj"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnAddTeam"
								CssClass="btn btn-warning"
								Text="Dodaj"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <label>Projekt</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnEditProjekt"
								CssClass="btn btn-success"
								Text="Uredi"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnViewProjekt"
								CssClass="btn btn-primary"
								Text="Pregledaj"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnAddProjekt"
								CssClass="btn btn-warning"
								Text="Dodaj"/>
					 </div>
				</div>
				<div class="col col-3 text-center">
					 <label>Klijent</label>
					 <div class="btn-group-vertical" role="group">
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnEditKlijent"
								CssClass="btn btn-success"
								Text="Uredi"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnViewKlijent"
								CssClass="btn btn-primary"
								Text="Pregledaj"/>
						  <asp:HyperLink 
								NavigateUrl="~/" 
								runat="server" 
								ID="BtnAddKlijent"
								CssClass="btn btn-warning"
								Text="Dodaj"/>
					 </div>
				</div>
		  </div>
	 </div>

</asp:Content>
