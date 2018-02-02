using ProjectBluefox.Database.Enums;
using System;

namespace ProjectBluefox.Models
{
    public class AccountInfo
    {
        public string EmailAddress { get; set; }

        public string Username { get; set; }

        public bool Enabled { get; set; }

        public int Role { get; set; }

        public DateTime? LastLogon { get; set; }

        public string RoleName
            => ((AccountRole)Role).ToString();

        public string FormattedLastLogon
            => LastLogon?.ToString("dd-MM-yyyy HH:mm") ?? "never";
    }
}