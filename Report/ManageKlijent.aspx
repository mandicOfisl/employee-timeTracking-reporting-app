<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageKlijent.aspx.cs" Inherits="Report.ManageKlijent" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdKlijent"/>
					 <asp:Label 
						  Text="Klijenti" 
						  runat="server"
						  ID="lblKlijenti" 
						  Font-Bold="True" meta:resourcekey="lblKlijentiResource1" />
					 <asp:ListBox 
						  runat="server" 
						  ID="LbKlijenti"
						  AutoPostBack="True"
						  OnSelectedIndexChanged="LbKlijenti_SelectedIndexChanged"
						  CssClass="form-control"
						  Height="250px" meta:resourcekey="LbKlijentiResource1">
					 </asp:ListBox>
				</div>
				<div class="col col-6 form-group">
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
						  Text="Telefon" 
						  runat="server"
						  ID="lblTelefon" 
						  Font-Bold="True" meta:resourcekey="lblTelefonResource1" />
					 <asp:TextBox 
						  runat="server"
						  ID="txtTelefon"
						  CssClass="form-control"
						  Enabled="False" meta:resourcekey="txtTelefonResource1"/>

					 <asp:Label 
						  Text="Email" 
						  runat="server"
						  ID="lblEmail" 
						  Font-Bold="True" meta:resourcekey="lblEmailResource1" />
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Email obavezan" 
									 ControlToValidate="txtEmail"
									 Text="*"
									 runat="server" meta:resourcekey="RequiredFieldValidatorResource2" />
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
