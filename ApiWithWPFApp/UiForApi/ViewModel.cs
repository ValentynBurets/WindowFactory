using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSideModel;

namespace UiForApi
{
    public class ViewModel: INotifyPropertyChanged
    {
        private StudentModel _item;
        public ObservableCollection<StudentModel> Students { get; set; }

        #region Item
        public StudentModel Item
        {
            get { return _item; }
            set
            {
                if (_item == null)
                    _item = new StudentModel();

                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public ViewModel()
        {
            Students = new ObservableCollection<StudentModel>();
            Item = new StudentModel();
            OnLoaded();
        }

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(async obj =>
                    {
                        await DeleteStudent();
                    }));
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(async obj =>
                    {
                        await PostStudent();
                    }));
            }
        }


        private async Task GetStudent(int studentId = 1)
        {
            Students.Clear();
            var student = await ApiDataManager.GetStudent(studentId, ConfigurationManager.AppSettings["ApiUriString"]);
            Students.Add(student);
        }
        private async Task PostStudent()
        {

            StudentModel newStudent = new StudentModel() { StudentId = 11111, Age = 20, Name = "Name", Surname = "Surname" };
            Students.Add(Item ?? newStudent);
            var resultUri = await ApiDataManager.PostStudent(Item ?? newStudent, ConfigurationManager.AppSettings["ApiUriString"]);
        }
        private async void OnLoaded()
        {
            //await PostStudent();
            await GetAllStudent();
            //await DeleteStudent();
        }
        private async Task GetAllStudent()
        {
            Students.Clear();
            var student = await ApiDataManager.GetStudents(ConfigurationManager.AppSettings["ApiUriString"]);
            foreach (var s in student)
            {
                Students.Add(s);
            }
        }

        private async Task DeleteStudent()
        {
            if (Students.Count == 0)
                return;

            await ApiDataManager.DeleteQuery(!(Students.LastOrDefault() is null) ? Students.Last().StudentId : -1, ConfigurationManager.AppSettings["ApiUriString"]);
            Students.Clear();
            await GetAllStudent();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}
