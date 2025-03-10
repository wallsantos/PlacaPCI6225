using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;


namespace ProjetoAquisicao;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private ObservableCollection<ISeries> _series;
    private Axis[] _xAxes;

    public ObservableCollection<ISeries> Series{
        get => _series;
        set{
            _series = value;
            OnPropertyChanged(nameof(Series));
        }
    }
    public Axis[] XAxes{
        get => _xAxes;
        set{
            _xAxes = value;
            OnPropertyChanged(nameof(XAxes));
        }
    }

    public MainWindow()
    {
        InitializeComponent();

        Series = new ObservableCollection<ISeries>{
            new LineSeries<double>{
                Name = "Canal 1",
                Values = new ObservableCollection<double> { 3, 5, 7, 9 }
            }
        };
        XAxes = new Axis[]{
            new Axis{
                Labels = new[]{"0","1","2","3"}
            }
        };
        
        DataContext = this;
    }
    private void AtualizarGrafico(){
        Series.Clear();

        if(Canal1CheckBox.IsChecked == true){
            Series.Add(new LineSeries<double>{
                Name = "Canal 1",
                Values = new ObservableCollection<double> { 1, 3, 5, 7 }
            });
        }
        if (Canal2CheckBox.IsChecked == true)
        {
            Series.Add(new LineSeries<double>
            {
                Name = "Canal 2",
                Values = new ObservableCollection<double> { 2, 4, 6, 8 }
            });
        }
        if (Canal3CheckBox.IsChecked == true)
        {
            Series.Add(new LineSeries<double>
            {
                Name = "Canal 3",
                Values = new ObservableCollection<double> { 3, 6, 9, 12 }
            });
        }
        if (Canal4CheckBox.IsChecked == true)
        {
            Series.Add(new LineSeries<double>
            {
                Name = "Canal 4",
                Values = new ObservableCollection<double> { 4, 8, 12, 16 }
            });
        }
        if (Canal5CheckBox.IsChecked == true)
        {
            Series.Add(new LineSeries<double>
            {
                Name = "Canal 5",
                Values = new ObservableCollection<double> { 5, 10, 15, 20 }
            });
        }
        if (Canal6CheckBox.IsChecked == true)
        {
            Series.Add(new LineSeries<double>
            {
                Name = "Canal 6",
                Values = new ObservableCollection<double> { 6, 12, 18, 24 }
            });
        }
    }

    private void Atualizarbutton_Click(object sender, RoutedEventArgs e){
        AtualizarGrafico();
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string PropertyName){
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}