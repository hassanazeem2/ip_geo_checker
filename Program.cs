using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        do
        {
            Console.Clear();
            await GetGeolocationInfo();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Do you want to check another IP address? (y/n): ");
        } while (Console.ReadLine()?.ToLower() == "y");
    }

    static async Task GetGeolocationInfo()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(" __       __  __      __   ______  \r\n");
        Console.Write("|  \\  _  |  \\|  \\    /  \\ /      \\ \r\n");
        Console.Write("| $$ / \\ | $$ \\$$\\  /  $$|  $$$$$$\\\r\n");
        Console.Write("| $$/  $\\| $$  \\$$\\/  $$ | $$__| $$\r\n");
        Console.Write("| $$  $$$\\ $$   \\$$  $$  | $$    $$\r\n");
        Console.Write("| $$ $$\\$$\\$$    \\$$$$   | $$$$$$$$\r\n");
        Console.Write("| $$$$  \\$$$$    | $$    | $$  | $$\r\n");
        Console.Write("| $$$    \\$$$    | $$    | $$  | $$\r\n");
        Console.Write(" \\$$      \\$$     \\$$     \\$$   \\$$\r\n");
        Console.Write("                                   ");
        Console.Write("                                   \r\n");
        Console.Write("                                   ");


        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Enter an IP address: ");
        string ipAddress = Console.ReadLine();

        if (!IsValidIP(ipAddress))
        {
            Console.WriteLine("Invalid IP address format. Please enter a valid IP address.");
            return;
        }
        string apiKey = "ENTER YOUR API KEY HERE"; // Replace with your ipstack API key
        string apiUrl = $"http://api.ipstack.com/{ipAddress}?access_key={apiKey}";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string response = await client.GetStringAsync(apiUrl);
                JObject json = JObject.Parse(response);

                string city = (string)json["city"];
                string region = (string)json["region_name"];

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{city}, {region}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error retrieving geolocation information: {e.Message}");
            }
        }
    }

    static bool IsValidIP(string ipAddress)
    {
        // Basic IP address format validation
        return System.Net.IPAddress.TryParse(ipAddress, out _);
    }
}