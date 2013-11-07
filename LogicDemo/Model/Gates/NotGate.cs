#region References

using Logic.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Logic.Model.Gates
{
    #region NotGate

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

    #endregion
}
