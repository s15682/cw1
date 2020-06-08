using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    public class Program
    {
        static string defaultAddress = "https://www.pja.edu.pl";

        public static async Task Main(string[] args)
        {
            string address;
            address = SetAddress(args);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(address); 
            if(response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z0-9]+@[a-z.]+");

                var matches = regex.Matches(html); 
                foreach ( var match in matches)
                {
                    Console.WriteLine(match); 
                }
            }
        }

        private static string SetAddress(string[] args)
        {
            string address;
            if (args.Length > 0)
            {
                address = args[0];
            }
            else
            {
                address = defaultAddress;
            }
            return address;
        }
    }
}

