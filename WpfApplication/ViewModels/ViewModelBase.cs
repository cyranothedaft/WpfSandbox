using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace WpfApplication.ViewModels {
   
   internal abstract class ViewModelBase : INotifyPropertyChanged {
      public event PropertyChangedEventHandler PropertyChanged;


      protected void OnPropertyChanged([CallerMemberName] string caller = null) {
         // make sure only to call this if the value actually changes

         var handler = PropertyChanged;
         if ( handler != null ) {
            handler(this, new PropertyChangedEventArgs(caller));
         }
      }
   }
}
