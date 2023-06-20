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

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.aaDataSet11.ProductPhoto);
            this.bindingSource1.DataSource = this.aaDataSet11.ProductPhoto;
            this.dataGridView1.DataSource = this.bindingSource1;


            var years = (from order in this.aaDataSet11.ProductPhoto
                         select order.ModifiedDate.Year).Distinct();

            this.comboBox3.DataSource = years.ToList();


            var result = from dt in this.aaDataSet11.ProductPhoto
                         let quarter = (dt.ModifiedDate.Month - 1) / 3 + 1
                         let quarters = Enumerable.Range(1, 3).Select(q => new { Year = dt.ModifiedDate.Year, Quarter = $"第{q}季" })
                         from q in quarters
                         group dt by q into g
                         select new
                         {
                             g.Key.Year,
                             g.Key.Quarter
                         };
            this.comboBox2.DisplayMember = "DisplayText";
            this.comboBox2.ValueMember = "Value";
            this.comboBox2.DataSource = result.Select(g => new
            {
                DisplayText = $"{g.Year} - {g.Quarter}",
                Value = g
            }).ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value is byte[] imageData)
                {
                    using (var ms = new MemoryStream(imageData))
                    {
                        Image image = Image.FromStream(ms);
                        pictureBox1.Image = image;
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            var result = from d in this.aaDataSet11.ProductPhoto
                         select d;

            this.dataGridView1.DataSource = result.ToList();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime selectedDateTime = dateTimePicker1.Value;
            DateTime selectedDateTime2 = dateTimePicker2.Value;
            var result = from dt in this.aaDataSet11.ProductPhoto
                         where dt.ModifiedDate >= selectedDateTime &&
                               dt.ModifiedDate <= selectedDateTime2
                         select dt;
            this.dataGridView1.DataSource = result.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedYear = this.comboBox3.Text;
            var result = from dt in this.aaDataSet11.ProductPhoto
                         where dt.ModifiedDate.Year.ToString() == selectedYear 
                         select dt;
            this.dataGridView1.DataSource = result.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
