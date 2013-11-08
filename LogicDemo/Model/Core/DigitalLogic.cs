#region References

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

#endregion

namespace Logic.Model.Core
{
    #region DigitalLogic

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
}
