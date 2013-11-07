#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Logic.Model.Diagrams
{
    #region IDigitalLogicDiagram

    public interface IDigitalLogicDiagram
    {
        ObservableCollection<LogicObject> Elements { get; set; }
    }

    #endregion
}
