using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CryptoCoin
{
    class Block
    {
        public string prev;
        public List<Transaction> transactions;
        DateTime time;
        long index;
        public string hash;
        private int nonce = 0;
        public Block(List<Transaction> transactions, long index)
        {
            this.transactions = transactions;
            this.time = DateTime.UtcNow;
            this.index = index;
            hash = calculateHash();
        }
        private string calculateHash()
        {
            string hashTransactions= "";
            foreach (Transaction trans in this.transactions)
            {
                hashTransactions+=trans.hash;
            }
            string hashString = this.time.ToString() + hashTransactions + this.prev + this.index.ToString() + this.nonce.ToString();
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

        public bool mineBlock(int difficulty)
        {
            string arr = "";
            for (int i = 0; i < difficulty; i++)
                arr += i.ToString();
            while (!this.hash.Substring(0, difficulty).Equals(arr))
            {
                this.nonce++;
                this.hash = this.calculateHash();
                Console.WriteLine("Nonce " + this.nonce);
                Console.WriteLine("Hash" + this.hash);
            }
            Console.WriteLine("Block Mined");
            return true;

        }
    }
}
