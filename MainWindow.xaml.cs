using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PV221_WPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Task> tasks;
        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                NotifyPropertyChanged(nameof(Tasks));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Tasks = new ObservableCollection<Task>();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbTopic.Text) &&
                !string.IsNullOrWhiteSpace(tbDescr.Text) &&
                DateTime.TryParse(tbDate.Text, out DateTime date))
            {
                Task newTask = new Task()
                {
                    Topic = tbTopic.Text,
                    Description = tbDescr.Text,
                    Date = date.ToString("yyyy-MM-dd")
                };

                Tasks.Add(newTask);

                tbTopic.Text = string.Empty;
                tbDescr.Text = string.Empty;
                tbDate.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Please enter valid data in all fields.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tasksDataGrid.SelectedItem is Task selectedTask)
            {
                Tasks.Remove(selectedTask);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (tasksDataGrid.SelectedItem is Task selectedTask)
            {
                selectedTask.Topic = tbTopic.Text;
                selectedTask.Description = tbDescr.Text;
                selectedTask.Date = tbDate.Text;
            }
        }

        private void tasksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tasksDataGrid.SelectedItem is Task selectedTask)
            {
                tbTopic.Text = selectedTask.Topic;
                tbDescr.Text = selectedTask.Description;
                tbDate.Text = selectedTask.Date;
            }
        }

        public class Task : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string topic;
            public string Topic
            {
                get { return topic; }
                set
                {
                    topic = value;
                    NotifyPropertyChanged(nameof(Topic));
                }
            }

            private string description;
            public string Description
            {
                get { return description; }
                set
                {
                    description = value;
                    NotifyPropertyChanged(nameof(Description));
                }
            }

            private string date;
            public string Date
            {
                get { return date; }
                set
                {
                    date = value;
                    NotifyPropertyChanged(nameof(Date));
                }
            }

            private void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}