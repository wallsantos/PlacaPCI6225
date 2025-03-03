using System;
using NationalInstruments.DAQmx;

class Program
{
    static void Main()
    {
        Console.WriteLine("Iniciando teste");
//Falta arrumar o DAQmx para versao aceita pelo codigo
        /*using(NationalInstruments.DAQmx.Task Tarefa = new NationalInstruments.DAQmx.Task()){
            Tarefa.AIChannels.CreateVoltageChannel(
            "Dev1/ai0",
            "CanalSimulado",
            AITerminalConfiguration.Differential,
            0,10,
            AIVoltageUnits.Volts);
            AnalogSingleChannelReader leitor = new AnalogSingleChannelReader(Tarefa.Stream);
            Tarefa.Start();

            for(int i = 0;i<10;i++){
                double valorLido = leitor.ReadSingleSample();
                Console.WriteLine($"Leitura{i+1}:{valorLido:F3} V");
                System.Threading.Thread.Sleep(500);
            }
            Tarefa.Stop();
        }*/
        Console.WriteLine("Finalizado");
    }
}