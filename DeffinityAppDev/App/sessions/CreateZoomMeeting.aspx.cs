//using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.Demo_Sample
{
    public partial class CreateZoomMeeting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //CreateZoomLink();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var apiSecret = "eOZc1SvAXNUTVLQuZ5Wfr2KQp5qdhgLnoilu";
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "pwA2av8IQWumZZQsa7COIg",
                    Expires = now.AddSeconds(800),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);





                //var client = new RestClient("https://api.zoom.us/v2/users/chethanreddy.ems@gmail.com/meetings");

                var client = new RestClient("https://api.zoom.us/v2/users/indra@deffinity.com/meetings");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { topic = "Meeting with Indra", duration = "10", start_time = "2022-06-16T05:00:00", type = "2" });
                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));



                IRestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                var jObject = JObject.Parse(restResponse.Content);
                Host.Text = (string)jObject["start_url"];
                Join.Text = (string)jObject["join_url"];
                Code.Text = Convert.ToString(numericStatusCode);

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }




        protected void Button1_Click1(object sender, EventArgs e)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var apiSecret = "eOZc1SvAXNUTVLQuZ5Wfr2KQp5qdhgLnoilu";
            byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "pwA2av8IQWumZZQsa7COIg",
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var client = new RestClient("https://api.zoom.us/v2/users/indra@deffinity.com/meetings");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { topic = "Meeting with Indra", duration = "10", start_time = "2022-06-15T05:00:00", type = "2" });

            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            var jObject = JObject.Parse(restResponse.Content);



            Host.Text = (string)jObject["start_url"];
            Join.Text = (string)jObject["join_url"];
            Code.Text = Convert.ToString(numericStatusCode);


        }





        public void CreateZoomLink()
        {
            System.Net.HttpWebRequest request = System.Net.HttpWebRequest.Create("https://api.zoom.us/v1/meeting/create 89") as System.Net.HttpWebRequest;

                //request.Method = "get";

            request.Method = "POST";
            request.ContentType = "application/json"; //formatting the search string so that it wont give  media type error
            request.MediaType = "application/json";
            request.Accept = "application/json";
            request.KeepAlive = true;

          //  request.Headers.Add("Content-type", "application/json");
         //   request.Headers.Add("authorization", "Bearer  xxxxxxxxxx");


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                api_key = "JKnOKOhMRMqbrE_01tvCag",

                api_secret = "wAcHGaptiIESm478FDJ2DeAbhDQiitIpI9yT",

                data_type = "application/json",

                host_id = "GC5g5_4-SI6OduvEN0Z5u",

                topic = "Testing my API",

                type = "1",

                registration_type = "1",

                option_audio = "both",

                option_auto_record_type = "local"

            });

            try
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(json);

                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                string newstream = request.GetRequestStream().ToString();


                string res = request.GetResponse().ToString();


                //System.IO.Stream newStream = request.GetRequestStream();
                //newStream.Write(data, 0, data.Length);
                //newStream.Close();

                // instantiate a new response object

                System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse;


               // string newstream = request.GetRequestStream().ToString();

                System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream());

                string returnedData = r.ReadToEnd();

                System.Console.WriteLine(returnedData);
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}