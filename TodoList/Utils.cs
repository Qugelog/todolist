using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoList
{
    class Utils
    {
        public static string convertBool(ComboBox box)
        {
            if(box.SelectedItem.ToString().Contains("Да"))
            {
                return box.SelectedItem.ToString().Replace("Да", "True").Trim();
            } 
            else
            {
               return box.SelectedItem.ToString().Replace("Нет", "False").Trim();
            }
        }
    }
}
