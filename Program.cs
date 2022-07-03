using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

dynamic data;

void GetData(string? getItem)
{
    // Make a HTTP request to https://api.hypixel.net/skyblock/bazaar/ and return the JSON data
    // The JSON data is stored in the variable "jsonData"
    HttpWebRequest jsonData = (HttpWebRequest)WebRequest.Create(@"https://api.hypixel.net/skyblock/bazaar");
    jsonData.ContentType = "application/json";
    jsonData.Method = "GET";
    jsonData.Accept = "application/json";
    jsonData.UserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";
    HttpWebResponse response = (HttpWebResponse)jsonData.GetResponse();
    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
    {
        var test = reader.ReadToEnd();
        data = JObject.Parse(test);
        Console.Clear();
        // If the user has not specified an item to get, get all items data
        if (string.IsNullOrEmpty(getItem))
        {
            foreach (JProperty item in data.products)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(item.Value["product_id"] + " Status Info: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SellPrice: " + item.Value["quick_status"]["sellPrice"]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("BuyPrice: " + item.Value["quick_status"]["buyPrice"] + "\n");
                Console.ResetColor();
            }
        }
        // If the user has specified an item to get, get that item's data
        else
        {
            foreach (JProperty item in data.products)
            {
                if (getItem == item.Name)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(item.Value["product_id"] + " Status Infoo: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("SellPrice: " + item.Value["quick_status"]["sellPrice"]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("BuyPrice: " + item.Value["quick_status"]["buyPrice"]);
                    Console.ResetColor();
                }
            }
        }

        Console.WriteLine("Press any key to close the program");
        Console.ReadKey();
    }
}

Console.WriteLine("Enter the item you want to get the prices for");
Console.WriteLine("Insert nothing to get all items:");

var input = Console.ReadLine();
Console.Clear();
Console.Write("Searching...");

// ToUpper since all items is UPPERCASE in the JSON data
GetData(input.ToUpper());