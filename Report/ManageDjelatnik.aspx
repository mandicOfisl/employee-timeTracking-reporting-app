<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDjelatnik.aspx.cs" Inherits="Report.ManageDjelatnik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manageDjelatnik.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="card">
		  <div class="card-header">Djelatnici</div>
		  <div class="card-body">
				<div class="row">
					 <div class="col form-group">
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
										  ControlToValidate="txtIme"
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


						  <div class="row align-items-center border">
								<div class="col form-group">
									 <asp:RequiredFieldValidator 
													 ErrorMessage="Zaporka obavezna" 
													 ControlToValidate="txtZaporka"
													 Text="*"
													 runat="server" />					 
									 <label>Zaporka</label>
									 <asp:TextBox 
										  runat="server"
										  ID="txtZaporka"
										  CssClass="form-control"
										  TextMode="Password"
										  Enabled="false"/>
									 <asp:CompareValidator 
										  ErrorMessage="Ponovno unesite zaporku" 
										  ControlToValidate="txtZaporka2" 
										  runat="server"
										  ControlToCompare="txtZaporka"
										  Text="*"/>
									 <label>Ponovi zaporku</label>
									 <asp:TextBox 
										  runat="server"
										  ID="txtZaporka2"
										  CssClass="form-control"
										  TextMode="Password"						  
										  Enabled="false"/>

								</div>
								<div class="col col-4 form-group">
									 <asp:Button 
										  runat="server" 
										  ID="BtnChangePass"
										  OnClick="BtnChangePass_Click"
										  CssClass="btn btn-outline-primary"
										  Text="Promijeni">
									 </asp:Button>
								</div>
						  </div>

						  <label>Tim</label>
						  <asp:DropDownList
								runat="server"
								ID="ddlTim"
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
						  </div>
					 </div>

					 <div class="col form-group">
						  <asp:ListBox 
								runat="server"
								ID="lbProjekti"
								CssClass="form-control"
								Height="250">
						  </asp:ListBox>
					 </div>
				</div>
		  </div>
	 </div>

</asp:Content>
