<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProjekt.aspx.cs" Inherits="Report.ManageProjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manageProjekt.css" rel="stylesheet" />
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
				<div class="col col-5 form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdProjekt"
						  value=""/>
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
									 ControlToValidate="txtDatumotvaranja"
									 Text="*"
									 runat="server" />
					 <label>Datum otvaranja</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatumOtvaranja"
						  TextMode="Date"
						  CssClass="form-control"
						  Enabled="false"/>

					 <label>Datum zatvaranja</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatumZatvaranja"
						  TextMode="Date"
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
								Enabled="false"
								OnClick="BtnEdit_Click"
								CausesValidation="false"
								CssClass="btn btn-primary"/>
						  <asp:Button 
								Text="Dodaj" 
								runat="server"
								ID="BtnAdd"
								CausesValidation="false"
								OnClick="BtnAdd_Click"
								CssClass="btn btn-warning"/>
						  <asp:Button 
								Text="Aktiviraj" 
								runat="server"
								ID="BtnAktiviraj"
								Enabled="false"
								CausesValidation="false"
								OnClick="BtnAktiviraj_Click"
								CssClass="btn btn-outline-success"/>
						  <asp:Button 
								Text="Deaktiviraj" 
								runat="server"
								ID="BtnDeaktiviraj"
								Enabled="false"
								CausesValidation="false"
								OnClick="BtnDeaktiviraj_Click"
								CssClass="btn btn-outline-danger"/>
						  <asp:Button 
								Text="Spremi" 
								runat="server"
								ID="BtnSave"
								Enabled="false"
								OnClick="BtnSave_Click"
								CssClass="btn btn-success"/>
					 </div>
				</div>

				<div class="col form-group">
					 <label>Zaposleni na projektu</label>
					 <asp:ListBox 
						  runat="server"
						  ID="LbZaposleniNaProjektu"
						  CssClass="form-control"
						  AutoPostBack="true"
						  OnSelectedIndexChanged="LbZaposleniNaProjektu_SelectedIndexChanged"
						  Height="250">
					 </asp:ListBox>
					 <asp:Button 
						  Text="Ukloni djelatnika" 
						  runat="server"
						  ID="BtnUkloniDjelatnika"
						  CssClass="btn btn-danger btnDjelatnici"
						  OnClick="BtnUkloniDjelatnika_Click"
						  CausesValidation="false"
						  Enabled="false"/>
					 <div class="form-group-d-flex">
						  <label>Djelatnik</label>
						  <asp:DropDownList 
								runat="server"
								ID="DdlDjelatnik"
								CssClass="form-control"
								AutoPostBack="true"
								OnSelectedIndexChanged="DdlDjelatnik_SelectedIndexChanged">
						  </asp:DropDownList>
					 </div>
					 <asp:Button 
						  Text="Dodaj djelatnika" 
						  runat="server"
						  ID="BtnDodajDjelatnika"
						  CssClass="btn btn-warning btnDjelatnici"
						  OnClick="BtnDodajDjelatnika_Click"
						  CausesValidation="false"
						  Enabled="false"/>
				</div>
		  </div>
		 
	 </div>


</asp:Content>
