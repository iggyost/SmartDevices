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
    /// Логика взаимодействия для AddEditUserWindow.xaml
    /// </summary>
    public partial class AddEditUserWindow : Window
    {
        public AddEditUserWindow()
        {
            InitializeComponent();
        }
        public AddEditUserWindow(Users currentUser)
        {
            InitializeComponent();
            currentUser = App.selectedUser;
            okBtn.Visibility = Visibility.Collapsed;
            editBtn.Visibility = Visibility.Visible;
            titleTb.Text = "Редактирование пользователя";
            this.DataContext = App.selectedUser;
            if (App.selectedUser.can_add == true)
            {
                canAddCb.IsChecked = true;
            }
            else
            {
                canAddCb.IsChecked = false;
            }
            if (App.selectedUser.can_redact == true)
            {
                canEditCb.IsChecked = true;
            }
            else
            {
                canEditCb.IsChecked = false;
            }
            if (App.selectedUser.can_delete == true)
            {
                canDeleteCb.IsChecked = true;
            }
            else
            {
                canDeleteCb.IsChecked = false;
            }
            if (App.selectedUser.role_id == 1)
            {
                isAdminCb.IsChecked = true;
            }
            else
            {
                isAdminCb.IsChecked = false;
            }
        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.selectedUser.name = userNameTb.Text;
                App.selectedUser.login = userLoginTb.Text;
                App.selectedUser.password = userPasswordTb.Text;
                App.selectedUser.can_add = canAddCb.IsChecked;
                App.selectedUser.can_redact = canEditCb.IsChecked;
                App.selectedUser.can_delete = canDeleteCb.IsChecked;
                if (isAdminCb.IsChecked == true)
                {
                    App.selectedUser.role_id = 1;
                }
                else
                {
                    App.selectedUser.role_id = 2;
                }
                App.context.Entry(App.selectedUser).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                CustomMessageBox.ShowBox("", "Пользователь изменён!");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                CustomMessageBox.ShowBox("Ошибка!", "Произошла ошибка при изменении пользователя");
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users newUser = new Users()
                {
                    name = userNameTb.Text,
                    login = userLoginTb.Text,
                    password = userPasswordTb.Text,
                    can_add = (bool)canAddCb.IsChecked,
                    can_redact = (bool)canEditCb.IsChecked,
                    can_delete = (bool)canDeleteCb.IsChecked,
                    role_id = (bool)isAdminCb.IsChecked ? 1 : 2,
                };
                App.context.Users.Add(newUser);
                App.context.SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                CustomMessageBox.ShowBox("Ошибка!", "Произошла ошибка при добавлении нового пользователя");
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
