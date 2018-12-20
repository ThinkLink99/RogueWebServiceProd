using System.Collections.Generic;
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
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public object[] GetUserInfo (string username, string password)
        {
            USERSTableAdapter users = new USERSTableAdapter();
            return new object[] { users.GetDataByLogin(username, password)[0]["USER_ID"], users.GetDataByLogin(username, password)[0]["USER_ID"] };
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
