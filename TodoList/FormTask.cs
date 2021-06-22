using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoList
{
    public partial class FormTask : Form
    {
        private readonly TodoListForm _parent;
        public string task_id, task_name, task_desc, task_status;
        public DateTime task_date;
        public FormTask(TodoListForm parent)
        {
            InitializeComponent();
            _parent = parent;
            dateTimePicker.Value = DateTime.Now;
        }

        public void UpdateInfo()
        {
            lblText.Text = "Изменить задачу";
            btnSave.Text = "Изменить";
            this.Text = "Изменить задачу";

            txtName.Text = task_name;
            txtDesc.Text = task_desc;
            dateTimePicker.Value = task_date;
            statusBox.Text = task_status;
        }

        public void SaveInfo()
        {
            lblText.Text = "Добавить задачу";
            btnSave.Text = "Добавить";
            this.Text = "Добавить задачу";
        }

        public void Clear()
        {
            txtName.Text = txtDesc.Text = string.Empty; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Trim().Length < 5)
            {
                MessageBox.Show("Минимальное длина названия 5 символов");
                return;
            }
            if (txtDesc.Text.Trim().Length < 15)
            {
                MessageBox.Show("Минимальное длина описания 15 символов");
                return;
            }

            if(btnSave.Text == "Добавить")
            {
                Task task = new Task(txtName.Text.Trim(), txtDesc.Text.Trim(), dateTimePicker.Value, Utils.convertBool(statusBox));
                DB.AddTask(task);
                Clear();
            }

            if(btnSave.Text == "Изменить")
            {
                Task task = new Task(txtName.Text.Trim(), txtDesc.Text.Trim(), dateTimePicker.Value, Utils.convertBool(statusBox));
                DB.UpdateTask(task, task_id);
                Clear();
            }
            _parent.Display();
        }
    }
}
