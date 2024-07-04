<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ManageTickets.aspx.cs" Inherits="DeffinityAppDev.App.Events.ManageTickets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

  <%--    <script type="text/javascript">
          window.addEventListener('message', function (event) {
              var token = JSON.parse(event.data);
              var mytoken = document.getElementById('mytoken');
              mytoken.value = token.message;
              var txtCardConnectNumber = document.getElementById("<%=txtCardConnectNumber.ClientID%>");
             txtCardConnectNumber.value = token.message;
             // console.log(txtCardConnectNumber.value);
         }, false);

      </script>--%>

      <script type="text/javascript">
          window.addEventListener('message', function (event) {
              debugger;
              var token = JSON.parse(event.data);
              //alert(token.message);
              var mytoken = document.getElementById("<%=mytoken.ClientID%>");
           mytoken.value = token.message;
          
          // alert(mytoken.value);
           var txtCardConnectNumber = document.getElementById("<%=txtCardConnectNumber.ClientID%>");
           txtCardConnectNumber.value = token.message;
           // console.log(txtCardConnectNumber.value);
       }, false);

      </script>
       <div class="row gy-5 g-xl-8 mb-6" id="pnlResult" runat="server" visible="false">
        <div class="col-xxl-12">

             
            </div>
        </div>

     <div class="row gy-5 g-xl-8 mb-6" id="pnlPrice" runat="server">

       

        <div class="col-xxl-7">

             <div class="card card-xxl-stretch" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white">Booking </h3>
												<div class="card-toolbar">
                                                     <asp:HiddenField ID="hfixedamount" runat="server" Value="0" ClientIDMode="Static" />
                                                      <asp:HiddenField ID="hTotal" runat="server" ClientIDMode="Static" Value="0" />   
                                                                    <asp:HiddenField ID="hfeepercent" runat="server" ClientIDMode="Static" Value="0" />   
                                                                     <asp:HiddenField ID="hfee" runat="server" ClientIDMode="Static" Value="0" />   
                                                     <asp:HiddenField ID="hplatformfee" runat="server" ClientIDMode="Static" Value="0" /> 
<asp:HiddenField ID="hplatformfeepercent" runat="server" ClientIDMode="Static" Value="0" /> 

                                                   <asp:HiddenField ID="hunid" runat="server" Value="0" />
                                                      <asp:HiddenField ID="hamount" runat="server" Value="0" ClientIDMode="Static" />
                                                     <asp:HiddenField ID="hbook_unid" runat="server" Value="0" />
                                                    </div>

                                                <div class="card-body p-0" >
                                                    

                                                       
    <div class="row mb-6">
         <div class="col-lg-8">
                    <asp:Label ID="lblDescription" runat="server" Text="Booking" Font-Bold="true" Font-Size="28px"></asp:Label> <br />

                  </div>
         <div class="col-lg-4">
              <asp:TextBox ID="txtAmountTotal" runat="server" SkinID="Price" ClientIDMode="Static" Text="0.00" Font-Size="32px" Visible="false" ></asp:TextBox>
             </div>
        </div>
<div class="row mb-4">
                                                <hr />
    </div>
                                                    <div class="row mb-6">
         <div class="col-lg-12 ">
             <asp:Label ID="lblTitle" runat="server" Font-Size="24px" Font-Bold="true"></asp:Label>
             </div>
                                                        </div>

                                                  <asp:Panel ID="pnlList" runat="server">
                                                   <asp:ListView ID="listCategory" runat="server" InsertItemPosition="None" OnItemDataBound="listCategory_ItemDataBound">
 <LayoutTemplate>
              <div class="form-group row">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>

<ItemTemplate>
  <div class="row mb-4 bg-light-primary px-3 py-5 rounded-2 ">
      <div class="row">
              <div class="col-lg-10 pt-6">
                   <div class="row md-6">
                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("TypeOfTicket") %>' Font-Size="22px"></asp:Label> 
                       </div>
                    <div class="row md-12">
                        <div class="col-lg-4" style="font-size:15px"> <label>Price: </label>  <asp:Label ID="Label2" runat="server" Text=' <%# getPrice( Eval("Price").ToString()) %>' Font-Bold="true"></asp:Label> </div>
                         <div class="col-lg-8 d-flex justify-content-end" style="font-size:15px"> 
                             </div>
                 
                       </div>
                  <%--<div class="row md-6">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# GetDescription( Eval("Description")) %>'></asp:Label> 
                      </div>--%>
                  </div>
        <div class="col-lg-2 ">
             <div class="row mb-3">
              <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" Text="0" Font-Size="22px" Visible="false"></asp:TextBox>
             <input type="number" value="0" min="0" max="100" step="1" runat="server" id="txtamount_list" class="form-control form-control-lg form-control-solid txtamt" style="text-align:right;font-size:22px;height:30px"  onkeyup="sum_amt();"   /> 
      
           
            <input type="hidden" runat="server" id='hprice_ctrl' class="hprice" value='<%#Eval("Price") %>' /> 
            <asp:LinkButton ID="btn" runat="server" ClientIDMode="Static" Text="+" SkinID="BtnLinkButton" OnClientClick="javascript:return  showdetails(this);" Font-Bold="true" Font-Size="32px" value='<%# Eval("ID") %>' Visible="false"></asp:LinkButton>
              <input type="hidden" id='h_<%# Eval("ID").ToString() %>' value="1" />     
                 </div>
            <div class="row">
                <asp:Label ID="lblAvilable" runat="server" Text='<%# Eval("RemainingSlotsText") %>'></asp:Label>
            </div>

        </div>
          </div>
        <div class="row md-12 d-flex justify-content-end">
                        <div class="col-lg-12 d-flex justify-content-end" style="font-size:15px">  <asp:RangeValidator ID="rangeValidator" runat="server"
    ControlToValidate="txtamount_list"
    Type="Integer" 
    MinimumValue="0" 
    MaximumValue="100" 
    ErrorMessage="Please enter a number between 1 and 100."
    
    Display="Dynamic" 
    ForeColor="Red" Font-Size="Small" ValidationGroup="gp1" >
</asp:RangeValidator> </div>
                      </div>
      </div>
    <div class="row mb-4" style="display:none;" id='<%# Eval("ID").ToString() %>'>
         <div class="col-lg-8">
                  
                  </div>
         <div class="col-lg-4">
           
             </div>
        </div>

</ItemTemplate>
</asp:ListView>
                                                        <div class="row mb-6 ms-0">
                                                                <div class="col-lg-12 fv-row fv-plugins-icon-container text-center">
                                                                    <asp:Button ID="btnAddContacts" runat="server" OnClick="btnAddContacts_Click" SkinID="btnDefault" Text="Add Contact Details"  Font-Size="20px" Width="60%" Height="80px" ValidationGroup="gp1"  /> 
                                                                    </div>

                                                              </div>
                                                      </asp:Panel>

                                                      <asp:Panel ID="pnlContactDetails" runat="server" Visible="false">


                                                          <div class="row mb-6">
                                                          <h3 class="card-title fw-bolder " style="font-size:22px">Contact Details </h3>
                                                              <hr />
                                                              </div>


                                                            <div class="row mb-6 ms-0">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Name </h5>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                    <asp:TextBox ID="txtName" runat="server"  Style="width:350px"  ></asp:TextBox>
                                                </div>

                                              
                                            </div>
                                                             <div class="row mb-6 ms-0">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Email </h5>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                    <asp:TextBox ID="txtemail" runat="server"  Style="width:350px"  ></asp:TextBox>
                                                </div>

                                              
                                            </div>
                                                             <div class="row mb-6 ms-0">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Phone </h5>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                    <asp:TextBox ID="txtphone" runat="server"  Style="width:350px"  ></asp:TextBox>
                                                </div>

                                              
                                            </div>

                                                        
                                                          
                                                          </asp:Panel>


                                                     <asp:Panel ID="pnlAddCustomerDetails" runat="server" Visible="false">
                                                          <div  id="pnluser"  >
                                                         <div class="row mb-6">
                                                        <asp:Label ID="Label4" runat="server" Text="Add Contact Details" Font-Bold="true" Font-Size="22px" style="padding-bottom:10px"></asp:Label>
                                                             
                                                               <hr  />
                                                    </div>
                                                              </div>


                                                         <asp:ListView ID="listUsers" runat="server">
                                                              <LayoutTemplate>
             
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
               
                  
              </LayoutTemplate>
                                                             <ItemTemplate>
                                                                <div class="row mb-6 border-light p-3" style="border-width:2px;border-style:solid;">

                                                                     <div class="col-lg-12">
                                                                          <label for="email">Ticket type:</label>
                                                                           <asp:Label Visible="false" ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                                                                              <asp:TextBox ID="txttickettype" runat="server" Text='<%# Eval("TicketType") %>' ReadOnly="true" ></asp:TextBox>
                                                                     </div>
                                                                      <div class="col-lg-12">
                                                                           <label for="email">First name:</label>
                                                                           <asp:TextBox ID="txtuser" runat="server" Text='<%# Eval("FirstName") %>' MaxLength="200"></asp:TextBox>
                                                                          <asp:RequiredFieldValidator ID="rfuser" runat="server" 
                            ControlToValidate="txtuser" Display="Dynamic" ErrorMessage="Please enter first name" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                     <div class="col-lg-12">
                                                                           <label for="email">Last name:</label>
                                                                           <asp:TextBox ID="txtlastname" runat="server" Text='<%# Eval("LastName") %>'  MaxLength="200"></asp:TextBox>
                                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtlastname" Display="Dynamic" ErrorMessage="Please enter last name" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                      <div class="col-lg-12">
                                                                           <label for="email">Email:</label>
                                                                           <asp:TextBox ID="txtuseremail" runat="server"  Text='<%# Eval("UserEmail") %>'  MaxLength="250"></asp:TextBox>
                                                                           <asp:RequiredFieldValidator ID="rfemail" runat="server" 
                            ControlToValidate="txtuseremail" Display="Dynamic" ErrorMessage="Please enter email" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                      <div class="col-lg-12">
                                                                           <label for="email">Cell number:</label>
                                                                           <asp:TextBox ID="txtusercontact" runat="server"  Text='<%# Eval("UserContact") %>'  MaxLength="100"></asp:TextBox>
                                                                          <asp:RequiredFieldValidator ID="rfcell" runat="server" 
                            ControlToValidate="txtusercontact" Display="Dynamic" ErrorMessage="Please enter cell number" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                     <div class="col-lg-12">
                                                                           <label for="State">Country:</label>
                                                                          <asp:DropDownList ID="ddlcountry" runat="server">
														
														
                <asp:ListItem Value="Afghanistan">Afghanistan</asp:ListItem>
                <asp:ListItem Value="Åland Islands">Åland Islands</asp:ListItem>
                <asp:ListItem Value="Albania">Albania</asp:ListItem>
                <asp:ListItem Value="Algeria">Algeria</asp:ListItem>
                <asp:ListItem Value="American Samoa">American Samoa</asp:ListItem>
                <asp:ListItem Value="Andorra">Andorra</asp:ListItem>
                <asp:ListItem Value="Angola">Angola</asp:ListItem>
                <asp:ListItem Value="Anguilla">Anguilla</asp:ListItem>
                <asp:ListItem Value="Antarctica">Antarctica</asp:ListItem>
                <asp:ListItem Value="Antigua and Barbuda">Antigua and Barbuda</asp:ListItem>
                <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                <asp:ListItem Value="Aruba">Aruba</asp:ListItem>
                <asp:ListItem Value="Australia">Australia</asp:ListItem>
                <asp:ListItem Value="Austria">Austria</asp:ListItem>
                <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                <asp:ListItem Value="Belize">Belize</asp:ListItem>
                <asp:ListItem Value="Benin">Benin</asp:ListItem>
                <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
                <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                <asp:ListItem Value="Bosnia and Herzegovina">Bosnia and Herzegovina</asp:ListItem>
                <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                <asp:ListItem Value="Bouvet Island">Bouvet Island</asp:ListItem>
                <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                <asp:ListItem Value="British Indian Ocean Territory">British Indian Ocean Territory</asp:ListItem>
                <asp:ListItem Value="Brunei Darussalam">Brunei Darussalam</asp:ListItem>
                <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                <asp:ListItem Value="Canada">Canada</asp:ListItem>
                <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
                <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                <asp:ListItem Value="Chad">Chad</asp:ListItem>
                <asp:ListItem Value="Chile">Chile</asp:ListItem>
                <asp:ListItem Value="China">China</asp:ListItem>
                <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
                <asp:ListItem Value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</asp:ListItem>
                <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
                <asp:ListItem Value="Congo">Congo</asp:ListItem>
                <asp:ListItem Value="Congo, The Democratic Republic of The">Congo, The Democratic Republic of The</asp:ListItem>
                <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                <asp:ListItem Value="Cote D'ivoire">Cote D'ivoire</asp:ListItem>
                <asp:ListItem Value="Croatia">Croatia</asp:ListItem>
                <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
                <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
                <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
                <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                <asp:ListItem Value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</asp:ListItem>
                <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
                <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                <asp:ListItem Value="Finland">Finland</asp:ListItem>
                <asp:ListItem Value="France">France</asp:ListItem>
                <asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
                <asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
                <asp:ListItem Value="French Southern Territories">French Southern Territories</asp:ListItem>
                <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                <asp:ListItem Value="Germany">Germany</asp:ListItem>
                <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                <asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
                <asp:ListItem Value="Greece">Greece</asp:ListItem>
                <asp:ListItem Value="Greenland">Greenland</asp:ListItem>
                <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                <asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
                <asp:ListItem Value="Guam">Guam</asp:ListItem>
                <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                <asp:ListItem Value="Guernsey">Guernsey</asp:ListItem>
                <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                <asp:ListItem Value="Guinea-bissau">Guinea-bissau</asp:ListItem>
                <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                <asp:ListItem Value="Heard Island and Mcdonald Islands">Heard Island and Mcdonald Islands</asp:ListItem>
                <asp:ListItem Value="Holy See (Vatican City State)">Holy See (Vatican City State)</asp:ListItem>
                <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                <asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
                <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                <asp:ListItem Value="Iceland">Iceland</asp:ListItem>
                <asp:ListItem Value="India">India</asp:ListItem>
                <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                <asp:ListItem Value="Iran, Islamic Republic of">Iran, Islamic Republic of</asp:ListItem>
                <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                <asp:ListItem Value="Isle of Man">Isle of Man</asp:ListItem>
                <asp:ListItem Value="Israel">Israel</asp:ListItem>
                <asp:ListItem Value="Italy">Italy</asp:ListItem>
                <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                <asp:ListItem Value="Japan">Japan</asp:ListItem>
                <asp:ListItem Value="Jersey">Jersey</asp:ListItem>
                <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                <asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
                <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                <asp:ListItem Value="Korea, Democratic People's Republic of">Korea, Democratic People's Republic of</asp:ListItem>
                <asp:ListItem Value="Korea, Republic of">Korea, Republic of</asp:ListItem>
                <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                <asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
                <asp:ListItem Value="Lao People's Democratic Republic">Lao People's Democratic Republic</asp:ListItem>
                <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                <asp:ListItem Value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</asp:ListItem>
                <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                <asp:ListItem Value="Macao">Macao</asp:ListItem>
                <asp:ListItem Value="Macedonia, The Former Yugoslav Republic of">Macedonia, The Former Yugoslav Republic of</asp:ListItem>
                <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                <asp:ListItem Value="Mali">Mali</asp:ListItem>
                <asp:ListItem Value="Malta">Malta</asp:ListItem>
                <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                <asp:ListItem Value="Martinique">Martinique</asp:ListItem>
                <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                <asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
                <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                <asp:ListItem Value="Micronesia, Federated States of">Micronesia, Federated States of</asp:ListItem>
                <asp:ListItem Value="Moldova, Republic of">Moldova, Republic of</asp:ListItem>
                <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                <asp:ListItem Value="Montenegro">Montenegro</asp:ListItem>
                <asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
                <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                <asp:ListItem Value="Namibia">Namibia</asp:ListItem>
                <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                <asp:ListItem Value="Netherlands">Netherlands</asp:ListItem>
                <asp:ListItem Value="Netherlands Antilles">Netherlands Antilles</asp:ListItem>
                <asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
                <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                <asp:ListItem Value="Niger">Niger</asp:ListItem>
                <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                <asp:ListItem Value="Niue">Niue</asp:ListItem>
                <asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
                <asp:ListItem Value="Northern Mariana Islands">Northern Mariana Islands</asp:ListItem>
                <asp:ListItem Value="Norway">Norway</asp:ListItem>
                <asp:ListItem Value="Oman">Oman</asp:ListItem>
                <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                <asp:ListItem Value="Palau">Palau</asp:ListItem>
                <asp:ListItem Value="Palestinian Territory, Occupied">Palestinian Territory, Occupied</asp:ListItem>
                <asp:ListItem Value="Panama">Panama</asp:ListItem>
                <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                <asp:ListItem Value="Peru">Peru</asp:ListItem>
                <asp:ListItem Value="Philippines">Philippines</asp:ListItem>
                <asp:ListItem Value="Pitcairn">Pitcairn</asp:ListItem>
                <asp:ListItem Value="Poland">Poland</asp:ListItem>
                <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                <asp:ListItem Value="Reunion">Reunion</asp:ListItem>
                <asp:ListItem Value="Romania">Romania</asp:ListItem>
                <asp:ListItem Value="Russian Federation">Russian Federation</asp:ListItem>
                <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                <asp:ListItem Value="Saint Helena">Saint Helena</asp:ListItem>
                <asp:ListItem Value="Saint Kitts and Nevis">Saint Kitts and Nevis</asp:ListItem>
                <asp:ListItem Value="Saint Lucia">Saint Lucia</asp:ListItem>
                <asp:ListItem Value="Saint Pierre and Miquelon">Saint Pierre and Miquelon</asp:ListItem>
                <asp:ListItem Value="Saint Vincent and The Grenadines">Saint Vincent and The Grenadines</asp:ListItem>
                <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                <asp:ListItem Value="Sao Tome and Principe">Sao Tome and Principe</asp:ListItem>
                <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                <asp:ListItem Value="Serbia">Serbia</asp:ListItem>
                <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                <asp:ListItem Value="Slovakia">Slovakia</asp:ListItem>
                <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                <asp:ListItem Value="South Africa" selected="True" >South Africa</asp:ListItem>
                <asp:ListItem Value="South Georgia and The South Sandwich Islands">South Georgia and The South Sandwich Islands</asp:ListItem>
                <asp:ListItem Value="Spain">Spain</asp:ListItem>
                <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                <asp:ListItem Value="Suriname">Suriname</asp:ListItem>
                <asp:ListItem Value="Svalbard and Jan Mayen">Svalbard and Jan Mayen</asp:ListItem>
                <asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
                <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                <asp:ListItem Value="Syrian Arab Republic">Syrian Arab Republic</asp:ListItem>
                <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                <asp:ListItem Value="Tanzania, United Republic of">Tanzania, United Republic of</asp:ListItem>
                <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                <asp:ListItem Value="Timor-leste">Timor-leste</asp:ListItem>
                <asp:ListItem Value="Togo">Togo</asp:ListItem>
                <asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
                <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                <asp:ListItem Value="Trinidad and Tobago">Trinidad and Tobago</asp:ListItem>
                <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                <asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
                <asp:ListItem Value="Turks and Caicos Islands">Turks and Caicos Islands</asp:ListItem>
                <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                <asp:ListItem Value="United Arab Emirates">United Arab Emirates</asp:ListItem>
                <asp:ListItem Value="United Kingdom"  >United Kingdom</asp:ListItem>
                <asp:ListItem Value="USA">USA</asp:ListItem>
                <asp:ListItem Value="Uruguay">Uruguay</asp:ListItem>
                <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                <asp:ListItem Value="Viet Nam">Viet Nam</asp:ListItem>
                <asp:ListItem Value="Virgin Islands, British">Virgin Islands, British</asp:ListItem>
                <asp:ListItem Value="Virgin Islands, U.S.">Virgin Islands, U.S.</asp:ListItem>
                <asp:ListItem Value="Wallis and Futuna">Wallis and Futuna</asp:ListItem>
                <asp:ListItem Value="Western Sahara">Western Sahara</asp:ListItem>
                <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
													
													</asp:DropDownList>
                                                                           <label for="State" class="cls_state">State:</label>
                                                                         <asp:TextBox ID="txtState" runat="server" Text='<%# Eval("State") %>' ></asp:TextBox>
                                                                     </div>
                                                                     <div class="col-lg-12">
                                                                           <label for="email">Company:</label>
                                                                          <asp:TextBox ID="txtCompany" runat="server"  Text='<%# Eval("Company") %>'  MaxLength="250"></asp:TextBox>
                                                                         </div>
                                                                     </div>
                                                                   
                                                             </ItemTemplate>
                                                             
                                                             <AlternatingItemTemplate>
                                                                  <div class="row mb-6 border-light p-3" style="border-width:2px;border-style:solid;">
                                                                     <div class="col-lg-12">
                                                                          <label for="email">Ticket type:</label>
                                                                           <asp:Label Visible="false" ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                                                                              <asp:TextBox ID="txttickettype" runat="server" Text='<%# Eval("TicketType") %>' ReadOnly="true" ></asp:TextBox>
                                                                     </div>
                                                                       <div class="col-lg-12">
                                                                           <label for="email">First Name:</label>
                                                                           <asp:TextBox ID="txtuser" runat="server" Text='<%# Eval("FirstName") %>'  MaxLength="250"></asp:TextBox>
                                                                          <asp:RequiredFieldValidator ID="rfuser" runat="server" 
                            ControlToValidate="txtuser" Display="Dynamic" ErrorMessage="Please enter first name" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                      <div class="col-lg-12">
                                                                           <label for="email">Last Name:</label>
                                                                           <asp:TextBox ID="txtlastname" runat="server" Text='<%# Eval("LastName") %>'  MaxLength="250"></asp:TextBox>
                                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="txtlastname" Display="Dynamic" ErrorMessage="Please enter last name" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                      <div class="col-lg-12">
                                                                           <label for="email">Email:</label>
                                                                           <asp:TextBox ID="txtuseremail" runat="server"  Text='<%# Eval("UserEmail") %>'  MaxLength="250"></asp:TextBox>
                                                                           <asp:RequiredFieldValidator ID="rfemail" runat="server" 
                            ControlToValidate="txtuseremail" Display="Dynamic" ErrorMessage="Please enter email" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                      <div class="col-lg-12">
                                                                           <label for="email">Cell Number:</label>
                                                                           <asp:TextBox ID="txtusercontact" runat="server"  Text='<%# Eval("UserContact") %>'  MaxLength="100"></asp:TextBox>
                                                                          <asp:RequiredFieldValidator ID="rfcell" runat="server" 
                            ControlToValidate="txtusercontact" Display="Dynamic" ErrorMessage="Please enter cell number" ValidationGroup="signup"></asp:RequiredFieldValidator>
                                                                     </div>
                                                                     <div class="col-lg-12">
                                                                            <label for="State">Country:</label>
                                                                          <asp:DropDownList ID="ddlcountry" runat="server">
														
														 <asp:ListItem Value="Afghanistan">Afghanistan</asp:ListItem>
                <asp:ListItem Value="Åland Islands">Åland Islands</asp:ListItem>
                <asp:ListItem Value="Albania">Albania</asp:ListItem>
                <asp:ListItem Value="Algeria">Algeria</asp:ListItem>
                <asp:ListItem Value="American Samoa">American Samoa</asp:ListItem>
                <asp:ListItem Value="Andorra">Andorra</asp:ListItem>
                <asp:ListItem Value="Angola">Angola</asp:ListItem>
                <asp:ListItem Value="Anguilla">Anguilla</asp:ListItem>
                <asp:ListItem Value="Antarctica">Antarctica</asp:ListItem>
                <asp:ListItem Value="Antigua and Barbuda">Antigua and Barbuda</asp:ListItem>
                <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                <asp:ListItem Value="Aruba">Aruba</asp:ListItem>
                <asp:ListItem Value="Australia">Australia</asp:ListItem>
                <asp:ListItem Value="Austria">Austria</asp:ListItem>
                <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                <asp:ListItem Value="Belize">Belize</asp:ListItem>
                <asp:ListItem Value="Benin">Benin</asp:ListItem>
                <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
                <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                <asp:ListItem Value="Bosnia and Herzegovina">Bosnia and Herzegovina</asp:ListItem>
                <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                <asp:ListItem Value="Bouvet Island">Bouvet Island</asp:ListItem>
                <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                <asp:ListItem Value="British Indian Ocean Territory">British Indian Ocean Territory</asp:ListItem>
                <asp:ListItem Value="Brunei Darussalam">Brunei Darussalam</asp:ListItem>
                <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                <asp:ListItem Value="Canada">Canada</asp:ListItem>
                <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
                <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                <asp:ListItem Value="Chad">Chad</asp:ListItem>
                <asp:ListItem Value="Chile">Chile</asp:ListItem>
                <asp:ListItem Value="China">China</asp:ListItem>
                <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
                <asp:ListItem Value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</asp:ListItem>
                <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
                <asp:ListItem Value="Congo">Congo</asp:ListItem>
                <asp:ListItem Value="Congo, The Democratic Republic of The">Congo, The Democratic Republic of The</asp:ListItem>
                <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                <asp:ListItem Value="Cote D'ivoire">Cote D'ivoire</asp:ListItem>
                <asp:ListItem Value="Croatia">Croatia</asp:ListItem>
                <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
                <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
                <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
                <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                <asp:ListItem Value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</asp:ListItem>
                <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
                <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                <asp:ListItem Value="Finland">Finland</asp:ListItem>
                <asp:ListItem Value="France">France</asp:ListItem>
                <asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
                <asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
                <asp:ListItem Value="French Southern Territories">French Southern Territories</asp:ListItem>
                <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                <asp:ListItem Value="Germany">Germany</asp:ListItem>
                <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                <asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
                <asp:ListItem Value="Greece">Greece</asp:ListItem>
                <asp:ListItem Value="Greenland">Greenland</asp:ListItem>
                <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                <asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
                <asp:ListItem Value="Guam">Guam</asp:ListItem>
                <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                <asp:ListItem Value="Guernsey">Guernsey</asp:ListItem>
                <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                <asp:ListItem Value="Guinea-bissau">Guinea-bissau</asp:ListItem>
                <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                <asp:ListItem Value="Heard Island and Mcdonald Islands">Heard Island and Mcdonald Islands</asp:ListItem>
                <asp:ListItem Value="Holy See (Vatican City State)">Holy See (Vatican City State)</asp:ListItem>
                <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                <asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
                <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                <asp:ListItem Value="Iceland">Iceland</asp:ListItem>
                <asp:ListItem Value="India">India</asp:ListItem>
                <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                <asp:ListItem Value="Iran, Islamic Republic of">Iran, Islamic Republic of</asp:ListItem>
                <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                <asp:ListItem Value="Isle of Man">Isle of Man</asp:ListItem>
                <asp:ListItem Value="Israel">Israel</asp:ListItem>
                <asp:ListItem Value="Italy">Italy</asp:ListItem>
                <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                <asp:ListItem Value="Japan">Japan</asp:ListItem>
                <asp:ListItem Value="Jersey">Jersey</asp:ListItem>
                <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                <asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
                <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                <asp:ListItem Value="Korea, Democratic People's Republic of">Korea, Democratic People's Republic of</asp:ListItem>
                <asp:ListItem Value="Korea, Republic of">Korea, Republic of</asp:ListItem>
                <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                <asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
                <asp:ListItem Value="Lao People's Democratic Republic">Lao People's Democratic Republic</asp:ListItem>
                <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                <asp:ListItem Value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</asp:ListItem>
                <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                <asp:ListItem Value="Macao">Macao</asp:ListItem>
                <asp:ListItem Value="Macedonia, The Former Yugoslav Republic of">Macedonia, The Former Yugoslav Republic of</asp:ListItem>
                <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                <asp:ListItem Value="Mali">Mali</asp:ListItem>
                <asp:ListItem Value="Malta">Malta</asp:ListItem>
                <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                <asp:ListItem Value="Martinique">Martinique</asp:ListItem>
                <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                <asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
                <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                <asp:ListItem Value="Micronesia, Federated States of">Micronesia, Federated States of</asp:ListItem>
                <asp:ListItem Value="Moldova, Republic of">Moldova, Republic of</asp:ListItem>
                <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                <asp:ListItem Value="Montenegro">Montenegro</asp:ListItem>
                <asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
                <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                <asp:ListItem Value="Namibia">Namibia</asp:ListItem>
                <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                <asp:ListItem Value="Netherlands">Netherlands</asp:ListItem>
                <asp:ListItem Value="Netherlands Antilles">Netherlands Antilles</asp:ListItem>
                <asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
                <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                <asp:ListItem Value="Niger">Niger</asp:ListItem>
                <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                <asp:ListItem Value="Niue">Niue</asp:ListItem>
                <asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
                <asp:ListItem Value="Northern Mariana Islands">Northern Mariana Islands</asp:ListItem>
                <asp:ListItem Value="Norway">Norway</asp:ListItem>
                <asp:ListItem Value="Oman">Oman</asp:ListItem>
                <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                <asp:ListItem Value="Palau">Palau</asp:ListItem>
                <asp:ListItem Value="Palestinian Territory, Occupied">Palestinian Territory, Occupied</asp:ListItem>
                <asp:ListItem Value="Panama">Panama</asp:ListItem>
                <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                <asp:ListItem Value="Peru">Peru</asp:ListItem>
                <asp:ListItem Value="Philippines">Philippines</asp:ListItem>
                <asp:ListItem Value="Pitcairn">Pitcairn</asp:ListItem>
                <asp:ListItem Value="Poland">Poland</asp:ListItem>
                <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                <asp:ListItem Value="Reunion">Reunion</asp:ListItem>
                <asp:ListItem Value="Romania">Romania</asp:ListItem>
                <asp:ListItem Value="Russian Federation">Russian Federation</asp:ListItem>
                <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                <asp:ListItem Value="Saint Helena">Saint Helena</asp:ListItem>
                <asp:ListItem Value="Saint Kitts and Nevis">Saint Kitts and Nevis</asp:ListItem>
                <asp:ListItem Value="Saint Lucia">Saint Lucia</asp:ListItem>
                <asp:ListItem Value="Saint Pierre and Miquelon">Saint Pierre and Miquelon</asp:ListItem>
                <asp:ListItem Value="Saint Vincent and The Grenadines">Saint Vincent and The Grenadines</asp:ListItem>
                <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                <asp:ListItem Value="Sao Tome and Principe">Sao Tome and Principe</asp:ListItem>
                <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                <asp:ListItem Value="Serbia">Serbia</asp:ListItem>
                <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                <asp:ListItem Value="Slovakia">Slovakia</asp:ListItem>
                <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                <asp:ListItem Value="South Georgia and The South Sandwich Islands">South Georgia and The South Sandwich Islands</asp:ListItem>
                <asp:ListItem Value="Spain">Spain</asp:ListItem>
                <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                <asp:ListItem Value="Suriname">Suriname</asp:ListItem>
                <asp:ListItem Value="Svalbard and Jan Mayen">Svalbard and Jan Mayen</asp:ListItem>
                <asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
                <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                <asp:ListItem Value="Syrian Arab Republic">Syrian Arab Republic</asp:ListItem>
                <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                <asp:ListItem Value="Tanzania, United Republic of">Tanzania, United Republic of</asp:ListItem>
                <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                <asp:ListItem Value="Timor-leste">Timor-leste</asp:ListItem>
                <asp:ListItem Value="Togo">Togo</asp:ListItem>
                <asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
                <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                <asp:ListItem Value="Trinidad and Tobago">Trinidad and Tobago</asp:ListItem>
                <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                <asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
                <asp:ListItem Value="Turks and Caicos Islands">Turks and Caicos Islands</asp:ListItem>
                <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                <asp:ListItem Value="United Arab Emirates">United Arab Emirates</asp:ListItem>
                <asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
                <asp:ListItem Value="USA" selected="True" >USA</asp:ListItem>
                <asp:ListItem Value="Uruguay">Uruguay</asp:ListItem>
                <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                <asp:ListItem Value="Viet Nam">Viet Nam</asp:ListItem>
                <asp:ListItem Value="Virgin Islands, British">Virgin Islands, British</asp:ListItem>
                <asp:ListItem Value="Virgin Islands, U.S.">Virgin Islands, U.S.</asp:ListItem>
                <asp:ListItem Value="Wallis and Futuna">Wallis and Futuna</asp:ListItem>
                <asp:ListItem Value="Western Sahara">Western Sahara</asp:ListItem>
                <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
  
													
													</asp:DropDownList>
                                                                           <label for="State" class="cls_state">State:</label>
                                                                          <asp:TextBox ID="txtState" runat="server" Text='<%# Eval("State") %>'></asp:TextBox>
														
                                                                     </div>
                                                                     <div class="col-lg-12">
                                                                           <label for="email">Company:</label>
                                                                          <asp:TextBox ID="txtCompany" runat="server"  Text='<%# Eval("Company") %>'  MaxLength="250"></asp:TextBox>
                                                                         </div>
                                                                     </div>
                                                                   
                                                             </AlternatingItemTemplate>
                                                         </asp:ListView>

                                                        <%-- <asp:GridView ID="gridusers" runat="server">
                                                             <Columns>
                                                                   <asp:TemplateField HeaderText="Ticket Type">
                                                                     <ItemTemplate>
                                                                         <asp:Label Visible="false" ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                                                                         <asp:TextBox ID="txttickettype" runat="server" Text='<%# Eval("TicketType") %>' ReadOnly="true" ></asp:TextBox>
                                                                     </ItemTemplate>
                                                                     
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Name">
                                                                     <ItemTemplate>

                                                                         <asp:TextBox ID="txtuser" runat="server" Text='<%# Eval("UserName") %>'></asp:TextBox>
                                                                     </ItemTemplate>
                                                                     
                                                                 </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Email">
                                                                     <ItemTemplate>

                                                                         <asp:TextBox ID="txtuseremail" runat="server"  Text='<%# Eval("UserEmail") %>'></asp:TextBox>
                                                                     </ItemTemplate>
                                                                     
                                                                 </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Cell Number">
                                                                     <ItemTemplate>

                                                                         <asp:TextBox ID="txtusercontact" runat="server"  Text='<%# Eval("UserContact") %>'></asp:TextBox>
                                                                     </ItemTemplate>
                                                                     
                                                                 </asp:TemplateField>
                                                                <%--  <asp:TemplateField HeaderText="Notes">
                                                                     <ItemTemplate>

                                                                         <asp:TextBox ID="txtnotes" runat="server"  Text='<%# Eval("AdminNotes") %>'></asp:TextBox>
                                                                     </ItemTemplate>
                                                                     
                                                                 </asp:TemplateField>

                                                             </Columns>
                                                         </asp:GridView>--%>
                                                              <div class="row mb-6 ms-0">
                                                                <div class="col-lg-12 fv-row fv-plugins-icon-container text-center">
                                                                    <asp:Button ID="btnSaveUsers" runat="server" OnClick="btnSaveUsers_Click" SkinID="btnDefault" Text="Save & Proceed" Font-Size="20px" Width="60%" Height="80px" ValidationGroup="signup" /> 
                                                                    </div>

                                                              </div>
                                                         </asp:Panel>


                                                      <asp:Panel ID="pnlPaymentDetails" runat="server" Visible="false">
                                                           <div  id="pnlPdetails">
                                                         <div class="row mb-6">
                                                        <asp:Label ID="Label5" runat="server" Text="Payment Details" Font-Bold="true" Font-Size="22px" style="padding-bottom:10px"></asp:Label>
                                                             
                                                               <hr  />
                                                    </div>
                                                               </div>


                                                       <asp:Panel ID="pnlnewCard" runat="server"  ClientIDMode="Static" Visible="false">

    <div class="row mb-6">
                                                        <asp:Label ID="Label3" runat="server" Text=" " Font-Bold="false" Font-Size="20px" ></asp:Label>
      
                                                    </div>
                     <div class="form-group row  mb-6" >
 <label class="col-lg-4 control-label">Card Type</label>
           <div class="col-lg-8">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_80">
                            <asp:ListItem></asp:ListItem>
                             <asp:ListItem Value="MASTERCARD" Text="MASTERCARD"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="VISA" Text="VISA"></asp:ListItem>
                   <asp:ListItem Value="DISCOVER" Text="DISCOVER"></asp:ListItem>
                    <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlCardType" Display="Dynamic" CssClass="error-text" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </div>
	
</div>
<div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Card Number</label>
           <div class="col-lg-8">
               <div id="pnlCreditCard" runat="server" visible="false">
                  
                   <asp:TextBox ID="txtCardNumber" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_90" MaxLength="20" ClientIDMode="Static"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rfCardnumber" runat="server" 
                            ControlToValidate="txtCardNumber" Display="Dynamic"  ErrorMessage="Please enter Card Number"></asp:RequiredFieldValidator>
                   </div>
               <div id="pnlCardConnect" runat="server" style="height:50px">
                    <asp:TextBox ID="txtCardConnectNumber" ClientIDMode="Static" runat="server" CssClass="paymentinfo-text" SkinID="txt_80"></asp:TextBox>
                   <div name="tokenform" id="tokenform">
                  <%-- <iframe id="tokenFrame" name="tokenFrame"  
                       src="https://fts.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="300" height="35" runat="server" ></iframe>--%>
                       <asp:HiddenField ID="mytoken" runat="server" ClientIDMode="Static" />
                       </div>
                
                   </div>
               <p>e.g: 4111222233334444</p>
            </div>
	
</div>
                    
    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Name </label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_80" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="None" ErrorMessage="Please enter name on card" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	
</div>
                    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Expiration</label>
           <div class="col-lg-8 d-flex d-inline">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" SkinID="ddl_125px">
                        </asp:DropDownList> &nbsp; &nbsp; &nbsp;
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  SkinID="ddl_125px">
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlMonth" Display="None"  
                            ErrorMessage="Please select month" ValidationGroup="p"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlYear" Display="None" 
                            ErrorMessage="Please select year" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	
</div>
                    <div class="form-group row  mb-6">
         
 <label class="col-lg-4 control-label">Card Security Code</label>
           <div class="col-lg-8">
                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px" MaxLength="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="None" 
                            ErrorMessage="Please enter CVV" ValidationGroup="p" ></asp:RequiredFieldValidator>
                        
               <p>e.g: 123 </p>
            </div>
	
</div>
                      <div class="card-footer text-center py-6 px-9">
                <asp:Button  ID="btnClose"  runat="server" CssClass="btn btn-light" Text="Back" OnClientClick="" Height="80px" Visible="false" /> 

              
               <div class="row d-flex justify-content-center">

                 <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Card & Pay Now" OnClick="btnSaveRegion_Click" Height="80px" Visible="false" />

                          </div>
            </div>

</asp:Panel>

                                                           <div class="row d-flex justify-content-center">
                                                           <asp:Button ID="btnProceed" runat="server" SkinID="btnDefault" Text="Pay Now" OnClick="btnProceed_Click" Height="60px" Width="60%" />
                                                              </div>
                                                          </asp:Panel>


                                                     <asp:Panel ID="pnlResultShow" runat="server" Visible="false">
                                                         <div class="card card-xxl-stretch" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white">Tickets <asp:HiddenField ID="hPortfolioid" runat="server" Value="0" />  </h3>
												<div class="card-toolbar">
                                                   

                                                    </div>

                                                <div class="card-body p-0" >
                                                    <div class="row mb-6 ">
                                                    <%--   <asp:PlaceHolder ID="plBarCode" runat="server" />--%>
         <div class="col-lg-8 col-sm-12 d-flex justify-content-center img-responsive center-block d-block mx-auto">
             <br />
             </div>
                                                        </div>
                                                     <div class="row mb-6">
         <div class="col-lg-12 col-sm-12 d-flex justify-content-center"  style="text-align:center;">
             <br /> <br /> <br /> <br />
                    <asp:Label ID="lblMsgResult" runat="server" Text="Donation" Font-Bold="true" Font-Size="28px" ForeColor="#7239EA"></asp:Label> <br />

                  </div>
        
        </div>
                                                   <%--  <div class="row mb-6">
                                                         
         <div class="col-lg-12 d-flex justify-content-center" style="text-align:center;">
             <br /><br /><br /><br />
             <asp:Button ID="btnSave" runat="server" Text="Go To Home" OnClick="btnSave_Click" Height="80px" Width="250px" Visible="false" />
             </div>
                                                        
                                                         </div>--%>

                                                    </div>
                                                </div>
                 </div>
                                                         </asp:Panel>
                                                    <br />
                                                    <br />
                                                     
                                                    </div>
                                                    </div>
                                                </div>
                 </div>
              <div class="col-xxl-4">
               <div class="card card-xxl-stretch" >
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white"> </h3>
												<div class="card-toolbar">


                                                    </div>
                                            <div class="card-body p-0">
                                                  <div class="row mb-5">
                                            <div class="overlay mt-8">
                                                 <asp:Image ID="img_event" runat="server"  CssClass="img-fluid" />
																<%-- <asp:Image ID="imgView" runat="server" ImageUrl='<%# GetImmage(Eval("ID").ToString()) %>' Height="300" />--%>
                                             <%--   <asp:Label ID="lblImg" runat="server" Text='<%# GetImmageString(Eval("ID").ToString()) %>'></asp:Label>--%>
																<%--<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px" style=background-image:url('<%= GetImmage() %>')>
                                                                   
																</div>  --%>
															</div>
                                        </div>
                                                  <div  id="pnl_paytype">

                                                       

                                                    <div class="row mb-6 text-center">
                                                        <asp:Label ID="lblTitleSub" runat="server" Text="Ticket Summary" Font-Bold="true" Font-Size="22px" ></asp:Label>
                                                         <hr  />
                                                    </div>

                                             

                                                 <div class="row mb-6">
                                                    
                                                       <div class="row"  >
                                                         <div class="col-lg-8">
                                                             <asp:Label ID="Label6" runat="server" Text="Tickets" Font-Size="18px"></asp:Label>
                                                         </div>
                                                           <div class="col-lg-4" style="text-align:right;float:right;">
                                                               <asp:Label ID="lbldtotal" runat="server" Text="Total" Font-Size="18px" style="font-weight:bold;" ClientIDMode="Static">0.00</asp:Label>
                                                           </div>
                                                         </div>
                                                       <div class="row" data-kt-buttons="true" >
                                                         <div class="col-lg-8">
                                                             <asp:Label ID="lblplatformfee" runat="server" Text="Platform fee" Font-Size="18px"></asp:Label>
                                                         </div>
                                                           <div class="col-lg-4" style="text-align:right;float:right;">
                                                             <asp:Label ClientIDMode="Static" id="lblfee" style="font-weight:bold;" Font-Size="18px" runat="server">0.00</asp:Label>
                                                           </div>
                                                         </div>
                                                     <hr />
                                                     <div class="row" data-kt-buttons="true" >
                                                         <div class="col-lg-8">
                                                             <asp:Label ID="lblTotal" runat="server" Text="Total" Font-Size="22px"></asp:Label>
                                                         </div>
                                                           <div class="col-lg-4" style="text-align:right;float:right;">
                                                               <label id="lbltotalval" style="font-size:22px;font-weight:bold;float:right;visibility:hidden;display:none;">$ 0.00</label>
                                                                <asp:Label ClientIDMode="Static" id="txtTotalNew" style="font-weight:bold;" Font-Size="22px" runat="server">0.00</asp:Label>
                                                               <div class="row" style="display:none;">
                                                               <asp:TextBox ID="txtTotal" runat="server" SkinID="Price" ClientIDMode="Static" Text="0.00" Font-Size="32px" style="display:none;"></asp:TextBox>
                                                                   </div>
                                                           </div>
                                                         </div>


                                                         <div class="row  mb-12 d-flex d-inline" style="display:none;visibility:hidden;">
                                                                <div class="col-lg-12">
                                                       <asp:CheckBox ID="chkfee" runat="server" Text="" Font-Size="20px" CssClass="mycheckBig" ClientIDMode="Static" Checked="true" Enabled="false" style="display:none;" />
                                                            <label style="font-size:20px;margin-left:5px;"> <input type="checkbox" checked="checked" id="mycheck" />  Yes! I would like to cover the transaction fee and platform support cost of  so that <asp:Label ID="lblOrg" runat="server"></asp:Label> can benefit from the full donation  </label> 
                                                                <script>
                                                                    document.getElementById("mycheck").disabled = true;
                                                                </script>      
                                                                </div>
                                                       </div>
                                                   
                                                     </div>
                                                       <div class="row mb-6 d-flex justify-content-center">

                                                    <asp:Button ID="btnPayDetails"  runat="server" Text="Payment Details" Font-Size="20px" Width="100%" Height="80px" OnClientClick="fnpaydetails();" Visible="false" />
                                                    </div>
                                                   <div class="row mb-6">
                                                       <%--<asp:CheckBox ID="chkSelect" runat="server" Text=" I want to Give anonymously" Font-Size="20px" />--%>
                                                       </div>
                                               
                                                </div>

                                                    <div  id="pnl_paydetails" style="display:none;" >
                                                         <div class="row mb-6">
                                                        <asp:Label ID="Label1" runat="server" Text="Payment Details" Font-Bold="true" Font-Size="22px" style="padding-bottom:10px"></asp:Label>
                                                             
                                                               <hr  />
                                                    </div>

                                                        <asp:HiddenField ID="hcount" runat="server" ClientIDMode="Static" Value="0" />
                                                     <%--   <asp:Panel ID="pnlListCards" runat="server" ClientIDMode="Static">

                                                              

  <div class="row mb-6">
    <asp:Button ID="btnAddNewCard" runat="server" Text="Add New Card" OnClientClick="shownewcard();" />
    </div>
    </asp:Panel>--%>

                                                      

                                                           

                                                            
                                                    </div>

                                            </div>
                                              
                                                </div>
                   </div>
                 
              </div>
             </div>
    <script type="text/javascript">

        $(document).ready(function () {
            //pnlRecurringOption
            //MainContent_MainContent_chkonetime2
            //MainContent_MainContent_chkRecurring
            //$('#MainContent_MainContent_taithingNewCtrl_btnAddNewCard').click(function () {
            //    //   alert($("#MainContent_MainContent_chkonetime2").is(": checked"));
            //    // alert($("#MainContent_MainContent_chkonetime2").is(":checked"));

            //    $("#pnlnewCard").show();
            //    $("#pnlListCards").hide();

            //    return false
            //});
            //$('#MainContent_MainContent_taithingNewCtrl_btnPayDetails').click(function () {


            //    if ($("#MainContent_MainContent_taithingNewCtrl_chkonetime2").is(":checked")) {
            //        $("#pnlRecurringOption").hide();
            //    }
            //    else {
            //        $("#pnlRecurringOption").show();
            //    }

            //    if ($("#hcount").val() == "0") {
            //        $('#pnl_paytype').hide();

            //        $('#pnl_paydetails').show();
            //    }
            //    else {
            //        $('#pnl_paytype').hide();

            //        $('#pnl_paydetails').show();
            //    }

            //    return false
            //});

            //$('#TithingNewCtrl_btnAddNewCard').click(function () {
            //    //   alert($("#MainContent_MainContent_chkonetime2").is(": checked"));
            //    // alert($("#MainContent_MainContent_chkonetime2").is(":checked"));

            //    $("#pnlnewCard").show();
            //    $("#pnlListCards").hide();

            //    return false
            //});


            //$('#MainContent_MainContent_taithingNewCtrl_btnAddNewCard').click(function () {
            //    //   alert($("#MainContent_MainContent_chkonetime2").is(": checked"));
            //    // alert($("#MainContent_MainContent_chkonetime2").is(":checked"));

            //    $("#pnlnewCard").show();
            //    $("#pnlListCards").hide();

            //    return false
            //});
            $('#MainContent_MainContent_taithingNewCtrl_btnPayDetails').click(function () {
                //   alert($("#MainContent_MainContent_chkonetime2").is(": checked"));
                // alert($("#MainContent_MainContent_chkonetime2").is(":checked"));

                if ($("#MainContent_MainContent_taithingNewCtrl_chkonetime2").is(":checked")) {
                    $("#pnlRecurringOption").hide();
                }
                else {
                    $("#pnlRecurringOption").show();
                }

                if ($("#hcount").val() == "0") {
                    $('#pnl_paytype').hide();

                    $('#pnl_paydetails').show();
                }
                else {
                    $('#pnl_paytype').hide();

                    $('#pnl_paydetails').show();
                }

                return false
            });

            $('#MainContent_btnPayDetails').click(function () {
                //   alert($("#MainContent_MainContent_chkonetime2").is(": checked"));
                // alert($("#MainContent_MainContent_chkonetime2").is(":checked"));

                //if ($("#TaithingNewCtrl_chkonetime2").is(":checked")) {
                //    $("#pnlRecurringOption").hide();
                //}
                //else {
                //    $("#pnlRecurringOption").show();
                //}

                if ($("#hcount").val() == "0") {
                    $('#pnl_paytype').hide();

                    $('#pnl_paydetails').show();
                }
                else {
                    $('#pnl_paytype').hide();

                    $('#pnl_paydetails').show();
                }

                return false
            });
        });




        $('#txtAmountTotal').on('keyup', function (e) {
            $('#txtTotal').val($('#txtAmountTotal').val());
            $('#txtTotalNew').html($('#txtTotal').val());
        });
        $('#txtAmount').on('keyup', function (e) {
            sum_amt();
        });
        $('.txtamt').on('keyup mouseup', function (e) {
            // console.log("keyup");
            sum_amt();
        });
        function sum_amt() {
            var add = 0;
            $(".txtamt").each(function () {
                var hp1 = $(this).next();
                console.log("hp1: " + $(hp1).val());
                add += (Number($(this).val()) * Number($(hp1).val())) ;
               
                
              
               
                //console.log("price:" + $(hp1).val());

                //console.log("price:" + $(this).closest('input[id]'));
            });
            // alert(add);
          // var eprice= parseFloat( $('#hamount').val());
            $('#txtAmountTotal').val(( add).toFixed(2));
            $('#txtTotal').val((add).toFixed(2));
            $('#txtTotalNew').html($('#txtTotal').val());
            $('#hTotal').val((add).toFixed(2));
            setFee();
            // console.log(add);
        }
        function setFee() {
            var t = Number($('#hTotal').val());
           // var p = Number($("#hfeepercent").val());
            var f = Number($("#hplatformfeepercent").val());
           // var x = Number($("#hfixedamount").val());

            var pr = 0;
            var x = 0;
            var p = 0;
            debugger;
            if (t > 0) {
                if (f > 0) {
                    pr = t * (f / 100).toFixed(2)
                    $("#hplatformfee").val(pr);
                    console.log('platform fee percent:' + f);
                    console.log('platform fee:' + pr);
                }
                debugger;
                //if percentage is zero
                if (p > 0) {
                    // t * (p / 100).toFixed(2)
                    var r = parseFloat((t).toFixed(2) * ((p).toFixed(2) / 100) + pr);

                    r = r + x;
                    //$('#txtAmountTotal').val(t + r);
                    $('#hfee').val(t * ((p).toFixed(2) / 100));
                    console.log('trans fee percent:' + p);
                    console.log('trans fee:' + r);
                   
                    $('#lbldtotal').html(t.toFixed(2));
                    $('#lblfee').html(r.toFixed(2));
                    $('#hamount').val((t + r).toFixed(2));
                    $('#txtTotal').val((t + r).toFixed(2));
                   
                    $('#lblptotal').html((t + r).toFixed(2));
                    // alert($('#chkfee').is(":checked"));
                    if ($('#chkfee').is(":checked")) {
                        $('#hamount').val((t + r).toFixed(2));
                        $('#txtTotal').val((t + r).toFixed(2));
                        $('#lblptotal').html((t + r).toFixed(2));

                    }
                    else {
                        $('#hamount').val((t).toFixed(2));
                        $('#txtTotal').val((t).toFixed(2));
                        $('#lblptotal').html((t).toFixed(2));
                    }

                    $('#txtTotalNew').html($('#txtTotal').val());
                }
                else {
                    //add plat form fee
                    if ($('#chkfee').is(":checked")) {
                        $('#lbldtotal').html(t.toFixed(2));
                        $('#lblfee').html(pr.toFixed(2));
                        $('#hamount').val((t + pr + x).toFixed(2));
                        $('#txtTotal').val((t + pr + x).toFixed(2));
                        $('#lblptotal').html((t + pr + x).toFixed(2));
                    }
                    else {
                        $('#hamount').val((t).toFixed(2));
                        $('#txtTotal').val((t).toFixed(2));
                        $('#lblptotal').html((t).toFixed(2));
                    }

                    $('#txtTotalNew').html($('#txtTotal').val());
                }
            }
        }

        function shownewcard() {
            $("#pnlnewCard").show();
          //  $("#pnlListCards").hide();
            return false;
        }

        function fnpaydetails() {
           // $('#pnl_paytype').hide();

            $('#pnl_paydetails').show();
            return false;
        }

    </script>

      <script type="text/javascript">


          $(document).ready(function () {
              //alert('t');
              setFee();
          });

        var ischeck = 1;
        function showdetails(id) {
         
            var divid = $(id).attr('value');
            ischeck = parseInt( $('#h_' + divid).val());
            if (ischeck == 1) {
                $(id).html("-");
                ischeck = 0;
                $('#h_' + divid).val(0);
                $('#' + divid).show();
            }
            else {
                $(id).html("+");
                ischeck = 1;
                $('#h_' + divid).val(1);
                $('#' + divid).hide();
            }
                 

            return false;
        }

      </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
