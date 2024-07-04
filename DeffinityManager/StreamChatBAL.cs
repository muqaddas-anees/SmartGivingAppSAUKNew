using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StreamChat;
using PortfolioMgt.Entity;
using System.Net;

namespace DeffinityManager
{

    public static class ClientFactory
    {
        private static string _apiKey = Deffinity.systemdefaults.GetChatkey();
        private static string _apiSecret = Deffinity.systemdefaults.GetChatSecret();
        private static StreamChat.Client _defaultClient = new StreamChat.Client(_apiKey, _apiSecret,
                new StreamChat.ClientOptions
                {
                    Timeout = 10000
                });

        public static StreamChat.IClient GetClient() => _defaultClient;

        public static StreamChat.IClient GetClient(HttpClient httpClient)
            => new StreamChat.Client(
                _apiKey,
                _apiSecret,
                httpClient,
                new StreamChat.ClientOptions { Timeout = 10000 }
            );

    }
    public class StreamChatBAL
    {
        private IClient _client;
        private IUsers _endpoint;
        private IChannel _myChannel;
        public StreamChatBAL()
        {
            _client = ClientFactory.GetClient();
            _endpoint = _client.Users;
           //_myChannel = _client.Channel.Channel();
           // _client.cha
        }
       

        // user having chat login
        public  void CreateUser(int userid, int portfolioid)
        {
            try
            {

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //
                var euserdetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == userid).FirstOrDefault();
                if (euserdetails != null)
                {
                    if (string.IsNullOrEmpty(euserdetails.Chat_ID.Trim()))
                    {
                        string chatid = euserdetails.ContractorName.ToString().Trim().Replace(".", "").Replace("@", "").Replace(" ", "-") + "-" + euserdetails.ID.ToString();

                        CreateUser(chatid, euserdetails.ContractorName);
                        var token = _client.CreateToken(chatid);
                        //update chat token
                        UserMgt.BAL.ContractorsBAL.UpdateUserChatDetails(euserdetails.ID, chatid, token);
                        //create channel 

                    }


                    euserdetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == userid).FirstOrDefault();
                    if (!string.IsNullOrEmpty(euserdetails.Chat_ID.Trim()))
                        CreateChannelAddMembers(portfolioid, userid);
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        //Create a channel add memebers
        public async Task CreateChannelAddMembers(int portfolioid, int userid)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //check channel name is not there
                var euserdetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == userid).FirstOrDefault();
                if (euserdetails != null)
                {
                    IPortfolioRepository<ProjectPortfolio> pr = new PortfolioRepository<ProjectPortfolio>();
                    var pDetails = pr.GetAll().Where(o => o.ID == portfolioid).FirstOrDefault();
                    if (pDetails != null)
                    {
                        var channel_id = pDetails.PortFolio.Replace(" ", "-");
                        if (string.IsNullOrEmpty(pDetails.Chat_ChannelID))
                        {
                            //var s = await this._client.GetChannelType("messaging");
                            var chan = this._client.Channel("messaging", channel_id);
                            //  var chanFromDB = await chan.Create(euserdetails.Chat_ID, new string[] { euserdetails.Chat_ID });
                            //var newData = new GenericData();
                            ////  newData.SetData("team", "red"); // if multi-tenant enabled
                            //newData.SetData("group", pDetails.PortFolio + "-1");
                            //newData.SetData("name", pDetails.PortFolio);
                            //await chan.Update(newData);
                            //// or partial update
                            ////await chan.PartialUpdate(new PartialUpdateChannelRequest
                            ////{
                            ////    Unset = new List<string> { "team" },
                            ////    Set = new Dictionary<string, object> { { "group", "gamma" } }
                            ////});
                            ///

                            await chan.Create(euserdetails.Chat_ID);
                            //update channel name
                            pDetails.Chat_ChannelID = channel_id;
                            //update chanel details
                            pr.Edit(pDetails);
                            // await chan.AddMembers(new string[] { chat_userid } );
                            if (euserdetails.SID == 1)
                                await chan.AddModerators(new string[] { euserdetails.Chat_ID });
                            else
                                await chan.AddMembers(new string[] { euserdetails.Chat_ID });



                          
                        }
                        //else
                        {
                            var chan = _client.Channel("messaging", channel_id);
                            if (euserdetails.SID == 1)
                                await chan.AddModerators(new string[] { euserdetails.Chat_ID });
                            else
                                await chan.AddMembers(new string[] { euserdetails.Chat_ID });
                            //await chan.(new string[] { chat_userid });

                        }

                        // chanFromDB.Members.ForEach(m => Console.WriteLine(m.User.ID));
                    }

                }

                //Update channel id in database

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public async Task AddChannelAddMembers(int userid,int portfolioid)
        {
            //check channel name is not there
            var euserdetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.ID == userid).FirstOrDefault();
            IPortfolioRepository<ProjectPortfolio> pr = new PortfolioRepository<ProjectPortfolio>();
            var pDetails = pr.GetAll().Where(o => o.ID == portfolioid).FirstOrDefault();

            var chan = _client.Channel("messaging", pDetails.Chat_ChannelID);
            if (euserdetails.SID == 1)
                await chan.AddModerators(new string[] { euserdetails.Chat_ID });
            else
                await chan.AddMembers(new string[] { euserdetails.Chat_ID });
            //await chan.(new string[] { chat_userid });



        }

        public async Task CreateUser(string userid,string username)
        {
            var user1 = new User()
            {
                ID = userid,
                Role = Role.Admin,

                
            };
            user1.SetData("name", username);
            await this._endpoint.Upsert(user1);

            await this._endpoint.MarkAllRead(user1.ID);
        }
    }
}
