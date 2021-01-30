<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDjelatnik.aspx.cs" Inherits="Report.ManageDjelatnik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <label>Djelatnici</label>
					 <asp:ListBox 
						  runat="server" 
						  ID="LbDjelatnici"
						  AutoPostBack="true"
						  OnSelectedIndexChanged="LbDjelatnici_SelectedIndexChanged"
						  CssClass="form-control"
						  Height="250">
					 </asp:ListBox>
				</div>
				<div class="col col-6 form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdDjelatnik"
						  value=""/>
					 <asp:RequiredFieldValidator 
									 ErrorMessage="Ime obavezno"
									 ControlToValidate="txtIme"
									 Text="*"
									 runat="server" />
					 <label>Ime</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtIme"
						  CssClass="form-control"
						  Enabled="false"/>

					 <asp:RequiredFieldValidator 
									 ErrorMessage="Prezime obavezno" 
									 ControlToValidate="txtPrezime"
									 Text="*"
									 runat="server" />
					 <label>Prezime</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtPrezime"
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

					 <asp:RequiredFieldValidator 
									 ErrorMessage="Datum obavezno" 
									 ControlToValidate="txtDatum"
									 Text="*"
									 runat="server" />
					 <label>Datum zaposlenja</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatum"
						  CssClass="form-control"
						  TextMode="Date"
						  Enabled="false"/>
				 
					 <label>Zaporka</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtZaporka"
						  CssClass="form-control"
						  TextMode="Password"
						  Enabled="false"/>
					 
					 <label>Tim</label>
					 <asp:DropDownList
						  runat="server"
						  ID="ddlTim"
						  CssClass="form-control"
						  Enabled="false">
					 </asp:DropDownList> 	 

					 <label>Tip djelatnika</label>
					 <asp:DropDownList
						  runat="server"
						  ID="ddlTipDjelatnika"
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
