using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace TermometerMonObs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Termometer_monitor tm;
        private static TemperatureGenerator tg;
        private static int intForLock = 0;
        private static object tempLock = intForLock;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            tm = new Termometer_monitor();
            tg = new TemperatureGenerator();

            ThreadStart GenerateTemperatureMethod = new ThreadStart(GenerateTemperature);
            Thread generateTemperatureThread = new Thread(GenerateTemperatureMethod);
            generateTemperatureThread.Start();

            ThreadStart GetTemperaturesMethod = new ThreadStart(GetTemperatures);
            Thread GetTemperaturesThread = new Thread(GetTemperaturesMethod);
            GetTemperaturesThread.Start();
        }

        private void GenerateTemperature()
        {
            while (true)
            {
                Monitor.Enter(tempLock);
                try
                {
                    int currentTemp = tg.ThreadMethod();
                    tm.SetCurrentTemp(currentTemp);

                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (radioButton.IsChecked == true)
                            if (tm.CheckMinAlarm(currentTemp) == true)
                                alarmTbl.Text = "Warning! Temperature is below 0 degrees!";
                            else
                                alarmTbl.Text = "";
                        if (radioButton2.IsChecked == true)
                            if (tm.CheckMaxAlarm(currentTemp) == true)
                                alarmTbl.Text = "Warning! Temperature is above 100 degrees!";
                            else
                                alarmTbl.Text = "";
                    }));
                }
                finally
                {
                    Monitor.Exit(tempLock);
                }
                Thread.Sleep(2000);
            }
        }

        private void GetTemperatures()
        {
            while (true)
            {
                Monitor.Enter(tempLock);
                try
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        currentTemperatureLbl.Content = "Current temperature: " + tm.GetCurrentTemp();
                        minimumTemperatureLbl.Content = "Minimum temperature: " + tm.GetMinTemp();
                        maximumTemperatureLbl.Content = "Maximum temperature: " + tm.GetMaxTemp();
                    }));
                }
                finally
                {
                    Monitor.Exit(tempLock);
                }
                Thread.Sleep(100);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Monitor.Enter(tempLock);
            try
            {
                tm.ClearMinAndMaxTemps();
            }
            finally
            {
                Monitor.Exit(tempLock);
            }
        }
    }
}
