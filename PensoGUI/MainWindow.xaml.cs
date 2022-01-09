using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PensoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        [DllImport("User32")] // * need 'User32.dll' for mouse position setting
        public static extern int SetCursorPos(int x, int y);
        private readonly ManualResetEvent stoppingEvent = new ManualResetEvent(false);
        private TimeSpan interval_;

        public MainWindow()
        {
            // init
            InitializeComponent();
            interval_ = TimeSpan.FromMilliseconds(1000);
            stoppingEvent.Reset();
            
            // set to 200, 200
            MouseSetPosCustom(200,200);
            
            // mouse position Label
            Label label = new Label();
            label.Width = 100; 
            label.Height = 30;
            this.Content = label;
            
            // get mouse position & print : loop
            this.Loaded += delegate { 
                System.Timers.Timer timer = new System.Timers.Timer(); 
                timer.Elapsed += delegate { 
                    this.Dispatcher.Invoke(new Action(delegate { 
                        Mouse.Capture(this); 
                        Point pointToWindow = Mouse.GetPosition(this); 
                        Point pointToScreen = PointToScreen(pointToWindow); 
                        label.Content = pointToScreen.ToString(); 
                        Mouse.Capture(null); 
                    })); 
                };
                
                timer.Interval = 1;
                timer.Start(); };
        }

        // set mouse cursor point
        public void MouseSetPosCustom(int x, int y)
        {
            try
            {
                SetCursorPos(x, y);
                stoppingEvent.WaitOne(interval_);
            }
            catch (Exception e)
            {
                MessageBox.Show("MouseSetPos\r\n" + e.Message);
            }
        }

    }
}
