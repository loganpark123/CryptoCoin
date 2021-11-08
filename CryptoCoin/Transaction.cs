using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoCoin
{
    class Transaction
    {
        public string sender;
        public string receiver;
        public float amt;
        DateTime time;
        public string hash;
        public Transaction(string sender, string receiver, float amt)
        {
            this.time = DateTime.UtcNow;
            this.sender = sender;
            this.receiver = receiver;
            this.amt = amt;
            this.hash = calculateHash();
        }
        private string calculateHash()
        {
            
            string hashString = this.time.ToString() + this.sender + this.receiver + this.amt.ToString();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return GetHash(sha256Hash, hashString);
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
