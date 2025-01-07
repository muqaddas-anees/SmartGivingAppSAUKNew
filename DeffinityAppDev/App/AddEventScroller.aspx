<%@ Page Language="C#" ValidateRequest="true" EnableEventValidation="true" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddEventScroller.aspx.cs" Inherits="DeffinityAppDev.App.AddEventScroller" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Wordpress Event Scroller
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


	 <div class="card mb-5 mb-xl-10">
    <!--begin::Card header-->
    <div class="card-header border-0 cursor-pointer" role="button"  aria-expanded="true" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">Wordpress Event Scroller</h3>
        </div>
        <!--end::Card title-->
        
        <!--begin::Card toolbar-->
        <div class="card-toolbar">
            <asp:LinkButton ID="btnSaveSettings" OnClientClick="return ClearValues()" OnClick="btnsave_Click1" runat="server" style="margin-right:10px" CssClass="btn btn-primary">Save</asp:LinkButton>
           <button id="copyCodeButton" type="button" class="btn btn-primary">Copy WordPress Code</button>

        </div>
        <!--end::Card toolbar-->
    </div>
    <!--end::Card header-->
    
    <!--begin::Content-->
    <div id="kt_account_profile_details" class="collapse show">
        <!--begin::Form-->
        <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
            <!--begin::Card body-->
            <div class="card-body border-top p-9">
             <div class="">
<div class="row">
    <div class="col-md-6">
                 <div class="row mb-3">
                     <div class="col-md-6">
                            <label for="panelHeight" class="form-label">Name</label>
   <input id="txtName" runat="server" clientidmode="Static" class="form-control">
                     </div>
                 </div>
    
    <div class="row mb-3">
        <div class="col-md-6">
            <label for="dropdown1" class="form-label">Select Event</label>
<select id="ddlEvents1" class="form-select">
    <!-- Example options -->
    <option value="1">Event 1</option>
    <option value="2">Event 2</option>
    <option value="3">Event 3</option>
</select>

        </div>
        <div class="col-md-6 d-flex align-items-end">
            <button type="button" id="btnAddEvent" class="btn btn-secondary">Add Additional Event</button>
        </div>
    </div>
    <div id="additionalDropdowns"></div>
    
    <div class="row mt-4">
        <div class="col-md-3">
            <label for="panelHeight" class="form-label">Panel Height(px)</label>
            <input type="number" runat="server" clientidmode="Static" id="panelHeight" class="form-control" placeholder="300">
        </div>
        <div class="col-md-3">
            <label for="panelWidth" class="form-label">Panel Width(px)</label>
            <input type="number" runat="server" clientidmode="Static" id="panelWidth" class="form-control" placeholder="500">
        </div>
    </div>
    
    <div class="row mt-4">
        <div class="col-md-6">
            <label for="titleBgColor" class="form-label">Event Title Background Colour</label>
            <input type="color" clientidmode="Static" runat="server" id="titleBgColor" class="form-control" value="#000000">
        </div> 

         <div class="col-md-6">
     <label for="titleBgColor" class="form-label">Event Title Background Transparency</label>
  <input id="titleTransparency" clientidmode="Static" runat="server" class="form-range" type="range" min="0" max="100" step="1" value="50"> </div> 

    </div>
    </div>
     <div class="col-md-6">


     <div id="carousel-container" style="width: 300px; height: 200px; overflow: hidden; position: relative;">
    <button id="prev" type="button" style="position: absolute; left: 0;background:none;border:none;font-size:30px; top: 50%; transform: translateY(-50%); z-index: 2;">&#10094;</button>
    <div id="carousel" style="display: flex; transition: transform 0.3s ease; width: max-content;">
        <!-- Slides will be dynamically injected here -->
    </div>
    <button id="next" type="button" style="position: absolute; right: 0;background:none;border:none;font-size:30px; top: 50%; transform: translateY(-50%); z-index: 2;">&#10095;</button>
</div>
              </div>

    </div>   
          <div class="row mt-4">
        <div class="col-md-6">
            <label for="titleFontColor" class="form-label">Event Title Font Colour</label>
            <input type="color" clientidmode="Static" runat="server" id="titleFontColor" class="form-control" value="#FFFFFF">
        </div></div>
   
    
    <div class="row mt-4">
        <div class="col-md-6">
            <label for="timeBgColor" class="form-label">Event Time Background Colour</label>
            <input type="color" clientidmode="Static" runat="server" id="timeBgColor" class="form-control" value="#8F0100">
        </div>


               <div class="col-md-6">
   <label for="titleBgColor" class="form-label">Event Time Background Transparency</label>
<input id="timeTransparency" clientidmode="Static" runat="server" class="form-range" type="range" min="0" max="100" step="1" value="50"> </div> 
    </div>
          <div class="row mt-4">
        <div class="col-md-6">
            <label for="timeFontColor" class="form-label">Event Time Font Colour</label>
            <input type="color" clientidmode="Static" runat="server" id="timeFontColor" class="form-control" value="#FFFFFF">
        </div></div>
    
    
    <div class="row mt-4">
        <div class="col-md-6">
            <label for="categoryBgColor" class="form-label">Event Category Background Colour</label>
            <input type="color" clientidmode="Static" runat="server" id="categoryBgColor" class="form-control" value="#8F0100">
        </div>

               <div class="col-md-6">
   <label for="titleBgColor" class="form-label">Event Category Background Transparency</label>
<input id="catTransparency" clientidmode="Static" runat="server" class="form-range" type="range" min="0" max="100" step="1" value="50"> </div> 

    </div>  <div class="row mt-4">
        <div class="col-md-6">
            <label for="categoryFontColor" class="form-label">Event Category Font Colour</label>
            <input type="color" clientidmode="Static" runat="server" id="categoryFontColor" class="form-control" value="#FFFFFF">
        </div>
    </div>

                 <div class="row mt-4">
   <div class="col-md-6">
    <label for="categoryFontColor" clientidmode="Static" class="form-label">Event Type</label>
    <asp:DropDownList runat="server" ID="ddltype" ClientIDMode="Static" CssClass="form-control" onchange="toggleTextbox()">
        <asp:ListItem Text="Highlights" Value="Highlights"></asp:ListItem>
        <asp:ListItem Text="Other" Value=""></asp:ListItem>
    </asp:DropDownList>
</div>

<!-- Hidden Textbox -->
<div class="col-md-6" id="otherTextbox" style="display:none;">
    <label for="otherType" class="form-label">Other Type</label>
    <asp:TextBox runat="server" ID="txtOtherType" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
</div>

<script>
    document.onload(toggleTextbox());
    function toggleTextbox() {
        var ddl = document.getElementById('<%= ddltype.ClientID %>');
        var textbox = document.getElementById('otherTextbox');

        // Check if 'Other' is selected
        if (ddl.value === '') {
            textbox.style.display = 'block'; // Show the textbox
          
        } else {
            textbox.style.display = 'none'; // Hide the textbox
            var Texboxinput = document.getElementById('<%= txtOtherType.ClientID %>');
            Texboxinput.value = ddl.value;  // Set the value of the textbox
        }
      
    }
</script>

    </div>

  
</div>

            </div>
            <!--end::Card body-->
        </form>
        <!--end::Form-->
    </div>
    <!--end::Content-->
</div>
    <div class="modal fade" id="codeModal" tabindex="-1" role="dialog" aria-labelledby="codeModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="codeModalLabel">Generated Carousel Code</h5>
                <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <textarea id="carouselCode" class="form-control" rows="10" readonly></textarea>
            </div>
            <div class="modal-footer">
                <button id="copyButton" type="button" class="btn btn-secondary">Copy</button>
                <button type="button" data-bs-dismiss="modal" class="btn btn-primary" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
    <asp:HiddenField ID="hfAllEvents" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfSavedEvents" ClientIDMode="Static" runat="server" />

 <script>
     document.addEventListener('DOMContentLoaded', function () {
         // Parse JSON data from the HiddenField
         const eventData = JSON.parse(document.getElementById('<%= hfAllEvents.ClientID %>').value);

      // Get saved events from the hidden field
      const savedEvents = document.getElementById('<%= hfSavedEvents.ClientID %>').value.split(',').filter(x => x);

    // Populate the first dropdown
    const firstDropdown = document.getElementById('ddlEvents1');
    populateDropdown(firstDropdown, eventData);
         firstDropdown.addEventListener('change', function () {
             handleSelectionChange(this);
         });
    // Set the first dropdown value if there's a saved event
    if (savedEvents.length > 0) {
        firstDropdown.value = savedEvents[0];
    }

    // Add additional dropdowns dynamically for remaining saved events
    const dropdownContainer = document.getElementById('additionalDropdowns');
    for (let i = 1; i < savedEvents.length; i++) {
        addNewDropdown(savedEvents[i]);
    }

    // Add event listener to "Add Event" button
    document.getElementById('btnAddEvent').addEventListener('click', function () {
        addNewDropdown();
    });

    // Function to populate a dropdown with event data
    function populateDropdown(dropdown, data) {
        dropdown.innerHTML = ''; // Clear any existing options

        // Add the default "Please Select" option
        const defaultOption = document.createElement('option');
        defaultOption.value = '0'; // Default value
        defaultOption.textContent = 'Please Select'; // Default text
        dropdown.appendChild(defaultOption);

        // Add the event data
        data.forEach(event => {
            const option = document.createElement('option');
            option.value = event.ID; // Use EventID as the value
            option.textContent = event.Title; // Use EventName as the display text
            dropdown.appendChild(option);
        });
    }

    // Handle selection change for a dropdown
    function handleSelectionChange(dropdown) {
        const selectedValue = dropdown.value; // Get the selected value
        const savedEvents = document.getElementById('<%= hfSavedEvents.ClientID %>');
        let selectedEvents = savedEvents.value ? savedEvents.value.split(',') : [];

        // Remove the previous value of this dropdown if it exists in the hidden field
        const previousValue = dropdown.dataset.previousValue || '0';
        if (previousValue !== '0') {
            selectedEvents = selectedEvents.filter(val => val !== previousValue);
        }

        // Add the new value if it's not the default "Please Select"
        if (selectedValue !== '0') {
            selectedEvents.push(selectedValue);
        }

        // Update the dataset with the current selected value
        dropdown.dataset.previousValue = selectedValue;

        // Update the hidden field
        savedEvents.value = selectedEvents.join(',');
    }

    // Add a new dropdown dynamically
    function addNewDropdown(selectedValue = '0') {
        // Create a new dropdown container
        const newDropdownDiv = document.createElement('div');
        newDropdownDiv.className = 'row mb-3';

        const newDropdownHTML = `
            <div class="col-md-6">
                <select class="form-select dynamic-dropdown"></select>
            </div>
            <div class="col-md-1">
                <button type="button" class="btn btn-danger btn-remove-dropdown">Remove</button>
            </div>`;
        newDropdownDiv.innerHTML = newDropdownHTML;

        // Append the new dropdown container
        dropdownContainer.appendChild(newDropdownDiv);

        // Get the newly created dropdown and populate it
        const newDropdown = newDropdownDiv.querySelector('select');
        populateDropdown(newDropdown, eventData);

        // Set the dropdown value if provided
        newDropdown.value = selectedValue;
        handleSelectionChange(newDropdown);

        // Add change listener to the newly created dropdown
        newDropdown.addEventListener('change', function () {
            handleSelectionChange(this);
        });

        // Add remove button functionality
        newDropdownDiv.querySelector('.btn-remove-dropdown').addEventListener('click', function () {
            const dropdownValue = newDropdown.value;

            // Remove the dropdown's value from the hidden field
            if (dropdownValue !== '0') {
                removeValueFromHiddenField(dropdownValue);
            }

            // Remove the dropdown container
            dropdownContainer.removeChild(newDropdownDiv);
        });
    }

    // Function to remove a value from the hidden field
    function removeValueFromHiddenField(valueToRemove) {
          const savedEvents = document.getElementById('<%= hfSavedEvents.ClientID %>');
          let selectedEvents = savedEvents.value ? savedEvents.value.split(',') : [];

          // Remove the value
          selectedEvents = selectedEvents.filter(val => val !== valueToRemove);

          // Update the hidden field
          savedEvents.value = selectedEvents.join(',');
      }
     });
     function ClearValues() {
         document.getElementById('<%= hfAllEvents.ClientID %>').value = "";
         return true;
     }

 </script>

<script>
    document.addEventListener("DOMContentLoaded", () => {
        const carousel = document.getElementById("carousel");
        const carouselContainer = document.getElementById("carousel-container");

        // Input elements
        const heightInput = document.getElementById("panelHeight");
        const widthInput = document.getElementById("panelWidth");
        const titleBgColorInput = document.getElementById("titleBgColor");
        const titleTransparencyInput = document.getElementById("titleTransparency");
        const titleFontColorInput = document.getElementById("titleFontColor");
        const timeBgColorInput = document.getElementById("timeBgColor");
        const timeTransparencyInput = document.getElementById("timeTransparency");
        const timeFontColorInput = document.getElementById("timeFontColor");
        const categoryBgColorInput = document.getElementById("categoryBgColor");
        const categoryTransparencyInput = document.getElementById("catTransparency");
        const categoryFontColorInput = document.getElementById("categoryFontColor");

        const prevButton = document.getElementById("prev");
        const nextButton = document.getElementById("next");

        let events = JSON.parse(document.getElementById("hfAllEvents").value);
        const savedEventIds = document.getElementById("hfSavedEvents").value.split(',');

        // Filter the events based on savedEventIds and update the 'events' array
        events = events.filter(event => savedEventIds.includes(event.ID.toString()));

        // Initialize styles on page load
        function applyStyles() {
            const height = parseInt(heightInput.value, 10) || 300; // Default height
            const width = parseInt(widthInput.value, 10) || 500;  // Default width

            carouselContainer.style.height = `${height}px`;
            carouselContainer.style.width = `${width}px`;

            carousel.innerHTML = ""; // Clear existing slides
            const timeBgColor = timeBgColorInput.value || "#8F0100";
            const timeTransparency = timeTransparencyInput.value / 100 || 0.5;
            const timeFontColor = timeFontColorInput.value || "#FFFFFF";
            const titleBgColor = titleBgColorInput.value || "#000000";
            const titleTransparency = titleTransparencyInput.value / 100 || 0.7;
            const titleFontColor = titleFontColorInput.value || "#FFFFFF";

            const catBgColor = categoryBgColorInput.value || "#000000";
            const catTransparency = categoryTransparencyInput.value / 100 || 0.7;
            const catFontColor = categoryFontColorInput.value || "#FFFFFF";

            events.forEach(event => {
                const slide = document.createElement("div");
                slide.style.cssText = `
                flex: 0 0 ${width}px; 
                height: ${height}px; 
                position: relative; 
                background-size: cover!important; 
                background-position: center;
                background-image: url('../../imagehandler.ashx?s=event&id=${event.unid}');
            `;
                

                // Title at the top right
                const title = document.createElement("div");
                title.textContent = document.getElementById('ddltype').value;
                title.style.cssText = `
                position: absolute; 
                top: 0px; 
                right: 1px; 
                background: rgba(${hexToRgb(catBgColor)}, ${catTransparency}); 
                color: ${catFontColor}; 
                padding: 5px; 
                font-weight: bold; 
                font-size: 34px;
            `;

           
                const time = new Date(event.StartDateTime).toLocaleTimeString([], {
                    hour: '2-digit',
                    minute: '2-digit'
                });
                // Highlights headline at the bottom
                const headline = document.createElement("div");
                headline.style.cssText = `
                position: absolute; 
                bottom: 0px; 
                left: 0; 
                right: 0; 
                background: rgba(${hexToRgb(catBgColor)}, ${catTransparency}); 
                color: ${catFontColor}; 
                padding: 0px; 
                text-align: left;
            `;
                headline.innerHTML = `
    <div style="display: flex; ">
        <div style="padding: 10px; border-right: 1px solid #998b8b; font-size: 30px; background: rgba(${hexToRgb(timeBgColor)}, ${timeTransparency}); color: ${timeFontColor};">
            ${time}
        </div>
        <div style="width: -webkit-fill-available;background: rgba(${hexToRgb(titleBgColor)}, ${titleTransparency}); color: ${titleFontColor};">
            <div style="font-weight: bold; font-size: 20px; padding: 10px;">
                ${event.Title}
            </div>
            <div style="padding: 10px;">
                ${event.Description}
            </div>
        </div>
    </div>
`;


                slide.appendChild(title);
                slide.appendChild(headline);
                carousel.appendChild(slide);
            });
        }

 

        // Attach event listeners to inputs
        [heightInput, widthInput, titleBgColorInput, titleTransparencyInput, titleFontColorInput,
            timeBgColorInput, timeTransparencyInput, timeFontColorInput, categoryBgColorInput,
            categoryTransparencyInput, categoryFontColorInput].forEach(input => {
                input.addEventListener("input", applyStyles);
            });

        // Carousel navigation
        let currentIndex = 0;
        const updateCarousel = () => {
            const width = parseInt(widthInput.value, 10) || 500;
            carousel.style.transform = `translateX(${-width * currentIndex}px)`;
        };

        prevButton.addEventListener("click", () => {
            if (currentIndex > 0) {
                currentIndex--;
                updateCarousel();
            }
        });

        nextButton.addEventListener("click", () => {
            if (currentIndex < events.length - 1) {
                currentIndex++;
                updateCarousel();
            }
        });

        // Apply styles initially
        applyStyles();
    });
    // Helper to convert any color value to RGB
    // Helper to convert any color value to RGB
    function hexToRgb(color) {
        // Ensure the input is a string
        if (typeof color !== 'string') {
            return '0, 0, 0'; // Return a default color (black) if the input is not a string
        }

        // Check if the color is in HEX format
        if (color.startsWith("#")) {
            // Handle HEX format (e.g., #RRGGBB or #RGB)
            if (color.length === 7) {
                // Full HEX (#RRGGBB)
                const r = parseInt(color.slice(1, 3), 16);
                const g = parseInt(color.slice(3, 5), 16);
                const b = parseInt(color.slice(5, 7), 16);
                return `${r}, ${g}, ${b}`;
            } else if (color.length === 4) {
                // Short HEX (#RGB)
                const r = parseInt(color[1] + color[1], 16);
                const g = parseInt(color[2] + color[2], 16);
                const b = parseInt(color[3] + color[3], 16);
                return `${r}, ${g}, ${b}`;
            }
        }

        // Check if the color is in RGB format (e.g., rgb(255, 255, 255))
        if (color.startsWith("rgb")) {
            // Extract the numbers from an rgb() string
            const rgbValues = color.match(/\d+/g);
            if (rgbValues && rgbValues.length === 3) {
                return `${rgbValues[0]}, ${rgbValues[1]}, ${rgbValues[2]}`;
            }
        }

        // Check if the color is a named color (e.g., "red", "blue", etc.)
        const colorNames = {
            "black": "0, 0, 0",
            "white": "255, 255, 255",
            "red": "255, 0, 0",
            "green": "0, 255, 0",
            "blue": "0, 0, 255",
            "yellow": "255, 255, 0",
            "cyan": "0, 255, 255",
            "magenta": "255, 0, 255",
            // Add more color names if needed
        };

        if (colorNames[color.toLowerCase()]) {
            return colorNames[color.toLowerCase()];
        }

        // If none of the above cases matched, return the input value as-is (assumed to be RGB already)
        return color;
    }


</script>

    <script>
        function generateCarousel() {
            // Get the necessary values from the existing elements
            const eventData = JSON.parse(document.getElementById('<%= hfAllEvents.ClientID %>').value);
            const savedEventIds = document.getElementById('<%= hfSavedEvents.ClientID %>').value.split(',').filter(x => x);

            // Get styles from input fields
            const styles = {
                width: parseInt(document.getElementById("panelWidth").value, 10) || 500,
                height: parseInt(document.getElementById("panelHeight").value, 10) || 300,
                titleBgColor: document.getElementById("titleBgColor").value || "#000000",
                titleTransparency: document.getElementById("titleTransparency").value / 100 || 0.7,
                titleFontColor: document.getElementById("titleFontColor").value || "#FFFFFF",
                timeBgColor: document.getElementById("timeBgColor").value || "#8F0100",
                timeTransparency: document.getElementById("timeTransparency").value / 100 || 0.5,
                timeFontColor: document.getElementById("timeFontColor").value || "#FFFFFF",
                categoryBgColor: document.getElementById("categoryBgColor").value || "#8F0100",
                categoryTransparency: document.getElementById("catTransparency").value / 100 || 0.5,
                categoryFontColor: document.getElementById("categoryFontColor").value || "#FFFFFF"
            };

            // Filter events based on savedEventIds
            const filteredEvents = eventData.filter(event => savedEventIds.includes(event.ID.toString()));

            // Create the HTML structure for the carousel
            const carouselHTML = `
        <div class="carousel-container" id="carousel-container" style="width: ${styles.width}px; height: ${styles.height}px;">
            <button id="prev" type="button" class="carousel-button">&#10094;</button>
            <div id="carousel" class="carousel">
               ${document.getElementById('carousel').innerHTML.replace("../","https://dev.plegit.ai/")}
            </div>
            <button id="next" type="button" class="carousel-button">&#10095;</button>
        </div>
    `;

            // Create the CSS styles for the carousel
            const carouselCSS = `
        <style>
            * {
                box-sizing: border-box;
            }
            .carousel-container {
                position: relative;
                overflow: hidden;
                border: 2px solid #ccc;
                border-radius: 10px;
            }
            .carousel {
                display: flex;
                transition: transform 0.3s ease;
            }
            .carousel-slide {
                min-width: 100%;
                height: 100%;
                background-size: cover;
                background-position: center;
                position: relative;
            }
            .carousel-button {
                position: absolute;
                top: 50%;
                transform: translateY(-50%);
                background: none;
                border: none;
                font-size: 30px;
                cursor: pointer;
                z-index: 2;
            }
            #prev {
                left: 10px;
            }
            #next {
                right: 10px;
            }
        </style>
    `;

            // Insert the HTML and CSS into the document
           

            // Create the JavaScript functionality for the carousel
            const script = document.createElement('script');
            script.textContent = `
        document.addEventListener("DOMContentLoaded", () => {
            const carousel = document.getElementById ("carousel");
            const prevButton = document.getElementById("prev");
            const nextButton = document.getElementById("next");

            let currentIndex = 0;

            function updateCarousel() {
                const offset = -currentIndex * 100; // Move the carousel
                carousel.style.transform = \`translateX(\${offset}%)\`;
            }

            prevButton.addEventListener("click", () => {
                currentIndex = (currentIndex > 0) ? currentIndex - 1 : 0;
                updateCarousel();
            });

            nextButton.addEventListener("click", () => {
                currentIndex = (currentIndex < carousel.children.length - 1) ? currentIndex + 1 : carousel.children.length - 1;
                updateCarousel();
            });
        });
    `;

            // Append the script to the document
          
            console.log(script.outerHTML)
            return carouselCSS+carouselHTML  + script.outerHTML;
        }
        document.getElementById('copyCodeButton').addEventListener('click', function () {
            // Generate the carousel code
            const carouselCode = generateCarousel(); // Call your function to generate the code

            // Populate the textarea in the modal
            document.getElementById('carouselCode').value = carouselCode;

            // Show the modal
            $('#codeModal').modal('show');
        });

        document.getElementById('copyButton').addEventListener('click', function () {
            // Select the text in the textarea
            const textarea = document.getElementById('carouselCode');
            textarea.select();
            textarea.setSelectionRange(0, 99999); // For mobile devices

            // Copy the text to the clipboard
            document.execCommand('copy');

            // Optionally, show an alert or change button text to indicate success
        });

        // Call the function to generate the carousel
    </script>
</asp:Content>