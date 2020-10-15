using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Bayonet_Ticket_Manager
{
    public class Ticket
    {
        private const char Separator = ':';

        public override string ToString()
        {
            string output = "";
            output += "TimeStamp       : " + TimeStamp + "\n";
            output += "User_Name      : " + UserName + "\n";
            output += "User_Email       : " + UserEmail + "\n";
            output += "Hostname        : " + UserHostname + "\n";
            output += "IP_Address      : " + UserIP + "\n";
            output += "Image_URL     : " + ImageURL + "\n";
            output += "Status              : " + Status + "\n";
            output += TicketDescription + "\n\n";
            if(Notes != null)
                output += "Notes       : \n" + Notes + "\n";
            return output;
        }

        public static string DetermineTicketStatus(bool complete, bool progress, bool pending)
        {
            if (complete && !progress && !pending)
                return "*Completed*";
            if (!complete && progress && !pending)
                return "*In Progress*";
            if (!complete && !progress && pending)
                return "*Pending Approval*";
            return "Error";
        }

        Ticket()
        {

        }

        public Ticket(string ticket_id, string user, string email, string hostname, string ip, string image, string description, string dt, string status, string notes)
        {
            this.TicketID = ticket_id;
            this.UserName = user;
            this.UserEmail = email;
            this.UserHostname = hostname;
            this.UserIP = ip;
            this.ImageURL = image;
            this.TicketDescription = description;
            this.TimeStamp = dt;
            this.Status = status;
            this.Notes = notes;
        }

        public static ArrayList InProgressTickets()
        {
            ArrayList progress = new ArrayList();

            JArray all_tickets = API.BayonetTickets();

            foreach (JToken message in all_tickets)
            {
                string msg = message["msg"].ToString();        
                if(!msg.Contains("*In Progress*"))
                    continue;

                string alias = message["alias"].ToString();
                string dt = message["ts"].ToString().Split()[0];
                string id = message["_id"].ToString();
                string user_name = GetTicketUserName(msg);
                string email = GetTicketUserEmail(msg);
                string ip = GetTicketUserIP(msg);
                string image = GetTicketUserImageURL(msg);
                string issue = GetTicketIssues(msg);
                string notes = GetTicketNotes(msg);
                string status = GetTicketStatus(msg);
                Ticket ticket = new Ticket(id, user_name, email, alias, ip, image, issue, dt, status, notes);
                progress.Add(ticket);
            }
            return progress;
        }

        public static ArrayList ActiveTickets()
        {
            ArrayList active = new ArrayList();

            JArray all_tickets = API.BayonetTickets();

            foreach (JToken message in all_tickets)
            {
                string msg = message["msg"].ToString();

                if (!msg.Contains("*Active*"))
                    continue;

                string alias = message["alias"].ToString();
                string dt = message["ts"].ToString().Split()[0];
                string id = message["_id"].ToString();
                string user_name = GetTicketUserName(msg);
                string email = GetTicketUserEmail(msg);
                string ip = GetTicketUserIP(msg);
                string image = GetTicketUserImageURL(msg);
                string issue = GetTicketIssues(msg);
                string notes = GetTicketNotes(msg);
                string status = GetTicketStatus(msg);

                Ticket ticket = new Ticket(id, user_name, email, alias, ip, image, issue, dt, status, notes);
                active.Add(ticket);
            }
            return active;
        }

        public static ArrayList BacklogTickets()
        {
            ArrayList backlog = new ArrayList();
            JArray all_tickets = API.BayonetTickets();

            foreach (JToken message in all_tickets)
            {
                string msg = message["msg"].ToString();
                if (!msg.Contains("*Prending Approval*"))
                    continue;

                string alias = message["alias"].ToString();
                string dt = message["ts"].ToString().Split()[0];
                string id = message["_id"].ToString();
                string user_name = GetTicketUserName(msg);
                string email = GetTicketUserEmail(msg);
                string ip = GetTicketUserIP(msg);
                string image = GetTicketUserImageURL(msg);
                string issue = GetTicketIssues(msg);
                string notes = GetTicketNotes(msg);
                string status = GetTicketStatus(msg);

                Ticket ticket = new Ticket(id, user_name, email, alias, ip, image, issue, dt, status, notes);
                backlog.Add(ticket);
            }
            return backlog;
        }

        public static string[] GetTicketLines(string message)
        {
            string[] message_contents = message.Split('\n');
            return message_contents;
        }

        public static string GetTicketUserName(string message)
        {
            string[] message_contents = GetTicketLines(message);
            foreach(string line in message_contents)
            {
                if (line.Contains("User_Name"))
                    return line.Split(Separator)[1];
            }
            return "No User Name Found";
        }

        public static string GetTicketUserEmail(string message)
        {
            string[] message_contents = GetTicketLines(message);
            foreach (string line in message_contents)
            {
                if (line.Contains("User_Email"))
                {
                    return line.Split(Separator)[1];
                }
            }
            return "No User Email Found";
        }

        public static string GetTicketUserHostname(string message)
        {
            string[] message_contents = GetTicketLines(message);
            foreach (string line in message_contents)
            {
                if (line.Contains("Host_Name"))
                    return line.Split(Separator)[1];
            }
            return "No User HostName Found";
        }

        public static string GetTicketUserIP(string message)
        {
            string[] message_contents = GetTicketLines(message);
            foreach (string line in message_contents)
            {
                if (line.Contains("IP_Address"))
                    return line.Split(Separator)[1];
            }
            return "No User IP Found";
        }

        public static string GetTicketUserImageURL(string message)
        {
            string[] message_contents = GetTicketLines(message);
            foreach (string line in message_contents)
            {
                if (line.Contains("Image_URL"))
                    return line.Split(Separator)[1];
            }
            return "No Image URL Found";
        }

        public static string GetTicketIssues(string message)
        {
            string[] issues = message.Split(new string[] { "Issue:" }, StringSplitOptions.None);
            string issue = issues[1];
            int indexOfSteam = issue.IndexOf("Status");
            if (indexOfSteam >= 0)
                issue = issue.Remove(indexOfSteam);
            return "Issue:\n" + issue;
        }

        public static string[] GetTicketIssuesLines(string message)
        {
            return message.Split(new string[] { "Issue" }, StringSplitOptions.None);
        }

        public static string GetTicketStatus(string message)
        {
            string[] issues = message.Split('\n');
            foreach(string line in issues)
            {
                if(line.Contains("Status"))
                {
                    string ticket_status = line.Split(':')[1];
                    return ticket_status;
                }
            }
            return "Status Not Found";
        }

        public static string GetTicketNotes(string message)
        {
            string notes = "";
            string[] issues = GetTicketIssuesLines(message);
            string[] enteredNotes = issues[1].Split(new string[] { "Notes" }, StringSplitOptions.None);            
            int count = enteredNotes.Count();
            if (count > 1)
                notes = enteredNotes[1];
            else
                notes = "";
            return notes;
        }
 
        public string TicketID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserHostname { get; set; }
        public string UserIP { get; set; }
        public string ImageURL { get; set; }
        public string TicketDescription { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }
        public string Notes { get; set; }
    }
}
