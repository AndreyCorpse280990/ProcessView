using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace ProcessViewer
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10); // Обновление каждые 10 секунд
            _timer.Tick += (s, e) => LoadProcesses();
            _timer.Start();
        }

        private void LoadProcesses()
        {
            var processes = Process.GetProcesses()
                .Select(p => new
                {
                    p.ProcessName,
                    p.Id,
                    MemoryUsage = $"{p.WorkingSet64 / 1024 / 1024} MB"
                }).ToList();
            ProcessListView.ItemsSource = processes;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        private void ProcessListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProcessListView.SelectedItem != null)
            {
                var selectedProcess = (dynamic)ProcessListView.SelectedItem;
                var processId = selectedProcess.Id;
                var detailsWindow = new ProcessDetailsWindow(processId);
                detailsWindow.Show();
            }
        }
    }
}
