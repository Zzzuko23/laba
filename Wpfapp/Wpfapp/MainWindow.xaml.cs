using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfDisciplines
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _currentTime;
        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTime"));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => CurrentTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            timer.Start();

           
            DisciplinesList.Items.Add("Математика | Экзамен | Сдан");
            DisciplinesList.Items.Add("Физика | Экзамен | Не сдан");
            DisciplinesList.Items.Add("Информатика | Зачет | Сдан");
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Все дисциплины";
            StatusText.Text = "Показаны все дисциплины";
        }

        private void ShowAdd_Click(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Добавление дисциплины";
            NameTextBox.Focus();
        }

        private void ShowDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinesList.SelectedItem != null)
            {
                DisciplinesList.Items.Remove(DisciplinesList.SelectedItem);
                StatusText.Text = "Дисциплина удалена";
            }
            else
            {
                MessageBox.Show("Выберите дисциплину для удаления!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowStatus_Click(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Изменение статуса";
        }

        private void ShowFilter_Click(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Фильтр";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "✓ Данные сохранены";
        }

        private void ExecuteAction_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                string discipline = $"{NameTextBox.Text} | {type} | {status}";
                DisciplinesList.Items.Add(discipline);

                NameTextBox.Clear();
                StatusText.Text = $"Добавлено: {NameTextBox.Text}";
            }
        }
    }
}