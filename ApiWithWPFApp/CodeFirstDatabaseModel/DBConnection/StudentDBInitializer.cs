using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDatabaseModel.DBConnection
{
    public class StudentDBInitializer : DropCreateDatabaseAlways<DataBaseModel>
    {
        protected override void Seed(DataBaseModel context)
        {
            var newStudent = new Student() {  Name = "Ivan", Surname = "Ivanovich", Age = 20 };
            context.Students.Add(newStudent);

            var newStudent2 = new Student() { Name = "Petro", Surname = "Petrov", Age = 21 };
            context.Students.Add(newStudent2);

            base.Seed(context);

        }
    }
}
