<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageKlijent.aspx.cs" Inherits="Report.ManageKlijent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <label>Klijenti</label>
					 <asp:ListBox 
						  runat="server" 
						  ID="LbKlijenti"
						  AutoPostBack="true"
						  OnSelectedIndexChanged="LbKlijenti_SelectedIndexChanged"
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

					 <label>Telefon</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtTelefon"
						  CssClass="form-control"
						  Enabled="false"/>

					 <asp:RequiredFieldValidator 
									 ErrorMessage="Email obavezan" 
									 ControlToValidate="txtEmail"
									 Text="*"
									 runat="server" />
					 <asp:RegularExpressionValidator 
									 ID="RegularExpressionValidator" 
									 runat="server" 
									 ControlToValidate="txtEmail"
									 ForeColor="Red" 
									 ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
									 Display = "Dynamic" 
									 ErrorMessage = "Pogrešna email adresa"/>
					 <label>Email</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtEmail"
						  CssClass="form-control"
						  Enabled="false"/>

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
					 <label>Projekti</label>
					 <asp:ListBox 
						  runat="server"
						  ID="lbProjekti"
						  CssClass="form-control"
						  Height="250">
					 </asp:ListBox>
				</div>
		  </div>
		 
	 </div>


</asp:Content>
