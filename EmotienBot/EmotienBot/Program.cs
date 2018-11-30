using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace EmotienBot
{
    static class Program
    {
        static void Main()
        {
            var makeAnalys = new MakeAnalyst();
            Console.WriteLine("Detect faces:");
            Console.Write(
                "Enter the path to an image with faces that you wish to analyze: ");
            //string imageFilePath = Console.ReadLine();
            string imageFilePath = @"c:\Users\User\Desktop\Alina\Phdfk.jpeg";

            if (File.Exists(imageFilePath))
            {
                try
                {
                    var g=makeAnalys.MakeAnalysisRequest(imageFilePath).GetAwaiter();
                    Console.WriteLine("\nWait a moment for the results to appear.\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n" + e.Message + "\nPress Enter to exit...\n");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid file path.\nPress Enter to exit...\n");
            }
            Console.ReadLine();
        }
    }
}