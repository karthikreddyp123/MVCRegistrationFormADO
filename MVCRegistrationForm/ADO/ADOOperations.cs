using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVCRegistrationForm.Models;
namespace MVCRegistrationForm.ADO
{
    public class ADOOperations
    {
        static SqlConnection cn;
        public ADOOperations()
        {
            cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["StudentConnection"].ToString();
            cn.Open();

        }
        public List<StudentModel> View()
        {
            List<StudentModel> StudentList = new List<StudentModel>();
            SqlCommand cmd = new SqlCommand
            {
                Connection = ADOOperations.cn,
                CommandType = CommandType.Text,
                CommandText = "SELECT * FROM Student"
            };
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    StudentList.Add(new StudentModel() {StudentID=Convert.ToInt32(dr["StudentID"]), StudentName = dr["StudentName"].ToString(), EmailID = dr["EmailID"].ToString() , Marks=Convert.ToInt32(dr["Marks"])});
                }
            }
            dr.Close();
            return StudentList;
        }
        public List<StudentModel> AdapterView()
        {
            List<StudentModel> StudentList = new List<StudentModel>();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Student", cn);
            DataSet ds = new DataSet(); 
            da.Fill(ds, "Student");
            foreach(DataRow row in ds.Tables["Student"].Rows)
            {
                StudentList.Add(new StudentModel() { StudentID = Convert.ToInt32(row["StudentID"]), StudentName = row["StudentName"].ToString(), EmailID = row["EmailID"].ToString(), Marks = Convert.ToInt32(row["Marks"]) });
            }
            return StudentList;
        }
        public void Insert(StudentModel studentModel)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Student", cn);
            SqlCommand insert = new SqlCommand(); 
            insert.Connection = cn; 
            insert.CommandType = CommandType.Text; 
            insert.CommandText = "INSERT INTO Student (StudentName,EmailID,Marks) VALUES (@StudentName, @EmailID, @Marks)";            
            insert.Parameters.Add(new SqlParameter("@StudentName", SqlDbType.VarChar, 50, "StudentName"));             
            insert.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar, 50, "EmailID"));
            insert.Parameters.Add(new SqlParameter("@Marks", SqlDbType.Int, 2,"Marks"));
            da.InsertCommand = insert;
            DataSet ds = new DataSet();
            da.Fill(ds, "Student");
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["StudentName"] = studentModel.StudentName;
            newRow["EmailID"] = studentModel.EmailID;
            newRow["Marks"] = studentModel.Marks;
            ds.Tables[0].Rows.Add(newRow);
            da.Update(ds.Tables[0]);
        }
        public void ConnectedInsert(StudentModel studentModel)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.Text,
                CommandText = "INSERT INTO Student (StudentName,EmailID,Marks) VALUES ('"+studentModel.StudentName+"', '"+studentModel.EmailID+"', '"+studentModel.Marks+"')"
            };
            cmd.ExecuteNonQuery();
        }
        ~ADOOperations()
        {
            cn.Close();
        }
    }
}