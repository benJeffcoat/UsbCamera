using System;
using System.Runtime.InteropServices;

namespace UsbControl
{
    static class Program
    {
        static void Main()
        {
            UsbControl usb_controller = new UsbControl();
            usb_controller.Run();
            return;
        }
    }
}
