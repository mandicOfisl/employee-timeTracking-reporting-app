<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTim.aspx.cs" Inherits="Report.ManageTim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <link href="Content/manage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


	 <div class="card card-body">
		  <div class="row">
				<div class="col form-group">
					 <asp:HiddenField
						  runat="server"
						  ID="hiddenIdTim"
						  value=""/>
					 <label>Timovi</label>
					 <asp:ListBox 
						  runat="server" 
						  ID="LbTimovi"
						  AutoPostBack="true"
						  OnSelectedIndexChanged="LbTimovi_SelectedIndexChanged"
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
									 ErrorMessage="Datum obavezan" 
									 ControlToValidate="txtDatum"
									 Text="*"
									 runat="server" />
					 <label>Datum kreiranja</label>
					 <asp:TextBox 
						  runat="server"
						  ID="txtDatum"
						  CssClass="form-control"
						  TextMode="Date"
						  Enabled="false"/>

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
								OnClick="BtnAktiviraj_Click"
								CausesValidation="false"
								Enabled="false"
								CssClass="btn btn-outline-success"/>
						  <asp:Button 
								Text="Deaktiviraj" 
								runat="server"
								ID="BtnDeaktiviraj"
								OnClick="BtnDeaktiviraj_Click"
								CausesValidation="false"
								Enabled="false"
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
					 <label>Članovi tima</label>
					 <asp:ListBox 
						  runat="server"
						  ID="lbClanoviTima"
						  CssClass="form-control"
						  Height="250">
					 </asp:ListBox>
				</div>
		  </div>
		 
	 </div>


</asp:Content>
