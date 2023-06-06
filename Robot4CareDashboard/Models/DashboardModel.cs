using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Robot4CareDashboard.Models
{
    public class DashboardModel
    {
        public EmergencyModel emergency { get; set; }
        public JamesModel james { get; set; }
    }
}
