<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProjekt.aspx.cs" Inherits="Report.ManageProjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <label>Projekti</label>
					 <asp:ListBox 
						  runat="server" 
						  ID="LbProjekti"
						  AutoPostBack="true"
						  OnSelectedIndexChanged="LbProjekti_SelectedIndexChanged"
						  CssClass="form-control"
						  Height="250">
					 </asp:ListBox>
				</div>
				<div class="col col-6 form-group">
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Ime obavezno"
									 ControlToValidate="txtNaziv"
									 Text="*"
									 runat="server" />
					 <label>Naziv</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtNaziv"
						  CssClass="form-control"
						  Enabled="false"/>

					 <label>Klijent</label>
					 <asp:DropDownList
						  runat="server"
						  ID="ddlKlijent"
						  CssClass="form-control"
						  Enabled="false">
					 </asp:DropDownList>

					 <asp:RequiredFieldValidator 
									 ErrorMessage="Prezime obavezno" 
									 ControlToValidate="txtDatum"
									 Text="*"
									 runat="server" />
					 <label>Datum otvaranja</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatum"
						  CssClass="form-control"
						  Enabled="false"/>

					 <label>Voditelj projekta</label>
					 <asp:DropDownList
						  runat="server"
						  ID="ddlVoditeljProjekta"
						  CssClass="form-control"
						  Enabled="false">
					 </asp:DropDownList>

					 <div class="form-group btnRow text-center">
						  <asp:Button 
								Text="Uredi" 
								runat="server"
								ID="BtnEdit"
								OnClick="BtnEdit_Click"
								CssClass="btn btn-primary"/>
						  <asp:Button 
								Text="Dodaj" 
								runat="server"
								ID="BtnAdd"
								OnClick="BtnAdd_Click"
								CssClass="btn btn-warning"/>
						  <asp:Button 
								Text="Spremi" 
								runat="server"
								ID="BtnSave"
								OnClick="BtnSave_Click"
								CssClass="btn btn-success"/>
						  <asp:Button 
								Text="Deaktiviraj" 
								runat="server"
								ID="BtnDeaktiviraj"
								OnClick="BtnDeaktiviraj_Click"
								CssClass="btn btn-danger"/>
					 </div>
				</div>

				<div class="col form-group">
					 <label>Zaposleni na projektu</label>
					 <asp:ListBox 
						  runat="server"
						  ID="lbZaposleniNaProjektu"
						  CssClass="form-control"
						  Height="250">
					 </asp:ListBox>
				</div>
		  </div>
		 
	 </div>


</asp:Content>
