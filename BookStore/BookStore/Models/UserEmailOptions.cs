using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class UserEmailOptions
    {

        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }

    }
}
