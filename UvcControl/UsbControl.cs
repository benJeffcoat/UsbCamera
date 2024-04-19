using System;
using GitHub.secile.Video;

namespace UsbControl
{
    public class Constants
    {
        public const string camera_name = "Logitech BRIO";
    }

    public class UsbControl
    {
        public void Run()
        {
            // get list of connected camera devices
            string[] devices = UsbCamera.FindDevices();
            if (devices.Length == 0){
                Console.WriteLine("No USB camera detected.");
                return; // no camera.
            }

            // find camera index
            int cameraIndex = -1;
            Console.WriteLine($"Connected Cameras:");
            for(int i=0; i<devices.Length; i++){
                Console.WriteLine(devices[i]);
                if (devices[i] == Constants.camera_name){
                    cameraIndex = i;
                }
            }
            if(cameraIndex == -1){
                Console.WriteLine($"{Constants.camera_name} is not connected.");
                return;
            }
            Console.WriteLine($"Connected to {Constants.camera_name}.");

            // connect to camera
            UsbCamera.VideoFormat[] formats = UsbCamera.GetVideoFormat(cameraIndex);
            var camera = new UsbCamera(cameraIndex, formats[0]);
            camera.Start();
            
            // adjust properties.
            UsbCamera.PropertyItems.Property prop;
            prop = camera.Properties[DirectShow.VideoProcAmpProperty.Brightness];
            if (prop.Available)
            {
                var val = prop.GetValue();
                Console.WriteLine($"Brightness level is {val}");

                var new_val = 0;
                if(val < 50){
                    new_val = 100;
                }

                prop.SetValue(DirectShow.CameraControlFlags.Manual, new_val);

                val = prop.GetValue();
                Console.WriteLine($"Brightness level is now {val}");
            }

            // adjust properties.
            prop = camera.Properties[DirectShow.CameraControlProperty.Zoom];
            if (prop.Available)
            {
                var val = prop.GetValue();
                Console.WriteLine($"Current zoom level is {val}");

                var min = prop.Min;
                var max = prop.Max;
                var def = prop.Default;
                var step = prop.Step;
                Console.WriteLine($"Zoom properties: Max = {max}, Min = {min}, default = {def}, step = {step}.");
                prop.SetValue(DirectShow.CameraControlFlags.Manual, def);
            }
            return;
        }
    }
}