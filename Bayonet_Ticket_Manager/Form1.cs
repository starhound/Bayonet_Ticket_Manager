using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Bayonet_Ticket_Manager
{
    public partial class Form1 : Form
    {
        string AUTH_TOKEN;
        string USER_ID;
        const string ROOM_ID = "";
        const string API_URL = "";
        const string BOT_NAME = "";
        const string BOT_PASSWORD = "";

        ArrayList tickets = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Clears values and resets application
        /// </summary>
        public void discard()
        {
            notesTextBox.Text = "";
            ticketDescriptionTextBox.Text = "";
            notesTextBox.Text = "";
            completedCheckBox.Checked = false;
            inProgressCheckBox.Checked = false;
            pendingCheckBox.Checked = false;
            activeTicketsListBox.Items.Clear();
            populateTicketBox();
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            discard();
        }

        /// <summary>
        /// Populates the ticket list box.
        /// </summary>
        public void populateTicketBox()
        {
            var client = new RestClient(API_URL);

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
            group_request.AddQueryParameter("roomId", ROOM_ID);

            group_request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            group_request.AddHeader("X-User-Id", USER_ID);
            group_request.AddHeader("Content-Type", "application/json");

            var ticket_response = ticket_client.Execute(group_request);

            dynamic ticket_data = JsonConvert.DeserializeObject(ticket_response.Content);

            JArray messages = ticket_data.messages;

            foreach (JToken message in messages)
            {
                string msg = message["msg"].ToString();
                if (msg.Contains("Status: *Completed*"))
                    continue;
                tickets.Add(message);
                string alias = message["alias"].ToString();
                string dt = message["ts"].ToString();
                activeTicketsListBox.Items.Add(alias + " " + dt);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            activeTicketsListBox.Items.Clear();
            populateTicketBox();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            activeTicketsListBox.Items.Clear();
            populateTicketBox();
        }

        /// <summary>
        /// Grabs the JToken ticket from the ArrayList of active tickets
        /// </summary>
        /// <returns></returns>
        public JToken getTicket()
        {
            string selected = activeTicketsListBox.GetItemText(activeTicketsListBox.SelectedItem);
            var firstSpaceIndex = selected.IndexOf(" ");
            var host_name = selected.Substring(0, firstSpaceIndex);
            var dt = selected.Substring(firstSpaceIndex + 1);
            foreach (JToken ticket in tickets)
            {
                string user_name = ticket["alias"].ToString();
                if (user_name == host_name)
                {
                    string ticket_time = ticket["ts"].ToString();
                    if (ticket_time == dt)
                    {
                        return ticket;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Grabs the ticket message from its JToken from the list box
        /// </summary>
        /// <returns>ticket message contents or error string</returns>
        public string getTicketMessage()
        {
            string selected = activeTicketsListBox.GetItemText(activeTicketsListBox.SelectedItem);
            var firstSpaceIndex = selected.IndexOf(" ");
            var host_name = selected.Substring(0, firstSpaceIndex);
            var dt = selected.Substring(firstSpaceIndex + 1);
            foreach (JToken ticket in tickets)
            {
                string user_name = ticket["alias"].ToString();
                if (user_name == host_name)
                {
                    string ticket_time = ticket["ts"].ToString();
                    if (ticket_time == dt)
                    {
                        string msg = ticket["msg"].ToString();
                        return msg.Replace("\n", Environment.NewLine);
                    }
                }
            }
            return "Error";
        }

        private void expandButton_Click(object sender, EventArgs e)
        {
            ticketDescriptionTextBox.Text = "";

            if(activeTicketsListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a ticket to expand.");
                return;
            }

            string msg = getTicketMessage();

            if(msg.Equals("Error"))
            {
                MessageBox.Show("Error.");
                return;
            }

            ticketDescriptionTextBox.Text = msg;
        }

        /// <summary>
        /// Determines the status for the ticket, returns error in string form on bad selections
        /// </summary>
        /// <returns></returns>
        public string determineCheckBox()
        {
            bool complete = completedCheckBox.Checked;
            bool progress = inProgressCheckBox.Checked;
            bool pending = pendingCheckBox.Checked;

            if (complete && !progress && !pending)
                return "Completed";
            if (!complete && progress && !pending)
                return "In Progress";
            if (!complete && !progress && pending)
                return "Pending";
            return "Error";
        }

        /// <summary>
        /// Sends a direct message containing the notes and status to a user
        /// </summary>
        /// <param name="notes">Notes to be added to the ticket</param>
        /// <param name="status">Status for the ticket to be set to</param>
        /// <param name="user_name">User to notify</param>
        public void notifyUser(string notes, string status, string user_name)
        {
            var client = new RestClient(API_URL);

            var request = new RestRequest("chat.postMessage", Method.POST);

            request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            request.AddHeader("X-User-Id", USER_ID);
            request.AddHeader("Content-Type", "application/json");

            string msg = "Your ticket has been marked as *" + status + "* by IT Staff with the following notes:\n";
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
        public void updateMessage(string msg, string room, string id)
        {
            var client = new RestClient(API_URL);

            var request = new RestRequest("chat.update", Method.POST);

            request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            request.AddHeader("X-User-Id", USER_ID);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody((new { roomId = room, msgId = id, text = msg }));

            client.Execute(request);
        }

        /// <summary>
        /// Finds and matches a user name from RC using a supplied email
        /// </summary>
        /// <param name="email">RC linked email address</param>
        /// <returns></returns>
        public string getUserName(string email)
        {
            var client = new RestClient(API_URL);

            var request = new RestRequest("users.list", Method.GET);

            request.AddHeader("X-Auth-Token", AUTH_TOKEN);
            request.AddHeader("X-User-Id", USER_ID);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            dynamic data = JsonConvert.DeserializeObject(response.Content);

            var users = data.users;

            foreach(var user in users)
            {
                string userEmail = user["emails"][0]["address"].ToString();
                if (email.Equals(userEmail))
                    return user["username"].ToString();
            }
            return "Error";
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //ensure single status box is checked
            string checkBox = determineCheckBox();
            if(checkBox.Equals("Error"))
            {
                MessageBox.Show("Please select a ticket status box.");
                return;
            }

            //things get awful here 

            //collect new notes
            string notes = notesTextBox.Text;
            string status = determineCheckBox();

            //grab ticket object
            JToken ticket = getTicket();

            //pull ticket from message
            string msg = ticket["msg"].ToString();

            //grab message ID from ticket object
            string msgId = ticket["_id"].ToString();

            //grab room ID as well
            string roomId = ticket["rid"].ToString();

            //for checking if this ticket has had notes entered already
            bool has_notes = false;
            bool has_status = false;

            //seperates the message into a string array by lines
            string[] old_message = msg.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            //if old message has notes and status in it, update the old message
            for(int i = 0; i < old_message.Length; i++)
            {
                //find status line, update status
                if (old_message[i].Contains("Status:"))
                {
                    //specify new status
                    old_message[i] = "Status: " + status;

                    //flag as having status already
                    has_status = true;
                }

                //find notes line, update notes
                if (old_message[i].Contains("Notes:"))
                {
                    //chop off the notes section, grab only content
                    var old_notes_content = old_message[i].Substring(old_message[i].LastIndexOf(':') + 1);

                    //append new note content to old notes, reform note section
                    string new_notes = "Notes:\n" + old_notes_content + "\n" + notes;

                    //set new notes into old message array
                    old_message[i] = new_notes;

                    //flag as having notes present already
                    has_notes = true;
                }
            }

            //formulate new message with notes and status
            string newMsg = msg + "\n" + "Status: *" + status + "*\nNotes: " + notes;

            //overwrite new message with updated old message
            if (has_notes || has_status)
            {
                newMsg = String.Join("\n", old_message);
            }

            updateMessage(newMsg, roomId, msgId);

            //obvious black magic 
            //gets the user email from the ticket issue string
            string strRegex = @"[A-Za-z0-9_\-\+]+@[A-Za-z0-9\-]+\.([A-Za-z]{2,3})(?:\.[a-z]{2})?";

            Regex myRegex = new Regex(strRegex, RegexOptions.None);
            string strTargetString = msg;

            foreach (Match myMatch in myRegex.Matches(strTargetString))
            {
                if (myMatch.Success)
                {
                    string user_name = getUserName(myMatch.Value);
                    if(user_name.Equals("Error"))
                    {
                        MessageBox.Show("User name not found for auto-reply.");
                        discard();
                        return;
                    }

                    //send a PM to user with updates and notes
                    notifyUser(notes, status, user_name);
                }
            }

            //clean up ticket values and re-display active tickets
            discard();
            activeTicketsListBox.Items.Clear();
            populateTicketBox();
        }

        private void remoteButton_Click(object sender, EventArgs e)
        {
            string ticket_text = ticketDescriptionTextBox.Text;
            if(ticket_text.Length == 0)
            {
                MessageBox.Show("Please select a ticket before attempting to remote in.");
                return;
            }

            string[] ticket = ticket_text.Split(':');
            string[] hostnameData = ticket[3].Split('\n');

            string hostname = hostnameData[0] + "@ad";
            string pwd = "/K echo BayonetDesk! | ";
            string application = "\"AnyDesk-2dfb0b0a_msi.exe\" ";
            string anyDesk = pwd + application + hostname + " --with-password --silent";

            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            proc1.WorkingDirectory = @"C:\Program Files (x86)\AnyDesk-2dfb0b0a_msi\";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Arguments = anyDesk;
            Process.Start(proc1);
        }

        private void backlogButton_Click(object sender, EventArgs e)
        {
            Backlog backlog = new Backlog();
            backlog.Show();
        }
    }
}
