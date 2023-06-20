﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList();
            arr.Add(10);
            arr.Add(100);

            var q = from n in arr.Cast<int>()
                                 where n > 3
                                 select  new { N = n, S = n * n, C = n * n * n };

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q = (from p in this.nwDataSet1.Products
                     orderby p.UnitsInStock descending
                     select p).Take(5);
           
             this.dataGridView1.DataSource= q.ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //I. 延遲查詢 (deferred execution)
            //定義時不會估算
            //使用時才估算


            int[] nums = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int i = 0;
            var q = from n in nums
                    select ++i;

            //foreach 執行 Query
            foreach (var v in q)
            {
                listBox1.Items.Add(string.Format("v = {0}, i = {1}", v, i));
            }
            listBox1.Items.Add("===========================================");

            //=======================================================

            i = 0;
            var q1 = (from n in nums
                      select ++i).ToList();

            foreach (var v in q1)
            {
                listBox1.Items.Add(string.Format("v = {0}, i = {1}", v, i));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //When execture Query q = 
      
            //1. foreach....
            //2. ToXXX()
            //3. Aggregaion sum(), min()

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.listBox1.Items.Add("Sum =" + nums.Sum());
            this.listBox1.Items.Add("Max =" + nums.Max());
            this.listBox1.Items.Add("Min =" + nums.Min());
            this.listBox1.Items.Add("Avg =" + nums.Average());
            this.listBox1.Items.Add("Count =" + nums.Count());
            //==============================================

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.listBox1.Items.Add("Max UnitsInStock =" + this.nwDataSet1.Products.Max(p => p.UnitsInStock));
            this.listBox1.Items.Add("Min UnitsInStock =" + this.nwDataSet1.Products.Min(p => p.UnitsInStock));

        }
    }
}