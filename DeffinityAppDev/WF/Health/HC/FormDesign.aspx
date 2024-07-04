<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HC_FormDesign" EnableEventValidation="false" Codebehind="FormDesign.aspx.cs" %>
<%@ Register Src="~/WF/CustomerAdmin/Controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Forms%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
  <%= Resources.DeffinityRes.FormDesign%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" runat="Server"> 
  <asp:HyperLink runat="Server" NavigateUrl="~/WF/Health/HC/FormList.aspx">
<i class="fa fa-arrow-left"></i> Back to Forms</asp:HyperLink>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <style>
        .ui-icon-plusthick:hover{
display: inline-block; 
width: 20px;
height: 20px;
}
    </style>
 <script  type="text/javascript">
     $(document).ready(function () {
         $('#navTab').hide();
     });
       </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<link href="../Scripts/jtable/themes/jqueryui/jtable_jqueryui.min.css" rel="stylesheet" />--%>
     
     <%--<script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>--%>
    <script src="../../../Content/jtable/external/json2.js" type="text/javascript"></script>
    <%--<link rel="stylesheet" href="../../../Content/HCstyle.css"/>
    <script type="text/javascript" src="../../../Scripts/HCform.js"></script>--%>
    

<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
   <style type="text/css">
  .ui-menu { width: 150px; }

       .input[type=text] {
           height:30px;
       }

      
  </style>

    
<div class="form-group row">
          <div class="col-md-12">
              <div id="lblProgress" style="color:green;" ></div>
            <div id="lblerror" style="color:white;"></div>
              <asp:Label ID="lblMsg1" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
	</div>
</div>
    <div class="form-group row">
     
	<div class="col-md-12">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.FormName%>:</label>
           <div class="col-sm-4">
               <asp:TextBox ID="txtFormName" runat="server" Width="350px" ClientIDMode="Static"></asp:TextBox> 
            </div>
         <div class="col-sm-4 form-inline">
             <asp:Button ID="btnUpdateform" runat="server" Text="Update Form" ClientIDMode="Static" />
              <asp:HiddenField ID="hformid" runat="server" ClientIDMode="Static" Value="0" /> 
                <asp:HiddenField ID="hcid" runat="server" ClientIDMode="Static" Value="0" /> 
            </div>
	</div>
	
</div>
     <div class="form-group row">
          <div class="col-md-12">
           <label class="col-sm-2 control-label">Associate Type of Request</label>
           <div class="col-sm-4">
               <asp:DropDownList ID="ddlTypeofRequest" runat="server"></asp:DropDownList>
               
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTypeofRequest"
                        Display="None" ErrorMessage="Please select type of request" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_f"></asp:RequiredFieldValidator>
            </div>
               <div class="col-md-6">
              <asp:Button ID="btnSave" runat="server" SkinID="btnSave" ValidationGroup="cat_f" OnClick="btnSave_Click"/>
              </div>
	</div>
         
          </div>
     <div class="row">
         <div class="col-md-9">
         
            <div class="form-group row">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Form%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                            
                            <asp:Panel id="pnlform" ClientIDMode="Static" ScrollBars="Both" runat="server" Height="900px" Width="100%" >
                                <div id="pnlheader" style="margin-right:3px;"></div>
                                <div id="pnlcontent" style="margin-right:3px;"></div>
                                <div id="pnlsignoff" style="margin-right:3px;"></div>
                                <div id="pnlfooter" style="margin-right:3px;"></div>
                            </asp:Panel>
            <div id="divdata"></div>
            <asp:HiddenField ID="hdata" runat="server" ClientIDMode="Static" />
              
           </div>
          <div class="col-md-3">
               <div class="form-group row" style="vertical-align:top">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.FormControls%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                           
                            <div class="form-group row">
                                <ul >
                                     <li>
                                <asp:LinkButton ID="btnHeader" runat="server" Text=" Add Header Section" ClientIDMode="Static"></asp:LinkButton>
                              </li>
                                    <li>
                               
                                <asp:LinkButton ID="btnAddPanel" runat="server" Text="Add Section" ClientIDMode="Static"></asp:LinkButton>
                              </li>
                                    <%-- <li>
                                <asp:LinkButton ID="btnSignoff" runat="server" Text="Add Sign off Panel" ClientIDMode="Static"></asp:LinkButton>
                              </li>--%>
                                    
                                    <%-- <li>
                                <asp:LinkButton ID="btnFooter" runat="server" Text="Add Footer" ClientIDMode="Static"></asp:LinkButton>
                              </li>--%>
                                    <li>
                                <asp:LinkButton ID="btnSetBack" runat="server" Text="Configure Form Background" ClientIDMode="Static"></asp:LinkButton>
                              </li>
                                    <li>
                                           <asp:LinkButton ID="btnSignature" runat="server" Text="<%$ Resources:DeffinityRes,AddSignatureStrip%>"
                                                ClientIDMode="Static"></asp:LinkButton>
                                    </li>
                                    <li>
                                <asp:LinkButton ID="btnPreview" runat="server" Text="Preview Form" OnClick="btnPreview_Click"
                                     CausesValidation="false"></asp:LinkButton>
                                    </li>
                                    <li style="display:none;">
                                         <asp:LinkButton ID="BtnPosition" runat="server" Visible="false"
                                            Text="Panel Position" OnClick="BtnPosition_Click"></asp:LinkButton>
                                        
                                        <asp:Label ID="l1" runat="server"></asp:Label>
                                        <ajaxToolkit:ModalPopupExtender ID="Ajaxpopup" runat="server"
                                     TargetControlID="l1" PopupControlID="PaneladdNew" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                       <asp:Panel ID="PaneladdNew" runat="server" BackColor="White"
                                            BorderStyle="Double" BorderColor="LightSteelBlue" 
                                               Style="display: none" ScrollBars="Auto" ClientIDMode="Static">
                                          <table>
                                              <tr>
                                         <td>
                                        <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" ForeColor="Green"></asp:Label>
                                         <asp:Label ID="lblscreen" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                         </td>
                    <td>
                        <div style="text-align: right;">
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false"
                                 SkinID="BtnLinkCancel"/>
                        </div>

                    </td>
                </tr>
                                              <tr>
                                                  <td>
                                                      Panel Position<br />
                                 <asp:ListBox ID="gridlist" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                                                  </td>
                                                  <td>
                                                      <br />
                                                      <button id="btn" type="button"><%=Resources.DeffinityRes.Up %></button>
                                          <button id="Btn2" type="button"><%=Resources.DeffinityRes.Down %></button>
                                          <button id="save" type="button"><%=Resources.DeffinityRes.Save %></button>
                                                  </td>
                                              </tr>
                                          </table>
                                        </asp:Panel>
                                    </li>
                                </ul>
                            </div>
                            <div id="pnlEdit" class="Basic dialog pnldialog" style="display:none" title="Edit panel">
                               <label> Panel Name: </label><br />
                                  <asp:TextBox ID="txtEditPanelName" runat="server" ClientIDMode="Static" Width="250px"
                                       MaxLength="250" Height="30px"></asp:TextBox>
                            </div>
                            <div id="pnlAdd" class="Basic dialog pnldialog" style="display:none;" title="Add New Panel"> 
                                <label> Panel Name: </label><br />
                                <asp:TextBox ID="txtPanelName" runat="server" ClientIDMode="Static" Height="30px" Width="250px"
                                     MaxLength="250"></asp:TextBox>
                                <label style="font-weight:bold">Layout Option</label><br />
                                <label style="display:inline-block;width:100px"> Number of Rows:</label>
                               <asp:TextBox ID="txtRows" runat="server" Width="50px" Text="1" ClientIDMode="Static" SkinID="txt_75px"></asp:TextBox> <br />
                                 <label  style="display:inline-block;width:100px"> Number of Columns:</label> 
                                <asp:TextBox ID="txtColumns" runat="server" Width="50px" Text="1" ClientIDMode="Static" SkinID="txt_75px"></asp:TextBox>
                            </div>

                             <div id="pnlColor" class="Basic dialog pnldialog" style="display:none;" title="Set Background Color"> 
                                  <div id="cblist" style="margin-top:2px;margin-left:10px">
                                   <div style="margin-top:2px">
                                      
                                     <input id="chk_1" type="checkbox" value="#0000FF" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#0000FF;"><span id="span_1" style="padding:0px 5px 0px 5px;background-color:#0000FF;">&nbsp;</span></span>
                                     <input id="chk_2" type="checkbox" value="#CFD8DC" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#CFD8DC;"><span id="span_2" style="padding:0px 5px 0px 5px;background-color:#CFD8DC;">&nbsp;</span></span>
                                     <input id="chk_3" type="checkbox" value="#F5F5F5" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#F5F5F5;"><span id="span_3" style="padding:0px 5px 0px 5px;background-color:#F5F5F5;">&nbsp;</span></span>
                                     <input id="chk_4" type="checkbox" value="#FFF9C4" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#FFF9C4;"><span id="span_4" style="padding:0px 5px 0px 5px;background-color:#FFF9C4;">&nbsp;</span></span>
                                     <input id="chk_5" type="checkbox" value="#B2EBF2" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#B2EBF2;"><span id="span_5" style="padding:0px 5px 0px 5px;background-color:#B2EBF2;">&nbsp;</span></span>
                                       </div>
                                   <div style="margin-top:5px">
                                     <input id="chk_6" type="checkbox" value="#E1BEE7" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#E1BEE7;"><span id="span_6" style="padding:0px 5px 0px 5px;background-color:#E1BEE7;">&nbsp;</span></span>
                                     <input id="chk_7" type="checkbox" value="#D7CCC8" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#D7CCC8;"><span id="span_7" style="padding:0px 5px 0px 5px;background-color:#D7CCC8;">&nbsp;</span></span>
                                     <input id="chk_8" type="checkbox" value="#F0F4C3" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#F0F4C3;"><span id="span_8" style="padding:0px 5px 0px 5px;background-color:#F0F4C3;">&nbsp;</span></span>
                                     <input id="chk_9" type="checkbox" value="#B3E5FC" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#B3E5FC;"><span id="span_9" style="padding:0px 5px 0px 5px;background-color:#B3E5FC;">&nbsp;</span></span>
                                     <input id="chk_10" type="checkbox" value="#B2DFDB" name="chk" /><span style="border-width:1px;border-style:solid;border-color:silver;background-color:#B2DFDB;"><span id="span_10" style="padding:0px 5px 0px 5px;background-color:#B2DFDB;">&nbsp;</span></span>
                                       </div>
                               </div>
                                 </div>
                             <div id="pnlControl" class="Basic dialog pnldialog" style="display:none;" title="Add Control"> 
                                 <div>
                                     <label>Type of Field</label><br />
                                     <asp:DropDownList ID="ddlfieldtype" runat="server" ClientIDMode="Static" Width="200px">
                                         <asp:ListItem Text="Textbox" Value="Textbox" Selected="True"></asp:ListItem>
                                         <asp:ListItem Text="Label" Value="Label"></asp:ListItem>
                                         <asp:ListItem Text="Textarea" Value="Textarea"></asp:ListItem>
                                         <asp:ListItem Text="Dropdown" Value="Dropdown"></asp:ListItem>
                                         <asp:ListItem Text="RadioButton" Value="RadioButton"></asp:ListItem>
                                         <asp:ListItem Text="Checkbox" Value="Checkbox"></asp:ListItem>
                                         <asp:ListItem Text="Checkbox List" Value="Checkboxlist"></asp:ListItem>
                                         <asp:ListItem Text="Image" Value="Image"></asp:ListItem>
                                         <asp:ListItem Text="Table" Value="Table"></asp:ListItem>
                                         <asp:ListItem Text="Date" Value="Date"></asp:ListItem>
                                     </asp:DropDownList>
                                     <br />
                                      
                                        <div id="divlbl">
                                      <label>Label</label><br />
                                     <asp:TextBox  ID="txtlabel" runat="server" ClientIDMode="Static" Width="200px"  MaxLength="1000"  Height="30px"/>
                                        </div> 

                                       <label>Help Text</label><br />
                                      <asp:TextBox ID="txthelptext" runat="server" ClientIDMode="Static" Width="200px"></asp:TextBox>

                                      <div id="divimagecontrol">
                                          <asp:FileUpload ID="ImageUploadcntl" runat="server" ClientIDMode="Static"  Height="30px"/>
                                       </div>
                                       <div id="columnDivList">
                                         <label>Column Values</label><br />
                                         <asp:TextBox ID="txtclist" runat="server" ClientIDMode="Static" Width="250px" MaxLength="4000"  Height="30px"></asp:TextBox>
                                           <label style="font-size:10px;color:silver;display:inline-block;">e.g: item1,item2,item3....</label>
                                     </div>
                                    
                                      <div class="l_class d_class" id="Div_DefaultValue">
                                     <label id="IDDefaultValue">Default Value</label><br />
                                      <asp:TextBox  ID="txtdefault" runat="server" ClientIDMode="Static" Width="200px" MaxLength="250"  Height="30px" /> 
                                     <br />
                                          </div>
                                     <div id="divlistval" class="l_class c_class">
                                     <label id="ListValues">List Values</label><br />
                                     <asp:TextBox  ID="txtlist" runat="server" ClientIDMode="Static" Width="250px" MaxLength="4000"  Height="30px"></asp:TextBox>
                                     <label style="font-size:10px;color:silver;display:inline-block;">e.g: item1,item2,item3....</label>
                                     <br />
                                     </div>
                                      <div id="DivDropdownTypes" style="display:none;">
                                        <label>Dropdown data</label>
                                         <asp:DropDownList ID="ddltypeOfData" runat="server" ClientIDMode="Static" Width="200px">
                                             <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                                             <asp:ListItem Text="List of Project Managers" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="List of Customer Sites" Value="2"></asp:ListItem>
                                             <asp:ListItem Text="List of Our Sites" Value="3"></asp:ListItem>
                                             <asp:ListItem Text="List of Resources" Value="4"></asp:ListItem>
                                             <asp:ListItem Text="List of Administrators" Value="5"></asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                    <div id="tableoptions">
                                     <label>Control option</label><br />
                                     <asp:RadioButtonList ID="rbcontrolsList" runat="server" ClientIDMode="Static"  Height="30px">
                                          <asp:ListItem Text="Textbox" Value="Textbox" Selected="True"></asp:ListItem>
                                           <asp:ListItem Text="Checkbox" Value="Checkbox"></asp:ListItem>
                                          <%-- <asp:ListItem Text="Radio Button" Value="RadioButton"></asp:ListItem>--%>
                                     </asp:RadioButtonList>
                                         </div>
                                     <div class="t_class">
                                     <label>Min Value</label><br />
                                     <asp:TextBox  ID="txtmin" runat="server" ClientIDMode="Static" MaxLength="10"  Height="30px" SkinID="txt_75px" /> 
                                       <br />
                                         </div>
                                      <div class="t_class">
                                     <label>Max Value</label><br />
                                     <asp:TextBox  ID="txtmax" runat="server" ClientIDMode="Static" MaxLength="10"  Height="30px" SkinID="txt_75px" /> 
                                       <br />
                                     </div>
                                     <div id="DivMandatory">
                                     <label>Mandatory</label><br />
                                     <asp:CheckBox ID="chkmandate" runat="server" ClientIDMode="Static" Checked="false"  Height="30px" /> 
                                     <br /></div>
                                     <div id="divrblist">
                                     <label>Options</label><br />
                                     <asp:RadioButtonList ID="rblist" runat="server" ClientIDMode="Static"  Height="30px">
                                         <asp:ListItem Value="Horizontal" Text="Horizontal"></asp:ListItem>
                                         <asp:ListItem Value="Vertical" Text="Vertical" Selected="True"></asp:ListItem>
                                     </asp:RadioButtonList> <br />
                                         </div>
                                      <div class="H_class">
                                     <label>Height</label><br />
                                      <asp:TextBox  ID="txtHeight" runat="server" ClientIDMode="Static" Width="50px" MaxLength="3" Text="10"  Height="30px" SkinID="txt_75px"></asp:TextBox><label>px</label>
                                       <br />
                                          </div>
                                     <div id="DivWidth">
                                     <label>Width</label><br />
                                      <asp:TextBox  ID="txtwidth" runat="server" ClientIDMode="Static" Width="50px" MaxLength="3" Text="80"  Height="30px" SkinID="75`"></asp:TextBox><label>%</label>
                                       <br /></div>
                                     <div id="DivPosition">
                                     <label>Position</label><br />
                                     <asp:DropDownList ID="ddlposition" runat="server" ClientIDMode="Static"  Height="30px" SkinID="ddl_50">
                                         <asp:ListItem Text="Left" Value="left" Selected="True"></asp:ListItem>
                                         <asp:ListItem Text="Center" Value="center"></asp:ListItem>
                                         <asp:ListItem Text="Right" Value="right"></asp:ListItem>
                                     </asp:DropDownList></div>
                                 </div>
                                 </div>
                            <div id="pnlFormadd" class="Basic dialog pnldialog" style="display:none;height:390px;" title="Create Form"> 
                                <label><asp:HiddenField ID="hcustomerid" runat="server" Value="0" ClientIDMode="Static" /> </label><br />
                                <div style="display:none;visibility:hidden;">
                                 <label>Customer</label><br />
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px" ClientIDMode="Static" Height="30px">
                                </asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="ccdCompny" runat="server" TargetControlID="ddlCustomer"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" BehaviorID="ccdc"
                                ClientIDMode="Static" />
                                <br />
                                    </div>
                                <label>Form Name</label><br />
                                <asp:TextBox ID="txtAddForm" runat="server" ClientIDMode="Static" MaxLength="100" Width="200px" Height="30"></asp:TextBox>
                                </div>

                            <div id="Panelposition" style="display:none;"  class="Basic dialog pnldialog" title="Panel Position">
                                  <asp:DropDownList ID="ddlpostion" runat="server" ClientIDMode="Static" Width="200px" Height="30px">
                                      <asp:ListItem Text="Move up" Value="Move up"></asp:ListItem>
                                       <asp:ListItem Text="Move down" Value="Move down"></asp:ListItem>
                                  </asp:DropDownList>
                            </div>
              </div>
         </div>
 <script  type="text/javascript">
        $(document).ready(function () {
           
            sideMenuActive('<%= Resources.DeffinityRes.Forms%>');
       });
       </script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="Scripts_Section" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
  <%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
</asp:Content>



