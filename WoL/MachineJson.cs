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
        private string _saveFilePath;
        private FileStream _addressJsonFile;

        public MachineJson()
        {
            _saveFilePath = "WolList.txt";
        }

        public MachineList ReadJsonFile()
        {
            MachineList result = new MachineList();
            _addressJsonFile = File.Open(_saveFilePath, FileMode.OpenOrCreate);
            if (_addressJsonFile.Length <= 2)
            {
                Machine defaultMachine = new Machine();
                result.Add(defaultMachine);
                JsonSerializer.SerializeAsync(_addressJsonFile, result);
                // _addressJsonFile.Write();
                _addressJsonFile.Close();
            }
            else
            {
                _addressJsonFile.Close();
                string jsonString = File.ReadAllText(_saveFilePath);
                result = JsonSerializer.Deserialize<MachineList>(jsonString);
            }
            return result;
        }
        
        async public void SaveJsonFile(MachineList list)
        {
            _addressJsonFile = File.Open("WolList.txt", FileMode.Create);
            await JsonSerializer.SerializeAsync(_addressJsonFile, list);
            _addressJsonFile.Close();
        }
    }
}
