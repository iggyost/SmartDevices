using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartDevices.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        public CustomMessageBox(string title, string description)
        {
            InitializeComponent();
            titleTb.Text = title;
            descriptionTb.Text = description;
        }
        static CustomMessageBox customMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public static DialogResult ShowBox(string title, string description)
        {
            customMessageBox = new CustomMessageBox();
            SystemSounds.Hand.Play();
            customMessageBox.titleTb.Text = title;
            customMessageBox.descriptionTb.Text = description;
            customMessageBox.ShowDialog();
            return result;
        }
        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.OK;
            this.DialogResult = true;
            this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.None;
            this.DialogResult = false;
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
