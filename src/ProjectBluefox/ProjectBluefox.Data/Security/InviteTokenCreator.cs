using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBluefox.Data.Security
{
    public sealed class InviteTokenCreator
    {
        /// <summary>
        /// Create a machine unique token of 8 charachters that changes each 24 hours.
        /// </summary>
        /// <returns></returns>
        public static string Create()
        {
            // Create a seed for the hash that changes each UTC day and is unique for each machine
            string seed = DateTime.UtcNow.ToString("dd-MM-yyyy") + "EKPZGXFQLA" + Environment.MachineName + "YSCTRMJW"  + Environment.UserDomainName;

            // Hash the seed string
            byte[] hashBytes = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(seed));

            // Convert the hash bytes to a string
            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            // Get the first 8 charachters of the hash
            return hash.Substring(0, 8);
        }
    }
}
