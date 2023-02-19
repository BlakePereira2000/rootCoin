using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace rootCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            Blockchain rootcoin = new Blockchain(2, 100);

            Console.WriteLine("Strat the Miner.");
            rootcoin.MinePendingtransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is $" + rootcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            rootcoin.AddPendingTransaction(tx1);
            Console.WriteLine("Start the miner.");
            rootcoin.MinePendingtransactions(wallet2);
            Console.WriteLine("\nBalance of wallet1 is $" + rootcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + rootcoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJson = JsonConvert.SerializeObject(rootcoin, formatting: Formatting.Indented);
            //Console.WriteLine(blockJson);

            //rootcoin.GetLatestBlock().PreviousHash = "12345";

            if (rootcoin.IsChainValid())
            {
                Console.WriteLine("Chain is valid!");
            }
            else
            {
                Console.WriteLine("Chain is not valid!");
            }

        }
    }
}
