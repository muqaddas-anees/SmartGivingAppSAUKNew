<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.VideoCtrl" %>

<title>Admin Videos</title>
<link href="Content/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/jquery-3.5.1.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
<style>
    img{
        height:100%;
        width:100%;
    }
    .video-item {
        padding: 20px; /* Adjust padding */
    }
    .row-space {
        margin-bottom: 30px; /* Increased margin for space between rows */
    }
    .center-horizontal {
        display: flex;
        justify-content: center; /* Centers horizontally */
        align-items: center;     /* Optional: Centers vertically */
        margin: 0;
    }
    .checkbox-container {
        display: flex;
        justify-content: center; /* Centers horizontally */
        margin: 0;
        padding: 0;
    }
    .d-flex {
        display: flex;
    }
    .column {
        flex: 1;
        margin: 10px;
    }
    .video-description{
        text-align:center;
        margin-top:10px;
       color:dimgray;


    }
   .video-div {
  display: flex;
  flex-wrap: wrap;
}
   .center-horizontal {
  text-align: center; /* center the contents horizontally */
}

.checkbox-container {
  display: inline-block; /* make the container an inline block */
  margin: 20px; /* add some space around the container */
}

.checkbox-container input[type="checkbox"] {
  display: none; /* hide the original checkbox */
}

.checkbox-container label {
  display: inline-block; /* make the label an inline block */
  width: 40px; /* set the width of the switch button */
  height: 20px; /* set the height of the switch button */
  background-color: #ddd; /* set the background color of the switch button */
  border-radius: 20px; /* make the switch button a circle */
  position: relative; /* set the position to relative */
  cursor: pointer; /* change the cursor to a pointer */
}

.checkbox-container label:before {
  content: ""; /* add an empty content property to create a pseudo-element */
  display: block; /* make the pseudo-element a block */
  width: 18px; /* set the width of the pseudo-element */
  height: 18px; /* set the height of the pseudo-element */
  background-color: #fff; /* set the background color of the pseudo-element */
  border-radius: 50%; /* make the pseudo-element a circle */
  position: absolute; /* set the position to absolute */
  top: 1px; /* set the top position of the pseudo-element */
  left: 1px; /* set the left position of the pseudo-element */
  transition: 0.3s; /* add a transition effect */
}

.checkbox-container input[type="checkbox"]:checked + label:before {
  left: 21px; /* move the pseudo-element to the right when the checkbox is checked */
  background-color: #337ab7; /* change the background color of the pseudo-element when the checkbox is checked */
}

.checkbox-container label:after {
   /* add the "Off" text */
  position: absolute; /* set the position to absolute */
  top: 0; /* set the top position of the text */
  left: 2px; /* set the left position of the text */
  color: #fff; /* set the text color */
  font-size: 12px; /* set the font size of the text */
  transition: 0.3s; /* add a transition effect */
}

.checkbox-container input[type="checkbox"]:checked + label:after {
   /* change the text to "On" when the checkbox is checked */
  left: 22px; /* move the text to the right when the checkbox is checked */
}

/* Mobile screen styles */
@media only screen and (max-width: 768px) {
  .video-div {
    flex-direction: column;
  }
}
</style>
<div class="container">
                    <h5 style="text-align:center"; class="card-title fs-3x text-success mx-5">Let's Get You Started</h5>

<!-- Header with Step 1 and Step 2 in columns -->
<div style="margin-top:30px" class="container">

    <div class="row">
        <!-- Step 1 Column -->
        <div class="col-md-6">
            <div class="list-group-item mb-10">
                <h6 style="text-align:center"; class="mb-1 fs-1">Step 1: Create Account</h6>
                <p class="fs-2">Create a Stripe Account or Connect Your Existing Account to Plegit</p>
            </div>
        </div>

        <!-- Step 2 Column -->
        <div class="col-md-6">
            <div class="list-group-item mb-10">
                <h5 style="text-align:center"; class="mb-1 fs-1">Step 2: Book an Onboarding Call</h5>
                <p class="fs-2">You'll be notified once your account is active and you can begin fundraising.</p>
            </div>
        </div>
    </div>
         <div class="row">
     <!-- Step 1 Column -->
     <div class="col-md-6">
         <div class="list-group-item mb-10">
           
             <asp:Button ID="Button1" runat="server" Text="Activate Stripe" CssClass="btn btn-success w-100"></asp:Button>
         </div>
     </div>

     <!-- Step 2 Column -->
     <div class="col-md-6">
         <div class="list-group-item mb-10">
          
             <a class="btn btn-success w-100" id="btnstep2" href="https://calendly.com/d/cpgp-hd5-vt5/free-consultation-with-a-charity-champion">Book a Call with a Charity Champion</a>
         </div>
     </div>
 </div>
</div>
   
</div>
<div class="container">
<!-- Videos Section -->
<div class="container">
   
    <div id="VideoContainer" runat="server">



    </div>  
    <div class="center-horizontal">
     <div class="checkbox-container"> <asp:CheckBox ID="chkHideTab" runat="server" Text="  "  OnCheckedChanged="chkHideTab_CheckedChanged" AutoPostBack="true" />


</div>
                                  <label>Do not show this tab next time</label>


    </div>
    <br /><br />
</div>
</div>