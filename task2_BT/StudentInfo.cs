using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2_BT
{
     public class StudentInfo : IComparable<StudentInfo>
    {
        public string StudentName { get; set; }
        public string TestName { get; set; }
        public DateTime TestTime { get; set; }
        public int TestMark { get; set; }

        public override string ToString()
        {
            return "Mark: " + TestMark + ", Student: " + StudentName;
        }


        // реализация CompareTo относительно типа StudentInfo
        int IComparable<StudentInfo>.CompareTo(StudentInfo st)
        {
            if (this.TestMark.CompareTo(st.TestMark) == 1) // сравнение на основе оценки студента
            {
                return 1;
            }
            else if (this.TestMark.CompareTo(st.TestMark) == -1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
