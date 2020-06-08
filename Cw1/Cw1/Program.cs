using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string address;
            HttpResponseMessage response = null;
            var httpClient = new HttpClient();
            try
            {
                address = SetAddress(args);
                response = await httpClient.GetAsync(address);
            } catch (HttpRequestException ex) {
                Console.WriteLine("Błąd w czasie pobierania strony"); 
            } catch  (Exception ex)
            {
                Console.WriteLine(ex); 
            }
            if (response!=null && response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z0-9]+@[a-z.]+");

                var matches = regex.Matches(html); 
                foreach ( var match in matches)
                {
                    Console.WriteLine(match); 
                }
            }
            httpClient.Dispose(); 
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
                throw new ArgumentNullException(); 
            }

            if (!Uri.IsWellFormedUriString(address, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException(); 
            }
            return address;
        }
    }
}

