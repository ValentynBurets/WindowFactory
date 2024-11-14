using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDatabaseModel
{
    public class DataManager
    {
        public void AddStudent(Student student)
        {
            DataBaseModel model = new DataBaseModel();
            try
            {
                model.Students.Add(student);
                model.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        public Student GetStudent(int id)
        {
            try
            {
                Student student;
                using (DataBaseModel model = new DataBaseModel())
                {
                    student = model.Students.FirstOrDefault(s => s.StudentId == id);
                }
                return student;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        public IEnumerable<Student> GetStudent()
        {
            using (DataBaseModel model = new DataBaseModel())
            {
                try
                {
                    List<Student> students = model.Students.Take(10).ToList();
                    return students;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
                
            }
        }

        public bool DeleteStudent(int studentId)
        {
            using (DataBaseModel model = new DataBaseModel())
            {
                try
                {
                    var student = model.Students.FirstOrDefault(s => s.StudentId == studentId);
                    if (!(student is null))
                    {
                        model.Students.Remove(student);
                        model.SaveChanges();
                        return true;
                    }

                    return false;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }
                
            }
        }
    }
}
