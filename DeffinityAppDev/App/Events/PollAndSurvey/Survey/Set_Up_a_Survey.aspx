<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Set_Up_a_Survey.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DeffinityAppDev.App.PollAndSurvey.Survey.Set_Up_a__Survey" %>
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
                <h3 class="fw-bolder m-0">Set up a Survey <asp:HiddenField ID="hunid" runat="server" /> </h3>
            </div>
            <div class="card-toolbar">
								
                             <asp:Button ID="btnBacktoSurvery" runat="server"  CssClass="btn btn-light" Text="Back to Survey" OnClick="btnBacktoSurvery_Click" />
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

                     <asp:Label ID="lblQuestion"  ForeColor="Red" runat="server" Text=""></asp:Label>

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
                    <asp:Label ID="lblDescription"  ForeColor="Red" runat="server" Text=""></asp:Label>

                    <!--begin::Input group-->
                    <div class="row mb-6">
                        <!--begin::Label-->
                       
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-12 fv-row fv-plugins-icon-container">
                                    
                                     <asp:TextBox ID="TextAreaDescription" runat="server" placeholder="Question Description"    TextMode="MultiLine" Rows="10"  ></asp:TextBox>
                                    
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>
                    <!--end::Input group-->








                    <br />
                    <br />
                    <br />


                     <label class="col-lg-2 col-form-label required fw-bold fs-6">Question Type:  </label>

                     <asp:Label ID="lblQuestionType"  ForeColor="Red" runat="server" Text=""></asp:Label>

                      <div class="row mb-6">
                        <!--begin::Label-->
                    
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-8 fv-row fv-plugins-icon-container">

                                    <asp:DropDownList ID="DropDownListQuestionType" OnSelectedIndexChanged="DropDownListQuestionType_SelectedIndexChanged" AutoPostBack="true"  runat="server" >
                                        <asp:ListItem Text="Please select..." Value=""></asp:ListItem>
                                        <asp:ListItem Text="Text" Value="Text"></asp:ListItem>
                                        <asp:ListItem Text="Detailed Answer" Value="Detailed Answer"></asp:ListItem>
                                       <%-- <asp:ListItem Text="Single Selection" Value="Single Selection"></asp:ListItem>--%>
                                        <asp:ListItem Text="Multiple Choice" Value="Multiple Choice"></asp:ListItem>
                                       
                                    </asp:DropDownList>

                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>




                      <br />
                    <br />
                    <br />




                      <div class="row mb-6">


                           <%-- <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-14 fv-row fv-plugins-icon-container">
                                   
                                      <asp:RadioButton GroupName="Option" Checked="true" Text="Multiple Choice" ID="rbtnMultipleChoise"   OnCheckedChanged="rbtnMultipleChoise_CheckedChanged"   runat="server"  BorderColor="White"  BackColor="White" BorderWidth="20"  />
                                  
                                     <asp:RadioButton GroupName="Option" Text="Single Option" ID="rbtnSingleOption" runat="server"  BorderColor="White"  BackColor="White" BorderWidth="20"  />
                                    
                                     <asp:RadioButton GroupName="Option" Text="Text" ID="rbtnText" runat="server"  BorderColor="White"  BackColor="White" BorderWidth="20"  />
                                   
                                     <asp:RadioButton GroupName="Option" Text="Detailed Answer" ID="rbtnDetailedAnswer" runat="server"  BorderColor="White"  BackColor="White" BorderWidth="20"  />
                                   
                                </div>
                                <!--end::Col-->

                            </div>--%>


                      
                        <div class="col-lg-6">
                          
                        </div>



                        <!--end::Col-->
                    </div>



                    <div  runat="server"  id="DisplayMultipleChoise"     >

                    
                   


                      <div class="row mb-6"   >
                      
                    

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

                       
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->

                                <asp:Label ID="lblTexboxReminder" ForeColor="Red" runat="server" Text=""></asp:Label>

                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                   <%-- <asp:TextBox ID="txtMultipleChoice1" runat="server"   placeholder="Choice 1"      ></asp:TextBox>   <br />
                                    <asp:TextBox ID="txtMultipleChoice2" runat="server"   placeholder="Choice 2"      ></asp:TextBox>   <br />
                                    <asp:TextBox ID="txtMultipleChoice3" runat="server"   placeholder="Choice 3"      ></asp:TextBox>  <br />
                                    <asp:TextBox ID="txtMultipleChoice4" runat="server"   placeholder="Choice 4"        ></asp:TextBox>
                                   

                                      <br />
                                <br />
                                <br />
                                <br />--%>


                                       <asp:Button ID="btnAddanAnswer" runat="server" Text="Add an Answer" OnClick="btnAddanAnswer_Click"   Width="300" Visible="false"  />


                                </div>
                                <!--end::Col-->


                              
                                 

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>




                   

                    <br />
                    <br />
                    <br />


                    <div  runat="server"  id="DisplayChoise"   >

                         <div class="row mb-6"     >
                      
                        <div class="col-lg-14">
                            <div class="row">
                                <!--begin::Col-->

                                <div class="col-lg-5 fv-row fv-plugins-icon-container">
                                    <h1>
                                        Select The Answer Choise
                                    </h1>

                                 
                                     

                                   
                                   <asp:Label ID="lblSelectChoiseReminder"  ForeColor="Red" runat="server" Text=""></asp:Label>
                                   <br />
                                      <asp:RadioButtonList ID="rdList" runat="server" CssClass="mycheckBig"></asp:RadioButtonList>
                                  <%--  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton GroupName="Choice" Text="" ID="RadioButtonChoice1" runat="server"  BorderColor="White"  BackColor="White" BorderWidth="20"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton GroupName="Choice" Text="" ID="RadioButtonChoice2" runat="server" BorderColor="White"  BackColor="White" BorderWidth="20" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                    <asp:RadioButton GroupName="Choice" Text="" ID="RadioButtonChoice3" runat="server" BorderColor="White"  BackColor="White" BorderWidth="20" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton GroupName="Choice" Text="" ID="RadioButtonChoice4" runat="server" BorderColor="White"  BackColor="White" BorderWidth="20" />
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   --%>


                                
                                   


                                </div>
                                <!--end::Col-->


                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                    </div>

                   

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

                      <div  runat="server"  id="DisplaySingleChoise"     >



                            

                      </div>



                    <div  runat="server"  id="DisplayText">

                                                 

                      </div>


                    <div  runat="server"  id="DisplayDetailedAnswer">

                                                                                            

                      </div>





                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class="card-footer d-flex py-6 px-9">
                     <asp:Button ID="btnAddNext" runat="server"  Text="Add a Question" OnClick="btnAddNext_Click" style="margin-right:10px;padding-right:10px"    />
                    <asp:Button ID="btnSaveChanges" runat="server"  Text="Save" OnClick="btnSaveChanges_Click"    />
                </div>
                <!--end::Actions-->
                
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>





</asp:Content>
