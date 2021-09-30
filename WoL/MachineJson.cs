using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace WoL
{
    class MachineJson
    {
        private FileStream _addressJsonFile;
        private string _addressListJson;

        public MachineJson()
        {
            _addressJsonFile = File.Open("WolList.txt", FileMode.OpenOrCreate);
            if (_addressJsonFile.Length == 0)
            {
                _addressListJson = JsonSerializer.Serialize("{ }");
                // _addressJsonFile.Write();
            }
            else
            {

            }
            _addressJsonFile.Close();
        }
        
        async public void SaveJsonFile(MachineList list)
        {
            _addressJsonFile = File.Open("WolList.txt", FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(_addressJsonFile, list);
            _addressJsonFile.Close();
        }
    }
}
