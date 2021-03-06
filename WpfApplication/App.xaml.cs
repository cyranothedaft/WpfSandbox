﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication.ViewModels;



namespace WpfApplication {
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application {
      protected override void OnStartup(StartupEventArgs e) {
         base.OnStartup(e);

         MainViewModel mainViewModel = new MainViewModel();

         MainWindow mainWindow = new MainWindow();
         mainWindow.ViewModel = mainViewModel;
         mainWindow.Show();
      }
   }
}
