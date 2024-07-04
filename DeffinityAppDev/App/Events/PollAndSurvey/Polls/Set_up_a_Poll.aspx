<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Set_up_a_Poll.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Polls.Set_up_a_Poll" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

    <style >
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }

 .BigCheckBox input {width:30px; height:30px;}




    select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }



    </style>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button"  data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Set up a Poll</h3>
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->
        <!--begin::Content-->
        <div id="kt_account_profile_details" class="collapse show" style="">
            <!--begin::Form-->
            <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
                <div class="card-body border-top p-9">
                    <!--begin::Input group-->


                        <label class="col-lg-2 col-form-label required fw-bold fs-6">Question </label>

                      <div class="row mb-6">
                        <!--begin::Label-->
                    
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtQuestion" runat="server"   placeholder="Question"  ></asp:TextBox>
                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                     <label class="col-lg-2 col-form-label  fw-bold fs-6"> Question Description  </label>
                    <!--begin::Input group-->
                    <div class="row mb-6">
                        <!--begin::Label-->
                       
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-12 fv-row fv-plugins-icon-container">

                                    <%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

                                    

                                    
                                     <asp:TextBox ID="TextAreaDescription" runat="server" placeholder="Question Description"    TextMode="MultiLine" Rows="10"  ></asp:TextBox>



                                    <%--<CKEditor:CKEditorControl ID="CKEditorTextArea" BasePath="~/Scripts/ckeditor/" runat="server"
                                        Height="200px" ClientIDMode="Static"></CKEditor:CKEditorControl>--%>

                                    
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>
                    <!--end::Input group-->








                    <br />
                   





                      <div class="row mb-6">
                        <!--begin::Label-->
                      <%--  <label class="col-lg-2 col-form-label  fw-bold fs-6"> Multiple Choice </label>--%>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-6">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-14 fv-row fv-plugins-icon-container">
                                   
                                    
                                    <input type="radio" id="html"  checked="checked" name="fav_language" value="HTML" style="tab-size:50" size="50"  />
                                    <label class="col-lg-4 col-form-label  fw-bold fs-6">Multiple Choice</label> 
                                    &emsp; 
                                    <input type="radio" id="css" name="fav_language" value="CSS" />
                                    <label class="col-lg-4 col-form-label  fw-bold fs-6">Single Option</label>
                                    

                                </div>
                                <!--end::Col-->

                            </div>
                        </div>



                        <!--end::Col-->
                    </div>

                      


                      <div class="row mb-6"   >
                      
                     <%--   <label class="col-lg-2 col-form-label required fw-bold fs-6"> Zoom API Key  </label>--%>
                       
                        <div class="col-lg-4">

                            	<asp:HiddenField ID="hmoney" runat="server" />
														<div class="row">
															<div class="col-lg-8"><asp:TextBox ID="txtMoney" runat="server" placeholder="enter Choice"></asp:TextBox>
																</div>
														<div class="col-lg-4">
																 <asp:Button ID="btnAddMoney" runat="server" CssClass="btn btn-light" OnClick="btnAddMoney_Click" Text="Add Choice" /></div>
													</div>
														<div class="row">
													<asp:GridView ID="GridMoney" runat="server" OnRowCommand="GridMoney_RowCommand" ShowHeader="false" Width="90%" >
														<Columns>
														<%--	<asp:TemplateField>
																<ItemTemplate>
																<asp:LinkButton ID="btnEdit" runat="server" CommandName="edit1" CommandArgument='<%#Eval("value","{0:F2}") %>' SkinID="BtnLinkEdit"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>--%>
															<asp:TemplateField HeaderText="">
																<ItemTemplate>
																<asp:TextBox ID="lblValue" runat="server" Text='<%#Eval("value") %>' MaxLength="10"></asp:TextBox>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField>
																<ItemTemplate>
																<asp:LinkButton ID="btnEdit" runat="server" CommandName="del" CommandArgument='<%#Eval("value") %>' SkinID="BtnLinkDelete"></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
															</div>


                            <div class="row" style="display:none;visibility:hidden;">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtMultipleChoice1" runat="server"   placeholder="Choice 1"      ></asp:TextBox>   <br />
                                    <asp:TextBox ID="txtMultipleChoice2" runat="server"   placeholder="Choice 2"      ></asp:TextBox>   <br />
                                    <asp:TextBox ID="txtMultipleChoice3" runat="server"   placeholder="Choice 3"      ></asp:TextBox>  <br />
                                    <asp:TextBox ID="txtMultipleChoice4" runat="server"   placeholder="Choice 4"      ></asp:TextBox>
                                   
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>




                      <div class="row mb-6">
                        <!--begin::Label-->
                      <%--  <label class="col-lg-2 col-form-label  fw-bold fs-6"> Multiple Choice </label>--%>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-6">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-14 fv-row fv-plugins-icon-container">
                                    <input type="radio" id="chkbarchart" name="chart" value="pie" style="tab-size:50" size="50" runat="server" />
                                    <label class="col-lg-4 col-form-label  fw-bold fs-6">Bar chart</label> 
                                    &emsp; 
                                    <input type="radio" id="chkpie" name="chart" value="pie" runat="server" />
                                    <label class="col-lg-4 col-form-label  fw-bold fs-6">Pie chart</label>
                                </div>
                                <!--end::Col-->
                            </div>
                        </div>
                        <!--end::Col-->
                    </div>


                    <div class="row mb-6">
                        <%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
                        <!--begin::Col-->
                        <div class="col-lg-14">
                            <div class="row">
                                <!--begin::Col-->

                                <div class="col-lg-5 fv-row fv-plugins-icon-container">

                                   <%-- <button class="btn btn-primary" type="button" title="Add an Answer" onclick="SelectFileForBackGround()">Add an Answer </button>--%>



                                   


                                </div>
                                <!--end::Col-->


                            </div>
                        </div>
                        <!--end::Col-->
                    </div>






                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class="card-footer d-flex py-6 px-9">
                    <%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

                    <asp:Button ID="btnSaveChanges" runat="server"  Text="Save" OnClick="btnSaveChanges_Click"    />
                </div>
                <!--end::Actions-->
                
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>


    







</asp:Content>
