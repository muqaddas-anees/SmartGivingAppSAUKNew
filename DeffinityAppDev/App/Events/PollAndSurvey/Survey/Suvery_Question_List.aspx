<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Suvery_Question_List.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Survey.Suvery_Question_List" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

    <style>
        .modalBackground {
            overflow : scroll;
           
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
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Suvery   </h3>
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->
        <!--begin::Content-->
        <div id="kt_account_profile_details" class="collapse show" style="">
            <!--begin::Form-->
            <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
                <div >
                    <!--begin::Input group-->




                    <br />

                   
                   
					<br />
                    <br />

                  

					
                    




              







					
                    






                



                 




















                    <div class="row g-6">


                        <div class="card-footer d-flex justify-content-end py-6 px-9">


                               <%--  <asp:Button ID="Button2" runat="server"  Text="Add New Banner" ToolTip="Add new Banner" OnClick="btnUpload_Click" Style="margin-right: 20px;" />   <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                           

                                   
                                    </div>


                        <div class="col-lg-2">
                            

                            </div>

                         

                        <div class="col-lg-8">



                            <div class="card card-stretch card-bordered mb-5">

                                <div class="card-header">



                                    <div class="d-flex flex-column flex-row-fluid">
                                        <div class="d-flex flex-column-auto h-70px  flex-center">
                                            <span class="text-white">
                                                <h1>Survey   </h1>
                                            </span>
                                        </div>


                                    </div>




                                </div>



                                <div class="card-body">

                                    <div>


                                        <table style="width: 90%">

                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblQuestion" Font-Size="Large" runat="server" /><br />
                                                    <br />
                                                </th>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:RadioButton Text="" Checked="false" GroupName="Survey" runat="server" OnCheckedChanged="OptionSelected_CheckedChanged" AutoPostBack="true" ID="rbtnOption1"></asp:RadioButton>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton Text="" Checked="false" GroupName="Survey" runat="server" AutoPostBack="true" OnCheckedChanged="OptionSelected_CheckedChanged" ID="rbtnOption2"></asp:RadioButton>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton Text="" Checked="false" GroupName="Survey" runat="server" AutoPostBack="true" OnCheckedChanged="OptionSelected_CheckedChanged" ID="rbtnOption3"></asp:RadioButton>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton Text="" Checked="false" GroupName="Survey" runat="server" AutoPostBack="true" OnCheckedChanged="OptionSelected_CheckedChanged" ID="rbtnOption4"></asp:RadioButton>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <div class=" d-flex justify-content py-6 px-9">

                                                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" ToolTip="Previous" OnClick="PreviousButtonClick" Style="margin-right: 20px;" />



                                                    </div>
                                                </td>
                                                <td>
                                                    <div class=" d-flex justify-content-end py-6 px-9">

                                                        <asp:Button ID="btnFinesh" runat="server" Text="Finesh Survey" ToolTip="Finesh Survey" OnClick="SubmitButtonClick" />

                                                        <asp:Button ID="btnNext" runat="server" Text="Next" ToolTip="Next" OnClick="NextButtonClick" />
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>

                                                    <div class=" d-flex justify-content-end py-6 px-9">



                                                        
                                                    </div>

                                                </td>
                                            </tr>



                                        </table>


                                    </div>
















                                </div>




                            </div>












                        </div>


                        

                    </div>










                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class=" d-flex  py-6 px-9">
                    <%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

                    <%--<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnSave" OnClick="btnSaveChanges_Click" Text="Save Changes" />--%>
                </div>
                <!--end::Actions-->
              
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>

















    









     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script type="text/javascript">


      //  FirstFunction();


      


















     








      


    </script>









    <script type="text/javascript">

       





    </script>














</asp:Content>
