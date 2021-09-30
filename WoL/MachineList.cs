using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoL
{
    public class MachineList : ObservableCollection<Machine>
    {
        public MachineList() : base()
        {

        }

        public void InsertMachine(Machine machine)
        {
            Add(machine);
        }
    }
}
