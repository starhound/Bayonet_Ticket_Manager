using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Bayonet_Ticket_Manager
{
    public partial class Form1 : Form
    {
        //ArrayList tickets = new ArrayList();
        ArrayList progressTickets = new ArrayList();
        ArrayList activeTickets = new ArrayList();
        ArrayList backlogTickets = new ArrayList();

        private static Timer timer;

        public bool isInProgressSelected()
        {
            if(inProgressTicketBox.SelectedIndex == -1)
                return false;      
            return true;
        }

        public bool isActiveSelected()
        {
            if (activeTicketsListBox.SelectedIndex == -1)
                return false;
            return true;
        }

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
            completedCheckBox.Checked = false;
            inProgressCheckBox.Checked = false;
            pendingCheckBox.Checked = false;
            activeTicketsListBox.Items.Clear();
            inProgressTicketBox.Items.Clear();
            progressTickets = null;
            activeTickets = null;
            backlogTickets = null;
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
            activeTickets = Ticket.ActiveTickets();
            progressTickets = Ticket.InProgressTickets();

            foreach(Ticket ticket in activeTickets)
               activeTicketsListBox.Items.Add(ticket.UserHostname + "." + ticket.TicketID);
            foreach (Ticket ticket in progressTickets)
                inProgressTicketBox.Items.Add(ticket.UserHostname + "." + ticket.TicketID);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            populateTicketBox();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            inProgressTicketBox.Items.Clear();
            activeTicketsListBox.Items.Clear();
            populateTicketBox();
        }

        public Ticket getTicket()
        {
            if((isInProgressSelected() && isActiveSelected()) || (!isInProgressSelected() && !isActiveSelected()))
            {
                MessageBox.Show("Somehow you've managed to select tickets from both or none of the ticket list boxes. Good job.");
                return null;
            }

            if(isInProgressSelected())
            {
                string ticket_info = inProgressTicketBox.SelectedItem.ToString();
                string[] ticket_split = ticket_info.Split('.');
                string host_name = ticket_split[0];
                string dt = ticket_split[1];
                foreach (Ticket ticket in progressTickets)
                {
                    string user_name = ticket.UserHostname;
                    if (user_name == host_name)
                    {
                        string ticket_time = ticket.TicketID;
                        if (ticket_time == dt)
                        {
                            return ticket;
                        }
                    }
                }
            } 

            if(isActiveSelected())
            {
                string ticket_info = activeTicketsListBox.SelectedItem.ToString();
                string[] ticket_split = ticket_info.Split('.');
                string host_name = ticket_split[0];
                string dt = ticket_split[1];
                foreach (Ticket ticket in activeTickets)
                {
                    string user_name = ticket.UserHostname;
                    if (user_name == host_name)
                    {
                        string ticket_time = ticket.TicketID;
                        if (ticket_time == dt)
                        {
                            return ticket;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Grabs the ticket message from its JToken from the list box
        /// </summary>
        /// <returns>ticket message contents or error string</returns>
        public string getTicketMessage(int box)
        {
            if(box == 1)
            {
                string ticket_info = activeTicketsListBox.SelectedItem.ToString();
                string[] ticket_split = ticket_info.Split('.');
                string host_name = ticket_split[0];
                string id = ticket_split[1];
                foreach (Ticket ticket in activeTickets)
                {
                    string user_name = ticket.UserHostname;
                    if (user_name == host_name)
                    {
                        string ticket_id = ticket.TicketID;
                        if (ticket_id == id)
                            return ticket.ToString().Replace("\n", Environment.NewLine);
                    }
                }
                return "Error";
            } 

            if(box == 2)
            {
                string ticket_info = inProgressTicketBox.SelectedItem.ToString();
                string[] ticket_split = ticket_info.Split('.');
                string host_name = ticket_split[0];
                string id = ticket_split[1];
                foreach (Ticket ticket in progressTickets)
                {
                    string user_name = ticket.UserHostname;
                    if (user_name == host_name)
                    {
                        string ticket_time = ticket.TicketID;
                        if (ticket_time == id)
                            return ticket.ToString().Replace("\n", Environment.NewLine);
                    }
                }
                return "Error";
            }
            return "Ticket Not Found";
        }

        private void expandButton_Click(object sender, EventArgs e)
        {
            ticketDescriptionTextBox.Text = "";
            string progress = "";
            string active = "";

            if(activeTicketsListBox.SelectedItem != null)
                 active = activeTicketsListBox.SelectedItem.ToString();
            if(inProgressTicketBox.SelectedItem != null)
                 progress = inProgressTicketBox.SelectedItem.ToString();

            if(active.Length == 0 && progress.Length == 0)
            {
                MessageBox.Show("Please select a ticket to expand.");
                return;
            }

            int box;
            if (active.Length > 0)
                box = 1;
            else
                box = 2;

            string msg = getTicketMessage(box);

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
        /// <returns>new ticket status</returns>
        public string determineCheckBox()
        {
            bool complete = completedCheckBox.Checked;
            bool progress = inProgressCheckBox.Checked;
            bool pending = pendingCheckBox.Checked;
            return Ticket.DetermineTicketStatus(complete, progress, pending);
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
            Ticket ticket = getTicket();
            //pull ticket from message
            string msg = ticket.TicketDescription;

            //grab message ID from ticket object
            string msgId = ticket.TicketID;

            //grab room ID as well
            string roomId = API.TICKET_ROOM();

            string old_notes = ticket.Notes;
            if (old_notes.Length > 0)
                ticket.Notes += "\n" + Environment.UserName + " - " + notes;
            else
                ticket.Notes = Environment.UserName + " - " + notes;

            ticket.Status = status;
            API.updateMessage(ticket.ToString(), roomId, msgId);

            if(ticket.UserEmail.Length > 0 && !ticket.UserEmail.Equals("No User Email Found"))
            {
                API.notifyUser(notes, status, ticket.UserEmail);
            } 
            else
            {
                MessageBox.Show("No user email found for auto-reply");
            }
            
            //obvious black magic 
            //gets the user email from the ticket issue string
            /*string strRegex = @"[A-Za-z0-9_\-\+]+@[A-Za-z0-9\-]+\.([A-Za-z]{2,3})(?:\.[a-z]{2})?";

            Regex myRegex = new Regex(strRegex, RegexOptions.None);
            string strTargetString = msg;

            foreach (Match myMatch in myRegex.Matches(strTargetString))
            {
                if (myMatch.Success)
                {
                    string user_name = API.getUserName(myMatch.Value);
                    if(user_name.Equals("Error"))
                    {
                        MessageBox.Show("User name not found for auto-reply.");
                        discard();
                        return;
                    }

                    //send a PM to user with updates and notes
                    API.notifyUser(notes, status, user_name);
                }
            }*/

            //clean up ticket values and re-display active tickets
            discard();
        }

        private void remoteButton_Click(object sender, EventArgs e)
        {
            string ticket_text = ticketDescriptionTextBox.Text;
            if(ticket_text.Length == 0)
            {
                MessageBox.Show("Please select a ticket before attempting to remote in.");
                return;
            }
            AnyDesk.RemoteConnect(ticket_text);
        }

        private void backlogButton_Click(object sender, EventArgs e)
        {
            Backlog backlog = new Backlog();
            backlog.Show();
        }

        private void inProgressTicketBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeTicketsListBox.ClearSelected();   
        }

        private void activeTicketsListBox_SelectedIndexChanged(object sender, EventArgs e)
        { 
            inProgressTicketBox.ClearSelected();
        }
    }
}
