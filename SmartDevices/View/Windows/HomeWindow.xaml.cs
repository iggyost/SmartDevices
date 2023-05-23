using SmartDevices.Model;
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
using System.Windows.Shapes;

namespace SmartDevices.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DevicesLb.ItemsSource = App.context.Devices.ToList();
            userNameTbl.Text = App.enteredUser.name;
            var roleName = App.context.Roles.Where(r => r.id == App.enteredUser.role_id).Select(r => r.name).FirstOrDefault();
            userRoleTbl.Text = roleName + ":";
            if (App.enteredUser.can_add == true)
            {
                addNewOrderBtn.Visibility = Visibility.Visible;
            }
            else 
            {
                addNewOrderBtn.Visibility = Visibility.Collapsed;
            }
            if (App.enteredUser.can_redact == true)
            {
                editOrderBtn.Visibility = Visibility.Visible;
            }
            else
            {
                editOrderBtn.Visibility = Visibility.Collapsed;
            }
            if (App.enteredUser.can_delete == true)
            {
                deleteOrderBtn.Visibility = Visibility.Visible;
            }
            else
            {
                deleteOrderBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void addNewOrderBtn_Click(object sender, object e)
        {
            AddEditOrderWindow addEditOrderWindow = new AddEditOrderWindow();
            addEditOrderWindow.ShowDialog();
            if (addEditOrderWindow.DialogResult == true)
            {
                DevicesLb.ItemsSource = App.context.Devices.ToList();
            }
            else
            {

            }
        }

        private void statusTbl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock statusTbl = (TextBlock)sender;
            int index = int.Parse(statusTbl.Tag.ToString());
            var currentOrder = App.context.Devices.Where(d => d.id == index).FirstOrDefault();
            if (currentOrder != null)
            {
                statusTbl.Text = App.context.Statuses.Where(s => s.id == currentOrder.status_id).Select(s => s.name).FirstOrDefault();
            }
        }

        private void deleteOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesLb.SelectedItem != null)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox();
                customMessageBox.titleTb.Text = "Внимание!";
                customMessageBox.descriptionTb.Text = "Вы действительно хотите удалить выбранный заказ?";
                customMessageBox.ShowDialog();
                if (customMessageBox.DialogResult == true)
                {
                    App.selectedDevice = DevicesLb.SelectedItem as Devices;
                    App.context.Devices.Remove(App.selectedDevice);
                    App.context.SaveChanges();
                    DevicesLb.ItemsSource = App.context.Devices.ToList();
                    CustomMessageBox.ShowBox("", "Заказ удалён!");
                }
                else
                {

                }
            }
            else
            {
                CustomMessageBox.ShowBox("Внимание!", "Выберите заказ из списка!");
            }
        }

        private void editOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesLb.SelectedItem != null)
            {
                App.selectedDevice = DevicesLb.SelectedItem as Devices;
                AddEditOrderWindow addEditOrderWindow = new AddEditOrderWindow(App.selectedDevice);
                addEditOrderWindow.ShowDialog();
                if (addEditOrderWindow.DialogResult == true)
                {
                    DevicesLb.ItemsSource = App.context.Devices.ToList();
                }
                else
                {

                }
            }
            else
            {
                CustomMessageBox.ShowBox("Внимание!", "Выберите заказ из списка!");
            }
        }

        private void onlyNewRdbtn_Checked(object sender, RoutedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.Where(d => d.status_id == 1).ToList();
        }

        private void onlyPaidRdbtn_Checked(object sender, RoutedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.Where(d => d.status_id == 2).ToList();
        }

        private void onlySendedRdbtn_Checked(object sender, RoutedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.Where(d => d.status_id == 3).ToList();
        }

        private void onlyDoneRdbtn_Checked(object sender, RoutedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.Where(d => d.status_id == 4).ToList();
        }

        private void allRdbtn_Checked(object sender, RoutedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.ToList();
        }

        private void orderNumberSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.Where(d => d.order_number.Contains(orderNumberSearchTb.Text)).ToList();
        }
        private void nameSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            DevicesLb.ItemsSource = App.context.Devices.Where(d => d.name.Contains(nameSearchTb.Text)).ToList();
        }

        private void orderNumberSearchTb_GotFocus(object sender, RoutedEventArgs e)
        {
            nameSearchTb.Text = string.Empty;
            onlyNewRdbtn.IsChecked = false;
            onlyPaidRdbtn.IsChecked = false;
            onlySendedRdbtn.IsChecked = false;
            onlyDoneRdbtn.IsChecked = false;
        }

        private void nameSearchTb_GotFocus(object sender, RoutedEventArgs e)
        {
            orderNumberSearchTb.Text = string.Empty;
            onlyNewRdbtn.IsChecked = false;
            onlyPaidRdbtn.IsChecked = false;
            onlySendedRdbtn.IsChecked = false;
            onlyDoneRdbtn.IsChecked = false;
        }

        private void orderNumberSearchTb_LostFocus(object sender, RoutedEventArgs e)
        {
            orderNumberSearchTb.Text = string.Empty;
        }

        private void nameSearchTb_LostFocus(object sender, RoutedEventArgs e)
        {
            nameSearchTb.Text = string.Empty;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            App.enteredUser = null;
            this.Close();
            authorizationWindow.Show();
        }
    }
}
