using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern void MouseEvent(int dwFlag, int dx, int dy, int cButtons, int dwExtraInfo);
        
        [DllImport("User32")]
        public static extern int SetCursorPos(int x, int y);

        private const int MouseEventMove = 0x0001;
        private const int MouseEventLeftDown = 0x0001;
        private const int MouseEventLeftUp = 0x0004;
        private const int MouseEventRightDown= 0x0008;

        private readonly ManualResetEvent stoppingEvent = new ManualResetEvent(false);
        private TimeSpan interval_;

        public MainWindow()
        {
            InitializeComponent();
            
            interval_ = TimeSpan.FromMilliseconds(1000);
            stoppingEvent.Reset();
            
            MouseSetPosCustom(200,200);
            
            Label label = new Label();
            label.Width = 100; 
            label.Height = 30;
            this.Content = label;
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

        public void MouseClickCustom(int interval = 100)
        {
            try
            {
                //MouseEvent(MouseEventLeftDown, 0, 0, 0, 0);
                //MouseEvent(MouseEventLeftUp, 0, 0, 0, 0);
                stoppingEvent.WaitOne(interval);
            }
            catch (Exception e)
            {
                MessageBox.Show("MouseClickCustom\r\n" + e.Message);
            }
        }

        public void MouseSetPosCustom(int x, int y)
        {
            try
            {
                SetCursorPos(x, y);
                stoppingEvent.WaitOne(interval_);
                MouseClickCustom(100);
            }
            catch (Exception e)
            {
                MessageBox.Show("MouseSetPos\r\n" + e.Message);
            }
        }

    }
}
