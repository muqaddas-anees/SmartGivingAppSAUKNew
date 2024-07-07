<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits=" DeffinityAppDev.Chat" %>

<html>
<head runat="server">
    <title>Metronic - The World's #1 Selling Bootstrap Admin Template by KeenThemes</title>
    <meta charset="utf-8" />
    <meta name="description" content="The most advanced Bootstrap 5 Admin Theme with 40 unique prebuilt layouts on Themeforest trusted by 100,000 beginners and professionals. Multi-demo, Dark Mode, RTL support and complete React, Angular, Vue, Asp.Net Core, Rails, Spring, Blazor, Django, Express.js, Node.js, Flask, Symfony & Laravel versions. Grab your copy now and get life-time updates for free." />
    <meta name="keywords" content="metronic, bootstrap, bootstrap 5, angular, VueJs, React, Asp.Net Core, Rails, Spring, Blazor, Django, Express.js, Node.js, Flask, Symfony & Laravel starter kits, admin themes, web design, figma, web development, free templates, free admin themes, bootstrap theme, bootstrap template, bootstrap dashboard, bootstrap dak mode, bootstrap button, bootstrap datepicker, bootstrap timepicker, fullcalendar, datatables, flaticon" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta property="og:locale" content="en_US" />
    <meta property="og:type" content="article" />
    <meta property="og:title" content="Metronic - The World's #1 Selling Bootstrap Admin Template by KeenThemes" />
    <meta property="og:url" content="https://keenthemes.com/metronic" />
    <meta property="og:site_name" content="Metronic by Keenthemes" />
    <link rel="canonical" href="https://preview.keenthemes.com/metronic8" />
    <link rel="shortcut icon" href="../assets/media/logos/favicon.ico" />
    <!--begin::Fonts(mandatory for all pages)-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700" />
    <!--end::Fonts-->
    <!--begin::Vendor Stylesheets(used for this page only)-->
    <link href="../assets/plugins/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Vendor Stylesheets-->
    <!--begin::Global Stylesheets Bundle(mandatory for all pages)-->
    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Global Stylesheets Bundle-->
    <script>// Frame-busting to prevent site from being loaded within a frame without permission (click-jacking) if (window.top != window.self) { window.top.location.replace(window.self.location.href); }</script>
    <style>
        .hover-effect {
    transition: background-color 0.3s ease, box-shadow 0.3s ease;
    cursor:pointer;
}

.hover-effect:hover {
    background-color: #f5f5f5; /* Light gray background on hover */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Subtle shadow on hover */
}

.hover-effect .symbol-label:hover {
    background-color: #e0e0e0; /* Slightly darker background for symbol label on hover */
}
    </style>
</head>












<body>



    <div class="d-flex flex-column flex-column-fluid">
        <!--begin::Toolbar-->
        <div id="kt_app_toolbar" class="app-toolbar pt-6 pb-2">
            <!--begin::Toolbar container-->
            <div id="kt_app_toolbar_container" class="app-container container-fluid d-flex align-items-stretch">
                <!--begin::Toolbar wrapper-->
                <div class="app-toolbar-wrapper d-flex flex-stack flex-wrap gap-4 w-100">
                    <!--begin::Page title-->
                    <div class="page-title d-flex flex-column justify-content-center gap-1 me-3">
                        <!--begin::Title-->
                        <h1 class="page-heading d-flex flex-column justify-content-center text-gray-900 fw-bold fs-3 m-0">Private Chat</h1>
                        <!--end::Title-->
                        <!--begin::Breadcrumb-->
                        <ul class="breadcrumb breadcrumb-separatorless fw-semibold fs-7 my-0">
                            <!--begin::Item-->
                            <li class="breadcrumb-item text-muted">
                                <a href="index.html" class="text-muted text-hover-primary">Home</a>
                            </li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item">
                                <span class="bullet bg-gray-500 w-5px h-2px"></span>
                            </li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item text-muted">Apps</li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item">
                                <span class="bullet bg-gray-500 w-5px h-2px"></span>
                            </li>
                            <!--end::Item-->
                            <!--begin::Item-->
                            <li class="breadcrumb-item text-muted">Chat</li>
                            <!--end::Item-->
                        </ul>
                        <!--end::Breadcrumb-->
                    </div>
                    <!--end::Page title-->
                    <!--begin::Actions-->
                    <div class="d-flex align-items-center gap-2 gap-lg-3">
                        <a href="#" class="btn btn-flex btn-outline btn-color-gray-700 btn-active-color-primary bg-body h-40px fs-7 fw-bold" data-bs-toggle="modal" data-bs-target="#kt_modal_view_users">Add Member</a>
                        <a href="#" class="btn btn-flex btn-primary h-40px fs-7 fw-bold" data-bs-toggle="modal" data-bs-target="#kt_modal_create_campaign">New Campaign</a>
                    </div>
                    <!--end::Actions-->
                </div>
                <!--end::Toolbar wrapper-->
            </div>
            <!--end::Toolbar container-->
        </div>
        <!--end::Toolbar-->
        <!--begin::Content-->
        <div id="kt_app_content" class="app-content flex-column-fluid">
            <!--begin::Content container-->
            <div id="kt_app_content_container" class="app-container container-fluid">
                <!--begin::Layout-->
                <div class="d-flex flex-column flex-lg-row">
                    <!--begin::Sidebar-->
                    <div class="flex-column flex-lg-row-auto w-100 w-lg-300px w-xl-400px mb-10 mb-lg-0">
                        <!--begin::Contacts-->
                        <div class="card card-flush">
                            <!--begin::Card header-->
                            <div class="card-header pt-7" id="kt_chat_contacts_header">
                                <!--begin::Form-->
                                <form class="w-100 position-relative" autocomplete="off">
                                    <!--begin::Icon-->
                                    <i class="ki-outline ki-magnifier fs-3 text-gray-500 position-absolute top-50 ms-5 translate-middle-y"></i>
                                    <!--end::Icon-->
                                    <!--begin::Input-->
                                    <input type="text" class="form-control form-control-solid px-13" name="search" value="" placeholder="Search by username or email...">
                                    <!--end::Input-->
                                </form>
                                <!--end::Form-->
                            </div>
                            <!--end::Card header-->
                            <!--begin::Card body-->
                            <div class="card-body pt-5" style="height:75vh;overflow-y:scroll" runat="server" id="kt_chat_contacts_body">
                                <!--begin::List-->
                                <div class="scroll-y me-n5 pe-5 h-200px h-lg-auto" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-max-height="auto" data-kt-scroll-dependencies="#kt_header, #kt_app_header, #kt_toolbar, #kt_app_toolbar, #kt_footer, #kt_app_footer, #kt_chat_contacts_header" data-kt-scroll-wrappers="#kt_content, #kt_app_content, #kt_chat_contacts_body" data-kt-scroll-offset="5px" style="max-height: 285px;">
                                    <!-- Dynamically populate chat contacts here -->
                                </div>
                                <!--end::List-->
                            </div>

                            <!--end::Card body-->
                        </div>
                        <!--end::Contacts-->
                    </div>
                    <!--end::Sidebar-->
                    <!--begin::Content-->
                    <div class="flex-lg-row-fluid ms-lg-7 ms-xl-10">
                        <!--begin::Messenger-->
                        
<div class="card" id="kt_chat_messenger">
    <div class="card-header" id="kt_chat_messenger_header">
        <div class="card-title">
            <div class="d-flex justify-content-center flex-column me-3">
                <a href="#" class="fs-4 fw-bold text-gray-900 text-hover-primary me-1 mb-2 lh-1" id="chatname">Start Chat</a>
                <div class="mb-0 lh-1">
                 
                </div>
            </div>
        </div>
        <div class="card-toolbar">
            <div class="me-n3">
                <button class="btn btn-sm btn-icon btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
                    <i class="ki-outline ki-dots-square fs-2"></i>
                </button>
                <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-semibold w-200px py-3" data-kt-menu="true">
                    <div class="menu-item px-3">
                        <div class="menu-content text-muted pb-2 px-3 fs-7 text-uppercase">Contacts</div>
                    </div>
                    <div class="menu-item px-3">
                        <a href="#" class="menu-link px-3" data-bs-toggle="modal" data-bs-target="#kt_modal_users_search">Add Contact</a>
                    </div>
                    <div class="menu-item px-3">
                        <a href="#" class="menu-link flex-stack px-3" data-bs-toggle="modal" data-bs-target="#kt_modal_invite_friends">Invite Contacts 
                            <span class="ms-2" data-bs-toggle="tooltip" aria-label="Specify a contact email to send an invitation" data-bs-original-title="Specify a contact email to send an invitation" data-kt-initialized="1">
                                <i class="ki-outline ki-information fs-7"></i>
                            </span></a>
                    </div>
                    <div class="menu-item px-3" data-kt-menu-trigger="hover" data-kt-menu-placement="right-start">
                        <a href="#" class="menu-link px-3">
                            <span class="menu-title">Groups</span>
                            <span class="menu-arrow"></span>
                        </a>
                        <div class="menu-sub menu-sub-dropdown w-175px py-4">
                            <div class="menu-item px-3">
                                <a href="#" class="menu-link px-3" data-bs-toggle="tooltip" data-bs-original-title="Coming soon" data-kt-initialized="1">Create Group</a>
                            </div>
                            <div class="menu-item px-3">
                                <a href="#" class="menu-link px-3" data-bs-toggle="tooltip" data-bs-original-title="Coming soon" data-kt-initialized="1">Invite Members</a>
                            </div>
                            <div class="menu-item px-3">
                                <a href="#" class="menu-link px-3" data-bs-toggle="tooltip" data-bs-original-title="Coming soon" data-kt-initialized="1">Settings</a>
                            </div>
                        </div>
                    </div>
                    <div class="menu-item px-3 my-1">
                        <a href="#" class="menu-link px-3" data-bs-toggle="tooltip" data-bs-original-title="Coming soon" data-kt-initialized="1">Settings</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card-body" id="kt_chat_messenger_body">
        <div class="scroll-y me-n5 pe-5 h-300px h-lg-auto" data-kt-element="messages" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-max-height="auto" data-kt-scroll-dependencies="#kt_header, #kt_app_header, #kt_app_toolbar, #kt_toolbar, #kt_footer, #kt_app_footer, #kt_chat_messenger_header, #kt_chat_messenger_footer" data-kt-scroll-wrappers="#kt_content, #kt_app_content, #kt_chat_messenger_body" data-kt-scroll-offset="5px" style="max-height: 381px;">
            <div class="d-flex justify-content-end mb-10 d-none" data-kt-element="template-out">
                <div class="d-flex flex-column align-items-end">
                    <div class="d-flex align-items-center mb-2">
                        <div class="me-3">
                            <span class="text-muted fs-7 mb-1">Just now</span>
                            <a href="#" class="fs-5 fw-bold text-gray-900 text-hover-primary ms-1">You</a>
                        </div>
                        <div class="symbol symbol-35px symbol-circle">
                            <img alt="Pic" src="../assets/media/avatars/300-1.jpg">
                        </div>
                    </div>
                    <div class="p-5 rounded bg-light-primary text-gray-900 fw-semibold mw-lg-400px text-end" data-kt-element="message-text"></div>
                </div>
            </div>
            <div class="d-flex justify-content-start mb-10 d-none" data-kt-element="template-in">
                <div class="d-flex flex-column align-items-start">
                    <div class="d-flex align-items-center mb-2">
                        <div class="symbol symbol-35px symbol-circle">
                            <img alt="Pic" src="../assets/media/avatars/300-25.jpg">
                        </div>
                        <div class="ms-3">
                           
                        </div>
                    </div>
                    <div class="p-5 rounded bg-light-info text-gray-900 fw-semibold mw-lg-400px text-start" data-kt-element="message-text"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="card-footer pt-4" id="kt_chat_messenger_footer">
        <textarea class="form-control form-control-flush mb-3" id="message" rows="1" data-kt-element="input" placeholder="Type a message"></textarea>
        <div class="d-flex flex-stack">
            <div class="d-flex align-items-center me-2">
                <button class="btn btn-sm btn-icon btn-active-light-primary me-1" type="button" data-bs-toggle="tooltip" aria-label="Coming soon" data-bs-original-title="Coming soon" data-kt-initialized="1">
                    <i class="ki-outline ki-paper-clip fs-3"></i>
                </button>
                <button class="btn btn-sm btn-icon btn-active-light-primary me-1" type="button" data-bs-toggle="tooltip" aria-label="Coming soon" data-bs-original-title="Coming soon" data-kt-initialized="1">
                    <i class="ki-outline ki-exit-up fs-3"></i>
                </button>
            </div>
            <button class="btn btn-primary" id="sendmessage" type="button" data-kt-element="send">Send</button>
        </div>
    </div>
</div>
                        
                        <!--end::Messenger-->
                    </div>
                    <!--end::Content-->
                </div>
                <!--end::Layout-->
                <!--begin::Modals-->


                <!--end::Modals-->
            </div>
            <!--end::Content container-->
        </div>
        <!--end::Content-->
    </div>
    <!--begin::Javascript-->
    <script>var hostUrl = "../assets/";</script>

    <!--begin::Global Javascript Bundle(mandatory for all pages)-->
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
    <script src="../assets/js/scripts.bundle.js"></script>
    <!--end::Global Javascript Bundle-->
    <!--begin::Vendors Javascript(used for this page only)-->
    <script src="../assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/index.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/xy.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/percent.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/radar.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/themes/Animated.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/map.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/worldLow.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/continentsLow.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/usaLow.js"></script>
    <script src="../Scripts/jquery.signalR-2.4.3.min.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/worldTimeZonesLow.js"></script>
    <script src="../Scripts/jquery-1.9.0.min.js"></script>
    <script src="https://cdn.amcharts.com/lib/5/geodata/worldTimeZoneAreasLow.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../assets/plugins/custom/datatables/datatables.bundle.js"></script>

    <!--end::Vendors Javascript-->
    <!--begin::Custom Javascript(used for this page only)-->
    <script src="../assets/js/widgets.bundle.js"></script>
    <script src="../assets/js/custom/widgets.js"></script>
    <script src="../assets/js/custom/apps/chat/chat.js"></script>
    <script src="../assets/js/custom/utilities/modals/upgrade-plan.js"></script>
    <script src="../assets/js/custom/utilities/modals/users-search.js"></script>
    <script src="../Scripts/jquery-1.9.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
        <script src="https://cdn.tutorialjinni.com/intl-tel-input/17.0.19/js/intlTelInput.min.js"></script>
    <script src="../Scripts/jquery-1.9.0.min.js"></script>
        <!--Reference the SignalR library. -->
    <script src="../Scripts/jquery.signalR-2.4.3.min.js"></script>
        <!--Reference the autogenerated SignalR hub script. -->
              <script src="http://localhost:55133/signalr/hubs"></script>
<script>
    hubUrl = "http://localhost:55133/signalr";
    console.log("admin" + hubUrl);

    // Function to render messages
    function renderMessages(messages) {
        var messagesContainer = $('[data-kt-element="messages"]');
        messagesContainer.empty(); // Clear previous messages
        messages.forEach(function (message) {
            var template = message.SenderEmail === currentSenderEmail ? $('[data-kt-element="template-out"]:first') : $('[data-kt-element="template-in"]:first');
            var newMessage = template.clone().removeClass('d-none');

            newMessage.find('[data-kt-element="message-text"]').text(message.Content);
            messagesContainer.append(newMessage);
        });

        messagesContainer.scrollTop(messagesContainer.prop('scrollHeight'));
    }

    // Client-side method to receive messages
    function receiveMessage(senderEmail, message) {
        console.log(message + " from " + senderEmail);
        appendMessage('received', message);
    }

    // Function to append messages to chat
    function appendMessage(type, message) {
        var messagesContainer = $('[data-kt-element="messages"]');
        var template = type === 'sent' ? $('[data-kt-element="template-out"]:first') : $('[data-kt-element="template-in"]:first');
        var newMessage = template.clone().removeClass('d-none');

        newMessage.find('[data-kt-element="message-text"]').text(message);
        messagesContainer.append(newMessage);
        messagesContainer.scrollTop(messagesContainer.prop('scrollHeight'));
    }

    // Function to handle received messages
    function receiveMessage(senderEmail, message) {
        appendMessage('received', message);
    }

    // Function to establish SignalR connection
    function establishConnection(senderEmail, receiverEmail) {
        $.connection.hub.qs = { 'email': senderEmail };

        // Use withUrl method with the dynamic hub URL
        $.connection.hub.url = hubUrl;
        console.log($.connection.hub);

        $.connection.hub.start().done(function () {
            console.log('SignalR connection established.');
            // Render existing messages on connection
            renderMessages(chatMessages || []); // Assuming chatMessages is available globally

            // Bind send message button click event after connection is established
            $('#sendmessage').off('click').on('click', function () {
                var message = $('#message').val();

                // Call server method to send message
                chat.server.sendMessage(senderEmail, receiverEmail, message);
                console.log(receiverEmail + " " + senderEmail + " " + message);
                appendMessage('sent', message);
            });
        }).fail(function (err) {
            console.error('SignalR connection error: ' + err.toString());
        });
    }

    // Function to start chat
    function startchat(senderEmail, receiverEmail) {
        // Clear previous messages
        $('[data-kt-element="messages"]').empty();

        // Set current sender and receiver emails
        currentSenderEmail = senderEmail;
        currentReceiverEmail = receiverEmail;

        // Get contact name text
        var contactName = $('#contactname').text().trim();

        // Set contact name as chat name
        $('#chatname').text(contactName);

        // Establish SignalR connection
        establishConnection(senderEmail, receiverEmail);
    }

    // Define global variables
    var chat = $.connection.chatHub;
    var currentSenderEmail = '';
    var currentReceiverEmail = '';

    // Client-side method to receive messages
    chat.client.receiveMessage = receiveMessage;

    // Initialize chat on document ready
    $(function () {
        // Prompt for sender and receiver emails
        currentReceiverEmail = prompt("Receiver email:");
        currentSenderEmail = prompt("Sender email:");

        // Start chat with the provided emails
        startchat(currentSenderEmail, currentReceiverEmail);
        console.log(chat);
    });
</script>

<!--end::Custom Javascript-->
    <!--end::Javascript-->

</body>
</html>
