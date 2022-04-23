using CRUDWinFormsMVP.Respositories;
using CRUDWinFormsMVP.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWinFormsMVP.Presenters
{
    public class MainPresenter
    {
        private IMainView mainView;

        public MainPresenter(IMainView mainView)
        {
            this.mainView = mainView;
            this.mainView.ShowPetVIew += MainView_ShowPetVIew;
        }

        private void MainView_ShowPetVIew(object sender, EventArgs e)
        {
            IPetView petView = PetView.getCurrentOpnePetView((MainView)mainView);
            string connectionString = ConfigurationManager.ConnectionStrings["petConnectionString"].ConnectionString;
            IPetRespository petRespository = new PetRespository(connectionString);
            new PetPresenter(petView, petRespository);
            petView.Show();
        }
    }
}
