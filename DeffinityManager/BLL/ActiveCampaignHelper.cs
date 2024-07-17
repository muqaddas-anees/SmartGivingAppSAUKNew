using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeffinityManager.BLL
{
    public class ActiveCampaignHelper
    {
        private RestClient client;

        public ActiveCampaignHelper()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var baseUrl = "https://plegituk.api-us1.com";//ConfigurationManager.AppSettings["ActiveCampaignBaseUrl"];
            var apiKey = "758bca26dbc62579e15cbfd6fe061caf3b7ee558e4054cd36d844daefeecaa50164ca87f";// ConfigurationManager.AppSettings["ActiveCampaignApiKey"];
            client = new RestClient(baseUrl);
            client.AddDefaultHeader("Api-Token", apiKey);
        }
        public int? GetContactIdByEmail(string email)
        {
            var request = new RestRequest("api/3/contacts", Method.GET);
            request.AddParameter("filters[email]", email);  // Assuming the API supports this filter

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
                foreach (var contact in data.contacts)
                {
                    if (contact.email == email)
                    {
                        return contact.id;
                    }
                }
            }

            return null;  // No contact found
        }

        public int? EnsureContact(string email, string firstName, string lastName, string organizationName = "")
        {
            // Check if the contact already exists
            var existingContactId = GetContactIdByEmail(email);
            if (existingContactId.HasValue)
            {
                return existingContactId.Value;
            }

            // Create the contact if it does not exist
            return CreateContact(email, firstName, lastName, organizationName);
        }
        public int? CreateContact(string email, string firstName, string lastName, string organizationName = "")
        {
            var request = new RestRequest("api/3/contacts", Method.POST);
            request.AddJsonBody(new
            {
                contact = new
                {
                    email = email,
                    firstName = firstName,
                    lastName = lastName,
                    fieldValues = new List<object>
            {
                new { field = "1", value = organizationName }  // Replace "1" with the actual Field ID of your custom field
            }
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

        public int? GetTagIdByName(string tagName)
        {
            var request = new RestRequest("api/3/tags", Method.GET);
            request.AddParameter("search", tagName);  // Assuming the API supports a search parameter

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
                foreach (var tag in data.tags)
                {
                    if (tag.tag == tagName)
                    {
                        return tag.id;
                    }
                }
            }

            return null;  // No tag found
        }

        public int? EnsureTag(string tagName)
        {
            // Check if the tag already exists
            var existingTagId = GetTagIdByName(tagName);
            if (existingTagId.HasValue)
            {
                return existingTagId.Value;
            }

            // Create the tag if it does not exist
            return CreateTag(tagName);
        }




        public bool RemoveTagFromContact(int contactId, int tagId)
        {
            var request = new RestRequest($"api/3/contactTags", Method.GET);
            request.AddParameter("filters[contact]", contactId);
            request.AddParameter("filters[tag]", tagId);

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
                foreach (var contactTag in data.contactTags)
                {
                    var deleteRequest = new RestRequest($"api/3/contactTags/{contactTag.id}", Method.DELETE);
                    var deleteResponse = client.Execute(deleteRequest);
                    if (!deleteResponse.IsSuccessful)
                    {
                        // Log error or handle it as needed
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public void RemoveTagByEmailAndTagName(string email, string tagName)
        {
            var contactId = GetContactIdByEmail(email);
            if (contactId == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            var tagId = GetTagIdByName(tagName);
            if (tagId == null)
            {
                Console.WriteLine("Tag not found.");
                return;
            }

            if (RemoveTagFromContact(contactId.Value, tagId.Value))
            {
                Console.WriteLine("Tag removed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to remove tag.");
            }
        }
    }
}
