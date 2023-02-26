using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        BackgroundWorker worker;
        bool CancelEventArgs = false;


        public SimulatorWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += giveTheOrder;
            worker.ProgressChanged += Worker_Change;
            worker.RunWorkerCompleted += Worker_Completed;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            stopWatch = new Stopwatch();


            worker.RunWorkerAsync();
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;
            }
        }
        //A function that allows or prevents closing the simulator window
        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CancelEventArgs;
        }

        private void Worker_Change(object? sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            timerTextBlock.Text = timerText;
        }

        private void Worker_Completed(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator1.StatusChangedEvent -= StartSimulator;
            Simulator1.FinishSimulatorEvent -= FinishSimulator;
            isTimerRun = false;
        }



        private void Stop_Simulator_Click(object sender, RoutedEventArgs e)
        {
            CancelEventArgs = true;
            worker.CancelAsync();
            Simulator1.Stop();
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
        }

        public void giveTheOrder(object? sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                Simulator1.Run();
                Simulator1.StatusChangedEvent += StartSimulator;
                Simulator1.FinishSimulatorEvent += FinishSimulator;
                while (isTimerRun)
                {
                    worker.ReportProgress(1);
                    Thread.Sleep(1000);
                }
            }
        }
        public void StartSimulator(Order order, string status, string newStatus, DateTime prev, DateTime next)
        {
            Dispatcher.Invoke(() =>
            {
                tb_id.Text = order?.ID.ToString();
                tb_current.Text = status;
                tb_next.Text = newStatus;
                tb_start_time.Text = prev.ToString();
                tb_end_time.Text = next.ToString();
            });
        }

        public void FinishSimulator(DateTime end, string reasonOfFinish = "")
        {
            Dispatcher.Invoke(() =>
            {
                Simulator1.FinishSimulatorEvent -= FinishSimulator;
                if (reasonOfFinish != "")
                {
                    MessageBox.Show("Finishing the simulator: " + end.ToString() + "\n" + "Becouse: " + reasonOfFinish);
                    CancelEventArgs = true;
                    this.Close();
                }
            });
        }
    }
}
