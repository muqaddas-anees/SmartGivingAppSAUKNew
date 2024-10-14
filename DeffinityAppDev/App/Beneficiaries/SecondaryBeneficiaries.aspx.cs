using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using DeffinityAppDev.App.Beneficiaries.Entities;
using System.Data.Entity.Infrastructure;
using Microsoft.Ajax.Utilities;


namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class SecondaryBeneficiaries : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindCountryCodes();
                LoadCountryDropdown();
                LoadSecondaryBeneficiaries();

                // Existing code...
            }

            else if (Request["__EVENTTARGET"] == "EditBeneficiary")
            {
                string beneficiaryID = Request["__EVENTARGUMENT"];
                LoadBeneficiaryDetails(beneficiaryID); // Load beneficiary details

                // Set hidden field value to "true"
                hfShowModal.Value = "true";
            }

        }


        private void BindCountryCodes()
        {

            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Afghanistan (+93)", "+93"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Albania (+355)", "+355"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Algeria (+213)", "+213"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("American Samoa (+1684)", "+1684"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Andorra (+376)", "+376"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Angola (+244)", "+244"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Anguilla (+1264)", "+1264"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Antarctica (+672)", "+672"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Antigua and Barbuda (+1268)", "+1268"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Argentina (+54)", "+54"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Armenia (+374)", "+374"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Aruba (+297)", "+297"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Australia (+61)", "+61"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Austria (+43)", "+43"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Azerbaijan (+994)", "+994"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bahamas (+1242)", "+1242"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bahrain (+973)", "+973"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bangladesh (+880)", "+880"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Barbados (+1246)", "+1246"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belarus (+375)", "+375"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belgium (+32)", "+32"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Belize (+501)", "+501"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Benin (+229)", "+229"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bermuda (+1441)", "+1441"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bhutan (+975)", "+975"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bolivia (+591)", "+591"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bosnia and Herzegovina (+387)", "+387"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Botswana (+267)", "+267"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Brazil (+55)", "+55"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("British Indian Ocean Territory (+246)", "+246"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Brunei (+673)", "+673"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Bulgaria (+359)", "+359"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Burkina Faso (+226)", "+226"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Burundi (+257)", "+257"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cambodia (+855)", "+855"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cameroon (+237)", "+237"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Canada (+1)", "+1"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cape Verde (+238)", "+238"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cayman Islands (+1345)", "+1345"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Central African Republic (+236)", "+236"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Chad (+235)", "+235"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Chile (+56)", "+56"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("China (+86)", "+86"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Christmas Island (+61)", "+61"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cocos (Keeling) Islands (+61)", "+61"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Colombia (+57)", "+57"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Comoros (+269)", "+269"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Congo (DRC) (+243)", "+243"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Congo (Republic) (+242)", "+242"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cook Islands (+682)", "+682"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Costa Rica (+506)", "+506"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Côte d’Ivoire (+225)", "+225"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Croatia (+385)", "+385"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cuba (+53)", "+53"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Curaçao (+5999)", "+5999"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Cyprus (+357)", "+357"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Czech Republic (+420)", "+420"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Denmark (+45)", "+45"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Djibouti (+253)", "+253"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Dominica (+1767)", "+1767"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Dominican Republic (+1809, +1829, +1849)", "+1809"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ecuador (+593)", "+593"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Egypt (+20)", "+20"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("El Salvador (+503)", "+503"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Equatorial Guinea (+240)", "+240"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Eritrea (+291)", "+291"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Estonia (+372)", "+372"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Eswatini (+268)", "+268"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ethiopia (+251)", "+251"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Falkland Islands (+500)", "+500"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Faroe Islands (+298)", "+298"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Fiji (+679)", "+679"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Finland (+358)", "+358"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("France (+33)", "+33"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("French Guiana (+594)", "+594"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("French Polynesia (+689)", "+689"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gabon (+241)", "+241"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gambia (+220)", "+220"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Georgia (+995)", "+995"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Germany (+49)", "+49"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ghana (+233)", "+233"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Gibraltar (+350)", "+350"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Greece (+30)", "+30"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Greenland (+299)", "+299"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Grenada (+1473)", "+1473"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guadeloupe (+590)", "+590"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guam (+1671)", "+1671"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guatemala (+502)", "+502"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guernsey (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guinea (+224)", "+224"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guinea-Bissau (+245)", "+245"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Guyana (+592)", "+592"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Haiti (+509)", "+509"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Honduras (+504)", "+504"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Hong Kong (+852)", "+852"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Hungary (+36)", "+36"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iceland (+354)", "+354"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("India (+91)", "+91"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Indonesia (+62)", "+62"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iran (+98)", "+98"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Iraq (+964)", "+964"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ireland (+353)", "+353"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Isle of Man (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Israel (+972)", "+972"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Italy (+39)", "+39"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jamaica (+1876)", "+1876"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Japan (+81)", "+81"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jersey (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Jordan (+962)", "+962"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kazakhstan (+7)", "+7"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kenya (+254)", "+254"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kiribati (+686)", "+686"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("North Korea (+850)", "+850"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Korea (+82)", "+82"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kosovo (+383)", "+383"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kuwait (+965)", "+965"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Kyrgyzstan (+996)", "+996"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Laos (+856)", "+856"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Latvia (+371)", "+371"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lebanon (+961)", "+961"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lesotho (+266)", "+266"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Liberia (+231)", "+231"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Libya (+218)", "+218"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Liechtenstein (+423)", "+423"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Lithuania (+370)", "+370"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Luxembourg (+352)", "+352"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Macau (+853)", "+853"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Madagascar (+261)", "+261"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malawi (+265)", "+265"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malaysia (+60)", "+60"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Maldives (+960)", "+960"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mali (+223)", "+223"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Malta (+356)", "+356"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Marshall Islands (+692)", "+692"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Martinique (+596)", "+596"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mauritania (+222)", "+222"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mauritius (+230)", "+230"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mexico (+52)", "+52"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Micronesia (+691)", "+691"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Moldova (+373)", "+373"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Monaco (+377)", "+377"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mongolia (+976)", "+976"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Montenegro (+382)", "+382"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Montserrat (+1664)", "+1664"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Morocco (+212)", "+212"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Mozambique (+258)", "+258"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Myanmar (+95)", "+95"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Namibia (+264)", "+264"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nauru (+674)", "+674"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nepal (+977)", "+977"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Netherlands (+31)", "+31"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("New Caledonia (+687)", "+687"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("New Zealand (+64)", "+64"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nicaragua (+505)", "+505"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Niger (+227)", "+227"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Nigeria (+234)", "+234"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Niue (+683)", "+683"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Norfolk Island (+672)", "+672"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Northern Mariana Islands (+1670)", "+1670"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Norway (+47)", "+47"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Oman (+968)", "+968"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Pakistan (+92)", "+92"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Palau (+680)", "+680"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Palestine (+970)", "+970"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Panama (+507)", "+507"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Papua New Guinea (+675)", "+675"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Paraguay (+595)", "+595"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Peru (+51)", "+51"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Philippines (+63)", "+63"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Pitcairn Islands (+64)", "+64"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Poland (+48)", "+48"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Portugal (+351)", "+351"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Puerto Rico (+1)", "+1"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Qatar (+974)", "+974"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Réunion (+262)", "+262"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Romania (+40)", "+40"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Russia (+7)", "+7"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Rwanda (+250)", "+250"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Barthélemy (+590)", "+590"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Helena (+290)", "+290"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Kitts and Nevis (+1869)", "+1869"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Lucia (+1758)", "+1758"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Martin (+590)", "+590"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Pierre and Miquelon (+508)", "+508"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saint Vincent and the Grenadines (+1784)", "+1784"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Samoa (+685)", "+685"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("San Marino (+378)", "+378"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("São Tomé and Príncipe (+239)", "+239"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Saudi Arabia (+966)", "+966"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Senegal (+221)", "+221"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Serbia (+381)", "+381"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Seychelles (+248)", "+248"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sierra Leone (+232)", "+232"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Singapore (+65)", "+65"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sint Maarten (+1721)", "+1721"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Slovakia (+421)", "+421"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Slovenia (+386)", "+386"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Solomon Islands (+677)", "+677"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Somalia (+252)", "+252"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Africa (+27)", "+27"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("South Sudan (+211)", "+211"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Spain (+34)", "+34"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sri Lanka (+94)", "+94"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sudan (+249)", "+249"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Suriname (+597)", "+597"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Svalbard and Jan Mayen (+47)", "+47"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Sweden (+46)", "+46"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Switzerland (+41)", "+41"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Syria (+963)", "+963"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Taiwan (+886)", "+886"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tajikistan (+992)", "+992"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tanzania (+255)", "+255"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Thailand (+66)", "+66"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Timor-Leste (+670)", "+670"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Togo (+228)", "+228"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tokelau (+690)", "+690"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tonga (+676)", "+676"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Trinidad and Tobago (+1868)", "+1868"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tunisia (+216)", "+216"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turkey (+90)", "+90"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turkmenistan (+993)", "+993"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Turks and Caicos Islands (+1649)", "+1649"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Tuvalu (+688)", "+688"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uganda (+256)", "+256"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Ukraine (+380)", "+380"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United Arab Emirates (+971)", "+971"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United Kingdom (+44)", "+44"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("United States (+1)", "+1"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uruguay (+598)", "+598"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Uzbekistan (+998)", "+998"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vanuatu (+678)", "+678"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vatican City (+39)", "+39"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Venezuela (+58)", "+58"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Vietnam (+84)", "+84"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Yemen (+967)", "+967"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Zambia (+260)", "+260"));
            ddlPhone.Items.Add(new System.Web.UI.WebControls.ListItem("Zimbabwe (+263)", "+263"));


            // Optionally add a default item
            ddlPhone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Country Code--", "0"));
        }
        private void LoadCountryDropdown()
        {
            //Get all specific cultures
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            // Use a HashSet to store unique country codes
            HashSet<string> countryCodes = new HashSet<string>();

            // Loop over the cultures and extract unique country codes
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    RegionInfo region = new RegionInfo(culture.Name);
                    countryCodes.Add(region.TwoLetterISORegionName);
                }
                catch
                {
                    // Ignore cultures that do not have region information
                }
            }

            // Now, build a list of unique RegionInfo objects from the country codes
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (string countryCode in countryCodes)
            {
                try
                {
                    RegionInfo region = new RegionInfo(countryCode);
                    countries.Add(region);
                }
                catch
                {
                    // Ignore invalid country codes
                }
            }

            // Sort countries by English name
            countries = countries.OrderBy(c => c.EnglishName).ToList();

            // Clear existing items
            ddlCountryModal.Items.Clear();

            // Add default item
            ddlCountryModal.Items.Add(new ListItem("-- Select Country --", ""));

            // Add countries to dropdown
            foreach (RegionInfo country in countries)
            {
                ddlCountryModal.Items.Add(new ListItem(country.EnglishName, country.TwoLetterISORegionName));
            }
        }


        protected void rptSecondaryBeneficiaries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "EditBeneficiary")
            {
                string beneficiaryID = e.CommandArgument.ToString(); // Get the ID of the selected beneficiary
                LoadBeneficiaryDetails(beneficiaryID); // Load details

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "alert('Modal should be shown');", true);
                //asp:Literals Script add Method 
            }
        }


        private void LoadBeneficiaryDetails(string beneficiaryID)
        {
            try
            {
                // Retrieve the PortfolioID from the session
                int portfolioID = sessionKeys.PortfolioID; // Default to 0 if null

                using (var context = new MyDatabaseContext())
                {
                    // Fetch the SecondaryBeneficiary with the specified ID and the correct PortfolioID
                    var beneficiary = context.SecondaryBeneficiary
                                             .FirstOrDefault(b => b.SecondaryBeneficiaryID.ToString() == beneficiaryID
                                                                  && b.TithingID == portfolioID);

                    if (beneficiary != null)
                    {
                        // Populate the modal fields with the retrieved data
                        ddlTypeModal.SelectedValue = beneficiary.Type;
                        ddlGenderModal.SelectedValue = beneficiary.Gender;
                        txtNameModal.Text = beneficiary.Name;
                        txtEmailModal.Text = beneficiary.Email;

                        txtDOBModal.Text = beneficiary.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty;
                        txtIDModal.Text = beneficiary.InternalIDNumber;
                        txtAddressModal.Text = beneficiary.Address;
                        txtTownModal.Text = beneficiary.Town;
                        txtCityModal.Text = beneficiary.City;
                        txtZipModal.Text = beneficiary.PostalCode;
                        ddlCountryModal.SelectedValue = beneficiary.Country;
                        txtBackgroundModal.Text = beneficiary.Background;
                        txtHealthConditionModal.Text = beneficiary.HealthCondition;
                        ddlEmploymentStatusModal.SelectedValue = beneficiary.EmploymentStatus;

                        // Handle PhoneNumber
                        string fullPhoneNumber = beneficiary.PhoneNumber;
                        string countryCode = "";
                        string phoneNumber = "";

                        if (!string.IsNullOrEmpty(fullPhoneNumber))
                        {
                            // Extract the first 3 or 4 characters as the country code (assuming the '+' sign is included)
                            countryCode = fullPhoneNumber.Substring(0, 3); // e.g., "+92"

                            // Some country codes may be longer (4 chars), so adjust accordingly
                            if (ddlPhone.Items.FindByValue(countryCode) == null)
                            {
                                // Try with a longer country code (e.g., "+971")
                                countryCode = fullPhoneNumber.Substring(0, 4);
                            }
                            if (ddlPhone.Items.FindByValue(countryCode) == null)
                            {
                                // Try with a longer country code (e.g., "+971")
                                countryCode = fullPhoneNumber.Substring(0, 5);
                            }

                            // Extract the remaining part of the string as the phone number
                            phoneNumber = fullPhoneNumber.Substring(countryCode.Length);

                            // Set the dropdown for country code
                            if (ddlPhone.Items.FindByValue(countryCode) != null)
                            {
                                ddlPhone.SelectedValue = countryCode;
                            }
                            else
                            {
                                ddlPhone.SelectedIndex = 0; // Default or error handling
                            }

                            // Set the phone number in the text box
                            txtPhone.Text = phoneNumber;
                        }

                        // Handle image data, if needed
                        if (beneficiary.ProfileImage != null)
                        {
                            byte[] imageData = beneficiary.ProfileImage;
                            string base64Image = Convert.ToBase64String(imageData);
                            // You can show the image in the modal
                            // Example: imgProfileImage.Attributes["src"] = "data:image/jpeg;base64," + base64Image;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void SaveSecondaryBeneficiary()
        {


            if (!Page.IsValid)
            {
                DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "please enter a unique Email", "Ok");
                // If the page is not valid (i.e., email validation failed), exit the method
                return;
            }

            // Get the beneficiary ID from the hidden field
            string beneficiaryID = hfBeneficiaryID.Value;

            try
            {
                using (var context = new MyDatabaseContext())
                {
                    

                    // Determine whether we are in "Edit" or "Add" mode
                    SecondaryBeneficiary beneficiary;

                    if (string.IsNullOrEmpty(beneficiaryID))
                    {
                        // Add mode: Insert a new beneficiary
                        beneficiary = new SecondaryBeneficiary();
                        context.SecondaryBeneficiary.Add(beneficiary);
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Secondary Beneficiary Saved Successully", "OK");

                        // Generate a new beneficiary ID (assuming an auto-increment mechanism)
                    }
                    else
                    {
                        // Edit mode: Update existing beneficiary
                        int parsedID = int.Parse(beneficiaryID);
                        beneficiary = context.SecondaryBeneficiary.FirstOrDefault(b => b.SecondaryBeneficiaryID == parsedID && b.TithingID==sessionKeys.PortfolioID);

                        if (beneficiary == null)
                        {
                            
                            return;
                        }
                    }

                    // Assign properties from the form inputs
                    beneficiary.TithingID = sessionKeys.PortfolioID; // Assuming this dropdown is for TithingID
                    beneficiary.Type = ddlTypeModal.SelectedValue;
                    beneficiary.Gender = ddlGenderModal.SelectedValue;
                    beneficiary.Name = txtNameModal.Text.Trim();
                    beneficiary.Email = txtEmailModal.Text.Trim();
                    beneficiary.PhoneNumber = ddlPhone.SelectedValue + txtPhone.Text;
                    beneficiary.DateOfBirth = string.IsNullOrEmpty(txtDOBModal.Text) ? (DateTime?)null : Convert.ToDateTime(txtDOBModal.Text);
                    beneficiary.InternalIDNumber = txtIDModal.Text.Trim();
                    beneficiary.Address = txtAddressModal.Text.Trim();
                    beneficiary.Town = txtTownModal.Text.Trim();
                    beneficiary.City = txtCityModal.Text.Trim();
                    beneficiary.PostalCode = txtZipModal.Text.Trim();
                    beneficiary.Country = ddlCountryModal.SelectedValue;
                    beneficiary.Background = txtBackgroundModal.Text.Trim();
                    beneficiary.HealthCondition = txtHealthConditionModal.Text.Trim();
                    beneficiary.EmploymentStatus = ddlEmploymentStatusModal.SelectedValue;
                    beneficiary.CreatedAt = DateTime.Now;

                    // Handle ProfileImage upload
                    if (fuProfileImage.HasFile)
                    {
                        Debug.WriteLine("File has been uploaded. File name: " + fuProfileImage.FileName);
                        string fileExtension = System.IO.Path.GetExtension(fuProfileImage.FileName).ToLower();
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            byte[] imageData = fuProfileImage.FileBytes;
                            Debug.WriteLine("File size: " + imageData.Length + " bytes.");
                            beneficiary.ProfileImage = imageData;
                        }
                        else
                        {
                            Debug.WriteLine("Invalid file type: " + fileExtension);
                            
                            return; // Exit without saving
                        }
                    }

                    // Save changes to the database
                    context.SaveChanges();
                    LoadSecondaryBeneficiaries();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Secondary Beneficiary Saved Successully", "OK");

                }




            }
         
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                Debug.WriteLine("General Exception: " + ex.Message);
               
            }

        }


       
        protected void btnSave_Click(object sender, EventArgs e)
        {
                try
                {

                    SaveSecondaryBeneficiary();

                    // Clear the form fields after saving
                    ClearForm();

                    // Reload the beneficiaries to include the newly added one
                    //LoadSecondaryBeneficiaries();

                }
                catch (Exception ex)
                {
                LogExceptions.WriteExceptionLog(ex);
                   
                }
        }

        protected void cvEmailUniqueModal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value.Trim();

            try
            {
                using (var context = new MyDatabaseContext())
                {
                    // Check if email already exists in the database
                    bool emailExists = context.SecondaryBeneficiary.Any(b => b.Email == email && b.TithingID==sessionKeys.PortfolioID);

                    if (emailExists)
                    {
                        // Email already exists
                        args.IsValid = false;
                        DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Email Already Exists", "OK");

                    }
                    else
                    {
                        // Email is unique
                        args.IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

                // Log or handle exception as necessary
                // For now, we'll assume it's invalid to prevent data from being saved in case of an error
                args.IsValid = false;

                // Optionally, log the error
                // LogError(ex); // Replace this with your logging method
            }
        }

        
        private void ClearForm()
        {
           
            ddlTypeModal.SelectedIndex = -1; // Reset dropdown
            ddlGenderModal.SelectedIndex = -1; // Reset dropdown
            txtNameModal.Text = string.Empty; // Clear name
            txtDOBModal.Text = string.Empty; // Clear Date of Birth
            txtIDModal.Text = string.Empty; // Clear Internal ID
            txtAddressModal.Text = string.Empty; // Clear Address
            txtTownModal.Text = string.Empty; // Clear Town
            txtCityModal.Text = string.Empty; // Clear City
            txtZipModal.Text = string.Empty; // Clear Zip code
            ddlCountryModal.SelectedIndex = 0; // Reset dropdown
            txtEmailModal.Text = string.Empty;
            txtPhone.Text = string.Empty;
            ddlEmploymentStatusModal.SelectedIndex = -1;
            txtHealthConditionModal.Text = string.Empty;
            txtBackgroundModal.Text = string.Empty;
            ddlPhone.SelectedIndex = 0;

        }
        private void LoadSecondaryBeneficiaries()
        {
            try
            {
                // Retrieve the PortfolioID from the session
                int portfolioID = sessionKeys.PortfolioID;// Default to 0 if null

                using (var context = new MyDatabaseContext())
                {
                    // Fetch data from the database where TithingID matches PortfolioID
                    var beneficiaries = context.SecondaryBeneficiary
                        .Where(b => b.TithingID == portfolioID) // Ensure the TithingID matches PortfolioID
                        .Select(b => new
                        {
                            b.SecondaryBeneficiaryID,
                            b.Gender,
                            b.Name,
                            b.Email,
                            b.DateOfBirth,
                            b.InternalIDNumber,
                            b.PhoneNumber,
                            b.ProfileImage // Fetch the binary image data as-is
                        })
                        .ToList(); // Perform the database query first

                    // Convert ProfileImage to Base64 string in-memory after data retrieval
                    var beneficiariesWithBase64Images = beneficiaries.Select(b => new
                    {
                        b.SecondaryBeneficiaryID,
                        b.Gender,
                        b.Name,
                        b.Email,
                        b.DateOfBirth,
                        b.InternalIDNumber,
                        b.PhoneNumber,
                        ProfileImageBase64 = b.ProfileImage != null
                            ? "data:image/jpeg;base64," + Convert.ToBase64String(b.ProfileImage)
                            : "~/metronic8/demo1/assets/media/avatars/300-1.jpg" // Default placeholder if image is null
                    }).ToList();

                    // Bind the result to the Repeater
                    rptSecondaryBeneficiaries.DataSource = beneficiariesWithBase64Images;
                    rptSecondaryBeneficiaries.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


    }

}