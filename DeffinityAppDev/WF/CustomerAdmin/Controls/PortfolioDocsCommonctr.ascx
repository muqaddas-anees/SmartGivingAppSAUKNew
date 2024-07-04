<%@ Control Language="C#" AutoEventWireup="true"  Inherits="controls_PortfolioDocsCommonctr" Codebehind="PortfolioDocsCommonctr.ascx.cs" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.2" Namespace="Infragistics.WebUI.UltraWebNavigator"
    TagPrefix="ignav" %>

<%--<script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="js/jquery-1.3.2.js" type="text/javascript"></script>
<script src="js/jquery.MultiFile.js" type="text/javascript"></script>--%>

<div class="toolbar" id="fileControls">
<asp:HiddenField runat="server" id="HiddenCustomerdocs" />
    <div class="form-group well">
      <div class="col-md-4">
          <label class="col-sm-4 control-label"> <asp:Literal ID="Literal1" runat="server"  Text="<%$ Resources:DeffinityRes,Doc_FolderName%>" /></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtCreateFolder" runat="server" SkinID="txt_90" />
            </div>
	</div>
	<div class="col-md-5 form-inline">
           <asp:LinkButton ID="lnkFolder" runat="server" SkinID="BtnLinkFolder"
                    OnClick="linkFolder_Click" CommandName="CreateFolder" AlternateText="<%$ Resources:DeffinityRes, CreateFolder%>" ToolTip="<%$ Resources:DeffinityRes, CreateFolder%>" />
        <asp:LinkButton ID="lnkRenameFolder" Text="<%$ Resources:DeffinityRes, Rename%>" runat="server" SkinID="BtnLinkFolderOpen"
                    OnClick="linkFolder_Click" CommandName="RenameFolder" AlternateText="<%$ Resources:DeffinityRes, RenameFolder%>" ToolTip="<%$ Resources:DeffinityRes, RenameFolder%>" />
         <asp:LinkButton ID="lnkDeleteFolder" Text="<%$ Resources:DeffinityRes, Delete%>" CommandName="DeleteFolder" SkinID="BtnLinkDelete"
                    runat="server" OnClick="linkFolder_Click" OnClientClick="return confirm('All the associated files will be deleted.  Do you want to delete the folder');"
                    AlternateText="<%$ Resources:DeffinityRes, DeleteFolder%>"  ToolTip="<%$ Resources:DeffinityRes, DeleteFolder%>" />
         <ASP:LINKBUTTON ID="btnResetCheckout"  ToolTip="Reset File"  RUNAT="server" COMMANDNAME="ResetCheckOut" ONCLICK="linkFile_Click" >
                </ASP:LINKBUTTON>   
         <ASP:LINKBUTTON SkinID="BtnLinkCopy" ID="btnCopyFile" RUNAT="server" COMMANDNAME="CopyFile" ToolTip="<%$ Resources:DeffinityRes, CopyFile%>"   ONCLICK="linkFile_Click" >
                           </ASP:LINKBUTTON>  
        <asp:LinkButton SkinID="BtnLinkPaste" ID="btnPasteFile" runat="server"  ToolTip="<%$ Resources:DeffinityRes, PasteFile%>"  CommandName="PasteFile" OnClick="linkFile_Click">
                    </asp:LinkButton>
         <asp:LinkButton SkinID="BtnLinkCut" ID="btnMoveFile" runat="server" ToolTip="<%$ Resources:DeffinityRes, CutFile%>"  CommandName="MoveFile" OnClick="linkFile_Click">
                   </asp:LinkButton>
        <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDeleteFile" runat="server" ToolTip="<%$ Resources:DeffinityRes, Delete%>" CommandName="DeleteFile" OnClick="linkFile_Click"
                    OnClientClick='return confirm("Do you really want to delete this file?");' >
                    </asp:LinkButton>
        <asp:LinkButton ID="btnMultiDownload" runat="server" Text="" OnClick="btnMultiDownload_Click" SkinID="BtnLinkDownload" ToolTip="Download Zip">
                </asp:LinkButton>
	</div>
	<div class="col-md-3 form-inline">
            <asp:TextBox ID="txtSearchBox1" runat="server" SkinID="txt_80"></asp:TextBox>
                 <asp:LinkButton SkinID="BtnLinkSearch" alt="<%$ Resources:DeffinityRes, SearchButton%>"  CommandName="SearchFile1" OnClick="ImageFile_Click" id="ImgBtnsearch1" title="<%$ Resources:DeffinityRes, Clicktosearch%>"  
                    runat="server" Style="vertical-align:baseline;" />
               
	</div>
     <hr class="no-top-margin" />
</div>

    
    <ajaxtoolkit:modalpopupextender ID="ModalControlExtender2" CancelControlID="lnkClose"
        BackgroundCssClass="modalBackground" runat="server" TargetControlID="BtntempSearchModal"
        PopupControlID="pnlSearchResults">
    </ajaxtoolkit:modalpopupextender>
   
   
  
    <asp:Panel ID="pnlSearchResults" runat="server" Style="display: none; width: 80%;
        height: 70%" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue"
        ScrollBars="Auto">
        <table width="60%" style="padding-top: 20px;">
            <tr>
                <td>
                    <img  runat="server"  src="../media/deffinity_logo.gif" alt="<%$ Resources:DeffinityRes,  DeffinityLogo%>" title=" <%$ Resources:DeffinityRes,Deffinity%>  " style="padding-left: 30px" />
                    
                </td>
                <td width="20px">
                </td>
                <td style="vertical-align: bottom">
                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:DeffinityRes, Search%>" />
                    &nbsp;  <asp:TextBox ID="txtSearchBox2" runat="server" style="width: 200px" ></asp:TextBox>
                
                </td>
                <td style="vertical-align: bottom">
                 <asp:LinkButton SkinID="BtnLinkSearch" alt=" <%$ Resources:DeffinityRes,SearchButton%>"  CommandName="SearchFile2" OnClick="ImageFile_Click" id="ImgBtnsearch2" title="<%$ Resources:DeffinityRes, Clicktosearch%>"
                    runat="server" style="vertical-align: bottom" />
                  
                </td>
                
                <td>
                
                </td>
                <td style="vertical-align: bottom">
                <asp:LinkButton SkinID="BtnLinkCancel" alt=" <%$ Resources:DeffinityRes,Close%>  "  id="lnkClose" title=" <%$ Resources:DeffinityRes,Close%> "
                 runat="server" style="vertical-align: bottom" />
                    

                </td>
            </tr>
        </table>
        <hr style="width: 98%; height: 3px" />

        <div id="divSearchResults" runat="server" style="padding-left: 20px; padding-right: 20px; padding-bottom: 20px">
        
        
        </div>
    </asp:Panel>
</div>
<asp:SqlDataSource ID="sqlFolderList" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
    SelectCommand="DEFFINITY_Folder" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter Name="ProjectReference" SessionField="folderID" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<div class="form-group row">
      <div class="col-md-4">
             <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" DataTextField="projectTitle"
                    DataValueField="ProjectReference" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
                    Visible="false" Width="1px">
                </asp:DropDownList>
            <div style="font-size: 13px; color: #999;font-weight: bold; vertical-align: middle;">
                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:DeffinityRes, DocumentStructure%>" /></div>
            <ignav:UltraWebTree ID="UltraWebTree1" runat="server" Height="360px" LoadOnDemand="Manual"
                CompactRendering="true" Visible="true" OnNodeClicked="UltraWebTree1_NodeClicked"
                WebTreeTarget="ClassicTree" Font-Names="Verdana" Font-Size="8pt" BorderWidth="1"
                BorderStyle="Solid">
                <SelectedNodeStyle Cursor="Hand" ForeColor="White" BackColor="DarkBlue" CssClass="SelectClass">
                </SelectedNodeStyle>
                <Images>
                </Images>
                <Padding Top="0px"></Padding>
                <NodeMargins Top="0px"></NodeMargins>
                <NodeStyle ForeColor="Black" />
                <Styles>
                    <ignav:Style Cursor="Hand" ForeColor="Black" BackColor="OldLace" CssClass="HiliteClass">
                    </ignav:Style>
                    <ignav:Style BorderWidth="0px" BorderColor="DarkGray" BorderStyle="Solid" BackColor="Gainsboro"
                        CssClass="Hover">
                        <Padding Top="0px"></Padding>
                    </ignav:Style>
                    <ignav:Style ForeColor="White" BackColor="#333333" CssClass="SelectClass">
                        <Padding Top="0px"></Padding>
                    </ignav:Style>
                </Styles>
            </ignav:UltraWebTree>          
            <br />
            <asp:Label ID="lblCounter" Font-Bold="true" runat="server"></asp:Label><br />        
            <asp:Label ID="lblSpaceUsed"  Font-Bold="true" runat="server"></asp:Label><br /><br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
          </div>
       <div class="col-md-8">
             <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
            <asp:GridView ID="gridFiles" EnableViewState="true" runat="server" AutoGenerateColumns="False"
                DataSourceID="sqlFileList" Width="100%" DataKeyNames="ID" OnRowCommand="gridFiles_RowCommand"
                AllowPaging="True" onrowupdating="gridFiles_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_ID%>" InsertVisible="False" ReadOnly="True"
                        SortExpression="ID" Visible="false" />
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Document%>" HeaderStyle-CssClass="header_bg_l">
                        <ItemTemplate>
                            <asp:Label ID="lblFileSize" runat="server" Text='<%#Eval("DataSize") %>' Visible="false" />
                            <asp:CheckBox ID="chkChecked" Visible='<%#CheckBoxInvisible()%>'  runat="server"></asp:CheckBox>
                            <asp:Label ID="imgFileIcon" runat="server" CssClass='<%#GetIcon(Eval("DocumentName").ToString())%>' />
                            <asp:LinkButton ID="lnkDownLoad" runat="server" Text='<%#Eval("DocumentName")%>' SkinID="BtnLinkDownload"
                                CommandArgument='<%#Eval("ID")%>' Enabled='<%#GetCheckInOutEnable(Eval("CheckOut").ToString())%>' CommandName="Download" />
                            
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' Visible="false" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditName" runat="server" Text='<% # DataBinder.Eval(Container.DataItem,"DocumentName")%>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle CssClass="header_bg_l"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="40" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Right" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_SizeinKB%>">
                        <ItemTemplate>
                            <asp:Label ID="lblDataSize" runat="server"  Text='<%#Eval("DataSize") %>' />
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="5" ItemStyle-Width="10" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Uploaded%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUploadDateTime" runat="server" Text='<%#Eval("UploadDateTime") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Author%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("UpdatedBy") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="15" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderStyle-Width="5" ItemStyle-Width="10" HeaderText="<%$ Resources:DeffinityRes, Doc_gridFiles_Version%>">
                    <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>                            
                            <a title="Click here to view all versions"  href="#" onclick="window.open('DocVersions.aspx?DocumentID=<%#DataBinder.Eval(Container.DataItem,"ID")%>&folderID=<%#DataBinder.Eval(Container.DataItem,"MasterID")%>',null,'height=750 width=1000 scrollbars=yes')" ><%#Eval("Version")%> </a>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="Portal View" HeaderStyle-Width="10">
                        <ItemTemplate>
                           <asp:LinkButton ID="btnPortalView" runat="server"    CommandArgument='<%#Eval("ID") + ","+Eval("projectDoc") %>' 
                                CommandName="PortalView" CssClass='<%#GetPortalViewImage(Eval("projectDoc").ToString())%>' ToolTip="Portal View" AlternateText="Portal View"  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"  />
                    </asp:TemplateField> 
                    <asp:TemplateField   ItemStyle-Width="1" ItemStyle-HorizontalAlign="Center" Visible="false" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate >                                                        
                           <asp:LinkButton ID="btnpermissions" runat="server"   CommandArgument='<%#Eval("ID")%>' CommandName="Select" 
                                SkinID="app" ImageUrl="/media/ico_set_permissions.png"  ToolTip="<%$ Resources:DeffinityRes, SetPermissions%>" />                                                        
                            </ItemTemplate>
                            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="1" ItemStyle-Width="1" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle">                    
                        <ItemTemplate>
                             <asp:LinkButton ID="btnCheckinOutIcon"  OnClick="ImageFile_Click"  OnClientClick='<%#GetCheckInOutUrl(Eval("ID").ToString(),Eval("CheckOut").ToString())%>'  
                                ToolTip='<%#GetCheckInOut(Eval("CheckOut").ToString(),Eval("UpdatedBy").ToString(),Eval("UploadDateTime").ToString())%>'   runat="server" CommandArgument='<%#Eval("ID")%>'                              
                             Enabled='<%#GetCheckInOutEnableByID(Eval("ContractorID").ToString(),Eval("CheckOut").ToString())%>' CssClass='<%#GetCheckInOutIcon(Eval("CheckOut").ToString())%>' AlternateText='<%#GetCheckInOut(Eval("CheckOut").ToString(),Eval("UpdatedBy").ToString(),Eval("UploadDateTime").ToString())%>'   />  
                              
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="1" ItemStyle-Width="1" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                    
                        <ItemTemplate>
                             <asp:LinkButton ID="btnCheckOutReset"  OnClick="ImageFile_Click" Enabled='<%#ResetEnable(Eval("ContractorID").ToString(),Eval("CheckOut").ToString())%>' CommandName="ResetCheckOut" 
                                runat="server" CommandArgument='<%#Eval("ID")%>' ToolTip="<%$ Resources:DeffinityRes, ResetToCheckOut%>"
                              CssClass='<%#ResetEnableIcon(Eval("ContractorID").ToString(),Eval("CheckOut").ToString())%>' AlternateText="<%$ Resources:DeffinityRes, ResetToCheckOut%>"  />  
                              
                        </ItemTemplate>                        
                    </asp:TemplateField>      
                    <asp:TemplateField>                    
                        <ItemTemplate>
                    <a title="Document Journals"  href="#" onclick="window.open('DocumentJournals.aspx?DocumentID=<%#DataBinder.Eval(Container.DataItem,"ID")%>',
                    null,'height=650 width=700 scrollbars=no')" ><img src="media/ico_journal.png" border="0" alt="<%$ Resources:DeffinityRes,DocumentJournals%> "  runat="server" id="imgJournal" /></a>
        
                        </ItemTemplate >
                            <ItemStyle Width="15" />                     
                    </asp:TemplateField>           
                    <asp:TemplateField HeaderStyle-Width="1" ItemStyle-Width="1" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnRename" Enabled='<%#GetCheckInOutEnable(Eval("CheckOut").ToString())%>'  runat="server" CommandName="Edit" ToolTip="<%$ Resources:DeffinityRes, Rename%>">
                              
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" SkinID="" CommandName="Update" ToolTip="<%$ Resources:DeffinityRes, Update%>">
                               </asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField   HeaderStyle-Width="1" ItemStyle-Width="1" HeaderStyle-CssClass="header_bg_r">                    
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server"     CommandArgument='<%#Eval("ID")%>'
                                CommandName="DeleteFile" OnClientClick='return confirm("Do you really want to delete this file?");'
                                SkinID="BtnLinkDelete" ToolTip="<%$ Resources:DeffinityRes, DeleteFile%>"  AlternateText="<%$ Resources:DeffinityRes, DeleteFile%>" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" ToolTip="<%$ Resources:DeffinityRes, Cancel%>"
                                CausesValidation="false">
                               </asp:LinkButton>&nbsp;
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                        <HeaderStyle CssClass="header_bg_r"></HeaderStyle>
                    </asp:TemplateField>                    
                    <asp:TemplateField   HeaderStyle-Width="10" ItemStyle-Width="10" HeaderStyle-CssClass="header_bg_r" HeaderText="<%$ Resources:DeffinityRes, Author%>" Visible="false">
                        <ItemTemplate>
                            <%#Eval("UpdatedBy")%>
                        </ItemTemplate>
                    </asp:TemplateField>  
                                     
                </Columns>
            </asp:GridView>
           
                <ajaxtoolkit:modalpopupextender ID="ModalControlExtender1" CancelControlID="imgPerClose"
                    BackgroundCssClass="modalBackground" runat="server" TargetControlID="BtntempPermissionModal"
                    PopupControlID="pnlPermission">
                </ajaxtoolkit:modalpopupextender>
                
                <asp:Label ID="BtntempPermissionModal" runat="server" />
                <asp:Label ID="BtntempSearchModal" runat="server" />
                
                <asp:Panel ID="PnlFileUpload"  Visible="false" Font-Bold="true" runat="server" BorderStyle="Double" BorderColor="LightSteelBlue"
                ScrollBars="Auto">
                               
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:DeffinityRes, Doc_gridFiles_Selectfiletoupload%>" />             
                <asp:FileUpload ID="FileUpload1" runat="server" maxlength="5" class="multi" />
                <br />
                <asp:Button ID="btnUpload" runat="server" Text="<%$ Resources:DeffinityRes, UploadAll%>" OnClick="btnUpload_Click"/>          
            </asp:Panel>   
            

           
            <asp:Panel ID="PnlAllowCustomers"  Visible="false" Font-Bold="true" runat="server"  BorderColor="LightSteelBlue" ScrollBars="Auto">
            <table>
            <tr>
            <td>
            <asp:CheckBox ID="chkAllowCustomers"  Text="Allow customers to upload documents?"  runat="server"></asp:CheckBox>
            </td>
            <td>
            <asp:Button ID="btnUpdateDoc" runat="server" Text="Update" OnClick="btnUpdateDoc_Click"/>                                  
            </td>
            </tr>
            </table>
            </asp:Panel>
          </div>
    </div>
            <asp:Panel ID="pnlPermission" runat="server" Style="display: none; width: 800px;
                height: 400px" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue"
                ScrollBars="Auto">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gridResources" runat="server" OnRowCommand="gridResources_RowCommand"
                                        Width="90%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Resource%>" HeaderStyle-CssClass="header_bg_l">
                                                <ItemTemplate>
                                                    <%#Eval("ContractorName")%>
                                                    <div id='lblContractorID' style='display: none'>
                                                        <%#Eval("ContractorID")%></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Restricted%>" >
                                                <ItemTemplate>
                                                    <%#Eval("IsRestricted")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r"  HeaderText="<%$ Resources:DeffinityRes, SetRole%>" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPermission" runat="server" Text="<%$ Resources:DeffinityRes, ChangeRule%>" CommandArgument='<%#Eval("DocumentID") + "|" + Eval("ContractorID")%>'
                                                        CommandName="SetPermissions" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">                               
                        <asp:LinkButton SkinID="BtnLinkCancel" alt="<%$ Resources:DeffinityRes,Close%> "  id="imgPerClose" title="<%$ Resources:DeffinityRes,Close%> "
                         runat="server" style="vertical-align: bottom" />                                
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
       
<asp:SqlDataSource ID="sqlFileList" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
    SelectCommand="DEFFINITY_FileList" SelectCommandType="StoredProcedure" UpdateCommandType="StoredProcedure" UpdateCommand="AC2P_Documents_Update" >
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="ParentID" QueryStringField="folderID"
            Type="Int32" />
        <asp:SessionParameter DefaultValue="0" Name="CustomerPortal" Type="Int32" SessionField="CustomerPortalID" />            

    </SelectParameters>
    <UpdateParameters>
    <asp:ControlParameter ControlID="gridFiles" Name="ID" PropertyName="SelectedValue" />
    <asp:Parameter Name="DocumentName" Type="String" />
    
    </UpdateParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="TxtFolderID" runat="server" />
<asp:HiddenField ID="txtFileID" runat="server" />
<asp:HiddenField ID="txtContractorID" runat="server" />

        <script type="text/javascript">

            function OpenChild(id,folder,mode) {
                
                if (mode == 'false') {
                    var one;
                    var two;
//                    one='height=0,width=0,top=0,left=0,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false';
//                    two='popup';
//                    window.open('POPCheckin.aspx?ID=' + id + '&mode=' + mode, one, two);
                    one = 'height=10,width=10,top=10,left=10,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false';
                    two = 'popup';
                    window.open('POPCheckin.aspx?ID=' + id + '&folder=' + folder + '&mode=' + mode, two, one);
                    //window.open('Handler.ashx?ID=' + id + '&folder=' + folder + '&mode=' + mode, two, one);
                    
                }
                else {
                    var one;
                    var two;
//                    one='height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false';
//                    two = 'POPCheckin';
//                    window.open('POPCheckin.aspx?ID=' + id + '&mode=' + mode ,one,two);
                    //window.open('POPCheckin.aspx?ID=' + id + '&mode=' + mode +','_blank','height=355,width=710,top=150,left=150,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false');

                    one = 'height=220,width=380,top=200,left=250,status=no,toolbar=no,center=yes,menubar=no,location=no,scrollbars=no,resizable=no,copyhistory=false';
                    two = 'POPCheckin';
                    window.open('POPCheckin.aspx?ID=' + id + '&folder=' + folder + '&mode=' + mode, two, one);
                }
                
            
            }
                        
                        
            function SelectAll(id) {
                //get reference of GridView control
                var grid = document.getElementById("<%= gridFiles.ClientID %>");
                //variable to contain the cell of the grid
                var cell;

                if (grid.rows.length > 0) {
                    //loop starts from 1. rows[0] points to the header.
                    for (i = 1; i < grid.rows.length; i++) {
                        //get the reference of first column
                        cell = grid.rows[i].cells[0];

                        //loop according to the number of childNodes in the cell
                        for (j = 0; j < cell.childNodes.length; j++) {
                            //if childNode type is CheckBox                 
                            if (cell.childNodes[j].type == "checkbox") {
                                //assign the status of the Select All checkbox to the cell checkbox within the grid
                                cell.childNodes[j].checked = document.getElementById(id).checked;
                            }
                        }
                    }
                }
            }

</script>