using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using NationalInstruments.DAQmx;
using LiveChartsCore.SkiaSharpView; // Namespace correto
using LiveChartsCore.SkiaSharpView.WinForms; // Para WinForms
using Task = System.Threading.Tasks.Task;

public class MainForm : Form
{
    private Task Tarefa;
    private ComboBox comboBoxCanais;
    private Button botaoIniciar;
    private CartesianChart grafico;  // Ajuste aqui
    private IChartSeries[] seriesCollection;  // Ajuste aqui
    private Thread aquisicaoThread;
    private volatile bool executando = false;

    public MainForm()
    {
        // Configuração da Janela
        this.Text = "Aquisição de Dados NI-DAQmx";
        this.Size = new System.Drawing.Size(800, 600);

        // ComboBox para seleção de canais
        comboBoxCanais = new ComboBox { Left = 20, Top = 20, Width = 200 };
        for (int i = 0; i <= 5; i++) comboBoxCanais.Items.Add($"Dev1/ai{i}");
        comboBoxCanais.SelectedIndex = 0;
        this.Controls.Add(comboBoxCanais);

        // Botão para iniciar aquisição
        botaoIniciar = new Button { Left = 250, Top = 20, Text = "Iniciar", Width = 100 };
        botaoIniciar.Click += BotaoIniciar_Click;
        this.Controls.Add(botaoIniciar);

        // Gráfico
        grafico = new CartesianChart  // Ajuste aqui
        {
            Left = 20,
            Top = 80,
            Width = 750,
            Height = 450
        };
        this.Controls.Add(grafico);

        // Inicializando gráfico
        seriesCollection = new[] { new LineSeries<double> { Values = new ChartValues<double>() } };
        grafico.Series = seriesCollection;
    }

    private void BotaoIniciar_Click(object sender, EventArgs e)
    {
        if (!executando)
        {
            botaoIniciar.Text = "Parar";
            executando = true;
            aquisicaoThread = new Thread(AquisicaoDinamica);
            aquisicaoThread.Start();
        }
        else
        {
            executando = false;
            botaoIniciar.Text = "Iniciar";
            Tarefa?.Stop();
            Tarefa?.Dispose();
        }
    }

    private void AquisicaoDinamica()
    {
        string canalSelecionado = comboBoxCanais.SelectedItem.ToString();
        Tarefa = new Task();
        Tarefa.AIChannels.CreateVoltageChannel(
            canalSelecionado,
            "CanalDinamico",
            AITerminalConfiguration.Differential,
            0, 10,
            AIVoltageUnits.Volts);

        AnalogSingleChannelReader leitor = new AnalogSingleChannelReader(Tarefa.Stream);
        Tarefa.Start();

        seriesCollection = new IChartSeries[] { new LiveChartsCore.SkiaSharpView.LineSeries<double> { Values = new ChartValues<double>() } }; // Ajuste aqui
        grafico.Series = seriesCollection;

        while (executando)
        {
            double valorLido = leitor.ReadSingleSample();
            this.Invoke((MethodInvoker)delegate { seriesCollection[0].Values.Add(valorLido); }); // Ajuste aqui
            Thread.Sleep(500);
        }

        Tarefa.Stop();
        Tarefa.Dispose();
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
