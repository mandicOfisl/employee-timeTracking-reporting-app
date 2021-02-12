<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KlijentReport.aspx.cs" Inherits="Report.KlijentReport" culture="auto" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	 <script src="Scripts/downloadCsv.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <div class="row">
		  <div class="card card-body">
				<div class="row form-group">
					 <div class="col">
						  <asp:Label 
								Text="Klijent" 
								runat="server"
								ID="lblKlijent"
								meta:resourcekey="lblKlijent"/>
						  <asp:DropDownList
								runat="server"
								ID="ddlKlijenti"
								CssClass="form-control">
						  </asp:DropDownList>
					 </div>
					 <div class="col">
						  <asp:Label 
								Text="Od" 
								runat="server"
								ID="lblFrom"
								meta:resourcekey="lblFrom"/>
						  <asp:TextBox 
								runat="server" 
								ID="DtpFrom"
								TextMode="Date"
								CssClass="form-control"/>
						  <asp:RequiredFieldValidator 
								ErrorMessage="*" 
								ControlToValidate="DtpFrom" 
								runat="server" />
					 </div>
					 <div class="col">
						  <asp:Label 
								Text="Do" 
								runat="server"
								ID="lblTo"
								meta:resourcekey="lblTo"/>
						  <asp:TextBox 
								runat="server" 
								ID="DtpTo"
								TextMode="Date"
								CssClass="form-control"/>
						  <asp:RequiredFieldValidator 
								ErrorMessage="*" 
								ControlToValidate="DtpTo" 
								runat="server"/>
					 </div>
				</div>
				<div class="row">
					 <div class="col">
						  <asp:Button
								runat="server"
								ID="BtnSearch"
								CssClass="btn btn-primary"
								OnClick="BtnSearch_Click"
								Text="Pretraži" 
								meta:resourcekey="BtnSearch"/>
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
					 ShowFooter="True">
				</asp:GridView>
		  </div>
	 </div>

	 <div class="row">
		  <asp:Button 
				Text="Izvezi csv" 
				runat="server"
				ID="BtnCsv"
				CssClass="btn btn-success"
				Enabled="False"
				OnClientClick="return export_table_to_csv();" 
				meta:resourcekey="BtnCsv"/>
	 </div>

</asp:Content>
