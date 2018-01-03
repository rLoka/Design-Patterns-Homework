using System;
using System.Text;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.Application.Entities.Devices;

namespace kgrlic_zadaca_3.Application.Helpers
{
    abstract class RansomwareHandler
    {
        protected RansomwareHandler _successor;

        protected void SetSuccessor(RansomwareHandler successor)
        {
            _successor = successor;
        }

        public abstract void HandleDevice(Device device);

        protected static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        protected static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        protected static bool IsBase64(string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
    

    class CryptedHandler : RansomwareHandler
    {

        public CryptedHandler()
        {
            SetSuccessor(new PlainHandler());
        }

        public override void HandleDevice(Device device)
        {
            if (IsBase64(device.Name))
            {
                device.Name = Base64Decode(device.Name);
            }
            else
            {
                _successor.HandleDevice(device);
            }
        }
    }

    class PlainHandler : RansomwareHandler
    {
        public override void HandleDevice(Device device)
        {
            device.Name = Base64Encode(device.Name);
        }
    }
}