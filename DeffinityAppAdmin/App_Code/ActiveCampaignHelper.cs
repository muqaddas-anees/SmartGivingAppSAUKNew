using RestSharp;
using Newtonsoft.Json;
using System.Configuration;

namespace DeffinityAppDev.App_Code
{
    public class ActiveCampaignHelper
    {
        private RestClient client;

        public ActiveCampaignHelper()
        {
            var baseUrl = "https://plegituk.api-us1.com";//ConfigurationManager.AppSettings["ActiveCampaignBaseUrl"];
            var apiKey = "758bca26dbc62579e15cbfd6fe061caf3b7ee558e4054cd36d844daefeecaa50164ca87f";// ConfigurationManager.AppSettings["ActiveCampaignApiKey"];
            client = new RestClient(baseUrl);
            client.AddDefaultHeader("Api-Token", apiKey);
        }

        public int? CreateContact(string email, string firstName, string lastName)
        {
            var request = new RestRequest("api/3/contacts", Method.POST);
            request.AddJsonBody(new
            {
                contact = new
                {
                    email = email,
                    firstName = firstName,
                    lastName = lastName
                }
            });

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
                int contactId = data.contact.id;
                return contactId;
            }
            else
            {
                // Log error or handle it as needed
                return null;
            }
        }

        public int? CreateTag(string tag)
        {
            var request = new RestRequest("api/3/tags", Method.POST);
            request.AddJsonBody(new
            {
                tag = new
                {
                    tag = tag,
                    tagType = "contact"
                }
            });

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
                int tagId = data.tag.id;
                return tagId;
            }
            else
            {
                // Log error or handle it as needed
                return null;
            }
        }

        public bool AddTagToContact(int contactId, int tagId)
        {
            var request = new RestRequest("api/3/contactTags", Method.POST);
            request.AddJsonBody(new
            {
                contactTag = new
                {
                    contact = contactId,
                    tag = tagId
                }
            });

            var response = client.Execute(request);
            return response.IsSuccessful;
        }
    }
}