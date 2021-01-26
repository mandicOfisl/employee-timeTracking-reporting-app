<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Report._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

		  <div class="row justify-content-center" style="margin-top: 10vh;">
				<div class="col col-6">
					 <div class="row">
						  <div class="col form-group">
								<asp:RequiredFieldValidator 
									 ErrorMessage="Email obavezan!" 
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
								<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />  
						  </div>
					 </div>
					 <div class="row">
						  <div class="col form-group">
								<asp:RequiredFieldValidator 
									 ErrorMessage="Lozinka obavezna" 
									 ControlToValidate="txtPass"
									 Text="*"
									 runat="server" />
								<label>Zaporka</label>
								<asp:TextBox runat="server" ID="txtPass" CssClass="form-control" TextMode="Password"/>
						  </div>
					 </div>
					 <div class="row">
						  <div class="col form-group">
								<asp:Button Text="Prijava" 
												runat="server" 
												ID="BtnLogin"
												OnClick="BtnLogin_Click"
												CssClass="btn btn-primary"/>
						  </div>
					 </div>

				</div>
		  </div>


</asp:Content>
