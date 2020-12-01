using System;
using System.Collections;
using System.Windows.Forms;

namespace Bayonet_Ticket_Manager
{
    public partial class Backlog : Form
    {
        ArrayList backlogTickets = new ArrayList();

        public Backlog()
        {
            InitializeComponent();
        }

        public Ticket getTicket()
        {
            string ticket_info = pendingListBox.SelectedItem.ToString();
            string[] ticket_split = ticket_info.Split('.');
            string host_name = ticket_split[0];
            string dt = ticket_split[1];
            foreach (Ticket ticket in backlogTickets)
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
            return null;
        }

        public void discard()
        {
            pendingListBox.SelectedIndex = -1;
            reasonTextBox.Text = "";
            approvedCheckBox.Checked = false;
            deniedCheckBox.Checked = false;
            populateTicketBox();
        }

        public void populateTicketBox()
        {
            backlogTickets = Ticket.BacklogTickets();

            foreach (Ticket ticket in backlogTickets)
                pendingListBox.Items.Add(ticket.UserHostname + "." + ticket.TicketID);
        }

        private void Backlog_Load(object sender, EventArgs e)
        {
            populateTicketBox();
        }

        private void expandButton_Click(object sender, EventArgs e)
        {
            descriptionTextBox.Text = "";
            string active = "";

            if (pendingListBox.SelectedItem != null)
                active = pendingListBox.SelectedItem.ToString();

            if (active.Length == 0)
            {
                MessageBox.Show("Please select a ticket to expand.");
                return;
            }

            Ticket ticket = getTicket();
            ;
            if (ticket == null)
            {
                MessageBox.Show("Error.");
                return;
            }

            descriptionTextBox.Text = ticket.ToString();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            populateTicketBox();
        }

        public string determineCheckBox()
        {
            if (approvedCheckBox.Checked && deniedCheckBox.Checked)
                return "BOTH";
            if (!approvedCheckBox.Checked && !deniedCheckBox.Checked)
                return "NONE";
            if (approvedCheckBox.Checked)
                return "APPROVED";
            if (deniedCheckBox.Checked)
                return "DENIED";
            return "Error";
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (descriptionTextBox.Text.Length < 0)
            {
                MessageBox.Show("Please expand a ticket first.");
                return;
            }

            string status = determineCheckBox();
            if (status.Equals("Error") || status.Equals("BOTH") || status.Equals("NONE"))
            {
                MessageBox.Show("Please select Approved or Denied before submitting an update.");
                return;
            }

            string reason = reasonTextBox.Text;
            if (reason.Length < 0)
            {
                MessageBox.Show("Please enter a reason for the approval or denying of selected ticket.");
                return;
            }

            Ticket ticket = getTicket();

            if (deniedCheckBox.Checked == true)
            {
                ticket.Status = "*Completed*";
                string notes = "Your ticket has been denied by the Network Administrator for the following reason:\n" + reasonTextBox.Text;
                notes += "\n\nIf you feel like this has been done in error, please contact Matt at extension 250.";
                notes += "\n\nThis is an automated message, replies to this account are not monitored.";
                ticket.Notes = notes;

                API.updateMessage(ticket.ToString(), API.TICKET_ROOM(), ticket.TicketID);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
