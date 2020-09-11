using System;
using Microsoft.Azure.Cosmos.Spatial;

namespace ReadImageExif
{
    public class Settings
    {
        public string Path { get; set; }
        public DateTime LastRun { get; set; }
        public Location[] Locations { get; set; }
    }

    public class Location{
        public string Name { get; set; }
        public bool Process { get; set; }
        public Double Threshold { get; set; }
        public Point Coordinates { get; set; }
    }
}