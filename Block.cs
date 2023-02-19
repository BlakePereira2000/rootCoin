using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace rootCoin
{
    class Block
    {
        public int Index { get; set; }
        public string PreviousHash { get; set; }
        public string TimeStamp { get; set; }
        public string Data { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }
        public List<Transaction> Transactions { get; set; }


        public Block(int index, string timestamp, List<Transaction> transactions, string previoushash = "")
        {
            this.Index = index;
            this.TimeStamp = timestamp;
            this.Transactions = transactions;
            this.PreviousHash = previoushash;
            this.Hash = CalcHash();
            this.Nonce = 0;
        }

        public string CalcHash()
        {
            string blockData = this.Index + this.PreviousHash + this.TimeStamp + this.Transactions.ToString() + this.Nonce;
            byte[] blockBytes = Encoding.ASCII.GetBytes(blockData);
            byte[] hashBytes = SHA256.Create().ComputeHash(blockBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public void Mine(int difficulty)
        {
            while (this.Hash.Substring(0, difficulty) != new String('0', difficulty))
            {
                this.Nonce++;
                this.Hash = this.CalcHash();
                Console.WriteLine("Mining: " + this.Hash);
            }

            Console.WriteLine("Block has been mined: " + this.Hash);
        }
    }
}
