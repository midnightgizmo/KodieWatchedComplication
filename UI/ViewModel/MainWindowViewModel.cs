using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region commands
        public ICommand LoadDataBaseDataCommand { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            this.LoadDataBaseDataCommand = new RelayCommand(() => { this.LoadDataFromDatabase(); });
        }

        public TVShowViewModel TvShowViewModel { get; set; } = new TVShowViewModel();


        private void LoadDataFromDatabase()
        {
            this.TvShowViewModel.LoadDataFromDatabase();
        }
    }
}
