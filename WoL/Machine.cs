using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoL
{

    public class Machine
    {
        private string _name;
        private string[] _macAddress;
        private byte[] _gatewayIP;
        private byte[] _subnet;

        private byte[] IPAND(byte[] a, byte[] b)
        {
            return new byte[] { (byte)(a[0] & b[0]), (byte)(a[1] & b[1]), (byte)(a[2] & b[2]), (byte)(a[3] & b[3]) };
        }

        private byte[] IPXOR(byte[] a, byte[] b)
        {
            return new byte[] { (byte)(a[0] ^ b[0]), (byte)(a[1] ^ b[1]), (byte)(a[2] ^ b[2]), (byte)(a[3] ^ b[3]) };
        }

        private byte[] IPNOT(byte[] a)
        {
            return new byte[] { (byte)(~a[0]), (byte)(~a[1]), (byte)(~a[2]), (byte)(~a[3]) };
        }

        private string byte2stringIP(byte[] ip)
        {
            return string.Format("{0}.{1}.{2}.{3}",ip[0], ip[1], ip[2], ip[3]);
        }
        private byte[] string2byteIP(string ip)
        {
            string[] stringIP = ip.Split('.');
            return new byte[] { Convert.ToByte(stringIP[0]), Convert.ToByte(stringIP[1]), Convert.ToByte(stringIP[2]), Convert.ToByte(stringIP[3]) };
        }
        private string string2stringAMAC(string[] mac)
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", mac[0], mac[1], mac[2], mac[3], mac[4], mac[5]);
        }
        private string[] stringA2stringMAC(string mac)
        {
            return mac.Split(':');

        }

        public string Name { get { return _name; } set { _name = value; } }
        public string MacAddress { get { return string2stringAMAC(_macAddress); } set { _macAddress = stringA2stringMAC(value); } }
        public string GatewayIP { get { return byte2stringIP(_gatewayIP); } set { string2byteIP(value); } }
        public string SubNet { get { return byte2stringIP(_subnet); } set { string2byteIP(value); } }
        public string TargetIP 
        { 
            get 
            {
                byte[] targetAddress = IPAND(_gatewayIP, _subnet);
                targetAddress = IPXOR(targetAddress, IPNOT(_subnet));
                return byte2stringIP(targetAddress);
            } 
        }

        public Machine(string name, string[] mac, byte[] gate, byte[] sub)
        {
            _name = name;
            _macAddress = mac;
            _gatewayIP = gate;
            _subnet = sub;
        }

        public string[] getMac()
        {
            return _macAddress;
        }

        public byte[] getGateway()
        {
            return _gatewayIP;
        }

        public byte[] getSubNet()
        {
            return _subnet;
        }

        public byte[] getTargetAddress()
        {
            byte[] targetAddress = IPAND(_gatewayIP, _subnet);
            return IPXOR(targetAddress, IPNOT(targetAddress));
        }
    }
}
