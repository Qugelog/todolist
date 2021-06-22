using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TodoList
{
    class DB
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=root;database=todolist2";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Connection!\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }

        public static void AddTask(Task task)
        {
            string sql = "INSERT INTO tasks VALUES (NULL, @TaskName, @TaskDesc, @TaskDate, @TaskStatus)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TaskName", MySqlDbType.VarChar).Value = task.Name;
            cmd.Parameters.Add("@TaskDesc", MySqlDbType.VarChar).Value = task.Desc;
            cmd.Parameters.Add("@TaskDate", MySqlDbType.DateTime).Value = task.Date;
            cmd.Parameters.Add("@TaskStatus", MySqlDbType.VarChar).Value = task.Status;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Задача успешно добавлена!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении задачи!\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Clone();
        }

        public static void UpdateTask(Task task, string id)
        {
            string sql = "UPDATE tasks SET task_name = @TaskName, task_desc = @TaskDesc, task_date  = @TaskDate, task_status = @TaskStatus WHERE task_id = @TaskID";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TaskID", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("@TaskName", MySqlDbType.VarChar).Value = task.Name;
            cmd.Parameters.Add("@TaskDesc", MySqlDbType.VarChar).Value = task.Desc;
            cmd.Parameters.Add("@TaskDate", MySqlDbType.DateTime).Value = task.Date;
            cmd.Parameters.Add("@TaskStatus", MySqlDbType.VarChar).Value = task.Status;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Задача успешно обновлена!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Произошла ошибка при обновлении задачи!\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Clone();
        }

        public static void DeleteTask(string id)
        {
            string sql = "DELETE FROM tasks WHERE task_id = @TaskID";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TaskID", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Задача успешно удалена!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Произошла ошибка при удалении задачи!\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Clone();
        }

        public static void ShowAndSearchTask(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);
            dgv.DataSource = tbl;
            con.Close();
        }
    }
}
