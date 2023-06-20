using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;


//Notes: LINQ 主要參考 
//組件 System.Core.dll,
//namespace {}  System.Linq
//public static class Enumerable
//
//public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

//1. 泛型 (泛用方法)                                                                         (ex.  void SwapAnyType<T>(ref T a, ref T b)
//2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
//3. Iterator                                                                                      (ex.  MyIterator)
//4. 擴充方法                                                                                     (ex.  MyStringExtend.WordCount(s);

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100; 
            int n2 = 200;

            MessageBox.Show(n1 + "," + n2);

            Swap(ref n1, ref n2);

            MessageBox.Show(n1 + "," + n2);

            //=====================
            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }
        void Swap(ref  int n1, ref int n2)
        {
            int temp = n2;
            n2 = n1;
            n1 = temp;
        }

        void Swap(ref string n1, ref  string n2)
        {
            string temp = n2;
            n2 = n1;
            n1 = temp;
        }

        void Swap(ref  Point n1, ref Point n2)
        {
            Point temp = n2;
            n2 = n1;
            n1 = temp;
        }

        void SwapObject(ref  object n1, ref object  n2)
        {
            //this.listBox1.Items.Add(777);
            //this.listBox1.Items.Add("sdfsd");
            object temp = n2;
            n2 = n1;
            n1 = temp;
        }

     public static  void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T temp = n2;
            n2 = n1;
            n1 = temp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;

            object o1, o2;
            o1 = n1;
            o2 = n2;
            MessageBox.Show(n1 + "," + n2);
            SwapObject(ref o1, ref o2);

            MessageBox.Show( (int) o1 +"," +(int)  o2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;

            MessageBox.Show(n1 + "," + n2);
            //SwapAnyType<int>(ref n1, ref n2);
            FrmLangForLINQ.SwapAnyType(ref n1, ref n2);          //推斷型別
            MessageBox.Show(n1 + "," + n2);

            //=======================
            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            SwapAnyType<string>(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //            嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS0123  'aaa' 沒有任何多載符合委派 'EventHandler' LinqLabs C:\shared\LINQ\LinqLabs(Solution)\LinqLabs\2.FrmLangForLINQ.cs    124 作用中

            // this.buttonX.Click += aaa;

            this.buttonX.Click += new EventHandler(bbb);//  bbb;
            this.buttonX.Click += ccc;

            //==================================
            //C# 2.0 匿名方法
            this.buttonX.Click += delegate  (object sender1, EventArgs e1) 
                                                                        {
                                                                            MessageBox.Show("C# 2.0 匿名方法");
                                                                        };

            //C# 3.0 匿名方法 =>
            this.buttonX.Click +=  (object sender1, EventArgs e1)=>
            {
                MessageBox.Show("C# 3.0 匿名方法");
            };
        }

        //void aaa()
        //{

        //}

        void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }
        void  ccc(object sender, EventArgs e)
        {
            MessageBox.Show("ccc");
        }

        bool Test(int n)
        {
            //if (n>5)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return n > 5;
        }

        bool IsEven(int n)
        {
            return n % 2 == 0;
        }

        //step 1: create delegate class
        //step 2: create delegate object
        //step 3: call method

        public delegate bool MyDelegate(int n);

        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(4);
            MessageBox.Show("result =" + result);

            //==================================
            //C# 1.0 具名方法
            MyDelegate delegateObj = new MyDelegate( Test);
            result = delegateObj(4);  //delegateObj(4) .Invoke(4);
            MessageBox.Show("result =" + result);

            //==================
            delegateObj = IsEven;
            result = delegateObj(8);
            MessageBox.Show("result =" + result);

            //==================================
            //C# 2.0 匿名方法
            delegateObj = delegate (int n)
                                                   {
                                                       return n > 5;
                                                   };

            result = delegateObj(4);
            MessageBox.Show("result =" + result);

            //C# 3.0 匿名方法簡潔板 labmda expression =>
            //Lambda 運算式是建立委派最簡單的方法 (參數型別也沒寫 / return 也沒寫 => 非常高階的抽象)
            delegateObj = n => n > 5;
            result = delegateObj(3);
            MessageBox.Show("result =" + result);


        }

        List<int> MyWhere(int[] nums,  MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                         list.Add(n);
                }
            }
            return list;
        }

        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
              foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return n;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            List<int> list1 =  MyWhere(nums,  Test);

            List<int> evenList = MyWhere(nums, n=>n%2==0);
            List<int> oddList = MyWhere(nums, n => n % 2 == 1);

            //===============================
            foreach (int n in evenList)
            {
                this.listBox1.Items.Add(n);
            }
            foreach (int n in oddList)
            {
                this.listBox2.Items.Add(n);
            }

        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = MyIterator(nums, n => n % 2 == 0);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //IEnumerable<int> q = from n in nums
            //                     where n > 5
            //                     select n;

            IEnumerable<Point> q = nums.Where<int>(n => n > 5).Select(n=>new Point(n, n*n) );

            foreach (Point n in q)
            {
                this.listBox1.Items.Add(n);
            }

            this.dataGridView1.DataSource = q.ToList();

            //===============================================
            string[] words = { "aaa", "bbbbb", "cccc" };

            IEnumerable<string> q2 =  words.Where<string>(s=>s.Length>3);

            foreach (string s in q2)
            {
                this.listBox2.Items.Add(s);
            }


        }
 
        private void button45_Click(object sender, EventArgs e)
        {
            //var 懶得寫(x)
            //========================
            //var 型別難寫
            //var for 匿名型別

           var n1 = 100;

            var s = "abc";
            s =  s.ToUpper();

            var p = new Point(100, 199);
            this.listBox1.Items.Add(p.X);

        }

        private void button41_Click(object sender, EventArgs e)
        {
            Font font1 = new Font("arial", 15); // ()=> { ....}

            //new MyPoint("aaa", "bbb");

            MyPoint pt0 = new MyPoint();                                                         //( ) constructor 建構子方法
            MyPoint pt1 = new MyPoint(100);
            MyPoint pt2 = new MyPoint(10, 10);

            List<MyPoint> list = new List<MyPoint>();
            list.Add(pt0);
            list.Add(pt1);
            list.Add(pt2);
            list.Add(new MyPoint { Field1 = "1111",  X = 11, Y = 1111 }); ;  //{ } object initialize 物件初始化
            list.Add(new MyPoint { Field1 = "1111", X = 11, Y = 1111 , P1=99999});

            this.dataGridView1.DataSource = list;

            //==================
            List<MyPoint> list2 = new List<MyPoint>() 
            { 
                 new MyPoint { X = 1, Y = 1 },
             new MyPoint { X = 111, Y = 1 },
              new MyPoint { X = 111, Y = 1 }
            };

            this.dataGridView2.DataSource = list2;

        }

        private void button43_Click(object sender, EventArgs e)
        {
            var    pt1 = new { P1 = 100, P2 = 200 };
            var pt2 = new { P1 = 200, P2 = 300 };

            this.listBox1.Items.Add(pt1.GetType());
            this.listBox1.Items.Add(pt2.GetType());
          //  pt1.P1 = 88;
            
            var pt3 = new { X = 200, Y = 300, Z=999, Name="xxx" };
            this.listBox1.Items.Add(pt3.GetType());

            //pt3.X

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var  q = from n in nums
            //                      where n > 5
            //                      select new { N = n, Square = n * n, Cube = n * n * n };

            var q = nums.Where(n => n > 5).Select(n => new
            {
                N = n,
                Square = n * n,
                Cube = n * n * n
            });

            this.dataGridView1.DataSource =  q.ToList();

            //===============================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            //var q2 = from p in this.nwDataSet1.Products
            //         where p.UnitPrice > 30
            //         select new
            //         {
            //             ID = p.ProductID,
            //             p.ProductName,
            //             p.UnitPrice,
            //             p.UnitsInStock,
            //             TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
            //         };

            var q2 = this.nwDataSet1.Products.Where(p => p.UnitPrice > 30)
                                                                            .Select(p => new
                                                                                                        {
                                                                                                            ID = p.ProductID,
                                                                                                            p.ProductName,
                                                                                                            p.UnitPrice,
                                                                                                            p.UnitsInStock,
                                                                                                            TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
                                                                                                        });


            this.dataGridView2.DataSource = q2.ToList();


        }

        private void button40_Click(object sender, EventArgs e)
        {
            //具名型別陣列
            Point[] pts = new Point[]{
                                 new Point(10,10),
                                 new Point(20, 20)
                                };

            //匿名型別陣列
            var arr = new[] {
                                new  { x = 1, y = 1 },
                                new { x = 2, y = 2 }
                             };


            foreach (var item in arr)
            {
                listBox1.Items.Add(item.x + ", " + item.y);

            }
            this.dataGridView1.DataSource = arr;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s = "abcde";
            int count = s.WordCount();
            MessageBox.Show("count =" + count);

            //===================
            string s1 = "123456789";
            count = s1.WordCount();
            MessageBox.Show("count =" + count);

            count = MyStingExtend.WordCount(s1);
            //=================
            char ch = s1.Chars(2);
            MessageBox.Show("ch =" + ch);

        }


    }

//    嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
//錯誤  CS0509	'MyString': 無法衍生自密封類型 'string'	LinqLabs C:\shared\LINQ\LinqLabs(Solution)\LinqLabs\2. FrmLangForLINQ.cs	444	作用中

//    class MyString :String
//    {
//      //  int WordCount(.....)
//    }

    public class MyPoint
{

        public MyPoint()
        {

        }
        public MyPoint(int p1)
        {
           this.P1 = p1;
        }
        public MyPoint(int x, int y)
        {
           this. X = x;
           this. Y = y;
        }
        public string Field1="aaaa", Filed2="bbbb";

    private int m_P1;
    public int P1
    {
        get
        {

            //........
            return m_P1;
        }
        set
        {
            //.....
           m_P1  =   value;
        }
    }

    public int X
    {
        get;set;
    }
    public int Y
    {
        get; set;
    }
}
}
public static  class MyStingExtend
{
     public static  int WordCount(this string s)
    {
        return s.Length;
    }

    public static  char Chars(this string s, int index)
    {
        return s[index];
    }
}


