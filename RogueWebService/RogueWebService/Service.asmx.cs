using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Web.Services;

#if DEBUG
using RogueWebService.RogueDBTableAdapters;
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

        [WebMethod]
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
        public DataTable GetUserInfo (string username, string password)
        {
            USERSTableAdapter users = new USERSTableAdapter();
            return users.GetDataByLogin(username, password);
        }

        [WebMethod]
        public DataTable GetCharacters (string userid)
        {
            CHARACTERSTableAdapter characters = new CHARACTERSTableAdapter();
            return characters.GetData(int.Parse(userid));
        }
    }
}
