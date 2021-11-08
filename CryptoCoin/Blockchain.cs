using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CryptoCoin
{
    class Blockchain
    {
        public List<Block> chain = new List<Block>();
        public List<Transaction> pendingTransactions = new List<Transaction>();
        int difficulty = 4;
        int minerReward = 50;
        int blockSize = 10;
        public Blockchain()
        {
            chain.Add(AddGenesisBlock());
        }
        public Block GetLastBlock()
        {
            return chain.Last();
        }
        public void AddBlock(Block block)
        {
            if (this.chain.Count > 0)
            {
                block.prev = this.GetLastBlock().hash;
            }
            else
            {
                block.prev = "none";
            }
            this.chain.Add(block);
        }
        public void minePendingTransactions(string miner)
        {
            int lenPT = this.pendingTransactions.Count;
            if(lenPT <= 1)
            {
                Console.WriteLine("Not enough transactions");
                return;
            }
            else
            {
                for(int i = 0; i<lenPT; i += this.blockSize)
                {
                    int end = i + this.blockSize;
                    if (end > lenPT)
                    {
                        end = lenPT;
                    }
                    var transactionSlice = pendingTransactions.GetRange(i, end-i);
                    Block newBlock = new Block(transactionSlice, this.chain.Count);

                    string hashVal = this.GetLastBlock().hash;
                    newBlock.prev = hashVal;
                    newBlock.mineBlock(this.difficulty);
                    this.chain.Add(newBlock);
                }
                Console.WriteLine("Mining Success!");
                var payMiner = new Transaction("Miner Rewards", miner, this.minerReward);
                this.pendingTransactions.Add(payMiner);
            }
        }
        private Block AddGenesisBlock()
        {
            List<Transaction> genTrans = new List<Transaction>();
            genTrans.Add(new Transaction("L", "dich", 10.0f));
            Block genesis = new Block(genTrans, 0);
            genesis.prev = "None";
            return genesis;
        }
        public override string ToString()
        {
            string output = "\n";
            foreach(Block block in chain)
            {
                output += "\nHash: "+block.hash + "\nprev: " + block.prev + "\ntransactions: ";
                foreach (Transaction t in block.transactions)
                {
                    output += t.sender + " to " + t.receiver + " amt: " + t.amt + "\n";
                }
            }
            
            return output;
        }
    }
}
