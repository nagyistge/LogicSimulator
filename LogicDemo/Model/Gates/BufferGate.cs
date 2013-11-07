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
    #region BufferGate

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

    #endregion
}
