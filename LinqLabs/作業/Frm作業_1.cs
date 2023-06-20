using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_1 : Form
    {


        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);

            var years = (from order in this.nwDataSet1.Orders
                         select order.OrderDate.Year).Distinct();
            
            this.comboBox1.DataSource = years.ToList();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(@"C:\Windows");
            FileInfo[] fileInfos = info.GetFiles();
            this.dataGridView1.DataSource = fileInfos.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            IEnumerable<NWDataSet.OrdersRow> q = from p in this.nwDataSet1.Orders
                                                   select p;
            IEnumerable<NWDataSet.Order_DetailsRow> oD = from p in this.nwDataSet1.Order_Details
                                                 select p;
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = oD.ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            string selectedYear = this.comboBox1.Text;

            IEnumerable<NWDataSet.OrdersRow> q = from order in this.nwDataSet1.Orders
                                                 where order.OrderDate.Year.ToString() == selectedYear
                                                 select order;

            IEnumerable<NWDataSet.Order_DetailsRow> oD = from orderDetail in this.nwDataSet1.Order_Details
                                                         join order in q on orderDetail.OrderID equals order.OrderID
                                                         select orderDetail;

            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = oD.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            string selectedYear = this.comboBox1.Text;
            int pageCounts;
            bool success = int.TryParse(this.textBox1.Text, out pageCounts);
            if (success)
            {
                IEnumerable<NWDataSet.OrdersRow> q = (from order in this.nwDataSet1.Orders
                                                      where order.OrderDate.Year.ToString() == selectedYear
                                                      select order).Skip(pageCounts).Take(pageCounts);

                IEnumerable<NWDataSet.Order_DetailsRow> oD = (from orderDetail in this.nwDataSet1.Order_Details
                                                              join order in q on orderDetail.OrderID equals order.OrderID
                                                              select orderDetail).Skip(pageCounts).Take(pageCounts);

                this.dataGridView1.DataSource = q.ToList();
                this.dataGridView2.DataSource = oD.ToList();
            }
            else
            {
                MessageBox.Show("必須是數字");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            string selectedYear = this.comboBox1.Text;
            int pageCounts;
            bool success = int.TryParse(this.textBox1.Text, out pageCounts);
            if (success)
            {
                IEnumerable<NWDataSet.OrdersRow> q = (from order in this.nwDataSet1.Orders
                                                      where order.OrderDate.Year.ToString() == selectedYear
                                                      select order).Take(pageCounts);

                IEnumerable<NWDataSet.Order_DetailsRow> oD = (from orderDetail in this.nwDataSet1.Order_Details
                                                              join order in q on orderDetail.OrderID equals order.OrderID
                                                              select orderDetail).Take(pageCounts);

                this.dataGridView1.DataSource = q.ToList();
                this.dataGridView2.DataSource = oD.ToList();
            }
            else
            {
                MessageBox.Show("必須是數字");
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            string selectedYear = this.comboBox1.Text;
            int pageCounts;
            bool success = int.TryParse(this.textBox1.Text, out pageCounts);
            if (success)
            {
                IEnumerable<NWDataSet.OrdersRow> q = (from order in this.nwDataSet1.Orders
                                                      where order.OrderDate.Year.ToString() == selectedYear
                                                      select order).Skip(Math.Max(0, pageCounts)).Take(pageCounts);

                IEnumerable<NWDataSet.Order_DetailsRow> oD = (from orderDetail in this.nwDataSet1.Order_Details
                                                              join order in q on orderDetail.OrderID equals order.OrderID
                                                              select orderDetail).Skip(Math.Max(0, pageCounts)).Take(pageCounts);

                this.dataGridView1.DataSource = q.ToList();
                this.dataGridView2.DataSource = oD.ToList();
            }
            else
            {
                MessageBox.Show("必須是數字");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            string directoryPath = @"C:\Windows";
            string searchPattern = "*.log";
            string[] filePaths = Directory.GetFiles(directoryPath, searchPattern);
            var logFiles = from filePath in filePaths
                           let fileYear = File.GetCreationTime(filePath).Year
                           where fileYear == 2017 && Path.GetExtension(filePath).Equals(".log", StringComparison.OrdinalIgnoreCase)
                           select filePath;
            this.dataGridView1.DataSource = logFiles.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            string directoryPath = @"C:\Windows";
            string searchPattern = "*.log";
            string[] filePaths = Directory.GetFiles(directoryPath, searchPattern);
            var maxFileSize = (from filePath in filePaths
                               let file = new FileInfo(filePath)
                               orderby file.Length descending
                               select file).FirstOrDefault();
            List<FileInfo> fileList = new List<FileInfo>();
            fileList.Add(maxFileSize);
            this.dataGridView1.DataSource = fileList;
        }
    }
}
