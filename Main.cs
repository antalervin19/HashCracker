using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static string originalHash = "1e417a55e01da8e7bb99e4e996a3bfe2a6f343aa847eb13ff9e0f4f56de0a6a2";

    static string Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    static string GenerateRandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static void CheckHash()
    {
        while (true)
        {
            string randomString = GenerateRandomString(25);
            if (Hash(randomString) == originalHash)
            {
                Console.WriteLine("Original hash is: " + randomString);
                return;
            }
        }
    }

    static void Main(string[] args)
    {
        int numThreads = 4;
        List<Task> tasks = new List<Task>();
        DateTime start = DateTime.Now;
        for (int i = 0; i < numThreads; i++)
        {
            tasks.Add(Task.Run(() => CheckHash()));
        }
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"Finished after {Math.Round((DateTime.Now - start).TotalSeconds, 2)} seconds");
    }
}
