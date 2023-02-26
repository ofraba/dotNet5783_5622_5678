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
using BO;
using Simulator;

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
        bool CancelEventArgs = false;


        public SimulatorWindow()
        {
            InitializeComponent();
            isTimerRun = true;
            stopWatch = new Stopwatch();
            stopWatch.Restart();
            worker.DoWork += giveTheOrder;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            timerThread = new Thread(runTimer);
            timerThread.Start();
        }
        //A function that allows or prevents closing the simulator window
        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CancelEventArgs == false)
                e.Cancel = true;
            else
                e.Cancel = false;
        }
        public void setTextInvok(string text)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTextInvok;
                Dispatcher.BeginInvoke(d, new object[] { text });
            }
            else
            {
                this.timerTextBlock.Text = text;
            }
        }

        public void runTimer()
        {
            while (isTimerRun)
            {
                string timerText = stopWatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);
                setTextInvok(timerText);
                Thread.Sleep(1000);
            }
        }

        private void Stop_Simulator_Click(object sender, RoutedEventArgs e)
        {
            CancelEventArgs = true;
        }

        public void giveTheOrder(object? sender, DoWorkEventArgs e)
        {
            Simulator1.Run();
            Simulator1.StatusChangedEvent += StartSimulator;
            Simulator1.FinishSimulatorEvent += FinishSimulator;
        }
        public void StartSimulator(Order order, string status, string newStatus, DateTime prev, DateTime next)
        {
            tb_id.Text = order.ID.ToString();
            tb_current.Text = status;
            tb_start_time.Text = prev.ToString();
            tb_end_time.Text= next.ToString();
            tb_next.Text = newStatus;
        }
        public void FinishSimulator(DateTime end, string reasonOfFinish = "")
        {

        }
    }
}
