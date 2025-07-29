using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency
{
    public class FileDownloader
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        public FileDownloader() {
            _worker.DoWork += DownLoadFile; // background task
            _worker.ProgressChanged += ReportProgress; // reports progress to the main UI thread
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync();

        }

        private void ReportProgress(object? sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"Main Thread: Download Progress: {e.ProgressPercentage}%");
        }

        private void DownLoadFile(object? sender, DoWorkEventArgs e)
        {
            // background task
            for (int i = 0; i <= 10; i++)
            {
                Thread.Sleep(100); // simulates file chunks
                _worker.ReportProgress(i*10);
            }        
        }
    }
}
