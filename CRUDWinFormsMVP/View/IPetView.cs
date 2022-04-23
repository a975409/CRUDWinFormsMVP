using CRUDWinFormsMVP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDWinFormsMVP.View
{
    public interface IPetView
    {
        int Pet_Id { get; set; }
        string SearchValue { get; set; }
        string Pet_Name { get; set; }
        string Pet_Type { get; set; }
        string Pet_Colour { get; set; }
        string Message { get; set; }
        CRUDState State { get; set; }
        bool IsSuccessfulle { get; set; }

        event EventHandler AddPetEvent;
        event EventHandler EditPetEvent;
        event EventHandler DeletePetEvent;
        event EventHandler SearchPetEvent;
        event EventHandler SaveEvent;

        void SetDataGridViewDataSource(BindingSource bindingSource);
        void Show();
    }
}
