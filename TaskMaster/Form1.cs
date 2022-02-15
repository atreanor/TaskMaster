using System;
using System.Linq;
using System.Windows.Forms;
using TaskMaster.Model;

namespace TaskMaster
{
    public partial class Form1 : Form
    {
        private TaskMasterDBContext dbContext;
        public Form1()
        {
            InitializeComponent();

            dbContext = new TaskMasterDBContext();

            var statuses = dbContext.TaskStatuses.ToList();

            foreach (TaskStatus s in statuses)
            {
                cboStatus.Items.Add(s);
            }
            refreshData();
        }
        
        private void refreshData()
        {
            BindingSource bindingSource = new BindingSource();

            var query = from t in dbContext.Tasks
                        orderby t.DueDate
                        select new { t.Id, TaskName = t.Name, StatusName = t.TaskStatus.Name, t.DueDate };
            bindingSource.DataSource = query.ToList();

            dataGridView1.DataSource = bindingSource;
            dataGridView1.Refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmdCreateTask_Click(object sender, EventArgs e)
        {
            if(cboStatus.SelectedItem != null && txtTask.Text != String.Empty)
            {
                var newTask = new Model.Task
                {
                    Name = txtTask.Text,
                    StatusId = (cboStatus.SelectedItem as Model.TaskStatus).Id,
                    DueDate = dateTimePicker1.Value
                };
                dbContext.Tasks.Add(newTask);
                dbContext.SaveChanges();
            } 
            else
            {
                MessageBox.Show("Please make sure all data has been entered");
            }
            refreshData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTask_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            var task = dbContext.Tasks.Find((int)dataGridView1.SelectedCells[0].Value);

            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();

            refreshData();
        }

        private void cmdUpdateTask_Click(object sender, EventArgs e)
        {
            if(cmdUpdateTask.Text == "Update")
            {
                txtTask.Text = dataGridView1.SelectedCells[1].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.SelectedCells[3].Value;
                foreach (TaskStatus s in cboStatus.Items)
                {
                    if (s.Name == dataGridView1.SelectedCells[2].Value.ToString())
                    {
                        cboStatus.SelectedItem = s;
                    }
                }
                cmdUpdateTask.Text = "Save";
            }
            else if (cmdUpdateTask.Text == "Save")
            {
                var t = dbContext.Tasks.Find((int)dataGridView1.SelectedCells[0].Value);

                t.Name = txtTask.Text;
                t.StatusId = (cboStatus.SelectedItem as TaskStatus).Id;
                t.DueDate = dateTimePicker1.Value;

                dbContext.SaveChanges();
                refreshData();
                cmdUpdateTask.Text = "Update";
                txtTask.Text = String.Empty;
                dateTimePicker1.Value = DateTime.Now;
                cboStatus.Text = "Please Select...";
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            cmdUpdateTask.Text = "Update";
            txtTask.Text = String.Empty;
            dateTimePicker1.Value = DateTime.Now;
            cboStatus.Text = "Please Select...";
        }
    }
}
