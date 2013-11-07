﻿#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Logic.Model.Gates
{
    #region XorGate

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

    #endregion
}
