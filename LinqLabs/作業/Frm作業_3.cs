using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //students_scores = new List<Student>()
            //                             {
            //                                new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
            //                                new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
            //                                new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
            //                                new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
            //                                new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
            //                                new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

            //                              };
        }

        private void button36_Click(object sender, EventArgs e)
        {
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績		

            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績		
            // NOTE 匿名型別


            // 找出學員 'bbb' 的成績	                       
    
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	!=

            //數學不及格...是誰
        }

        private void button37_Click(object sender, EventArgs e)
        {
            // 統計 每個學生個人成績
            // Rank by 三科成績加總 並排序
            // 國文權重加倍
            // 依平均分計算 Grade & Pass 

            // NOTE Rank
            //this.lblMaster.Text = "Rank";
            //this.lblDetails.Text = "";

            //int rank = 0;
            //var q = from s in students_scores....
            //        select new
            //        {
            //            s.Name,
            //            s.Gender,
            //            s.Class,
            //            s.Chi,
            //            s.Eng,
            //            s.Math,
            //            Min...
            //            Max....
            //            Avg = ...
            //            Sum =
            //            Weight = ...,
            //            ...
            //            Pass = ..Grade
            //            Rank = ++rank,
            //        };
        }

        private void button33_Click(object sender, EventArgs e)
        {

        }
    }
}
