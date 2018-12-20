using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Services;

#if DEBUG
#else
using RogueWebService.RogueDBTableAdapters;
#endif

namespace RogueWebService
{
    

    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "https://roguewebservice.azurewebsites.net/RogueWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        #region hidden
        const string gmailPassword = "u1R6zNsg";
        #endregion

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public void Email (string subject, string message, string email)
        {
            MailMessage mail = new MailMessage("RogueWS@gmail.com", email);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("linkin562@gmail.com", gmailPassword);

            mail.Subject = subject;
            mail.Body = message;

            client.Send(mail);
        }

        /// <summary>
        /// Returns an array containing the USER_ID and DISPLAYNAME given by the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author: Trey Hall
        /// Date: 12/20/18
        /// </remarks>
        [WebMethod]
        public object[] GetUserInfo (string username, string password)
        {
            USERSTableAdapter users = new USERSTableAdapter();

            string id = "", displayname = "";

            try
            {
                id = users.GetDataByLogin(username, password)[0]["USER_ID"].ToString();
                displayname = users.GetDataByLogin(username, password)[0]["USER_ID"].ToString();
            }
            catch(Exception ex)
            {
                id = "";
                displayname = "";

                Email("RogueWS Error - GetUserInfo", "GetUserInfo broke with this exception: " + ex.Message, "linkin562@gmail.com");
            }

            object[] obj = new object[] { id, displayname };
            return obj;
        }

        [WebMethod]
        public List<object[]> GetCharacters (string userid)
        {
            CHARACTERSTableAdapter characters = new CHARACTERSTableAdapter();
            int r = characters.GetData(int.Parse(userid)).Rows.Count,
                c = characters.GetData(int.Parse(userid)).Columns.Count;

            List<object[]> Characters = new List<object[]>();
            object[] character_records = new object[r];

            for(int i = 0; i < r; i++)
            {
                for(int j = 0; j < c; j++)
                {
                    character_records[j] = characters.GetData(int.Parse(userid))[i][j];
                }
                Characters.Add(character_records);
            }

            return Characters;
        }
    }
}
