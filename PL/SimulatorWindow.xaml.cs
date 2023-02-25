using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private Stopwatch stopWatch;
        private bool isTimerRun;
        private Thread timerThread;
        BackgroundWorker worker = new();
        bool CancelEventArgs=false;


        public SimulatorWindow()
        {
            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            isTimerRun = true;
            stopWatch = new Stopwatch();
            stopWatch.Restart();
            worker.DoWork += giveTheOrder;
            timerThread = new Thread(runTimer);
            timerThread.Start();
        }

         void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(CancelEventArgs==false)
                e.Cancel = true;
            else 
                e.Cancel= false;
        }
        public void setTextInvok(string text)
        {
            if(!CheckAccess())
            {
                Action<string> d = setTextInvok;
                Dispatcher.BeginInvoke(d,new object[] {text});
            }
            else
            {
                this.timerTextBlock.Text = text;    
            }
        }
        public void runTimer()
        {
            while(isTimerRun)
            {
                string timerText=stopWatch.Elapsed.ToString();
                timerText=timerText.Substring(0,8);
                setTextInvok(timerText);
                Thread.Sleep(1000);
            }
        }

        private void Stop_Simulator_Click(object sender, RoutedEventArgs e)
        {
            CancelEventArgs= true;
        }
        public void giveTheOrder(object? sender, DoWorkEventArgs e)
        {

        }

    }
}
