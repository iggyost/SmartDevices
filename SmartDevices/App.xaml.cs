using SmartDevices.Model;
using SmartDevices.View.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SmartDevices
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SmartDevicesDbEntities context = new SmartDevicesDbEntities();
        public static Users enteredUser = new Users();
        public static Devices selectedDevice = new Devices();
        public static Users selectedUser = new Users();
    }
}
