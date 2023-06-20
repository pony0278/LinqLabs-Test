using LinqLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }
 
        private void button4_Click(object sender, EventArgs e)
        {
            // public interface IEnumerable<T>
            //    System.Collections.Generic 的成員

            //摘要:
            //公開支援指定類型集合上簡單反覆運算的列舉值。
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //syntax sugar   $".."
            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            //====================================
            this.listBox1.Items.Add("==================");
            System.Collections.IEnumerator en = nums.GetEnumerator();

             while (   en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 111 };

            foreach (int n in list)
            {
                this.listBox1.Items.Add(n);
            }
             //====================================
            this.listBox1.Items.Add("==================");

            List<int>.Enumerator en = list.GetEnumerator();
         
            while( en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //            int w = 100;
            //            嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS1579 因為 'int' 不包含 'GetEnumerator' 的公用執行個體或延伸模組定義，所以 foreach 陳述式無法在型別 'int' 的變數上運作 LinqLabs    C:\shared\LINQ\LinqLabs(Solution)\LinqLabs\1.FrmHelloLinq.cs  69  作用中


            //           foreach (int n in w)
            //            {

            //            }

            foreach (char s   in "abcd")
            {
                this.listBox1.Items.Add(s);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //step 1: define source
            //step 2: define query
            //step 3: execute query

            //step 1: define data source object
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
         
            //Setp2: Define Query
            //define query  (IEnumerable<int> q 是一個  Iterator 物件, 如陣列集合一般 (陣列集合也是一個  Iterator 物件
            IEnumerable<int> q = from  n in nums
                                                      //where n % 2 == 0 && (n>=5 && n<=10)
                                                      where n<=3  || n>=9
                                                      select n;

            //execute query
            //execute query (執行 iterator - 逐一查看集合的item)
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                  where IsEven(n)
                                 select n;

             foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

        }

        private bool IsEven(int n)
        {
            if (n % 2 == 0)
            {
                return true;
            }
            else // if (n%2==1)
            {
                return false;
            }

            //   return n % 2 == 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool result = true && A();
            MessageBox.Show("result = " + result);

            //===================
           result = true  || A();
            MessageBox.Show("result = " + result);
        }

        bool A()
        {
            MessageBox.Show("A()");
            //.....
            return true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = from n in nums
                                                     where IsEven(n)
                                                     select new Point(n, n * n);
            
            foreach (Point n in q)
            {
                this.listBox1.Items.Add(n);
            }
            //==========================
            //execute query - ToXXX()
          List<Point> list =   q.ToList();  //foreach (.... in q)  { .......   list.Add(...)}   return list
           
            this.dataGridView1.DataSource = list;

            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers= "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].Color = Color.Orange;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string s1 = "Apple";
            //// s1 =  s1.ToLower();
            //bool result =   s1.ToLower().ToUpper().ToString().Contains("apple"); ;


            string[] words = { "aaa", "Apple", "xxxApple", "pineapple", "yyyApple" };

            IEnumerable<string> q = from w in words
                                    where w.ToLower().Contains("apple") && w.Length>5
                                    orderby w
                                    select w.ToUpper();

            foreach (string s in q)
            {
                this.listBox1.Items.Add(s);
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Int32 n1 = 100;
            //int n2 = 100;

            //var n = 100;

            IEnumerable<NWDataSet.ProductsRow> q = from p in this.nwDataSet1.Products
                                                     //   where ! p.IsUnitPriceNull() //&& 
                                                  where (p.UnitPrice > 30 && p.UnitPrice < 100)  && p.ProductName.StartsWith("P")
                                                   select p;

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from order in this.nwDataSet1.Orders
                    where order.OrderDate.Year == 1997  //&& order.OrderDate.Month ==
                    select order;

            this.bindingSource1.DataSource = q.ToList();

            this.dataGridView1.DataSource = this.bindingSource1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3,99, 100, 66 };

            // nums.Where

            //   public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)


            //var q = from n in nums
            //        where n > 5
            //        select n;

            //   var q = nums.Where(delegateObj....).Select(.....delegateObj);

            int Max = nums.Max();

        }

        private void button49_Click(object sender, EventArgs e)
        {
            //Assembly -      system.core.dll
            //{} nameSpace -  System.Linq
            //public static class Enumerable

            //   public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)


            //            #region 組件 System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //            // 位置不明
            //            // Decompiled with ICSharpCode.Decompiler 7.1.0.6543
            //            #endregion

            //            using System.Collections;
            //            using System.Collections.Generic;
            //            using System.Threading;

            //namespace System.Linq
            //    {
            //        [__DynamicallyInvokable]
            //        public static class Enumerable

        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();
            //files[0].

            this.dataGridView1.DataSource = files.Skip(3).Take(2).ToList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            int OrderID = 1111;//???? 
                               //this.bindingSource1.Current

            var q = from od in this.nwDataSet1.Order_Details
                    where od.OrderID == OrderID
                    select od;
        }
    }
}
