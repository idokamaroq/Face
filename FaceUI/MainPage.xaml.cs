using FaceTracker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FaceUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent(); //UI only

            //for the time being, this is playing the role of FaceManager


            //get a face image
            //var image = "";
            //send image to FaceTracker
            //var imageBytes = Tracker.ConvertToByteArray(image);
            var imageFilePath = "D:/Downloads/Faces/neutral.jpg";
            var faceTracker = new Tracker();

            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                var (emotion, direction) = faceTracker.Analyze(imageFileStream).GetAwaiter().GetResult();
                Debug.WriteLine(emotion);
                Debug.WriteLine(direction);
            }

        }
    }
}
