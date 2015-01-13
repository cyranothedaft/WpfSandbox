using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication.Commands;



namespace WpfApplication.ViewModels {
   internal sealed class MainViewModel : ViewModelBase {
      public MainViewModel() {
         ToggleCheckBox1Command = new RelayCommand(
            x => executeToggleCheckBox1Command());
         //ToggleCheckBox2Command = new RelayCommand(
         //   x => executeToggleCheckBoxCommand(),
         //   x => IsCheckBoxToggleAllowed);
      }


      public bool CheckBox1IsChecked { get; set; }
      public RelayCommand ToggleCheckBox1Command { get; private set; }


      private void executeToggleCheckBox1Command() {
         CheckBox1IsChecked = !CheckBox1IsChecked;
      }
   }
}
