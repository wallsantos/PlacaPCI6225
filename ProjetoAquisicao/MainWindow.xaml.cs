using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace ProjetoAquisicao
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<ISeries> SeriesCanal1 { get; set; }
        public ObservableCollection<ISeries> SeriesCanal2 { get; set; }
        public ObservableCollection<ISeries> SeriesCanal3 { get; set; }
        public ObservableCollection<ISeries> SeriesCanal4 { get; set; }
        public ObservableCollection<ISeries> SeriesCanal5 { get; set; }
        public ObservableCollection<ISeries> SeriesCanal6 { get; set; }

        private Visibility _canal1Visivel = Visibility.Collapsed;
        private Visibility _canal2Visivel = Visibility.Collapsed;
        private Visibility _canal3Visivel = Visibility.Collapsed;
        private Visibility _canal4Visivel = Visibility.Collapsed;
        private Visibility _canal5Visivel = Visibility.Collapsed;
        private Visibility _canal6Visivel = Visibility.Collapsed;

        public Visibility Canal1Visivel { get => _canal1Visivel; set { _canal1Visivel = value; OnPropertyChanged(nameof(Canal1Visivel)); } }
        public Visibility Canal2Visivel { get => _canal2Visivel; set { _canal2Visivel = value; OnPropertyChanged(nameof(Canal2Visivel)); } }
        public Visibility Canal3Visivel { get => _canal3Visivel; set { _canal3Visivel = value; OnPropertyChanged(nameof(Canal3Visivel)); } }
        public Visibility Canal4Visivel { get => _canal4Visivel; set { _canal4Visivel = value; OnPropertyChanged(nameof(Canal4Visivel)); } }
        public Visibility Canal5Visivel { get => _canal5Visivel; set { _canal5Visivel = value; OnPropertyChanged(nameof(Canal5Visivel)); } }
        public Visibility Canal6Visivel { get => _canal6Visivel; set { _canal6Visivel = value; OnPropertyChanged(nameof(Canal6Visivel)); } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            SeriesCanal1 = new ObservableCollection<ISeries>();
            SeriesCanal2 = new ObservableCollection<ISeries>();
            SeriesCanal3 = new ObservableCollection<ISeries>();
            SeriesCanal4 = new ObservableCollection<ISeries>();
            SeriesCanal5 = new ObservableCollection<ISeries>();
            SeriesCanal6 = new ObservableCollection<ISeries>();
        }

        private void Atualizarbutton_Click(object sender, RoutedEventArgs e)
        {
            SeriesCanal1.Clear();
            SeriesCanal2.Clear();
            SeriesCanal3.Clear();
            SeriesCanal4.Clear();
            SeriesCanal5.Clear();
            SeriesCanal6.Clear();

            Canal1Visivel = Canal1CheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Canal2Visivel = Canal2CheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Canal3Visivel = Canal3CheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Canal4Visivel = Canal4CheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Canal5Visivel = Canal5CheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Canal6Visivel = Canal6CheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;

            if (Canal1CheckBox.IsChecked == true)
                SeriesCanal1.Add(new LineSeries<double> { Name = "Canal 1", Values = new ObservableCollection<double> { 1, 3, 5, 7 } });

            if (Canal2CheckBox.IsChecked == true)
                SeriesCanal2.Add(new LineSeries<double> { Name = "Canal 2", Values = new ObservableCollection<double> { 2, 4, 6, 8 } });

            if (Canal3CheckBox.IsChecked == true)
                SeriesCanal3.Add(new LineSeries<double> { Name = "Canal 3", Values = new ObservableCollection<double> { 3, 6, 9, 12 } });

            if (Canal4CheckBox.IsChecked == true)
                SeriesCanal4.Add(new LineSeries<double> { Name = "Canal 4", Values = new ObservableCollection<double> { 4, 8, 12, 16 } });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}