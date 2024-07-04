<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProgrammeAssessment" Title="Untitled Page" Codebehind="ProgrammeAssessment.aspx.cs" %>

<%@ Register src="controls/ProgrammeManagement.ascx" tagname="ProgrammeManagement" tagprefix="uc1" %>
<%@ Register src="controls/ProgramTitle.ascx" tagname="ProgrammeName" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProgrammeManagement ID="ProgrammeManagement1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Programme Assessment <uc2:ProgrammeName ID="ProgramName" runat="server" Visible="false" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>Add Assessment</strong>
            <hr class="no-top-margin" />
            </div>
</div>


    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ProgresstoDate%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtProgress" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
    <asp:HiddenField ID="id" runat="server" />
					</div>
				</div>
                </div>

    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Benefits%> </label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtBenefits" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
					</div>
				</div>
                </div>

    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Eval_emer_Opportunities%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtOpportunities" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
					</div>
				</div>
                </div>

    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.PaceofProgress%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtPaceOfProgress" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
					</div>
				</div>
                </div>

    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-8 form-inline"><asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" 
        onclick="btnSubmit_Click" />
<asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" 
        onclick="btnCancel_Click"  />
					</div>
				</div>
                </div>


    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>Assessment(s)</strong>
            <hr class="no-top-margin" />
            </div>
</div>



     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="SqlDataSource1" Width="100%" 
        onselectedindexchanging="GridView1_SelectedIndexChanging">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" SkinID="BtnLinkEdit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" Visible="false" />
            <asp:BoundField DataField="ProgrammeID" HeaderText="ProgrammeID" 
                SortExpression="ProgrammeID"  Visible="false" />
            <asp:BoundField DataField="ProgressToDate" HeaderText="Progress To Date" 
                SortExpression="ProgressToDate" />
            <asp:BoundField DataField="Benefits" HeaderText="Benefits" 
                SortExpression="Benefits" />
            <asp:BoundField DataField="EmergentOpportunities" 
                HeaderText="Emergent Opportunities" 
                SortExpression="Emergent Opportunities" />
            <asp:BoundField DataField="PaceOfProgress" HeaderText="Pace Of Progress" 
                SortExpression="PaceOfProgress" />
            <asp:BoundField DataField="RaisedDate" HeaderText="Raised Date" 
                SortExpression="RaisedDate" DataFormatString="{0:d}" 
                HtmlEncode="false" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
           <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" Width="40px" /> 
                        <ItemTemplate >
                        <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:LinkButton ID="deletebut" runat="server" CommandName="delete" SkinID="BtnLinkDelete"
                            OnClientClick="return confirm('Do you want to delete the Assessment?');" ToolTip="Delete"
                            Visible="True" />
                            </ItemTemplate>
                            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DBstring %>" 
        DeleteCommand="delete from ProgrammeAssessment where ID=@ID" 
        SelectCommand="SELECT ProgrammeAssessment.* FROM ProgrammeAssessment where ProgrammeID=@ProgrammeID">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="ProgrammeID" Type="Int32" SessionField="ProgrammeID" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="ID" />
        </DeleteParameters>
    </asp:SqlDataSource>
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>

