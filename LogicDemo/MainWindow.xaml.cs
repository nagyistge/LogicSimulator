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

    public static class DigitalExtensions
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
