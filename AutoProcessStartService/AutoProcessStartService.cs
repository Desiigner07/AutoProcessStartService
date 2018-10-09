using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AutoProcessStartService
{
    public partial class AutoProcessStartService : ServiceBase
    {
        private Timer timer;
        private DateTime scheduleTime;
        private string pathToProcess;

        public AutoProcessStartService()
        {
            InitializeComponent();
            this.pathToProcess = ConfigurationManager.AppSettings["pathToProcess"];
            this.timer = new Timer();
            this.scheduleTime = DateTime.Today.AddDays(1).AddHours(2);  //Start every day at 2 am
        }

        protected override void OnStart(string[] args)
        {
            this.timer.Enabled = true;
            this.timer.Interval = scheduleTime.Subtract(DateTime.Now).TotalSeconds * 1000;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StartProcess();

            if (this.timer.Interval != 24 * 60 * 60 * 1000)
            {
                this.timer.Interval = 24 * 60 * 60 * 1000;
            }
        }

        private void StartProcess()
        {
            Process p = Process.Start(pathToProcess);
        }
    }
}
