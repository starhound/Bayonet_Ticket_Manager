using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace Bayonet_Ticket_Manager
{
    class API
    {
        static string AUTH_TOKEN;
        static string USER_ID;
        const string ROOM_ID = "axwJ7pXTwWM9JPJ9f";
        const string API_URL = "https://bayonetchat.com/api/v1/";
        const string BOT_NAME = "bayonet.tickets";
        const string BOT_PASSWORD = "bayonet-9";

        public static RestClient client = new RestClient(API_URL);

        public static string TICKET_ROOM()
        {
            return ROOM_ID;
        }

        public static JArray BayonetTickets()
        {
            client.Authenticator = new SimpleAuthenticator("user", BOT_NAME, "password", BOT_PASSWORD);
            var request = new RestRequest("login", Method.POST);
            var response = client.Execute(request);
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            var data = content.data;

            //grab auth token and bot id
            string auth = data.authToken.ToString();
            string userId = data.userId.ToString();

            AUTH_TOKEN = auth;
            USER_ID = userId;

            var ticket_client = new RestClient(API_URL);

            var group_request = new RestRequest("groups.history", Method.GET);
            group_request.AddQueryParameter("count", "100");
            group_request.AddQueryParameter("roomId", ROOM_ID);

            group_request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            group_request.AddHeader("X-User-Id", USER_ID);
            group_request.AddHeader("Content-Type", "application/json");

            var ticket_response = ticket_client.Execute(group_request);

            dynamic ticket_data = JsonConvert.DeserializeObject(ticket_response.Content);

            JArray messages = ticket_data.messages;
            return messages;
        }

        /// <summary>
        /// Finds and matches a user name from RC using a supplied email
        /// </summary>
        /// <param name="email">RC linked email address</param>
        /// <returns></returns>
        public static string getUserName(string email)
        {
            var client = new RestClient(API_URL);

            var request = new RestRequest("users.list", Method.GET);

            request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            request.AddHeader("X-User-Id", USER_ID);
            request.AddHeader("Content-Type", "application/json");


            var response = client.Execute(request);
            dynamic data = JsonConvert.DeserializeObject(response.Content);

            var users = data.users;

            foreach (var user in users)
            {
                string userEmail = user["emails"][0]["address"].ToString();
                if (email.Equals(userEmail))
                {
                    return user["username"].ToString();
                }
            }
            return "Error";
        }

        /// <summary>
        /// Sends a direct message containing the notes and status to a user
        /// </summary>
        /// <param name="notes">Notes to be added to the ticket</param>
        /// <param name="status">Status for the ticket to be set to</param>
        /// <param name="user_name">User to notify</param>
        public static void notifyUser(string notes, string status, string user_email)
        {
            string trimmed = user_email.Trim(' ');
            string user_name = getUserName(trimmed);
            if(user_name.Equals("Error"))
            {
                MessageBox.Show("No email found for auto reply");
                return;
            }
            var client = new RestClient(API_URL);

            var request = new RestRequest("chat.postMessage", Method.POST);

            request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            request.AddHeader("X-User-Id", USER_ID);
            request.AddHeader("Content-Type", "application/json");

            string msg = "Your ticket has been marked as " + status + " by IT Staff with the following notes:\n";
            msg += "_" + notes + "_" + Environment.NewLine;
            msg += "\nThis is an automated message, replies and direct messages to this account are not monitored.\n";

            request.AddJsonBody((new { channel = "@" + user_name, text = msg, }));
            client.Execute(request);
        }

        /// <summary>
        /// Updates the ticket message 
        /// </summary>
        /// <param name="msg">Ticket message for updating</param>
        /// <param name="room">Designated tickets channel</param>
        /// <param name="id">ID of RC message to alter</param>
        public static void updateMessage(string msg, string room, string id)
        {
            var client = new RestClient(API_URL);

            var request = new RestRequest("chat.update", Method.POST);

            request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            request.AddHeader("X-User-Id", USER_ID);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody((new { roomId = room, msgId = id, text = msg }));

            client.Execute(request);
        }
    }
}
