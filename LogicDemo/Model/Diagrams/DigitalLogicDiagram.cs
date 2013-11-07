#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

#endregion

namespace Logic.Model.Diagrams
{
    #region DigitalLogicDiagram

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
}
