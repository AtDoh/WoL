using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WoL
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private wol wolInfo;
        private MachineList machineList;
        private MachineJson machineJson;
        public MainWindow()
        {
            wolInfo = new wol();
            machineJson = new MachineJson();
            machineList = machineJson.ReadJsonFile();
            InitializeComponent();
            MachineList.ItemsSource = machineList;
            MachineList.SelectedIndex = 0;
        }

        private byte str2hex(string strData)
        {
            return Convert.ToByte(strData, 16);
        }

        private byte[] ConvertSA2BA(string[] stringA)
        {
            byte[] result = new byte[4];
            for(int idx = 0; idx < stringA.Length; ++idx)
            {
                result[idx] = Convert.ToByte(stringA[idx]);
            }
            return result;
        }

        private void setMAC(string[] mac)
        {
            addressA.Text = mac[0];
            addressB.Text = mac[1];
            addressC.Text = mac[2];
            addressD.Text = mac[3];
            addressE.Text = mac[4];
            addressF.Text = mac[5];
        }

        private string[] getMAC()
        {
            return new string[] { addressA.Text, addressB.Text, addressC.Text, addressD.Text, addressE.Text, addressF.Text, };
        }

        private void setGateway(byte[] gate)
        {
            gateA.Text = gate[0].ToString();
            gateB.Text = gate[1].ToString();
            gateC.Text = gate[2].ToString();
            gateD.Text = gate[3].ToString();
        }

        private byte[] getGateway()
        {
            return ConvertSA2BA(new string[] { gateA.Text, gateB.Text, gateC.Text, gateD.Text });
        }

        private void setSubNet(byte[] sub)
        {
            subA.Text = sub[0].ToString();
            subB.Text = sub[1].ToString();
            subC.Text = sub[2].ToString();
            subD.Text = sub[3].ToString();
        }

        private byte[] getSubNet()
        {
            return ConvertSA2BA(new string[] { subA.Text, subB.Text, subC.Text, subD.Text });
        }

        private void Button_Boot(object sender, RoutedEventArgs e)
        {
            Machine selected = (Machine)MachineList.SelectedItem;
            wolInfo.bootMachine(selected);
            return;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            Machine selected = MachineList.SelectedItem as Machine;
            selected.setMachine(Name.Text, getMAC(), getGateway(), getSubNet());
            machineJson.SaveJsonFile(machineList);
            MachineList.Items.Refresh();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
           if(MachineList.SelectedItem != null)
            {
                machineList.Remove(MachineList.SelectedItem as Machine);
                machineJson.SaveJsonFile(machineList);
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            machineList.Add(new Machine(Name.Text, getMAC(), getGateway(), getSubNet()));
            machineJson.SaveJsonFile(machineList);
        }

        private void MachineList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((sender as ListView).SelectedItem as Machine) != null)
            {
                Name.Text = ((sender as ListView).SelectedItem as Machine).Name;
                setMAC(((sender as ListView).SelectedItem as Machine).getMac());
                setGateway(((sender as ListView).SelectedItem as Machine).getGateway());
                setSubNet(((sender as ListView).SelectedItem as Machine).getSubNet());
            }
        }

    }
}
