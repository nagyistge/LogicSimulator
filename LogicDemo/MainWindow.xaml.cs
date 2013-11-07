
namespace WpfApplication1
{
    #region References

    using System;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Runtime.Serialization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml;
    using Logic.Model;

    #endregion

    #region MainWindow

    public partial class MainWindow : Window
    {
        #region Constructor

        private IScheduler scheduler = Scheduler.Default;

        public MainWindow()
        {
            InitializeComponent();

            // Test Diagram #1 (Timers)
            this.DataContext = LogicDiagramTests.GetTestDigitalLogicDiagram1(scheduler);

            // Test Diagram #2 (SR NOR latch)
            //this.DataContext = LogicDiagramTests.GetTestDigitalLogicDiagram2(scheduler);
        }

        #endregion

        #region Events

        private void menuItemFileOpenDiagram_Click(object sender, RoutedEventArgs e)
        {
            var diagram = Serializer.OpenDiagram();
            if (diagram != null)
            {
                // initialize diagram and elements
                diagram.ObserveInputs(scheduler);
                diagram.ObserveElements(scheduler);

                this.DataContext = diagram;
            }
        }

        private void menuItemFileSaveDiagram_Click(object sender, RoutedEventArgs e)
        {
            var diagram = this.DataContext as DigitalLogicDiagram;
            if (diagram != null)
            {
                Serializer.SaveDiagram(diagram);
            }
        }

        private void menuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }

    #endregion

    #region Tests: Digital Logic Gates

    public static class LogicGatesTests
    {
        #region AndGateTests

        public static void AndGateTests()
        {
            System.Diagnostics.Debug.Print("Running AndGate Tests:");

            // 01
            {
                var g = new AndGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 02
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 03
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 04
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 05
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 06
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 07
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            // 08
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            //09
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 10
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 11
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 12
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 13
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 14
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 15
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            // 16
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 17
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 18
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 19
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 20
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            // 21
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 22
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            // 23
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            // 24
            {
                var g = new AndGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done AndGate Tests\n");
        }

        #endregion

        #region OrGateTests

        public static void OrGateTests()
        {
            System.Diagnostics.Debug.Print("Running OrGate Tests:");

            {
                var g = new OrGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new OrGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done OrGate Tests\n");
        }

        #endregion

        #region NotGateTests

        public static void NotGateTests()
        {
            System.Diagnostics.Debug.Print("Running NotGate Tests:");

            {
                var g = new NotGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NotGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new NotGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done NotGate Tests\n");
        }

        #endregion

        #region BufferGateTests

        public static void BufferGateTests()
        {
            System.Diagnostics.Debug.Print("Running BufferGate Tests:");

            {
                var g = new BufferGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == null);
                System.Diagnostics.Debug.Print("{1} => [null] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == null ? "ok" : "nok");
            }

            {
                var g = new BufferGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new BufferGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done BufferGate Tests\n");
        }

        #endregion

        #region NandGateTests

        public static void NandGateTests()
        {
            System.Diagnostics.Debug.Print("Running NandGate Tests:");

            {
                var g = new NandGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NandGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NandGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NandGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new NandGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new NandGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new NandGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done NandGate Tests\n");
        }

        #endregion

        #region NorGateTests

        public static void NorGateTests()
        {
            System.Diagnostics.Debug.Print("Running NorGate Tests:");

            {
                var g = new NorGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new NorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new NorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done NorGate Tests\n");
        }

        #endregion

        #region XorGateTests

        public static void XorGateTests()
        {
            System.Diagnostics.Debug.Print("Running XorGate Tests:");

            {
                var g = new XorGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done XorGate Tests\n");
        }

        #endregion

        #region XnorGateTests

        public static void XnorGateTests()
        {
            System.Diagnostics.Debug.Print("Running XnorGate Tests:");

            {
                var g = new XnorGate();
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");

            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(false));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == false);
                System.Diagnostics.Debug.Print("{1} => [false] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == false ? "ok" : "nok");
            }

            {
                var g = new XnorGate();
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Inputs.Add(new DigitalSignal(true));
                g.Outputs.Add(new DigitalSignal());
                g.Calculate();
                System.Diagnostics.Debug.Assert(g.Outputs.First().State == true);
                System.Diagnostics.Debug.Print("{1} => [true] g={0}", g.Outputs.First().State.ToString(), g.Outputs.First().State == true ? "ok" : "nok");
            }

            System.Diagnostics.Debug.Print("Done XnorGate Tests\n");
        }

        #endregion
    }

    #endregion

    #region Tests: Digital Logic Diagrams

    public static class LogicDiagramTests
    {
        #region Test Diagram #1 (Timers)

        public static DigitalLogicDiagram GetTestDigitalLogicDiagram1(IScheduler scheduler)
        {
            // create diagram
            var diagram = new DigitalLogicDiagram() { Id = Guid.NewGuid() };

            // create signals
            var input1 = DigitalLogicFactory.CreateDigitalSignal("input1", 0, 90, 0);
            var input2 = DigitalLogicFactory.CreateDigitalSignal("input2", 0, 150, 0);

            var output1 = DigitalLogicFactory.CreateDigitalSignal("output1", 0, 0, 0);
            var output2 = DigitalLogicFactory.CreateDigitalSignal("output2", 0, 0, 0);
            var output3 = DigitalLogicFactory.CreateDigitalSignal("output3", 450, 90, 0);

            // create elements
            var andGate1 = DigitalLogicFactory.CreateAndGate("andGate1", 180, 90, 0);
            var timerOnDelay1 = DigitalLogicFactory.CreateTimerOnDelay("timerOnDelay1", 270, 90, 0, 2.0);
            var timerPulse1 = DigitalLogicFactory.CreateTimerPulse("timerPulse1", 360, 90, 0, 1.0);

            // create digital wires with signal and pin bindings
            var wire1 = DigitalLogicFactory.CreateDigitalWire("wire1", input1.OutputPin, andGate1.Pins[3], input1);
            var wire2 = DigitalLogicFactory.CreateDigitalWire("wire2", input2.OutputPin, andGate1.Pins[2], input2);

            var wire3 = DigitalLogicFactory.CreateDigitalWire("wire3", andGate1.Pins[1], timerOnDelay1.Pins[3], output1);
            var wire4 = DigitalLogicFactory.CreateDigitalWire("wire4", timerOnDelay1.Pins[1], timerPulse1.Pins[3], output2);
            var wire5 = DigitalLogicFactory.CreateDigitalWire("wire5", timerPulse1.Pins[1], output3.InputPin, output3);

            // connect signals to elements
            andGate1.Outputs.Add(output1);
            andGate1.Inputs.Add(input1);
            andGate1.Inputs.Add(input2);

            timerOnDelay1.Outputs.Add(output2);
            timerOnDelay1.Inputs.Add(output1);

            timerPulse1.Outputs.Add(output3);
            timerPulse1.Inputs.Add(output2);

            // add all digital elements/signals/wires to list
            diagram.Elements.Add(wire1);
            diagram.Elements.Add(wire2);
            diagram.Elements.Add(wire3);
            diagram.Elements.Add(wire4);
            diagram.Elements.Add(wire5);

            diagram.Elements.Add(andGate1);
            diagram.Elements.Add(timerOnDelay1);
            diagram.Elements.Add(timerPulse1);

            diagram.Elements.Add(input1);
            diagram.Elements.Add(input2);

            diagram.Elements.Add(output3);

            // initialize input/output vector
            output1.State = false;
            output2.State = false;
            output3.State = false;

            input1.State = false;
            input2.State = false;

            // initialize diagram and elements
            diagram.ObserveInputs(scheduler);
            diagram.ObserveElements(scheduler);

            return diagram;
        }

        #endregion

        #region Test Diagram #2 (SR NOR latch)

        public static DigitalLogicDiagram GetTestDigitalLogicDiagram2(IScheduler scheduler)
        {
            //
            // SR NOR latch
            //
            // SR latch operation
            // S	R	Action
            // 0	0	No Change
            // 0	1	Q = 0
            // 1	0	Q = 1
            // 1	1	Restricted combination
            //
            // mofre info: http://en.wikipedia.org/wiki/Flip-flop_(electronics)
            //

            // create diagram
            //var diagram = new DigitalLogicDiagram(collectionScheduler, simulationScheduler) { Id = Guid.NewGuid() };
            var diagram = new DigitalLogicDiagram() { Id = Guid.NewGuid() };

            // create digital input signals
            var input1 = new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = "R",
                X = 0,
                Y = 90
            };

            var input2 = new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = "S",
                X = 0,
                Y = 150
            };

            // create digital output signals
            var output1 = new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = "Q",
                X = 270,
                Y = 90
            };

            var output2 = new DigitalSignal() // TODO: Q' must be 'true' for the init value for the SR ("set-reset") latch
            {
                Id = Guid.NewGuid(),
                Name = "Q'",
                X = 270,
                Y = 150
            };

            // create logic element and signal bindings
            //var norGate1 = new NorGate(collectionScheduler, simulationScheduler)
            var norGate1 = new NorGate()
            {
                Id = Guid.NewGuid(),
                Name = "NorGate1",
                Inputs = { input1, output2 },
                Outputs = { output1 }, // TODO: add Inputs before Outputs to properly init diagram
                X = 180,
                Y = 90
            };

            //var norGate2 = new NorGate(collectionScheduler, simulationScheduler)
            var norGate2 = new NorGate()
            {
                Id = Guid.NewGuid(),
                Name = "NorGate2",
                Inputs = { input2, output1 },
                Outputs = { output2 }, // TODO: add Inputs before Outputs to properly init diagram
                X = 180,
                Y = 150
            };

            // create digital wires with signal and pin bindings
            var wire1 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire1",
                Signal = input1,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 120,
                    Y = 105
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 180,
                    Y = 105
                }
            };

            var wire2 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire2",
                Signal = input2,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 120,
                    Y = 165
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 180,
                    Y = 165
                }
            };

            var wire3 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire3",
                Signal = output1,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 218,
                    Y = 105
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 270,
                    Y = 105
                }
            };

            var wire4 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire4",
                Signal = output2,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 218,
                    Y = 165
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 270,
                    Y = 165
                }
            };

            var wire5 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire5",
                Signal = output1,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 240,
                    Y = 105
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 195,
                    Y = 150
                }
            };

            var wire6 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire6",
                Signal = output2,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 240,
                    Y = 165
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 195,
                    Y = 120
                }
            };

            /*
            // add all digital input signals to list
            diagram.Inputs.Add(input1);
            diagram.Inputs.Add(input2);

            // add all digital output signals to list
            diagram.Outputs.Add(output1);
            diagram.Outputs.Add(output2);

            // add all digital wires to list
            diagram.Wires.Add(wire1);
            diagram.Wires.Add(wire2);
            diagram.Wires.Add(wire3);
            diagram.Wires.Add(wire4);
            diagram.Wires.Add(wire5);
            diagram.Wires.Add(wire6);
            */

            // add all digital elements/signals/wires to list

            diagram.Elements.Add(wire1);
            diagram.Elements.Add(wire2);
            diagram.Elements.Add(wire3);
            diagram.Elements.Add(wire4);
            diagram.Elements.Add(wire5);
            diagram.Elements.Add(wire6);

            diagram.Elements.Add(norGate1);
            diagram.Elements.Add(norGate2);

            diagram.Elements.Add(input1);
            diagram.Elements.Add(input2);

            diagram.Elements.Add(output1);
            diagram.Elements.Add(output2);

            // initialize input/output vector
            output1.State = false; // Q
            output2.State = false; // Q'

            input2.State = false; // S
            input1.State = false; // R

            // initialize diagram and elements
            diagram.ObserveInputs(scheduler);
            diagram.ObserveElements(scheduler);

            return diagram;
        }

        #endregion
    }

    #endregion
}

namespace Logic.Model
{
    #region References

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Runtime.Serialization;
    using System.Xml;

    #endregion

    #region Digital Logic Core

    public interface ILogicObject { }

    public interface IId
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    public interface ILocation
    {
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
    }

    public abstract class LogicObject : INotifyPropertyChanged, ILogicObject, IId, ILocation
    {
        #region INotifyPropertyChanged Implementation

        public virtual void Notify(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IId Implementation

        private Guid id = Guid.Empty;
        private string name = string.Empty;

        public Guid Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    Notify("Id");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    Notify("Name");
                }
            }
        }

        #endregion

        #region ILocation Implementation

        private double x;
        private double y;
        private double z;

        public double X
        {
            get { return x; }
            set
            {
                if (value != x)
                {
                    x = value;
                    Notify("X");
                }
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                if (value != y)
                {
                    y = value;
                    Notify("Y");
                }
            }
        }

        public double Z
        {
            get { return z; }
            set
            {
                if (value != z)
                {
                    z = value;
                    Notify("Z");
                }
            }
        }

        #endregion
    }

    public interface IDigitalPin 
    {
        DigitalSignal Signal { get; set; }
    }

    public class DigitalPin : LogicObject, IDigitalPin
    {
        #region IDigitalPin Implementation

        private DigitalSignal signal;

        public DigitalSignal Signal
        {
            get { return signal; }
            set
            {
                if (value != signal)
                {
                    signal = value;
                    Notify("Signal");
                }
            }
        }

        #endregion
    }

    public interface IDigitalSignal
    {
        bool? State { get; set; }
        DigitalPin InputPin { get; set; }
        DigitalPin OutputPin { get; set; }
    }

    public class DigitalSignal : LogicObject, IDigitalSignal
    {
        #region Constructor

        public DigitalSignal() { }

        public DigitalSignal(bool state)
            : this()
        {
            this.state = state;
        }

        #endregion

        #region IDigitalSignal Implementation

        private bool? state = null;
        private DigitalPin inputPin = null;
        private DigitalPin outputPin = null;

        public bool? State
        {
            get { return state; }
            set
            {
                if (value != state)
                {
                    state = value;
                    Notify("State");
                }
            }
        }

        public DigitalPin InputPin
        {
            get { return inputPin; }
            set
            {
                if (value != inputPin)
                {
                    inputPin = value;
                    Notify("InputPin");
                }
            }
        }

        public DigitalPin OutputPin
        {
            get { return outputPin; }
            set
            {
                if (value != outputPin)
                {
                    outputPin = value;
                    Notify("OutputPin");
                }
            }
        }

        #endregion
    }

    public interface IDigitalWire
    {
        DigitalSignal Signal { get; set; }
        DigitalPin StartPin { get; set; }
        DigitalPin EndPin { get; set; }
    }

    public class DigitalWire : LogicObject, IDigitalWire
    {
        #region IDigitalWire Implementation

        private DigitalSignal signal;
        private DigitalPin startPin;
        private DigitalPin endPin;

        public DigitalSignal Signal
        {
            get { return signal; }
            set
            {
                if (value != signal)
                {
                    signal = value;
                    Notify("Signal");
                }
            }
        }

        public DigitalPin StartPin
        {
            get { return startPin; }
            set
            {
                if (value != startPin)
                {
                    startPin = value;
                    Notify("StartPin");
                }
            }
        }

        public DigitalPin EndPin
        {
            get { return endPin; }
            set
            {
                if (value != endPin)
                {
                    endPin = value;
                    Notify("EndPin");
                }
            }
        }

        #endregion
    }

    public interface IDigitalLogic
    {
        ObservableCollection<DigitalSignal> Inputs { get; set; }
        ObservableCollection<DigitalSignal> Outputs { get; set; }
        ObservableCollection<DigitalPin> Pins { get; set; }
    }

    public abstract class DigitalLogic : LogicObject, IDigitalLogic
    {
        #region IDigitalLogic Implementation

        private ObservableCollection<DigitalSignal> inputs = new ObservableCollection<DigitalSignal>();
        private ObservableCollection<DigitalSignal> outputs = new ObservableCollection<DigitalSignal>();
        private ObservableCollection<DigitalPin> pins = new ObservableCollection<DigitalPin>();

        public virtual ObservableCollection<DigitalSignal> Inputs
        {
            get { return inputs; }
            set
            {
                if (value != inputs)
                {
                    inputs = value;
                    Notify("Inputs");
                }
            }
        }

        public virtual ObservableCollection<DigitalSignal> Outputs
        {
            get { return outputs; }
            set
            {
                if (value != outputs)
                {
                    outputs = value;
                    Notify("Outputs");
                }
            }
        }

        public virtual ObservableCollection<DigitalPin> Pins
        {
            get { return pins; }
            set
            {
                if (value != pins)
                {
                    pins = value;
                    Notify("Pins");
                }
            }
        }

        #endregion

        #region Constructor

        /*
        protected DigitalLogic() 
            : base()
        {
        }

        protected DigitalLogic(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : this()
        {
            //Initialize(collectionScheduler, simulationScheduler);
        }
        */

        #endregion

        #region Abstract Interface

        public abstract void Calculate();

        #endregion

        #region Initialize

        /*
        private Dictionary<Guid, IDisposable> observables = new Dictionary<Guid, IDisposable>();
        private IDisposable disposableAddedValues = null;
        private IDisposable disposableRemovedValues = null;

        public void InitializeInput(DigitalSignal input, IScheduler simulationScheduler)
        {
            // observe input state changes
            var dispose = input.FromPropertyChange("State").ObserveOn(simulationScheduler).Subscribe(sender =>
            {
                //System.Diagnostics.Debug.Print("simulationScheduler: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
                Calculate();
            });

            // store IDisposable
            observables.Add(input.Id, dispose);
        }

        public void DeInitializeInput(DigitalSignal input)
        {
            // dispose input state changes observable
            var id = input.Id;
            observables[id].Dispose();
            observables.Remove(id);
        }

        public void Initialize(IScheduler collectionScheduler, IScheduler simulationScheduler)
        {
            //
            // TODO: handle all cases of NotifyCollectionChangedAction
            // Add		One or more items were added to the collection.
            // Remove	One or more items were removed from the collection.
            // Replace	One or more items were replaced in the collection.
            // Move	One or more items were moved within the collection.
            // Reset	The content of the collection changed dramatically.
            //

            // subscribe to Inputs collection Add action
            disposableAddedValues = Inputs.ObserveAddedValues().ObserveOn(collectionScheduler).Subscribe(input =>
            {
                //System.Diagnostics.Debug.Print("collectionScheduler: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

                // initialize input subscription
                InitializeInput(input, simulationScheduler);

                // update logic output
                Calculate();
            });

            // subscribe to Input sollection Remove action
            disposableRemovedValues = Inputs.ObserveRemovedValues().ObserveOn(collectionScheduler).Subscribe(input =>
            {
                //System.Diagnostics.Debug.Print("collectionScheduler: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

                // cancel input subscription
                DeInitializeInput(input);

                // update logic output
                Calculate();
            });

            // initialize existing Inputs (necessary for serialization)
            if (Inputs.Count > 0)
            {
                foreach (var input in Inputs)
                {
                    InitializeInput(input, simulationScheduler);
                }

                // update logic output
                Calculate();
            }
        }

        public void DeInitialize()
        {
            // cancel Inputs collection Add action subscription
            if (disposableAddedValues != null)
            {
                disposableAddedValues.Dispose();
                disposableAddedValues = null;
            }

            // cancel Inputs collection Remove action subscription
            if (disposableRemovedValues != null)
            {
                disposableRemovedValues.Dispose();
                disposableRemovedValues = null;
            }
        }
        */

        #endregion
    }

    #endregion

    #region Digital Logic Gates

    public class AndGate : DigitalLogic
    {
        #region Constructor
        /*
        public AndGate()
            : base()
        {
        }

        public AndGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("AndGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = (Inputs.Count < 2) ? false : Inputs.All(i => (bool)i.State == true);
            }
        }

        #endregion
    }

    public class OrGate : DigitalLogic
    {
        #region Constructor
        /*
        public OrGate()
            : base()
        {
        }

        public OrGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("OrGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = Inputs.Count < 2 ? false : !Inputs.All(i => i.State == false);
            }
        }

        #endregion
    }

    public class NotGate : DigitalLogic
    {
        #region Constructor
        /*
        public NotGate()
            : base()
        {
        }

        public NotGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("NotGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = Inputs.Count != 1 ? false : !Inputs.First().State;
            }
        }

        #endregion
    }

    public class BufferGate : DigitalLogic
    {
        #region Constructor
        /*
        public BufferGate()
            : base()
        {
        }

        public BufferGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("BufferGate Calculate() (Name={0})", Name);

            if (Inputs.Count > 0 && (Inputs.Count == Outputs.Count) && Inputs.All(i => i.State.HasValue))
            {
                for (int i = 0; i < Inputs.Count; i++)
                {
                    Outputs[i].State = Inputs[i].State;
                }
            }
        }

        #endregion
    }

    public class NandGate : DigitalLogic
    {
        #region Constructor
        /*
        public NandGate()
            : base()
        {
        }

        public NandGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("NandGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = Inputs.Count < 2 ? false : !Inputs.All(i => i.State == true);
            }
        }

        #endregion
    }

    public class NorGate : DigitalLogic
    {
        #region Constructor
        /*
        public NorGate()
            : base()
        {
        }

        public NorGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("NorGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = Inputs.Count < 2 ? false : Inputs.All(i => i.State == false);
            }
        }

        #endregion
    }

    public class XorGate : DigitalLogic
    {
        #region Constructor
        /*
        public XorGate()
            : base()
        {
        }

        public XorGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("XorGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = Inputs.Count < 2 ? false : Inputs.Count(i => i.State == true) % 2 != 0;
            }
        }

        #endregion
    }

    public class XnorGate : DigitalLogic
    {
        #region Constructor
        /*
        public XnorGate()
            : base()
        {
        }

        public XnorGate(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */
        #endregion

        #region Calculate Implementation

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("XnorGate Calculate() (Name={0})", Name);

            if (Outputs.Count == 1 && Inputs.All(i => i.State.HasValue))
            {
                Outputs.First().State = Inputs.Count < 2 ? false : Inputs.Count(i => i.State == true) % 2 == 0;
            }
        }

        #endregion
    }

    #endregion

    #region Digital Logic Timers

    public class TimerPulse : DigitalLogic
    {
        #region Constructor

        public TimerPulse()
            : base()
        {
        }

        public TimerPulse(double delay)
            : this()
        {
            this.delay = delay;
        }

        /*
        public TimerPulse(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
            scheduler = simulationScheduler;
        }

        public TimerPulse(IScheduler collectionScheduler, IScheduler simulationScheduler, double delay)
            : this(collectionScheduler, simulationScheduler)
        {
            this.delay = delay;
        }
        */
        #endregion

        #region Properties

        private double delay = double.NaN;

        public virtual double Delay
        {
            get { return delay; }
            set
            {
                if (value != delay)
                {
                    delay = value;

                    Notify("Delay");
                }
            }
        }

        #endregion

        #region Calculate Implementation

        private IDisposable disposable = null;
        private IScheduler scheduler = null;

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("TimerPulse Calculate() (Name={0})", Name);

            if (Inputs.Count == 1 && Outputs.Count == 1)
            {
                if (Inputs.First().State == true)
                {
                    if (disposable == null)
                    {
                        // create timer
                        var observable = Observable.Timer(DateTimeOffset.Now.AddSeconds(delay), scheduler == null ? Scheduler.Default : scheduler);

                        // start pulse
                        Outputs.First().State = true;

                        //var s = System.Diagnostics.Stopwatch.StartNew();

                        // subcribe to timer
                        disposable = observable.Subscribe(x =>
                        {
                            //s.Stop();
                            //System.Diagnostics.Debug.Print("{0} TimerPulse Subscribe (Name={1})", s.Elapsed.ToString(), Name);

                            // update output
                            if (Outputs.Count == 1)
                            {
                                // end pulse after time 'delay'
                                Outputs.First().State = false;
                            }

                            // dispose timer
                            disposable.Dispose();
                            disposable = null;
                        });
                    }
                }
            }
        }

        #endregion
    }

    public class TimerOnDelay : DigitalLogic
    {
        #region Constructor

        public TimerOnDelay()
            : base()
        {
        }

        public TimerOnDelay(double delay)
            : this()
        {
            this.delay = delay;
        }

        /*
        public TimerOnDelay(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
            scheduler = simulationScheduler;
        }

        public TimerOnDelay(IScheduler collectionScheduler, IScheduler simulationScheduler, double delay)
            : this(collectionScheduler, simulationScheduler)
        {
            this.delay = delay;
        }
        */

        #endregion

        #region Properties

        private double delay = double.NaN;

        public virtual double Delay
        {
            get { return delay; }
            set
            {
                if (value != delay)
                {
                    delay = value;

                    Notify("Delay");
                }
            }
        }

        #endregion

        #region Calculate Implementation

        private IDisposable disposable = null;
        private IScheduler scheduler = null;

        public override void Calculate()
        {
            //System.Diagnostics.Debug.Print("TimerOnDelay Calculate() (Name={0})", Name);

            if (Inputs.Count == 1 && Outputs.Count == 1)
            {
                if (Inputs.First().State == true)
                {
                    if (disposable == null)
                    {
                        // create timer
                        var observable = Observable.Timer(DateTimeOffset.Now.AddSeconds(delay), scheduler == null ? Scheduler.Default : scheduler);

                        //var s = System.Diagnostics.Stopwatch.StartNew();

                        // subcribe to timer
                        disposable = observable.Subscribe(x =>
                        {
                            //s.Stop();
                            //System.Diagnostics.Debug.Print("{0} TimerOnDelay Subscribe (Name={1})", s.Elapsed.ToString(), Name);

                            // update output
                            if (Outputs.Count == 1)
                            {
                                Outputs.First().State = Inputs.Count != 1 ? false : Inputs.First().State;
                            }

                            // dispose timer
                            disposable.Dispose();
                            disposable = null;
                        });
                    }
                }
                else
                {
                    // dispose timer
                    if (disposable != null)
                    {
                        disposable.Dispose();
                        disposable = null;
                    }

                    // update output
                    if (Outputs.Count == 1)
                    {
                        Outputs.First().State = Inputs.Count != 1 ? false : Inputs.First().State;
                    }
                }
            }
        }

        #endregion
    }

    #endregion

    #region Digital Logic Diagram

    public interface IDigitalLogicDiagram
    {
        ObservableCollection<LogicObject> Elements { get; set; }
    }

    public class DigitalLogicDiagram : DigitalLogic, IDigitalLogicDiagram
    {
        #region Constructor

        /*
        public DigitalLogicDiagram()
            : base()
        {
        }

        public DigitalLogicDiagram(IScheduler collectionScheduler, IScheduler simulationScheduler)
            : base(collectionScheduler, simulationScheduler)
        {
        }
        */

        #endregion

        #region IDigitalLogicDiagram Implementation

        private ObservableCollection<LogicObject> elements = new ObservableCollection<LogicObject>();

        public ObservableCollection<LogicObject> Elements
        {
            get
            {
                return elements;
            }
            set
            {
                if (value != elements)
                {
                    elements = value;
                    Notify("Elements");
                }
            }
        }

        #endregion

        #region Calculate Implementation

        public override void Calculate() { }

        #endregion
    }

    #endregion

    #region Reactive Extensions

    public static class ReactiveExtensions
    {
        public static IObservable<EventPattern<NotifyCollectionChangedEventArgs>> FromCollectionChange(this INotifyCollectionChanged collection)
        {
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                        h => (s, e) => h(s, e),
                        h => collection.CollectionChanged += h,
                        h => collection.CollectionChanged -= h);
        }

        public static IObservable<object> FromPropertyChange(this INotifyPropertyChanged propertyChange, string propertyName)
        {
            return Observable.FromEventPattern<PropertyChangedEventArgs>(propertyChange, "PropertyChanged")
                .Where(p => p.EventArgs.PropertyName == propertyName)
                .Select(p => p.Sender);
        }

        public static IObservable<T> ObserveAddedValues<T>(this ObservableCollection<T> collection)
        {
            return collection.FromCollectionChange()
                .Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add)
                .SelectMany(e => e.EventArgs.NewItems.Cast<T>());
        }

        public static IObservable<T> ObserveRemovedValues<T>(this ObservableCollection<T> collection)
        {
            return collection.FromCollectionChange()
                .Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Remove)
                .SelectMany(e => e.EventArgs.OldItems.Cast<T>());
        }
    }

    #endregion

    #region Digital Logic Extensions

    public static class DigitalLogicExtensions
    {
        public static void ObserveElements(this DigitalLogicDiagram diagram, IScheduler scheduler)
        {
            // initialize elements
            var q = diagram.Elements.Where(x => x is DigitalLogic).Select(x => x as DigitalLogic);
            foreach (var element in q)
            {
                element.ObserveInputs(scheduler);
            }
        }

        public static void ObserveInputs(this DigitalLogic logic, IScheduler scheduler)
        {
            Dictionary<Guid, IDisposable> observables = new Dictionary<Guid, IDisposable>();

            //
            // TODO: handle all cases of NotifyCollectionChangedAction
            // Add		One or more items were added to the collection.
            // Remove	One or more items were removed from the collection.
            // Replace	One or more items were replaced in the collection.
            // Move	One or more items were moved within the collection.
            // Reset	The content of the collection changed dramatically.
            //

            // subscribe to Inputs collection Add action
            logic.Inputs.ObserveAddedValues().ObserveOn(scheduler).Subscribe(input =>
            {
                // observe input state changes
                var dispose = input.FromPropertyChange("State").ObserveOn(scheduler).Subscribe(sender =>
                {
                    //System.Diagnostics.Debug.Print("simulationScheduler: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
                    logic.Calculate();
                });

                // store IDisposable
                observables.Add(input.Id, dispose);

                // update logic output
                logic.Calculate();
            });

            // subscribe to Input sollection Remove action
            logic.Inputs.ObserveRemovedValues().ObserveOn(scheduler).Subscribe(input =>
            {
                //System.Diagnostics.Debug.Print("collectionScheduler: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

                // dispose input state changes observable
                var id = input.Id;
                observables[id].Dispose();
                observables.Remove(id);

                // update logic output
                logic.Calculate();
            });

            // initialize existing Inputs (necessary for serialization)
            if (logic.Inputs.Count > 0)
            {
                foreach (var input in logic.Inputs)
                {
                    // observe input state changes
                    var dispose = input.FromPropertyChange("State").ObserveOn(scheduler).Subscribe(sender =>
                    {
                        //System.Diagnostics.Debug.Print("simulationScheduler: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
                        logic.Calculate();
                    });

                    // store IDisposable
                    observables.Add(input.Id, dispose);
                }

                // update logic output
                logic.Calculate();
            }
        }
    }

    #endregion

    #region Digital Logic Factory

    public static class DigitalLogicFactory
    {
        public static DigitalPin CreateDigitalPin(string name, double x, double y, double z)
        {
            return new DigitalPin()
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z
            };
        }

        public static DigitalSignal CreateDigitalSignal(string name, double x, double y, double z)
        {
            return new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                InputPin = CreateDigitalPin("pin1", x, y + 15, 0), // left
                OutputPin = CreateDigitalPin("pin2", x + 120, y + 15, 0) // right
            };
        }

        public static DigitalWire CreateDigitalWire(string name, DigitalPin startPin, DigitalPin endPin, DigitalSignal signal)
        {
            startPin.Signal = signal;
            endPin.Signal = signal;

            return new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Signal = signal,
                StartPin = startPin,
                EndPin = endPin
            };
        }

        public static AndGate CreateAndGate(string name, double x, double y, double z)
        {
            return new AndGate()
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                Pins =
                {
                    CreateDigitalPin("pin1", x + 15, y, 0), // top
                    CreateDigitalPin("pin2", x + 30, y + 15, 0), // right
                    CreateDigitalPin("pin3", x + 15, y + 30, 0), // bottom
                    CreateDigitalPin("pin4", x, y + 15, 0) // left
                }
            };
        }

        public static TimerOnDelay CreateTimerOnDelay(string name, double x, double y, double z, double delay)
        {
            return new TimerOnDelay(delay)
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                Pins =
                {
                    CreateDigitalPin("pin1", x + 15, y, 0), // top
                    CreateDigitalPin("pin2", x + 30, y + 15, 0), // right
                    CreateDigitalPin("pin3", x + 15, y + 30, 0), // bottom
                    CreateDigitalPin("pin4", x, y + 15, 0) // left
                }
            };
        }

        public static TimerPulse CreateTimerPulse(string name, double x, double y, double z, double delay)
        {
            return new TimerPulse(delay)
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                Pins =
                {
                    CreateDigitalPin("pin1", x + 15, y, 0), // top
                    CreateDigitalPin("pin2", x + 30, y + 15, 0), // right
                    CreateDigitalPin("pin3", x + 15, y + 30, 0), // bottom
                    CreateDigitalPin("pin4", x, y + 15, 0) // left
                }
            };
        }
    }

    #endregion

    #region Digital Logic Serializer

    public static class Serializer
    {
        public static Type[] GetDiagramTypes()
        {
            return new Type[]
            { 
                typeof(DigitalPin),
                typeof(DigitalSignal),
                typeof(DigitalWire),
                typeof(AndGate),
                typeof(OrGate),
                typeof(NotGate),
                typeof(BufferGate),
                typeof(NandGate),
                typeof(NorGate),
                typeof(XorGate),
                typeof(XnorGate),
                typeof(TimerPulse),
                typeof(TimerOnDelay),
                typeof(DigitalLogicDiagram)
            };
        }

        public static DigitalLogicDiagram OpenDiagram()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog()
            {
                DefaultExt = "xml",
                Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*",
                FilterIndex = 0
            };

            if (dlg.ShowDialog() == true)
            {
                var s = new DataContractSerializer(typeof(DigitalLogicDiagram), GetDiagramTypes(), int.MaxValue, true, true, null);

                using (var reader = XmlReader.Create(dlg.FileName))
                {
                    return (DigitalLogicDiagram)s.ReadObject(reader);
                }
            }
            return null;
        }

        public static void SaveDiagram(DigitalLogicDiagram diagram)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog()
            {
                DefaultExt = "xml",
                Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*",
                FilterIndex = 0,
                FileName = "diagram1"
            };

            if (dlg.ShowDialog() == true)
            {
                if (diagram != null)
                {
                    var s = new DataContractSerializer(diagram.GetType(), GetDiagramTypes(), int.MaxValue, true, true, null);

                    using (var writer = XmlWriter.Create(dlg.FileName, new XmlWriterSettings() { Indent = true, IndentChars = "    " }))
                    {
                        s.WriteObject(writer, diagram);
                    }
                }
            }
        }
    }

    #endregion
}
