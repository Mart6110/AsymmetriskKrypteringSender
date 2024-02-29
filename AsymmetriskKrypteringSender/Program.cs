using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace AsymmetricEncryptionSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Continuously send encrypted messages until the user decides to exit
            while (true)
            {
                SendEncryptedMessage(); // Call the method to send an encrypted message

                // Prompt the user to continue or exit
                Console.WriteLine("Press Enter to send another encrypted message or type 'exit' to quit.");
                string input = Console.ReadLine().ToLower();
                if (input == "exit")
                    break; // Exit the loop if the user types 'exit'
            }
        }

        static void SendEncryptedMessage()
        {
            // Simulate user input for public key (Exponent)
            Console.WriteLine("Enter public key (Exponent): ");
            string exponentInput = Console.ReadLine();
            byte[] exponentBytes = exponentInput.Split('-').Select(s => Convert.ToByte(s, 16)).ToArray();
            BigInteger exponent = new BigInteger(exponentBytes);

            // Simulate user input for public key (Modulus)
            Console.WriteLine("Enter public key (Modulus): ");
            string modulusInput = Console.ReadLine();
            byte[] modulusBytes = modulusInput.Split('-').Select(s => Convert.ToByte(s, 16)).ToArray();
            BigInteger modulus = new BigInteger(modulusBytes);

            // Create RSA key with the public key data
            RSAParameters rsaParams = new RSAParameters
            {
                Exponent = exponent.ToByteArray(),
                Modulus = modulus.ToByteArray()
            };

            // Encrypt message
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);

                // Simulate user input for message
                Console.WriteLine("Enter message: ");
                string message = Console.ReadLine();

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] encryptedBytes = rsa.Encrypt(messageBytes, false);
                string encryptedText = BitConverter.ToString(encryptedBytes);

                // Display encrypted message
                Console.WriteLine("Encrypted message: " + encryptedText);
            }
        }
    }
}
