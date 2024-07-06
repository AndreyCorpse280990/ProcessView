using System.Diagnostics;
using System.Windows;

namespace ProcessViewer
{
    public partial class ProcessDetailsWindow : Window
    {
        private int _processId;

        public ProcessDetailsWindow(int processId)
        {
            InitializeComponent();
            _processId = processId;
            LoadProcessDetails();
        }

        private void LoadProcessDetails()
        {
            try
            {
                var process = Process.GetProcessById(_processId);
                ProcessDetailsTextBlock.Text = $"Name: {process.ProcessName}\n" +
                                               $"ID: {process.Id}\n" +
                                               $"Start Time: {process.StartTime}\n" +
                                               $"Total Processor Time: {process.TotalProcessorTime}\n" +
                                               $"Memory Usage: {process.WorkingSet64 / 1024 / 1024} MB\n" +
                                               $"Threads: {process.Threads.Count}\n";
            }
            catch (Exception ex)
            {
                ProcessDetailsTextBlock.Text = $"Error: {ex.Message}";
            }
        }
    }
}
