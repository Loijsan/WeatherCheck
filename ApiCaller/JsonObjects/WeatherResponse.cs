using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCaller.JsonObjects
{
    internal class WeatherResponse
    {
        public class WeatherRootObject
        {
            public RESPONSE RESPONSE { get; set; }
        }
        public class RESPONSE
        {
            public RESULT[] RESULT { get; set; }
        }
        public class RESULT
        {
            public Rootobject[] rootObject { get; set; }
        }
        public class Rootobject
        {
            public DateTime approvedTime { get; set; }
            public DateTime referenceTime { get; set; }
            public Geometry geometry { get; set; }
            public Timesery[] timeSeries { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[][] coordinates { get; set; }
        }

        public class Timesery
        {
            public DateTime validTime { get; set; }
            public Parameter[] parameters { get; set; }
        }

        public class Parameter
        {
            public string name { get; set; }
            public string levelType { get; set; }
            public int level { get; set; }
            public string unit { get; set; }
            public float[] values { get; set; }
        }
    }
}
