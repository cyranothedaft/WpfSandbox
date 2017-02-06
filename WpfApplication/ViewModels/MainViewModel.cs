using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

         DropDownItems = new ObservableCollection<CustomDropDownItem>(new CustomDropDownItem[]
                                                                      {
                                                                         new CustomDropDownItem(1000, "First Item"),
                                                                         new CustomDropDownItem(1010, "Second Item"),
                                                                         new CustomDropDownItem(1020, "Third Item"),
                                                                      });

         ListViewItems = new ObservableCollection<CustomListViewItem>(new CustomListViewItem[]
                                                                      {
                                                                         new CustomListViewItem("Name 1", "Desc 1"),
                                                                         new CustomListViewItem("Name 2", "Desc 2"),
                                                                         new CustomListViewItem("Name 3", "Desc 3"),
                                                                      });

         TextBoxText = "[initial]";
         NumericTextBoxValue = 0;

         SetImageCommand = new RelayCommand(
            x => executeSetImageCommand());

         ImageFilePath = "[image filepath]";
      }


      #region Notification properties
      private string _button1Content;
      public string Button1Content {
         get { return _button1Content; }
         set {
            _button1Content = value;
            OnPropertyChanged();
         }
      }

      private string _button2Content;
      public string Button2Content {
         get { return _button2Content; }
         set {
            _button2Content = value;
            OnPropertyChanged();
         }
      }

      private ObservableCollection<CustomDropDownItem> _dropDownItems; 
      public ObservableCollection<CustomDropDownItem> DropDownItems {
         get { return _dropDownItems; }
         set {
            _dropDownItems = value;
            OnPropertyChanged();
         }
      }

      private CustomDropDownItem _dropDownSelectedItem;
      public CustomDropDownItem DropDownSelectedItem {
         get { return _dropDownSelectedItem; }
         set {
            _dropDownSelectedItem = value;
            OnPropertyChanged();
            DropDownSelectedItemText = string.Format("{0}: {1}", value.Id, value.Desc);
         }
      }

      private string _dropDownSelectedItemText;
      public string DropDownSelectedItemText {
         get { return _dropDownSelectedItemText; }
         set {
            _dropDownSelectedItemText = value;
            OnPropertyChanged();
         }
      }

      private ObservableCollection<CustomListViewItem> _listViewItems;
      public ObservableCollection<CustomListViewItem> ListViewItems {
         get { return _listViewItems; }
         set {
            _listViewItems = value;
            OnPropertyChanged();
         }
      }

      private string _textBoxText;
      public string TextBoxText {
         get { return _textBoxText; }
         set {
            _textBoxText = value;
            OnPropertyChanged();
         }
      }

      private int _numericTextBoxValue;
      public int NumericTextBoxValue {
         get { return _numericTextBoxValue; }
         set {
            _numericTextBoxValue = value;
            OnPropertyChanged();
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



      public RelayCommand SetImageCommand { get; private set; }

      private void executeSetImageCommand() {
         Task.Factory.StartNew(setImageFilePath);
      }

      private string _imageFilePath;
      public string ImageFilePath {
         get { return _imageFilePath; }
         set {
            _imageFilePath = value;
            OnPropertyChanged();
            var image = loadImage(ImageFilePath);
            executeForUI(() => { Image = image; });
         }
      }

      private ImageSource _image;
      public ImageSource Image {
         get { return _image; }
         set {
            _image= value;
            OnPropertyChanged();
         }
      }


      private void setImageFilePath() {
         string filePath = promptForImageFile();
         if ( !string.IsNullOrEmpty(filePath) )
            executeForUI(() => { this.ImageFilePath = filePath; });
      }


      private static string promptForImageFile() {
         // Create OpenFileDialog 
         Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

         // Set filter for file extension and default file extension 
         dlg.DefaultExt = ".png";
         dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

         // Display OpenFileDialog by calling ShowDialog method 
         bool? result = dlg.ShowDialog();

         // Get the selected file name and display in a TextBox 
         return ( result == true )
                   ? dlg.FileName
                   : null;
      }


      private static ImageSource loadImage(string filePath) {
         if ( File.Exists(filePath)) {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = File.Open(filePath, FileMode.Open, FileAccess.Read);
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
         }
         else {
            return null;
         }
      }


      private void executeForUI(Action action) {
         Task.Factory.StartNew(action);
         //var uiContext = TaskScheduler.FromCurrentSynchronizationContext();
         //Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, uiContext);
      }



      internal sealed class CustomDropDownItem {
         public CustomDropDownItem(int id, string desc) {
            Id = id;
            Desc = desc;
         }

         public int Id { get; set; }
         public string Desc { get; set; }
      }



      internal sealed class CustomListViewItem {
         public CustomListViewItem(string name, string desc) {
            Name = name;
            Desc = desc;
         }

         public string Name { get; set; }
         public string Desc { get; set; }
      }
   }
}
