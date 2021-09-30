using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WoL
{
    class wol
    {
        private UdpClient clt;
        private IPEndPoint broadcastEP;
        private byte[] magicPacekt;
        private byte[] macAddress;

        private byte str2hex(string strData)
        {
            return Convert.ToByte(strData, 16);
        }

        public wol()
        {
            magicPacekt = new byte[102];
            for (int i = 0; i < 6; ++i)
            {
                magicPacekt[i] = 0xFF;
            }
        }

        public void bootMachine(Machine machine)
        {
            broadcastEP = new IPEndPoint(IPAddress.Broadcast, 0);
            clt = new UdpClient(machine.TargetIP, 0);
            string[] macAddressStr = machine.getMac();
            macAddress = new byte[] { str2hex(macAddressStr[0]),str2hex(macAddressStr[1]),str2hex(macAddressStr[2]),str2hex(macAddressStr[3]),str2hex(macAddressStr[4]),str2hex(macAddressStr[5]) };
            
            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                    magicPacekt[6 + i * 6 + j] = macAddress[j];
                }
            }
            clt.Send(magicPacekt, magicPacekt.Length);
            clt.Close();
            return;
        }

        public void setMacAddress(byte a,byte b, byte c, byte d, byte e, byte f)
        {
            broadcastEP = new IPEndPoint(IPAddress.Broadcast, 0);
            clt = new UdpClient("192.168.0.255", 0);
            macAddress = new byte[] { a,b,c,d,e,f};
            for(int i = 0; i < 16; ++i)
            {
                for(int j = 0;j<6;++j)
                {
                    magicPacekt[6 + i * 6 + j] = macAddress[j];
                }
            }
            clt.Send(magicPacekt, magicPacekt.Length);
            clt.Close();
            return;
        }
    }
}
