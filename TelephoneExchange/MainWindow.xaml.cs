using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TelephoneExchange.Models;
using TelephoneExchange.Services;

namespace TelephoneExchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private readonly IAgentService agentService;
        private readonly ICallService callService;
        private readonly IConsoleService consoleService;
        private readonly IGenerator generator;

        private readonly AppSettings settings;
        private Thread _Logic;
        public static Queue<Call> Calls { get; set; }
        public static List<Call> ActiveCall { get; set; }


        public MainWindow(IGenerator generator, ICallService callService, IAgentService agentService, IConsoleService consoleService, IOptions<AppSettings> settings)
        {
            InitializeComponent();

            this.generator = generator;
            this.callService = callService;
            this.consoleService = consoleService;
            this.agentService = agentService;
            this.settings = settings.Value;

            var allAgents = agentService.GetAllAgents();
            foreach (var agent in allAgents)
            {
                AgenstList.Items.Add(agent);
            }

            

            Calls = new Queue<Call>();
            ActiveCall = new List<Call>();
            var newCalls = callService.GenerateCalls();
            foreach (var call in newCalls)
            {
                Calls.Enqueue(call);
            }


            LogConsole.Items.Add(consoleService.CallInfo(Calls.Count()));
            Task.Factory.StartNew(() =>
            {

                BeginInvokeExample();


            });

      

   
        }

        private void BeginInvokeExample()
        {

            while (true)
            {
                Thread.Sleep(5000);
                DispatcherOperation op = Dispatcher.BeginInvoke((Action)(() => {

                    var toRemove = new List<Call>();
                    foreach (var activeCall in ActiveCall)
                    {
                        if(activeCall.CallStart.AddSeconds(activeCall.DurationInSec) <= DateTime.Now)
                        {
                            LogConsole.Items.Add(consoleService.EndCall(activeCall.AgentName, activeCall.DurationInSec));
                            agentService.AgentDismissal(activeCall.AgentId);
                            toRemove.Add(activeCall);
                        }
                    }

                    foreach (var tr in toRemove)
                    {
                        ActiveCall.Remove(tr);
                    }


                    var call = Calls.Peek();
                    
                    if (call != null)
                    {
                        
                        var agent = agentService.GetFreeAgent();
                        if(agent != null)
                        {
                            Calls.Dequeue();

                            call.AgentName = agent.Name;
                            call.AgentId = agent.Id;
                            call.CallStart = DateTime.Now;

                            ActiveCall.Add(call);
                            LogConsole.Items.Add(consoleService.StartCall(call.AgentName));
                        }                                               
                    }                    
                }));
            }
           
        }

        private void buttonAddAgent_Click(object sender, RoutedEventArgs e)
        {
            if (NameNewAgent.Text == string.Empty)
            {
                labelError.Visibility = Visibility.Visible;
            }
            else
            {
                labelError.Visibility = Visibility.Hidden;
                var agent = agentService.AddNewAgent(NameNewAgent.Text);
                NameNewAgent.Text = "";
                AgenstList.Items.Add(agent);
                LogConsole.Items.Add(consoleService.AddNewAgent(agent.Name));
            }



        }

        private void RemoveAgent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Agent agentToRemove = (Agent)AgenstList.SelectedItem;
                agentService.RemoveAgent(agentToRemove.Id);
                AgenstList.Items.Remove(agentToRemove);
                LogConsole.Items.Add(consoleService.RemoveAgent(agentToRemove.Name));
            }
            catch (Exception)
            {


            }

        }

        private void CallAgent_Click(object sender, RoutedEventArgs e)
        {
            var maxId = Calls.Max(a => a.Id);
            var currentId = maxId + 1;
            var newCall = new Call()
            {
                Id = currentId,
                DurationInSec = generator.GenerateNumber()
            };

            Calls.Enqueue(newCall);
            LogConsole.Items.Add(consoleService.AddNewCall());
            LogConsole.Items.Add(consoleService.CallInfo(Calls.Count()));
        }
    }
}
