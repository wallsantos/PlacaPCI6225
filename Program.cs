using System;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using NationalInstruments.DAQmx;

class Program{
    static void Main(){
        string canal = "Dev1/ai0";
        bool rodando = true;
        try{
            using(Task Tarefa = new Task()){
                Tarefa.AIChannels.CreateVoltageChannel(canal,"Meu Canal",AITerminalConfiguration.Rse,-10,10,AIVoltageUnits.Volts);
                AnalogSingleChannelReader leitor = new AnalogSingleChannelReader(Tarefa.Stream);
                Tarefa.Start();
                Console.WriteLine("Pressione qualquer tecla para parar...");
                Thread leituraThread = new Thread(()=>{
                    while(rodando){
                        double valorLido = leitor.ReadSingleSample();
                        Console.WriteLine($"Valor lido do canal {canal}: {valorLido:F3}V");
                        Thread.Sleep(500);
                    }
                });
                leituraThread.Start();
                Console.ReadKey();
                rodando = false;
                leituraThread.Join();
                Tarefa.Stop();
            }
        }
        catch(DaqException ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}