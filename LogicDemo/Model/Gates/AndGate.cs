#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Logic.Model.Gates
{
    #region AndGate

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

    #endregion
}
