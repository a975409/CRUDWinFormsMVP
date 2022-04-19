using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDWinFormsMVP.Views;
using CRUDWinFormsMVP.Models;
using CRUDWinFormsMVP.Respositories;
using CRUDWinFormsMVP.Presenters;

namespace CRUDWinFormsMVP.Respositories
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowPetView += MainView_ShowPetView;
        }

        private void MainView_ShowPetView(object sender, EventArgs e)
        {
            IPetView view = PetView.GetInstance((MainView)mainView);
            new PetPresenter(view, new PetRespository(sqlConnectionString));
        }
    }
}
