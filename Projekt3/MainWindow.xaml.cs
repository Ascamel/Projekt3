using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            angle_X.Content = "X-Axis: " + Math.Floor(vScroll.Value).ToString() + "*";
            Cube.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), vScroll.Value));
        }

        private void ScrollBar_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            angle_Y.Content = "Y-Axis: " + Math.Floor(hScroll.Value).ToString() + "*";
            Cube.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), hScroll.Value));
        }       

        private void CMYKTextChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProcessColourCMYK();
        }

        private void CMYKTextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessColourCMYK();
        }

        private void RGBTextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessColourRGB();
        }

        private void RGBTextChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProcessColourRGB();
        }

        public void ProcessColourCMYK()
        {
            float C = (float)(CSlider.Value / 100);
            float M = (float)(MSlider.Value / 100);
            float Y = (float)(YSlider.Value / 100);
            float K = (float)(KSlider.Value / 100);

            float R = (float)Math.Round(255 * (1 - C) * (1 - K));
            float G = (float)Math.Round(255 * (1 - M) * (1 - K));
            float B = (float)Math.Round(255 * (1 - Y) * (1 - K));

            Color color = Color.FromRgb((byte)R, (byte)G, (byte)B);


            MyGrid.Background = new SolidColorBrush(color);

        }

        public void ProcessColourRGB()
        {
            Color color = Color.FromRgb((byte)RSlider.Value, (byte)GSlider.Value, (byte)BSlider.Value);

            float R = (float)RSlider.Value;
            float G = (float)GSlider.Value;
            float B = (float)BSlider.Value;


            MyGrid.Background = new SolidColorBrush(color);

        }
        private void ToCmykButtonClick(object sender, RoutedEventArgs e)
        {
            float R = (float)RSlider.Value / 255;
            float G = (float)GSlider.Value / 255;
            float B = (float)BSlider.Value / 255 ;



            float K = ClampCmyk(1 - (Math.Max(Math.Max(R, G), B)));
            float C = ClampCmyk((1 - R - K) / (1 - K));
            float M = ClampCmyk((1 - G - K) / (1 - K));
            float Y = ClampCmyk((1 - B - K) / (1 - K));

            CSlider.Value = Math.Round(C * 100);
            MSlider.Value = Math.Round(M * 100);
            YSlider.Value = Math.Round(Y * 100);
            KSlider.Value = Math.Round(K * 100) ;
        }

        private static float ClampCmyk(float value)
        {
            if (value < 0 || float.IsNaN(value))
            {
                value = 0;
            }

            return value;
        }
        private void ToRGBButtonClick(object sender, RoutedEventArgs e)
        {
            // K*=100;
            float C = (float)(CSlider.Value/100);
            float M = (float)(MSlider.Value/100);
            float Y = (float)(YSlider.Value/100);
            float K = (float)(KSlider.Value/100);



            float R = (float)Math.Round(255 * (1 - C) * (1 - K));
            float G = (float)Math.Round(255 * (1 - M) * (1 - K));
            float B = (float)Math.Round(255 * (1 - Y) * (1 - K));

            RSlider.Value = Math.Round(R);
            GSlider.Value = Math.Round(G);
            BSlider.Value = Math.Round(B);
        }

    }
}
