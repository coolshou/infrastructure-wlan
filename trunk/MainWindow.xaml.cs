using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;


namespace Infrastructure_Wlan
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox1.Text = "xbmc";
            passwordBox1.Password = "AnnikaDominik";

        }
        /// <summary>
        /// Erstellt ein Hostednetwork mit SSID und Passwort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_create_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText,ssid,pswd;
            ssid = textBox1.Text;
            pswd = Convert.ToString(passwordBox1.Password);
            strCmdText = "/C netsh wlan set hostednetwork mode=allow ssid=" + ssid + " key="+ pswd +" keyUsage=persistent";
            cmd_admin_start(strCmdText, this);

        }
        /// <summary>
        /// Startet einen Commandprompt mit Adminrechten und übergibt eine einzelne Befehlszeile
        /// </summary>
        /// <param name="strCmdText">Befehlszeile mit führendem /C</param>
        private static void cmd_admin_start(string strCmdText, MainWindow e)
        {

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = "cmd.exe";
            
            startInfo.Arguments = strCmdText;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.Verb = "runas";
            

            process.StartInfo = startInfo;
            process.Start();
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                e.textBox2.AppendText(result);
            }
            
        }
        /// <summary>
        /// Startet das Hostednetwork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_start_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C netsh wlan start hostednetwork";
            cmd_admin_start(strCmdText,this);
            
        }
        /// <summary>
        /// Stopp das Hostednetwork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_stop_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C netsh wlan stop hostednetwork";
            cmd_admin_start(strCmdText,this);
        }
        /// <summary>
        /// Ändert das Passwort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_pswd_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText, pswd;
            pswd = Convert.ToString(passwordBox1.Password);

            strCmdText = "/C netsh wlan set hostednetwork key=" +pswd;
            cmd_admin_start(strCmdText,this);
        }
        /// <summary>
        /// Ändert sie SSID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_ssid_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText, ssid;
            ssid = textBox1.Text;

            strCmdText = "/C netsh wlan set hostednetwork ssid=" + ssid;
            cmd_admin_start(strCmdText,this);
        }
        /// <summary>
        /// Ruft Informationen zum Hostednetwork ab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_info_Click(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = "/C netsh wlan show hostednetwork";
            cmd_admin_start(strCmdText, this);
        }
        /// <summary>
        /// Scrollt die Log-Textbox bis zum Ende
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox2.ScrollToEnd();
        }
    }
}
