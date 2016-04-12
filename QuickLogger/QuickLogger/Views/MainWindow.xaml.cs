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

namespace QuickLogger.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.NotifyIcon _notifyIcon;

        private HotKey _hotkey;

        public MainWindow()
        {
            InitializeComponent();
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon("Main.ico");
            _notifyIcon.Visible = true;
            _notifyIcon.Click += SetWindowStateToNormal;
            _notifyIcon.DoubleClick += SetWindowStateToNormal;
            _notifyIcon.MouseDown += NotifyIcon_MouseDown;

            _hotkey = new HotKey(ModifierKeys.Control, System.Windows.Forms.Keys.Q, this);
            _hotkey.HotKeyPressed += _hotkey_HotKeyPressed;
        }

        private void _hotkey_HotKeyPressed(HotKey obj)
        {
            ToggleWindowState();
        }

        void SetWindowStateToNormal(object sender, EventArgs e)
        {
            ToggleWindowState();
        }

        private void ToggleWindowState()
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void LogiItButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            this.WindowState = System.Windows.WindowState.Minimized;
        }

        void NotifyIcon_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var menu = this.FindResource("NotifierContextMenu") as ContextMenu;
                menu.IsOpen = true;
            }
        }

        protected void Menu_Exit(object sender, RoutedEventArgs e)
        {
            _notifyIcon.Visible = false;
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
