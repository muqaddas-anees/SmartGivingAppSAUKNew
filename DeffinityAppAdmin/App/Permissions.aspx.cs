using DeffinityAppDev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Configuration;
using UserMgt.BAL;

namespace Permissions
{
    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private string GetRoleColor(string role)
        {
            switch (role.ToLower())
            {
                case "administration":
                    return "primary"; // Blue color
                case "team player":
                    return "warning"; // Yellow color
                case "volunteer":
                    return "success"; // Green color
                case "developer":
                    return "danger";
                case "analyst":
                    return "success";
                case "support":
                    return "info";
                case "trial":
                    return "warning";
                default:
                    return "secondary";
            }
        }

         private void LoadData()
          {
              // Fetch contractors
              var contractors = ContractorsBAL.Contractor_SelectAll_WithOutCompany()

                  .ToList();

              // Create a dictionary to map Person_ID to contractors
              var contractorDictionary = contractors.ToDictionary(c => c.ID);
              string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];

              using (var context = new UserPermissionsDataContext(ConnectionString))
              {
                  var personCategories = context.Person_Categories.ToList();
                  var categories = context.Categories.ToList();
                  var responseData = new
                  {
                      PersonCategories = personCategories,
                      Categories = categories
                  };

                  // Serialize the object to JSON
             
                  // Write the script tag to the response

                  // Write the script tag to the response
                  var data = from pc in personCategories
                             join c in categories on pc.Category_ID equals c.Category_ID
                             where contractorDictionary.ContainsKey(pc.Person_ID)
                             select new
                             {
                                 CategoryName = c.Category_Name,
                                 Contractor = contractorDictionary[pc.Person_ID],
                                 CreatedDate = contractorDictionary[pc.Person_ID].CreatedDate
                             };

                foreach (var item in data)
                  {
                      string categoryName = item.CategoryName;
                      string contractorName = item.Contractor.ContractorName;
                      string contractorRole = item.Contractor.Type;
                      DateTime createdDate = item.CreatedDate ?? DateTime.MinValue;

                      string roleClass = GetRoleColor(contractorRole);
                      string roleHtml = $"<a href=\"apps/user-management/roles/view.html\" class=\"badge badge-light-{roleClass} fs-7 m-1\">{contractorRole}</a>";

                    string rowHtml = $@"
<tr class='table-row-dark'>
    <td class='min-w-125px sorting text-center' style='width: 200px;'>{categoryName}</td>
    <td class='min-w-125px sorting_disabled text-center' style='width: 250px;'>{contractorName}</td>
    <td class='text-end min-w-125px sorting_disabled text-center' style='width: 200px;'>{roleHtml}</td>
    <td class='min-w-125px sorting text-center' style='width: 200px;' data-order='{createdDate:yyyy-MM-ddTHH:mm:sszzz}'>{createdDate:dd MMM yyyy, hh:mm tt}</td>
</tr>";


                    // Add the rowHtml to the table body
                    tablepermission.InnerHtml += rowHtml;
                  }
              }
          }
        

        // Dummy method to simulate your GetRoleColor method
     

    }
}
