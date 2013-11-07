#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace WpfApplication1.Selectors
{
    public class LogicValueDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            try
            {
                var key = item.GetType().Name + "ValueDataTemplateKey";
                return (DataTemplate)Application.Current.FindResource(key);
                //return (DataTemplate)Application.Current.Windows[0].FindResource(item.GetType().Name + "ValueDataTemplateKey");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Exception: {0}", ex.Message);
            }
            return null;
        }
    }
}
