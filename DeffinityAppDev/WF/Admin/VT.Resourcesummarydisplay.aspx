<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrameNoPanel.Master" AutoEventWireup="true" Inherits="Resourcesummarydisplay" Codebehind="VT.Resourcesummarydisplay.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:DataList ID="dlist_summary" runat="server" Width="100%" SkinID="ProgrammeList" RepeatLayout="Flow">
                                                              <HeaderTemplate>
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <div class="form-group">
      <div class="col-xs-8">
           <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("Titles") %>' Font-Size="Smaller" Font-Bold="true" />
	</div>
	<div class="col-xs-4">
          <asp:Label ID="lblsum_values" runat="server" Text='<%# Eval("sum_values") %>'  Font-Size="Smaller"  />
	</div>
	
</div>
                                                                 
                                                              </ItemTemplate>
 </asp:DataList>
</asp:Content>

