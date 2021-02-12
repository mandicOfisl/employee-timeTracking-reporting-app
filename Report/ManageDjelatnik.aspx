<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDjelatnik.aspx.cs" Inherits="Report.ManageDjelatnik" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <asp:Label 
						  Text="Djelatnici" 
						  runat="server"
						  ID="lblDjelatnici" 
						  Font-Bold="True" meta:resourcekey="lblDjelatniciResource1" />
					 <asp:ListBox 
						  runat="server" 
						  ID="LbDjelatnici"
						  AutoPostBack="True"
						  OnSelectedIndexChanged="LbDjelatnici_SelectedIndexChanged"
						  CssClass="form-control"
						  Height="250px" meta:resourcekey="LbDjelatniciResource1">
					 </asp:ListBox>
				</div>
				<div class="col col-6 form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdDjelatnik"/>
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
						  Text="Prezime" 
						  runat="server"
						  ID="lblPrezime" 
						  Font-Bold="True" meta:resourcekey="lblPrezimeResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Prezime obavezno" 
									 ControlToValidate="txtPrezime"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource2" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtPrezime"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtPrezimeResource1"/>

					 <asp:Label 
						  Text="Email" 
						  runat="server"
						  ID="lblEmail" 
						  Font-Bold="True" meta:resourcekey="lblEmailResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Email obavezan" 
									 ControlToValidate="txtEmail"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource3" />
					 <asp:RegularExpressionValidator 
									 ID="RegularExpressionValidator" 
									 runat="server" 
									 ControlToValidate="txtEmail"
									 ForeColor="Red" 
									 ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
									 Display = "Dynamic" 
									 ErrorMessage = "Pogrešna email adresa" meta:resourcekey="RegularExpressionValidatorResource1"/>
					 <asp:TextBox 
						  runat="server"
						  ID="txtEmail"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtEmailResource1"/>

					 <asp:Label 
						  Text="Datum zaposlenja" 
						  runat="server"
						  ID="lblDatumZap" 
						  Font-Bold="True" meta:resourcekey="lblDatumZapResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Datum obavezno" 
									 ControlToValidate="txtDatum"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource4" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatum"
						  CssClass="form-control"
						  TextMode="Date"
						  Enabled="False" meta:resourcekey="txtDatumResource1"/>
				 
					 <asp:Label 
						  Text="Zaporka" 
						  runat="server"
						  ID="lblZaporka" 
						  Font-Bold="True" meta:resourcekey="lblZaporkaResource1" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtZaporka"
						  CssClass="form-control"
						  TextMode="Password"
						  Enabled="False" meta:resourcekey="txtZaporkaResource1"/>
					 
					 <asp:Label 
						  Text="Tim" 
						  runat="server"
						  ID="lblTim" 
						  Font-Bold="True" meta:resourcekey="lblTimResource1" />
					 <asp:DropDownList
						  runat="server"
						  ID="ddlTim"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="ddlTimResource1">
					 </asp:DropDownList> 	 

					 <asp:Label 
						  Text="Tip djelatnika" 
						  runat="server"
						  ID="lblTipDjelatnika" 
						  Font-Bold="True" meta:resourcekey="lblTipDjelatnikaResource1" />
					 <asp:DropDownList
						  runat="server"
						  ID="ddlTipDjelatnika"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="ddlTipDjelatnikaResource1">
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
						  Text="Projekti" 
						  runat="server"
						  ID="lblProjekti" 
						  Font-Bold="True" meta:resourcekey="lblProjektiResource1" />
					 <asp:ListBox 
						  runat="server"
						  ID="lbProjekti"
						  CssClass="form-control"
						  Height="250px" meta:resourcekey="lbProjektiResource1">
					 </asp:ListBox>
				</div>
		  </div>

	 </div>

</asp:Content>
