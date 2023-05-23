using Microsoft.Win32;
using SmartDevices.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditOrderWindow.xaml
    /// </summary>
    public partial class AddEditOrderWindow : Window
    {
        public AddEditOrderWindow()
        {
            InitializeComponent();
            titleTb.Text = "Добавление заказа";
            orderNumberTb.IsEnabled = false;
            statusCmb.IsEnabled = false;
            statusCmb.ItemsSource = App.context.Statuses.ToList();
            statusCmb.SelectedIndex = statusCmb.SelectedIndex - 1;
            editBtn.Visibility = Visibility.Collapsed;
            okBtn.Visibility = Visibility.Visible;
        }
        public AddEditOrderWindow(Devices selectedDevice)
        {
            InitializeComponent();
            titleTb.Text = "Редактирование заказа";
            editBtn.Visibility = Visibility.Visible;
            okBtn.Visibility = Visibility.Collapsed;
            orderNumberTb.IsEnabled = false;
            statusCmb.IsEnabled = true;
            statusCmb.ItemsSource = App.context.Statuses.ToList();
            selectedDevice = App.selectedDevice;
            nameTb.Text = App.selectedDevice.name;
            orderNumberTb.Text = App.selectedDevice.order_number;
            customerNameTb.Text = App.selectedDevice.customer_name;
            countSlider.Value = App.selectedDevice.count;
            sliderCountTbl.Text = App.selectedDevice.count.ToString();
            costTb.Text = App.selectedDevice.cost.ToString();
            addressTb.Text = App.selectedDevice.address;
            statusCmb.SelectedIndex = App.selectedDevice.status_id - 1;
            this.DataContext = App.selectedDevice;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        OpenFileDialog ofdPicture = new OpenFileDialog();
        byte[] image_bytes;
        private void addImageOrder_Click(object sender, RoutedEventArgs e)
        {
            ofdPicture.Filter =
                "Image files|*.jpg;*.jpeg*;*.png|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                photoImg.Source = new BitmapImage(new Uri(ofdPicture.FileName));
                image_bytes = File.ReadAllBytes(ofdPicture.FileName);
            }

        }
        public string GenerateOrderNumber()
        {
            string newOrderNumber;
            newOrderNumber = "ORD" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            return newOrderNumber;
        }
        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Devices device = new Devices()
                {
                    name = nameTb.Text,
                    order_number = GenerateOrderNumber(),
                    customer_name = customerNameTb.Text,
                    count = int.Parse(sliderCountTbl.Text),
                    date = DateTime.Now,
                    cost = Convert.ToDecimal(costTb.Text),
                    address = addressTb.Text,
                    status_id = 1,
                    image = image_bytes,
                };
                App.context.Devices.Add(device);
                App.context.SaveChanges();
                CustomMessageBox.ShowBox("", "Мероприятие успешно добавлено");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {

                CustomMessageBox.ShowBox("Ошибка", "Неправильно заполнены данные!");
            }
        }

        private void countSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderCountTbl.Text = Convert.ToString(Convert.ToInt32(countSlider.Value));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.selectedDevice.name = nameTb.Text;
                App.selectedDevice.customer_name = customerNameTb.Text;
                App.selectedDevice.count = int.Parse(sliderCountTbl.Text);
                App.selectedDevice.cost = Convert.ToDecimal(costTb.Text);
                App.selectedDevice.address = addressTb.Text;
                App.selectedDevice.status_id = statusCmb.SelectedIndex + 1;
                if (image_bytes != null)
                {
                    App.selectedDevice.image = image_bytes;
                }
                else
                {

                }
                App.context.Entry(App.selectedDevice).State = System.Data.EntityState.Modified;
                App.context.SaveChanges();
                CustomMessageBox.ShowBox("", "Заказ изменён!");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {
                CustomMessageBox.ShowBox("Ошибка!", "Неправильно заполнены данные!");
            }
        }
    }
}
