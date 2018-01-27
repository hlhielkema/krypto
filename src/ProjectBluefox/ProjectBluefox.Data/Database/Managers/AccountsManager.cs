using ProjectBluefox.Database.Enums;
using ProjectBluefox.Database.Tables;
using ProjectBluefox.Database.Util;
using ProjectBluefox.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Database.Managers
{
    public class AccountsManager
    {
        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns></returns>
        public static List<AccountInfo> GetAccounts()
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Create the query
                var query = from x in accounts
                            select new AccountInfo()
                            {
                                EmailAddress = x.EmailAddress,
                                Username = x.Username,
                                Enabled = x.Enabled,
                                Role = x.Role,
                                LastLogon = x.LastLogon,
                            };

                // Execute the query
                return query.ToList();
            }
        }        

        /// <summary>
        /// Authenticate account credentials
        /// </summary>
        /// <param name="username">email address or username</param>
        /// <param name="password">password</param>
        /// <returns>sign-in result</returns>
        public static SignInResult Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return new SignInResult(false, username)
                {
                    Reason = "Invalid Credentials"
                };

            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Get the account for the username
                AccountTable account = accounts.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() || x.EmailAddress.ToLower() == username.ToLower());

                // Return false if the account does not exist
                if (account == null)
                {
                    return new SignInResult(false, username)
                    {
                        Reason = "Invalid Credentials"
                    };
                }

                if (!account.Enabled)
                {
                    return new SignInResult(false, account.Username)
                    {
                        Reason = "Account Disabled"
                    };
                }

                // Check the password hash
                bool validPassword = BCrypt.CheckPassword(password, account.Password);

                if (validPassword)
                {
                    return new SignInResult(true, account.Username);
                }
                else
                {
                    return new SignInResult(false, username)
                    {
                        Reason = "Invalid Credentials"
                    };
                }
            }
        }

        /// <summary>
        /// Gets if an account has a role of one of the higher role
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="role">role</param>
        /// <returns>true=has role or higher role, false=does not</returns>
        public static bool HasRole(string username, AccountRole role)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Get the account for the username
                AccountTable account = accounts.FirstOrDefault(x => x.Username == username);
                
                return account != null && account.Enabled && account.Role >= (int)role;                
            }
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="email">email address</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public static bool CreateAccount(string username, string email, string password)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Check if the username and e-mail address are free
                if (accounts.Any(x => x.Username.ToLower() == username.ToLower()))
                    return false;
                if (accounts.Any(x => x.EmailAddress.ToLower() == email.ToLower()))
                    return false;

                // Insert the new user
                accounts.InsertOnSubmit(new AccountTable()
                {
                    Username = username.ToLower(),
                    EmailAddress = email.ToLower(),
                    Password = BCrypt.HashPassword(password, BCrypt.GenerateSalt()),
                    Enabled = true,
                    Role = (int)AccountRole.Editor,
                });

                // Submit the changes
                connection.SubmitChanges();

                // Success
                return true;
            }
        }

        /// <summary>
        /// Set the enabled state of an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="enabled">new enabled state</param>
        /// <returns>true=state updated, false=state did not changed</returns>
        public static bool SetAccountEnabled(string username, bool enabled)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Get the account
                AccountTable account = accounts.FirstOrDefault(x => x.Username == username);
                if (account == null)
                    throw new InvalidOperationException("Account not found");

                // Only continue if the new enabled state is different from the current enabled state
                if (account.Enabled != enabled)
                {
                    // Set the enabled state
                    account.Enabled = enabled;

                    // Submit the changes
                    connection.SubmitChanges();

                    // Success
                    return true;
                }
                else
                    // No state change
                    return false;
            }
        }

        /// <summary>
        /// Set the password of an account
        /// </summary>
        /// <param name="username">account username</param>
        /// <param name="password">new plain text password</param>
        public static void SetAccountPassword(string username, string password)
        {
            // Connect to the MSSQL database
            using (MSSqlConnection connection = MSSqlConnection.GetConnection())
            {
                // Get the table
                Table<AccountTable> accounts = connection.GetTable<AccountTable>();

                // Get the account
                AccountTable account = accounts.FirstOrDefault(x => x.Username == username);
                if (account == null)
                    throw new InvalidOperationException("Account not found");

                string passwordHash = BCrypt.HashPassword(password, BCrypt.GenerateSalt());

                // Update the password
                account.Password = BCrypt.HashPassword(password, BCrypt.GenerateSalt());

                // Submit the changes
                connection.SubmitChanges();                
            }
        }

        /// <summary>
        /// Get the database id of an account
        /// </summary>
        /// <param name="connection">MSSQL database connection</param>
        /// <param name="username">account username</param>
        /// <returns>account database id</returns>
        public static int GetAccountId(MSSqlConnection connection, string username)
        {
            // Get the table
            Table<AccountTable> accounts = connection.GetTable<AccountTable>();

            // Get the account
            AccountTable account = accounts.FirstOrDefault(x => x.Username == username);
            if (account == null)
                throw new InvalidOperationException("Account not found");

            // Return the account id
            return account.Id;
        }
    }
}