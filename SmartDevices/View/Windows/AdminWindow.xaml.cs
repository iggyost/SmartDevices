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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            EmployeesLb.ItemsSource = App.context.Users.ToList();
        }

        private void canAddCb_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = int.Parse(checkBox.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (userInList.can_add == true)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void canEditCb_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = int.Parse(checkBox.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (userInList.can_redact == true)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void canDeleteCb_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = int.Parse(checkBox.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (userInList.can_delete == true)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void canAddCb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = int.Parse(cb.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (cb.IsChecked == true)
            {
                userInList.can_add = true;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
            else
            {
                userInList.can_add = false;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
        }

        private void canEditCb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = int.Parse(cb.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (cb.IsChecked == true)
            {
                userInList.can_redact = true;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
            else
            {
                userInList.can_redact = false;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
        }

        private void canDeleteCb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = int.Parse(cb.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (cb.IsChecked == true)
            {
                userInList.can_delete = true;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
            else
            {
                userInList.can_delete = false;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
        }

        private void isAdminCb_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = int.Parse(checkBox.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (userInList.role_id == 1)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
        private void isAdminCb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = int.Parse(cb.Tag.ToString());
            var userInList = App.context.Users.Where(u => u.id == index).FirstOrDefault();
            if (cb.IsChecked == true)
            {
                userInList.role_id = 1;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
            else
            {
                userInList.role_id = 2;
                App.context.Entry(userInList).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditUserWindow addEditUserWindow = new AddEditUserWindow();
            addEditUserWindow.ShowDialog();
            if (addEditUserWindow.DialogResult == true)
            {
                EmployeesLb.ItemsSource = App.context.Users.ToList();
            }
        }

        private void removeUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesLb.SelectedItem != null)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox();
                customMessageBox.titleTb.Text = "Внимание!";
                customMessageBox.descriptionTb.Text = "Вы действительно хотите удалить выбранного пользователя?";
                customMessageBox.ShowDialog();
                if (customMessageBox.DialogResult == true)
                {
                    App.selectedUser = EmployeesLb.SelectedItem as Users;
                    App.context.Users.Remove(App.selectedUser);
                    App.context.SaveChanges();
                    EmployeesLb.ItemsSource = App.context.Users.ToList();
                    CustomMessageBox.ShowBox("", "Пользователь удалён!");
                }
                else
                {

                }
            }
            else
            {
                CustomMessageBox.ShowBox("Внимание!", "Выберите пользователя!");
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            App.enteredUser = null;
            authorizationWindow.Show();
            this.Close();
        }

        private void editUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesLb.SelectedItem != null)
            {
                App.selectedUser = EmployeesLb.SelectedItem as Users;
                AddEditUserWindow addEditUserWindow = new AddEditUserWindow(App.selectedUser);
                addEditUserWindow.ShowDialog();
                if (addEditUserWindow.DialogResult == true)
                {
                    EmployeesLb.ItemsSource = App.context.Users.ToList();
                }
            }
            else
            {
                CustomMessageBox.ShowBox("Внимание!", "Выберите пользователя!");
            }
        }
    }
}
