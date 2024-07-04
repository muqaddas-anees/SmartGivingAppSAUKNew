using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeffinityManager
{
    public class RandomPassword
    {

        public static string GetPassword()
        {
            string str = "prayer1!#;faith2!#;divine3!#;sacred4!#;holy5!#;devotion6!#;spiritual7!#;pious8!#;rightous9!#;orthodox1!#;holy2!#";
            List<string> getItems = str.Split(';').ToList();

            return getItems.OrderBy(o => Guid.NewGuid()).FirstOrDefault();

        }
        public static string GetCustomerPortalPassword()
        {
            string str = "prayer1!#;faith2!#;divine3!#;sacred4!#;holy5!#;devotion6!#;spiritual7!#;pious8!#;rightous9!#;orthodox1!#;holy2!#";
            List<string> getItems = str.Split(';').ToList();

            return getItems.OrderBy(o => Guid.NewGuid()).FirstOrDefault();

        }
    }
}
