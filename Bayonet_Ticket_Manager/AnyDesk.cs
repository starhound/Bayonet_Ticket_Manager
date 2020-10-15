using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayonet_Ticket_Manager
{
    class AnyDesk
    {

        public AnyDesk()
        {

        }

        public static void RemoteConnect(string ticket_text)
        {
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
    }
}
