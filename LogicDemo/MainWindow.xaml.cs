#region References

using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Logic.Model;
using Logic.Tests;

#endregion

namespace Logic
{
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
                diagram.ObserveInputs(scheduler);
                diagram.ObserveElements(scheduler);
                this.DataContext = diagram;
            }
        }

        private void menuItemFileSaveDiagram_Click(object sender, RoutedEventArgs e)
        {
            var diagram = this.DataContext as DigitalLogicDiagram;
            if (diagram != null)
                Serializer.SaveDiagram(diagram);
        }

        private void menuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

    #region Digital Logic Factory

    public static class Factory
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
