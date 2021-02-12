<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTim.aspx.cs" Inherits="Report.ManageTim" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdTim"/>
					 <asp:Label 
						  Text="Timovi" 
						  runat="server"
						  ID="lblTimovi" 
						  Font-Bold="True" meta:resourcekey="lblTimoviResource1" />
					 <asp:ListBox 
						  runat="server" 
						  ID="LbTimovi"
						  AutoPostBack="True"
						  OnSelectedIndexChanged="LbTimovi_SelectedIndexChanged"
						  CssClass="form-control"
						  Height="250px" meta:resourcekey="LbTimoviResource1">
					 </asp:ListBox>
				</div>
				<div class="col col-6 form-group">
					 <asp:Label 
						  Text="Ime" 
						  runat="server"
						  ID="lblIme" 
						  Font-Bold="True" meta:resourcekey="lblImeResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Ime obavezno"
									 ControlToValidate="txtIme"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource1" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtIme"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtImeResource1"/>

					 <asp:Label 
						  Text="Datum kreiranja" 
						  runat="server"
						  ID="lblDatumKreiranja" 
						  Font-Bold="True" meta:resourcekey="lblDatumKreiranjaResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Datum obavezan" 
									 ControlToValidate="txtDatum"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource2" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatum"
						  CssClass="form-control"
						  TextMode="Date"
						  Enabled="False" meta:resourcekey="txtDatumResource1"/>

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
								OnClick="BtnAktiviraj_Click"
								CausesValidation="False"
								Enabled="False"
								CssClass="btn btn-outline-success" meta:resourcekey="BtnAktivirajResource1"/>
						  <asp:Button 
								Text="Deaktiviraj" 
								runat="server"
								ID="BtnDeaktiviraj"
								OnClick="BtnDeaktiviraj_Click"
								CausesValidation="False"
								Enabled="False"
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
						  Text="Članovi tima" 
						  runat="server"
						  ID="lblClanovi" 
						  Font-Bold="True" meta:resourcekey="lblClanoviResource1" />
					 <asp:ListBox 
						  runat="server"
						  ID="lbClanoviTima"
						  CssClass="form-control"
						  Height="250px" meta:resourcekey="lbClanoviTimaResource1">
					 </asp:ListBox>
				</div>
		  </div>
		 
	 </div>


</asp:Content>
