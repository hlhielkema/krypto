using ProjectBluefox.Database.Enums;

namespace ProjectBluefox.Models
{
    public class AccountInfo
    {
        public string EmailAddress { get; set; }

        public string Username { get; set; }

        public bool Enabled { get; set; }

        public int Role { get; set; }

        public string RoleName
            => ((AccountRole)Role).ToString();
    }
}