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
    public partial class TodoListForm : Form
    {
        FormTask form;
        public TodoListForm()
        {
            InitializeComponent();
            form = new FormTask(this);
        }

        public void Display()
        {
            DB.ShowAndSearchTask("SELECT task_id, task_name, task_desc, task_date, task_status FROM tasks", dataGridView);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            form.Clear();
            form.SaveInfo();
            form.ShowDialog();
        }

        private void TodoListForm_Load(object sender, EventArgs e)
        {

        }

        private void TodoListForm_Shown(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DB.ShowAndSearchTask("SELECT task_id, task_name, task_desc, task_date, task_status FROM tasks WHERE task_name LIKE'%" + txtSearch.Text.Trim() + "%' OR task_desc LIKE '%" + txtSearch.Text.Trim() +"%'", dataGridView);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                form.Clear();
                // Edit task
                form.task_id = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.task_name = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.task_desc = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.task_date = DateTime.Parse(dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                form.task_status = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                form.UpdateInfo();
                form.ShowDialog();
                return;
            }
            // Delete task
            if(e.ColumnIndex == 1)
            {
                if(MessageBox.Show("Вы действительно хотите удалить запись?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DB.DeleteTask(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
