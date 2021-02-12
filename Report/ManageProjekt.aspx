<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProjekt.aspx.cs" Inherits="Report.ManageProjekt" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manageProjekt.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <asp:Label 
						  Text="Projekti" 
						  runat="server"
						  ID="lblProjekti" 
						  Font-Bold="True" meta:resourcekey="lblProjektiResource1" />
					 <asp:ListBox 
						  runat="server" 
						  ID="LbProjekti"
						  AutoPostBack="True"
						  OnSelectedIndexChanged="LbProjekti_SelectedIndexChanged"
						  CssClass="form-control"
						  Height="250px" meta:resourcekey="LbProjektiResource1">
					 </asp:ListBox>
				</div>
				<div class="col col-5 form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdProjekt"/>
					 <asp:Label 
						  Text="Naziv" 
						  runat="server"
						  ID="lblNaziv" 
						  Font-Bold="True" meta:resourcekey="lblNazivResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Ime obavezno"
									 ControlToValidate="txtNaziv"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource1" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtNaziv"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtNazivResource1"/>

					 <asp:Label 
						  Text="Klijent" 
						  runat="server"
						  ID="lblKlijent" 
						  Font-Bold="True" meta:resourcekey="lblKlijentResource1" />
					 <asp:DropDownList
						  runat="server"
						  ID="ddlKlijent"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="ddlKlijentResource1">
					 </asp:DropDownList>

					 <asp:Label 
						  Text="Datum otvaranja" 
						  runat="server"
						  ID="lblDatumOtvaranja" 
						  Font-Bold="True" meta:resourcekey="lblDatumOtvaranjaResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Prezime obavezno" 
									 ControlToValidate="txtDatumotvaranja"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource2" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatumOtvaranja"
						  TextMode="Date"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtDatumOtvaranjaResource1"/>

					 <asp:Label 
						  Text="Datum zatvaranja" 
						  runat="server"
						  ID="lblDatumZatvaranja" 
						  Font-Bold="True" meta:resourcekey="lblDatumZatvaranjaResource1" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatumZatvaranja"
						  TextMode="Date"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtDatumZatvaranjaResource1"/>

					 <asp:Label 
						  Text="Voditelj projekta" 
						  runat="server"
						  ID="lblVoditeljProjekta" 
						  Font-Bold="True" meta:resourcekey="lblVoditeljProjektaResource1" />
					 <asp:DropDownList
						  runat="server"
						  ID="ddlVoditeljProjekta"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="ddlVoditeljProjektaResource1">
					 </asp:DropDownList>

					 <div class="form-group btnRow text-center">
						  <asp:Button 
								Text="Uredi" 
								runat="server"
								ID="BtnEdit"
								Enabled="False"
								OnClick="BtnEdit_Click"
								CausesValidation="False"
								CssClass="btn btn-primary" meta:resourcekey="BtnEditResource1"/>
						  <asp:Button 
								Text="Dodaj" 
								runat="server"
								ID="BtnAdd"
								CausesValidation="False"
								OnClick="BtnAdd_Click"
								CssClass="btn btn-warning" meta:resourcekey="BtnAddResource1"/>
						  <asp:Button 
								Text="Aktiviraj" 
								runat="server"
								ID="BtnAktiviraj"
								Enabled="False"
								CausesValidation="False"
								OnClick="BtnAktiviraj_Click"
								CssClass="btn btn-outline-success" meta:resourcekey="BtnAktivirajResource1"/>
						  <asp:Button 
								Text="Deaktiviraj" 
								runat="server"
								ID="BtnDeaktiviraj"
								Enabled="False"
								CausesValidation="False"
								OnClick="BtnDeaktiviraj_Click"
								CssClass="btn btn-outline-danger" meta:resourcekey="BtnDeaktivirajResource1"/>
						  <asp:Button 
								Text="Spremi" 
								runat="server"
								ID="BtnSave"
								Enabled="False"
								OnClick="BtnSave_Click"
								CssClass="btn btn-success" meta:resourcekey="BtnSaveResource1"/>
					 </div>
				</div>

				<div class="col form-group">
					 <asp:Label 
						  Text="Zaposleni na projektu" 
						  runat="server"
						  ID="lblZaposleniNaProjektu" 
						  Font-Bold="True" meta:resourcekey="lblZaposleniNaProjektuResource1" />
					 <asp:ListBox 
						  runat="server"
						  ID="LbZaposleniNaProjektu"
						  CssClass="form-control"
						  AutoPostBack="True"
						  OnSelectedIndexChanged="LbZaposleniNaProjektu_SelectedIndexChanged"
						  Height="250px" meta:resourcekey="LbZaposleniNaProjektuResource1">
					 </asp:ListBox>
					 <asp:Button 
						  Text="Ukloni djelatnika" 
						  runat="server"
						  ID="BtnUkloniDjelatnika"
						  CssClass="btn btn-danger btnDjelatnici"
						  OnClick="BtnUkloniDjelatnika_Click"
						  CausesValidation="False"
						  Enabled="False" meta:resourcekey="BtnUkloniDjelatnikaResource1"/>
					 <div class="form-group-d-flex">
						  <asp:Label 
						  Text="Djelatnik" 
						  runat="server"
						  ID="lblDjelatnik" 
						  Font-Bold="True" meta:resourcekey="lblDjelatnikResource1" />
						  <asp:DropDownList 
								runat="server"
								ID="DdlDjelatnik"
								CssClass="form-control"
								AutoPostBack="True"
								OnSelectedIndexChanged="DdlDjelatnik_SelectedIndexChanged" meta:resourcekey="DdlDjelatnikResource1">
						  </asp:DropDownList>
					 </div>
					 <asp:Button 
						  Text="Dodaj djelatnika" 
						  runat="server"
						  ID="BtnDodajDjelatnika"
						  CssClass="btn btn-warning btnDjelatnici"
						  OnClick="BtnDodajDjelatnika_Click"
						  CausesValidation="False"
						  Enabled="False" meta:resourcekey="BtnDodajDjelatnikaResource1"/>
				</div>
		  </div>
		 
	 </div>


</asp:Content>
