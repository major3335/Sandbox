using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> toppings = new List<string>();
            StringBuilder sb = new StringBuilder();

            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("http://files.olo.com/pizzas.json").Result.Content.ReadAsStringAsync().Result;

            JArray jArray = JArray.Parse(response);
            
            for (int i = 0; i < jArray.Count; i++)
            {
                JToken jtopping = jArray[i]["toppings"];

                foreach (var topping in jtopping)
                {
                    sb.Append(sb.Length > 0 ? ", " + topping.ToString() : topping.ToString()); //case sensitivity can be tested here if necessary
                }

                toppings.Add(sb.ToString());
                sb.Clear();
            }

            var counts = (toppings.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count()).OrderByDescending(f => f.Value).Take(20));

            foreach (var item in counts)
            {
                Console.WriteLine($"{item.Key} was orderd {item.Value} times");
            }

            Console.ReadLine();
        }
    }


}
