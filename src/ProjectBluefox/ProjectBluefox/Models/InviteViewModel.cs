using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Model
{
    public sealed class InviteViewModel
    {
        public string Url { get; private set; }

        public InviteViewModel(string url)
        {
            Url = url;
        }
    }
}