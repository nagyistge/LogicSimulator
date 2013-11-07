#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

#endregion

namespace Logic.Model.Rx
{
    #region DigitalExtensions

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
}
