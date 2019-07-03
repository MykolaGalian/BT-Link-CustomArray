using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task2
{
    public static class Str
    {   // метод расширения для создания произвольных строк
        public static string Random(this string chars, int length = 4)
        {
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomString.Append(chars[random.Next(chars.Length)]);
                Thread.Sleep(10); // для создания разных строк
            }
            return randomString.ToString();
        }
    }

    class CustomArray<T> : IEnumerable<T>
    {
        private int start;
        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        private int end;
        public int End
        {
            get
            {
                return end;
            }

            set
            {
                if (value <= start) throw new WrongIndexExeption("Invalid index");
                end = value;
            }
        }

        T[] array;

        public int Lenght { get { return array.Length; } }

        public CustomArray(int start, int end)
        {
			if (end <= start) throw new WrongIndexExeption("Invalid index");
			
            this.start = start;
            this.end = end+1;

            var len = Math.Abs(end+1 - start); // for negative index
            array = new T[len];
        }

        //индексатор
        public T this[int index]
        {
            get
			{ 
			    if (index >= start || index <= end) return array[index - start];
                else throw new WrongIndexExeption("Invalid index");
			} 
            set 
			{ 
				if (index >= start || index <= end) array[index - start] = value;
                else throw new WrongIndexExeption("Invalid index");
			}
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
             return GetEnumerator();
        }


    }
}
