using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Bayonet_Ticket_Manager
{
    class Ticket
    {
        private string user_name;
        private string user_email;
        private string host;
        private string ip_address;
        private string image_url;
        private string ticket_description;
        private string status;
        private string notes;

        Ticket()
        {

        }

        Ticket(string user, string email, string hostname, string ip, string image, string description)
        {
            this.user_name = user;
            this.user_email = email;
            this.host = hostname;
            this.ip_address = ip;
            this.image_url = image;
            this.ticket_description = description;
        }

        public string UserName
        {
            get 
            { 
                return this.user_name; 
            }
            set 
            { 
                this.user_name = value; 
            }
        }

        public string UserEmail
        {
            get 
            { 
                return this.user_email; 
            }
            set 
            { 
                this.user_email = value; 
            }
        }

        public string UserHostname
        {
            get 
            { 
                return this.host;  
            }
            set
            {
                this.host = value;
            }
        }

        public string UserIP
        {
            get
            {
                return this.ip_address;
            }
            set
            {
                this.ip_address = value;
            }
        }

        public string ImageURL
        {
            get
            {
                return this.image_url;
            }
            set
            {
                this.image_url = value;
            }
        }

        public string TicketDescription
        {
            get
            {
                return this.ticket_description;
            }
            set
            {
                this.ticket_description = value;
            }
        }

        public string GetStatus
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string GetNotes
        {
            get
            {
                return this.notes;
            }
            set
            {
                this.notes = value;
            }
        }
    }
}
