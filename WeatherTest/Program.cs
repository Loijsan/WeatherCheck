using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCaller;

namespace WeatherTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string latitude = "57.844579";
            string longitude = "11.896699";

            Caller caller = new();

            caller.TempCaller(latitude, longitude);
        }
    }
}
