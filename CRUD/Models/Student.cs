using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Mời nhập Họ và tên ")]
        [Display(Name = "Họ và tên: ")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Mời Address ")]
        [Display(Name = "Địa chỉ: ")]
        public string Address { get; set; }

    }
    class StudentList
    {
        DBConnection db;
        public StudentList()
        {
            db = new DBConnection();
        }
        public List<Student> GetStudent(string ID)
        {
            string sql;
            if(string.IsNullOrEmpty(ID))
            {
                sql = "select * from Students ";
            }
            else
            {
                sql = "select * from Students where ID = " + ID;
            }
            List<Student> stuList =new List<Student>();
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            Student tmpstu;
            for (int i = 0; i <dt.Rows.Count; i++){
                tmpstu = new Student();
                tmpstu.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                tmpstu.FullName = dt.Rows[i]["FullName"].ToString();
                tmpstu.Address = dt.Rows[i]["Address"].ToString();
                stuList.Add(tmpstu);
            }
            return stuList;
        }
        public void AddStudent(Student stu)
        {
            string sql = " INSERT INTO Students(FullName,Address) VALUES (N'" + stu.FullName +
                "', N'" + stu.Address + "')";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void UpdateStudent(Student stu)
        {
            string sql = " update Students set FullName = N'" + stu.FullName + "',Address = N'" + stu.Address + "' where  ID = "+stu.ID;
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        
        public void DeleteStudent(Student stu)
        {
            string sql = " delete Students where  ID = " + stu.ID;
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}