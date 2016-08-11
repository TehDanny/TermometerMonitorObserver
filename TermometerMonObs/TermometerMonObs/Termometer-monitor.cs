using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermometerMonObs
{
    class Termometer_monitor
    {
        public int CurrentTemp { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int MinAlarm { get; set; }
        public int MaxAlarm { get; set; }
        delegate int MinAlarmDelegate(int temp);
        delegate int MaxAlarmDelegate(int temp);

        public int GetCurrentTemp()
        {
            return CurrentTemp;
        }

        public int GetMinTemp()
        {
            return MinTemp;
        }

        public int GetMaxTemp()
        {
            return MaxTemp;
        }

        public void SetCurrentTemp(int temp)
        {
            CurrentTemp = temp;

            if (temp < MinTemp)
                MinTemp = temp;

            if (temp > MaxTemp)
                MaxTemp = temp;
        }

        public void ClearMinAndMaxTemps()
        {
            MinTemp = CurrentTemp;
            MaxTemp = CurrentTemp;
        }
    }
}
