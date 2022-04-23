using CRUDWinFormsMVP.Enums;
using CRUDWinFormsMVP.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDWinFormsMVP
{
    public partial class PetView : Form, IPetView
    {
        public PetView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPagePetDetail);
        }

        /// <summary>
        /// 定義控制項的事件
        /// </summary>
        private void AssociateAndRaiseViewEvents()
        {
            btnAddNew.Click += delegate {
                AddPetEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetList);
                tabControl1.TabPages.Add(tabPagePetDetail);
            };

            btnCancel.Click += delegate {
                tabControl1.TabPages.Remove(tabPagePetDetail);
                tabControl1.TabPages.Add(tabPagePetList);
            };

            btnClose.Click += (s, e) => { petView.Close(); };

            btnDelete.Click += delegate {

                DialogResult result = MessageBox.Show("是否要刪除此筆資料?", "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                DeletePetEvent?.Invoke(this, EventArgs.Empty);

                if (isSuccessfulle)
                    MessageBox.Show("刪除成功", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

            btnEdit.Click += delegate {
                EditPetEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetList);
                tabControl1.TabPages.Add(tabPagePetDetail);
                tabPagePetDetail.Text = "Edit pet";
            };

            btnSave.Click += delegate {
                SaveEvent?.Invoke(this, EventArgs.Empty);

                if (isSuccessfulle)
                {
                    MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.TabPages.Remove(tabPagePetDetail);
                    tabControl1.TabPages.Add(tabPagePetList);
                }
                else
                    MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            btnSearch.Click += delegate {
                SearchPetEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        #region 取得目前正在執行的視窗
        private static PetView petView;
        public static IPetView getCurrentOpnePetView(Form parentContainer)
        {
            if (petView == null || petView.IsDisposed)
            {
                petView = new PetView();
                petView.MdiParent = parentContainer;
                petView.FormBorderStyle = FormBorderStyle.None;
                petView.Dock = DockStyle.Fill;
            }
            else
            {
                if (petView.WindowState == FormWindowState.Minimized)
                    petView.WindowState = FormWindowState.Normal;
                petView.BringToFront();
            }

            return petView;
        }
        #endregion

        public int Pet_Id { 
            get 
            {
                int id = -1;
                int.TryParse(txtPetId.Text, out id);
                return id;
            } 
            set 
            {
                txtPetId.Text = value.ToString();
            } 
        }

        public string SearchValue 
        {
            get 
            {
                return txtSearch.Text;
            }
            set 
            {
                txtSearch.Text = value;
            }
        }
        public string Pet_Name 
        {
            get
            {
                return txtPetName.Text;
            }
            set
            {
                txtPetName.Text = value;
            }
        }
        public string Pet_Type
        {
            get
            {
                return txtPetType.Text;
            }
            set
            {
                txtPetType.Text = value;
            }
        }
        public string Pet_Colour 
        {
            get
            {
                return txtPetColour.Text;           
            }
            set
            {
                txtPetColour.Text = value;
            }
        }
        private string message;
        public string Message 
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        private CRUDState state;
        public CRUDState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        private bool isSuccessfulle;
        public bool IsSuccessfulle 
        {
            get
            {
                return isSuccessfulle;
            }
            set
            {
                isSuccessfulle = value;
            }
        }

        public event EventHandler AddPetEvent;
        public event EventHandler EditPetEvent;
        public event EventHandler DeletePetEvent;
        public event EventHandler SearchPetEvent;
        public event EventHandler SaveEvent;

        public void SetDataGridViewDataSource(BindingSource bindingSource)
        {
            dataGridView.DataSource = bindingSource;
            txtPetId.DataBindings.Add("Text", bindingSource, "Id");
            txtPetName.DataBindings.Add("Text", bindingSource, "Name");
            txtPetType.DataBindings.Add("Text", bindingSource, "Type");
            txtPetColour.DataBindings.Add("Text", bindingSource, "Color");
        }
    }
}
