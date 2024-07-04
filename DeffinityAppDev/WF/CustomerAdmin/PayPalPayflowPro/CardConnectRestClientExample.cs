using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro
{
    public class ReponseValues
    {
        public string key { set; get; }
        public string value { set; get; }
    }
    public class CardConnectRestClientExt
    {
        private static String BILLINGPOINT = "https://api-uat.cardconnect.com/billingplan/create";
        private static String ENDPOINT = "https://fts-uat.cardconnect.com/cardconnect/rest";
        private static String USERNAME = "testing";
        private static String PASSWORD = "testing123";

        private static String merchid_value = "840000000054";


        public static String authMID(string mid, string username, string password, string url = "https://fts.cardconnect.com/cardconnect/rest")
        {


            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(url, username, password);

            // Send an AuthTransaction request
            JObject response = client.inquireMarchant(mid);

            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }

            return (String)response.GetValue("enabled");
        }

        public static String authMID(string mid)
        {
            

            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an AuthTransaction request
            JObject response = client.inquireMarchant(mid);

            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }

            return (String)response.GetValue("retref");
        }

        //public static void Main(String[] args)
        //{
        //    // Send an Auth Transaction request
        //    String retref = authTransaction();
        //    // Void transaction
        //    voidTransaction(retref);

        //    // Send an Auth Transaction w/ user fields
        //    retref = authTransactionWithUserFields();
        //    // Inquire transaction
        //    inquireTransaction(retref);

        //    // Send an Auth w/ Capture
        //    retref = authTransactionWithCapture();
        //    // Void 
        //    voidTransaction(retref);

        //    // Send normal Auth
        //    retref = authTransaction();
        //    // Explicit capture
        //    captureTransaction(retref);

        //    // Settlement Status
        //    settlementStatusTransaction();

        //    // Deposit Status
        //    depositTransaction();

        //    // Auth with Profile
        //    String profileid = authTransactionWithProfile();

        //    // Get profile
        //    getProfile(profileid);

        //    // Delete profile
        //    deleteProfile(profileid);

        //    // Create profile
        //    addProfile();

        //    Console.ReadLine();
        //}

        /**
        * Authorize Transaction REST Example
        * @return
        */
        public static String authTransaction()
        {
            Console.WriteLine("\nAuthorization Request");

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Card Type
            request.Add("accttype", "VI");
            // Card Number
            request.Add("account", "4788250000121443");
            // Card Expiry
            request.Add("expiry", "1025");
            // Card CCV2
            request.Add("cvv2", "776");
            // Transaction amount
            request.Add("amount", "100");
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("orderid", "12345");
            // Cardholder Name
            request.Add("name", "Test User");
            // Cardholder Address
            request.Add("Street", "123 Test St");
            // Cardholder City
            request.Add("city", "TestCity");
            // Cardholder State
            request.Add("region", "TestState");
            // Cardholder Country
            request.Add("country", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");
            // Return a token for this card number
            request.Add("tokenize", "Y");

            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an AuthTransaction request
            JObject response = client.authorizeTransaction(request);

            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }

            return (String)response.GetValue("retref");
        }

        /**
        * Authorize Transaction with User Fields REST Example
        * @return
        */
        public static String authTransactionWithUserFields()
        {
            Console.WriteLine("\nAuthorization With User Fields Request");

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Card Type
            request.Add("accttype", "VI");
            // Card Number
            request.Add("account", "4444333322221111");
            // Card Expiry
            request.Add("expiry", "0914");
            // Card CCV2
            request.Add("cvv2", "776");
            // Transaction amount
            request.Add("amount", "100");
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("orderid", "12345");
            // Cardholder Name
            request.Add("name", "Test User");
            // Cardholder Address
            request.Add("Street", "123 Test St");
            // Cardholder City
            request.Add("city", "TestCity");
            // Cardholder State
            request.Add("region", "TestState");
            // Cardholder Country
            request.Add("country", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");
            // Return a token for this card number
            request.Add("tokenize", "Y");

            // Create user fields
            JArray fields = new JArray();
            JObject field = new JObject();
            field.Add("Field1", "Value1");
            fields.Add(field);
            request.Add("userfields", fields);

            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an AuthTransaction request
            JObject response = client.authorizeTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }

            return (String)response.GetValue("retref");
        }


        /**
         * Authorize Transaction With Capture REST Example
         * @return
         */
        public static String authTransactionWithCapture()
        {
            Console.WriteLine("\nAuthorization With Capture Request");

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Card Type
            request.Add("accttype", "VI");
            // Card Number
            request.Add("account", "4444333322221111");
            // Card Expiry
            request.Add("expiry", "0914");
            // Card CCV2
            request.Add("cvv2", "776");
            // Transaction amount
            request.Add("amount", "100");
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("orderid", "12345");
            // Cardholder Name
            request.Add("name", "Test User");
            // Cardholder Address
            request.Add("Street", "123 Test St");
            // Cardholder City
            request.Add("city", "TestCity");
            // Cardholder State
            request.Add("region", "TestState");
            // Cardholder Country
            request.Add("country", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");
            // Return a token for this card number
            request.Add("tokenize", "Y");
            // Capture auth
            request.Add("capture", "Y");

            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an AuthTransaction request
            JObject response = client.authorizeTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }

            return (String)response.GetValue("retref");
        }


        /**
         * Authorize Transaction with Profile REST Example
         * @return
         */
        public static String authTransactionWithProfile()
        {
            Console.WriteLine("\nAuthorization With Profile Request");

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Card Type
            request.Add("accttype", "VI");
            // Card Number
            request.Add("account", "4788250000121443");
            // Card Expiry
            request.Add("expiry", "1025");
            // Card CCV2
            request.Add("cvv2", "776");
            // Transaction amount
            request.Add("amount", "100");
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("orderid", "12345");
            // Cardholder Name
            request.Add("name", "First Profile 1 Test User");
            // Cardholder Address
            request.Add("Street", "First Profile 123 Test St");
            // Cardholder City
            request.Add("city", "TestCity");
            // Cardholder State
            request.Add("region", "TestState");
            // Cardholder Country
            request.Add("country", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");
            // Return a token for this card number
            request.Add("tokenize", "Y");
            // Create Profile
            request.Add("profile", "Y");

            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an AuthTransaction request
            JObject response = client.authorizeTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }

            return (String)response.GetValue("profileid");
        }


        public static List<ReponseValues> authTransactionWithProfile_Recurring(string _ENDPOINT, string _USERNAME, string _PASSWORD,
           string mid, string account, string accountType, string expiry, string cvv, string amount,
           string orderid, string name, string street, string city, string region, string country, string postal,string startdate)
        {

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", mid);
            // Card Type
            request.Add("accttype", accountType);
            // Card Number
            request.Add("account", account);
            // Card Expiry
            request.Add("expiry", expiry);
            // Card CCV2
            request.Add("cvv2", cvv);
            // Transaction amount
            request.Add("amount", string.Format("{0:F2}", Convert.ToDouble(amount)));
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("orderid", orderid);
            // Cardholder Name
            request.Add("name", name);
            // Cardholder Address
            request.Add("Street", street);
            // Cardholder City
            request.Add("city", city);
            // Cardholder State
            request.Add("region", region);
            // Cardholder Country
            // request.Add("country", country);
            // Cardholder Zip-Code
            //request.Add("postal", postal);
            // Return a token for this card number
            request.Add("tokenize", "Y");
            // Create Profile
            request.Add("profile", "Y");
            //ecomind
            //r is recurring
            request.Add("ecomind", "R");
            //capture
            request.Add("capture", "Y");
            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(_ENDPOINT, _USERNAME, _PASSWORD);
            // Send an AuthTransaction request
            JObject response = client.authorizeTransaction(request);
            List<ReponseValues> rValues = new List<ReponseValues>();
            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                rValues.Add(new ReponseValues { key = key, value = value.ToString() });
            }

           // var profileid = rValues.Where(o => o.key == "profileid").FirstOrDefault() != null ? rValues.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;


            //if (!string.IsNullOrEmpty(profileid))
            //{
            //    //Get the profile id create bullin plan
            //    JObject requestBilling = new JObject();
            //    //requestBilling.Add("billingPlanId", null);

            //    requestBilling.Add("merchid", mid);
            //    // Card Type

            //    requestBilling.Add("profileId", profileid);
            //    // Card Number
            //    // requestBilling.Add("acctId", 1);

            //    // Transaction amount
            //    requestBilling.Add("amount", string.Format("{0:F2}", Convert.ToDouble(amount)));
            //    // Transaction currency
            //    //timeSpan		number	yes	1 for daily, 2 for weekly, 3 for monthly, or 4 for yearly
            //    requestBilling.Add("timeSpan", 3);

            //    requestBilling.Add("every", 1);
            //    requestBilling.Add("untilCondition", "C");
            //    requestBilling.Add("untilNumPayments", null);
            //    requestBilling.Add("untilDate", null);

            //    requestBilling.Add("currencySymbol", "$");
            //    requestBilling.Add("startDate", startdate);
            //    requestBilling.Add("billingPlanName", name + " - billing plan");
            //    requestBilling.Add("planStatus", "A");

            //    CardConnectRestClient clientBilling = new CardConnectRestClient(BILLINGPOINT, _USERNAME, _PASSWORD);
            //    // Send an AuthTransaction request
            //    JObject responseBilling = clientBilling.authorizeTransaction(requestBilling);
            //    List<ReponseValues> rValuesBilling = new List<ReponseValues>();
            //    // Handle response
            //    foreach (var x in responseBilling)
            //    {
            //        String key = x.Key;
            //        JToken value = x.Value;
            //        rValues.Add(new ReponseValues { key = key, value = value.ToString() });
            //    }
            //}
            return rValues;
        }

        public static List<ReponseValues> authTransactionWithProfile(string _ENDPOINT,string _USERNAME,string _PASSWORD,
            string mid,string account,string accountType,string expiry,string cvv,string amount,
            string orderid,string name,string street, string city,string region,string country,string postal)
        {

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", mid);
            // Card Type
            request.Add("accttype", accountType);
            // Card Number
            request.Add("account", account);
            // Card Expiry
            request.Add("expiry", expiry);
            // Card CCV2
           // request.Add("cvv2", cvv);
            // Transaction amount
            request.Add("amount", string.Format("{0:F2}", Convert.ToDouble(amount)));
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("orderid", orderid);
            // Cardholder Name
            request.Add("name", name);
            // Cardholder Address
            request.Add("Street", "NY");
            // Cardholder City
            //request.Add("city", city);
            // Cardholder State
           // request.Add("region", region);
            // Cardholder Country
           // request.Add("country", country);
            // Cardholder Zip-Code
            //request.Add("postal", postal);
            // Return a token for this card number
           // request.Add("tokenize", "Y");
            // Create Profile
            request.Add("profile", "Y");
            //ecomind
            request.Add("ecomind", "E");
            //capture
            request.Add("capture", "Y");
            // Create the REST client
            CardConnectRestClient client = new CardConnectRestClient(_ENDPOINT, _USERNAME, _PASSWORD);
            // Send an AuthTransaction request
            JObject response = client.authorizeTransaction(request);
            List<ReponseValues> rValues = new List<ReponseValues>();
            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                rValues.Add(new ReponseValues{ key = key, value = value.ToString() });
            }
            return rValues; 
        }
        /**
         * Capture Transaction REST Example
         * @param retref
         */
        public static void captureTransaction(String retref)
        {
            Console.WriteLine("\nCapture Transaction Request");

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Transaction amount
            request.Add("amount", "100");
            // Transaction currency
            request.Add("currency", "USD");
            // Order ID
            request.Add("retref", retref);
            // Purchase Order Number
            request.Add("ponumber", "12345");
            // Tax Amount
            request.Add("taxamnt", "007");
            // Ship From ZipCode
            request.Add("shipfromzip", "11111");
            // Ship To Zip
            request.Add("shiptozip", "11111");
            // Ship to County
            request.Add("shiptocountry", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");

            // Line item details
            JArray items = new JArray();
            // Singe line item
            JObject item = new JObject();
            item.Add("lineno", "1");
            item.Add("material", "12345");
            item.Add("description", "Item Description");
            item.Add("upc", "0001122334455");
            item.Add("quantity", "5");
            item.Add("uom", "each");
            item.Add("unitcost", "020");
            items.Add(item);
            // Add items to request
            request.Add("items", items);

            // Authorization Code from auth response
            request.Add("authcode", "0001234");
            // Invoice ID
            request.Add("invoiceid", "0123456789");
            // Order Date
            request.Add("orderdate", "20140131");
            // Total Order Freight Amount
            request.Add("frtamnt", "1");
            // Total Duty Amount
            request.Add("dutyamnt", "1");

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send a captureTransaction request
            JObject response = client.captureTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }
        }


        /**
         * Void Transaction REST Example
         * @param retref
         */
        public static void voidTransaction(String retref)
        {
            Console.WriteLine("\nVoid Transaction Request");

            // Create Update Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Transaction amount
            request.Add("amount", "0");
            // Transaction currency
            request.Add("currency", "USD");
            // Return Reference code from authorization request
            request.Add("retref", retref);

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send a voidTransaction request
            JObject response = client.voidTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }
        }


        public static void voidTransaction(string _ENDPOINT, string _USERNAME, string _PASSWORD, string retref,string mid,string amount,string currency="USD" )
        {
            Console.WriteLine("\nVoid Transaction Request");

            // Create Update Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", mid);
            // Transaction amount
            request.Add("amount", amount);
            // Transaction currency
            request.Add("currency", currency);
            // Return Reference code from authorization request
            request.Add("retref", retref);

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(_ENDPOINT, _USERNAME, _PASSWORD);

            // Send a voidTransaction request
            JObject response = client.voidTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }
        }
        /**
         * Refund Transaction REST Example
         * @param retref
         */
        public static void refundTransaction(String retref)
        {
            Console.WriteLine("\nRefund Transaction Request");

            // Create Update Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Transaction amount
            request.Add("amount", "-100");
            // Transaction currency
            request.Add("currency", "USD");
            // Return Reference code from authorization request
            request.Add("retref", retref);

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an refundTransaction request
            JObject response = client.refundTransaction(request);

            // Handle response
            foreach (var x in response)
            {
                String key = x.Key;
                JToken value = x.Value;
                Console.WriteLine(key + ": " + value.ToString());
            }
        }


        /**
         * Inquire Transaction REST Example
         * @param retref
         */
        public static void inquireTransaction(String retref)
        {
            Console.WriteLine("\nInquire Transaction Request");
            String merchid = merchid_value;

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Send an inquire Transaction request
            JObject response = client.inquireTransaction(merchid, retref);

            // Handle response
            if (response != null)
            {
                foreach (var x in response)
                {
                    String key = x.Key;
                    JToken value = x.Value;
                    Console.WriteLine(key + ": " + value.ToString());
                }
            }
        }


        /**
         * Settlement Status REST Example
         */
        public static void settlementStatusTransaction()
        {
            Console.WriteLine("\nSettlement Status Transaction Request");
            // Merchant ID
            String merchid = merchid_value;
            String date = "0401";

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            JArray responses = client.settlementStatus(merchid, date);
            //JSONArray responses = client.settlementStatus(null, null);

            // Handle response
            if (responses != null)
            {
                foreach (JObject response in responses)
                {
                    foreach (var x in response)
                    {
                        String key = x.Key;
                        JToken value = x.Value;
                        if ("txns".Equals(key))
                        {
                            Console.WriteLine("transactions: ");
                            foreach (JObject txn in value)
                            {
                                Console.WriteLine("  ===");
                                foreach (var t in txn)
                                {
                                    String tkey = t.Key;
                                    JToken tvalue = t.Value;
                                    Console.WriteLine("  " + tkey + ": " + tvalue.ToString());
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(key + ": " + response.GetValue(key));
                        }
                    }
                }
            }
        }


        /** 
         * Deposit Transaction REST Example
         */
        public static void depositTransaction()
        {
            Console.WriteLine("\nDeposit Transaction Request");
            // Merchant ID
            String merchid = merchid_value;
            // Date
            String date = "20140131";

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            JObject response = client.depositStatus(merchid, date);

            // Handle response
            if (response != null)
            {
                foreach (var x in response)
                {
                    String key = x.Key;
                    JToken value = x.Value;
                    if ("txns".Equals(key))
                    {
                        Console.WriteLine("transactions: ");
                        foreach (JObject txn in value)
                        {
                            Console.WriteLine("  ===");
                            foreach (var t in txn)
                            {
                                String tkey = t.Key;
                                JToken tvalue = t.Value;
                                Console.WriteLine("  " + tkey + ": " + tvalue.ToString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(key + ": " + response.GetValue(key));
                    }
                }
            }
        }


        /**
         * Get Profile REST Example
         * @param profileid
         */
        private static void getProfile(String profileid)
        {
            Console.WriteLine("\nGet Profile Request");
            // Merchant ID
            String merchid = merchid_value;
            // Account ID
            String accountid = "1";

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Retrieve profile from Profile Service
            JArray response = client.profileGet(profileid, accountid, merchid);

            // Handle response
            if (response != null)
            {
                foreach (JObject obj in response)
                {
                    foreach (var x in obj)
                    {
                        String xkey = x.Key;
                        JToken xvalue = x.Value;
                        Console.WriteLine(xkey + ": " + xvalue.ToString());
                    }
                }
            }
        }


        /**
         * Delete Profile REST Example
         * @param profileid
         */
        private static void deleteProfile(String profileid)
        {
            Console.WriteLine("\nDelete Profile Request");
            // Merchant ID
            String merchid = merchid_value;
            String accountid = "";

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Delete profile using Profile Service
            JObject response = client.profileDelete(profileid, accountid, merchid);

            // Handle response
            if (response != null)
            {
                foreach (var x in response)
                {
                    String xkey = x.Key;
                    JToken xvalue = x.Value;
                    Console.WriteLine(xkey + ": " + xvalue.ToString());
                }
            }
        }


        /**
         * Add Profile REST Example
         */
        private static void addProfile()
        {
            Console.WriteLine("\nAdd Profile Request");

            // Create Profile Request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", merchid_value);
            // Default account
            request.Add("defaultacct", "Y");
            // Card Number
            request.Add("account", "4788250000121443");
            // Card Expiry
            request.Add("expiry", "1025");
            // Cardholder Name
            request.Add("name", "First Profile");
            // Cardholder Address
            request.Add("address", "First Profile 123 Test St");
            // Cardholder City
            request.Add("city", "TestCity");
            // Cardholder State
            request.Add("region", "TestState");
            // Cardholder Country
            request.Add("country", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");

            // Create the CardConnect REST client
            CardConnectRestClient client = new CardConnectRestClient(ENDPOINT, USERNAME, PASSWORD);

            // Create profile using Profile Service
            JObject response = client.profileCreate(request);

            // Handle response
            foreach (var x in response)
            {
                String xkey = x.Key;
                JToken xvalue = x.Value;
                Console.WriteLine(xkey + ": " + xvalue.ToString());
            }
        }
    }
}