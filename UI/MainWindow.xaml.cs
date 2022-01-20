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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Core.Models.TV;
using Microsoft.Win32;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private ViewModel.MainWindowViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new ViewModel.MainWindowViewModel();
            this.DataContext = ViewModel;

  



        }

        private void cmdLoadFileFromDisk_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files | *.json";
            if(openFileDialog.ShowDialog() == true)
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(openFileDialog.OpenFile());
                string jsonStringData = streamReader.ReadToEnd();
                streamReader.Close();


                this.ViewModel.TvShowViewModel.LoadFromJsonData(jsonStringData);
                
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json files | *.json";
            saveFileDialog.DefaultExt = "json";
            if (saveFileDialog.ShowDialog() == true)
            {
                string FileName = saveFileDialog.FileName;

                // if we don't have .json on the end of the file name, add it on
                if (FileName.ToLower().EndsWith(".json") == false)
                    FileName += ".json";
                System.IO.File.WriteAllText(FileName, this.ViewModel.TvShowViewModel.SaveToJsonString());
            }
        
        }
    }
}
