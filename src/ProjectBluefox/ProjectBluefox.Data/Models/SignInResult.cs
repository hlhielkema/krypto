namespace ProjectBluefox.Models
{
    public class SignInResult
    {
        public bool Ok { get; private set; }

        public string Username { get; private set; }

        public string Reason { get; set; }        

        public SignInResult(bool ok, string username)
        {
            Ok = ok;
            Username = username;
        }
    }
}