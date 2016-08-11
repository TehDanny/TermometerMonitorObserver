using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermometerMonObs
{
    class TemperatureGenerator
    {
        public int ThreadMethod()
        {
            Random rnd = new Random();
            int temp = rnd.Next(-20, 120);
            return temp;
        }
    }
}
