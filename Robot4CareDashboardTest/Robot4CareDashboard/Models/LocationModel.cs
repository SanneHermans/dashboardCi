using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Robot4CareDashboard.Models
{
    public class LocationModel
    {
        public int? Id { get; set; }
        public int RouteId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }
    }
}
