using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication.Commands;



namespace WpfApplication.ViewModels {
   internal sealed class MainViewModel : ViewModelBase {
      public MainViewModel() {
         Button1Command = new RelayCommand(
            x => executeButton1Command());
         Button2Command = new RelayCommand(
            x => executeButton2Command(),
            x => canExecuteButton2Command());

         IsButton1On = true;
         IsButton2On = true;
      }


      #region Notification properties
      private string _button1Content;
      public string Button1Content {
         get { return _button1Content; }
         set {
            _button1Content = value;
            OnPropertyChanged("Button1Content");
         }
      }

      private string _button2Content;
      public string Button2Content {
         get { return _button2Content; }
         set {
            _button2Content = value;
            OnPropertyChanged("Button2Content");
         }
      }

      #endregion Notification properties


      public RelayCommand Button1Command { get; private set; }
      public RelayCommand Button2Command { get; private set; }


      private bool _isButton1On;
      private bool IsButton1On {
         get { return _isButton1On; }
         set {
            _isButton1On = value;
            Button1Content = string.Format("Button 1 is {0}", _isButton1On ? "on" : "off");
         }
      }


      private bool _isButton2On;
      private bool IsButton2On {
         get { return _isButton2On; }
         set {
            _isButton2On = value;
            Button2Content = string.Format("Button 2 is {0}", _isButton2On ? "on" : "off");
         }
      }


      private void executeButton1Command() {
         IsButton1On = !IsButton1On;
      }


      private void executeButton2Command() {
         IsButton2On = !IsButton2On;
      }


      private bool canExecuteButton2Command() {
         return IsButton1On;
      }
   }
}
