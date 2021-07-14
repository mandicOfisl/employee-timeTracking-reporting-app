<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimReport.aspx.cs" Inherits="Report.TimReport" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <script src="Scripts/downloadCsv.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="row">
		  <div class="card card-body">
				<div class="row form-group">
					 <div class="col">
						  <asp:Label 
								Text="Tim" 
								runat="server"
								ID="lblTim" meta:resourcekey="lblTimResource1"/>
						  <asp:DropDownList
								runat="server"
								ID="ddlTimovi"
								CssClass="form-control" meta:resourcekey="ddlTimoviResource1">
						  </asp:DropDownList>
					 </div>
					 <div class="col">
						  <asp:Label 
								Text="Od" 
								runat="server"
								ID="lblFrom" meta:resourcekey="lblFromResource1"/>
						  <asp:TextBox 
								runat="server" 
								ID="DtpFrom"
								TextMode="Date"
								CssClass="form-control" meta:resourcekey="DtpFromResource1"/>
						  <asp:RequiredFieldValidator 
								ErrorMessage="*" 
								ControlToValidate="DtpFrom" 
								runat="server" meta:resourcekey="RequiredFieldValidatorResource1" />
					 </div>
					 <div class="col">
						  <asp:Label 
								Text="Do" 
								runat="server"
								ID="lblTo" meta:resourcekey="lblToResource1"/>
						  <asp:TextBox 
								runat="server" 
								ID="DtpTo"
								TextMode="Date"
								CssClass="form-control" meta:resourcekey="DtpToResource1"/>
						  <asp:RequiredFieldValidator 
								ErrorMessage="*" 
								ControlToValidate="DtpTo" 
								runat="server" meta:resourcekey="RequiredFieldValidatorResource2" />
					 </div>
				</div>
				<div class="row">
					 <div class="col">
						  <asp:Button
								runat="server"
								ID="BtnSearch"
								CssClass="btn btn-primary"
								OnClick="BtnSearch_Click"
								Text="Pretraži" meta:resourcekey="BtnSearchResource1"/>
					 </div>
				</div>
		  </div>
	 </div>

	 <div class="row">
		  <div class="card card-body">
				<asp:GridView 
					 runat="server"
					 ID="gvTable"
					 HeaderStyle-BackColor="LightGray"
					 FooterStyle-BackColor="LightGray"
					 CssClass="form-control"
					 Visible="False"
					 ShowFooter="True" meta:resourcekey="gvTableResource1">
				</asp:GridView>
		  </div>
	 </div>	 

	 <div class="row">
		  <asp:Button 
				Text="Izvezi csv" 
				runat="server"
				ID="BtnCsv"
				CssClass="btn btn-success"
				OnClientClick="return export_table_to_csv();"
				Enabled="False" meta:resourcekey="BtnCsvResource1"/>
	 </div>

</asp:Content>
