using System;
using System.Collections.Generic;

namespace CryptoCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();
            List<Transaction> transactions = new List<Transaction>();
            Transaction trans = new Transaction("Brandon", "Jess", 50.0f);
            blockchain.pendingTransactions.Add(trans);
            Transaction trans2 = new Transaction("Brandon", "Hayden", 50.0f);
            blockchain.pendingTransactions.Add(trans2);
            Transaction trans3 = new Transaction("Jordan", "Jess", 10.0f);
            blockchain.pendingTransactions.Add(trans3);
            blockchain.minePendingTransactions("Logan");
            //Block block = new Block(transactions, 0);
            //blockchain.AddBlock(block);

            //block = new Block(transactions, 1);
            //blockchain.AddBlock(block);

            //block = new Block(transactions, 2);
            //blockchain.AddBlock(block);

            Console.WriteLine(blockchain.ToString());
            Console.WriteLine("Length" + blockchain.chain.Count);
        }
    }
}
