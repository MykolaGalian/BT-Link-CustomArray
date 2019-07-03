using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2_Linq
{
    class Film
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var films = new List<Film>
            {
                new Film { Name = "Jaws", Year = 1975 },
                new Film { Name = "Singing in the Rain", Year = 1952 },
                new Film { Name = "Some like it Hot", Year = 1959 },
                new Film { Name = "The Wizard of Oz", Year = 1939 },
                new Film { Name = "It’s a Wonderful Life", Year = 1946 },
                new Film { Name = "American Beauty", Year = 1999 },
                new Film { Name = "High Fidelity", Year = 2000 },
                new Film { Name = "The Usual Suspects", Year = 1995 }
            };

            //Создание многократно используемого делегата для вывода списка на консоль
            Action<Film> print = film => Console.WriteLine($"Name={film.Name}, Year={film.Year}");

            //Вывод на консоль исходного списка
            films.ForEach(print);

            //Создание и вывод отфильтрованного списка
            films.FindAll(film => film.Year < 1960).ForEach(print);

            //Сортировка исходного списка
            films.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));
            //or
            films.OrderBy(film => film.Name);
            Console.WriteLine("\n");

            {
                // OrderByDescending, Skip, SkipWhile, Take, TakeWhile, Select, Concat
                int[] n = { 1, 3, 5, 6, 3, 6, 7, 8, 45, 3, 7, 6 };

                IEnumerable<int> res;
                res = n.OrderByDescending(g => g).Skip(3);
                foreach (var nn in res) { Console.WriteLine(nn); }
                Console.WriteLine("\n");

                res = n.OrderByDescending(g => g).Take(3);
                foreach (var nn in res) { Console.WriteLine(nn); }
                Console.WriteLine("\n");

                res = n.Select(g => g * 2);
                foreach (var nn in res) { Console.WriteLine(nn); }
                Console.WriteLine("\n");

                // to delete from array number 45
                res = n.TakeWhile(g => g != 45).Concat(n.SkipWhile(s => s != 45).Skip(1));
                foreach (var nn in res) { Console.WriteLine(nn); }
                Console.WriteLine("\n");
            }

            {
                //Дана последовательность непустых строк. 
                //Объединить все строки в одну.
                List<string> str = new List<string> { "1qwerty", "cqwertyc", "cqwe", "c" };
                string amount = str.Aggregate<string>((x, y) => x + y);
                foreach (var amounts in amount) { Console.Write(amounts); }
                Console.WriteLine("\n");
            }

            {
                //LinqBegin3. Дано целое число L (> 0) и строковая последовательность A.
                //Вывести  строку из A, начинающуюся с цифры и имеющую длину L.
                //Если требуемых строк в последовательности A нет, то вывести строку «Not found».
                //Указание.Для обработки ситуации, связанной с отсутствием требуемых строк, использовать операцию ??.

                int length = 7;
                List<string> str = new List<string> { "1qwerty", "2qwerty", "7qwe" };
                string res = str.FirstOrDefault(x => (Char.IsDigit(x[0])) && (x.Length == length)) ?? "Not found";
                Console.WriteLine(res);
            }

            {
                //LinqBegin5. Дан символ С и строковая последовательность A.
                //Найти количество элементов A, которые содержат более одного символа и при этом начинаются и оканчиваются символом C.

                char c = 'c';
                List<string> str = new List<string> { "1qwerty", "cqwertyc", "cqwe", "c" };
                int amount = str.Count(x => (x.StartsWith(c.ToString())) && (x.EndsWith(c.ToString())) && (x.Length > 1));
                Console.WriteLine(amount);
            }

            {
                //LinqBegin6. Дана строковая последовательность.
                //Найти сумму длин всех строк, входящих в данную последовательность.
                //TODO
                List<string> str = new List<string> { "1qwerty", "cqwertyc", "cqwe", "c" };
                string amount = str.Aggregate<string>((x, y) => x + y);
                int sum = amount.Length;
                Console.WriteLine(sum);

            }

            {
                //LinqBegin11. Дана последовательность непустых строк. 
                //Получить строку, состоящую из начальных символов всех строк исходной последовательности.
                //TODO
                List<string> str = new List<string> { "1qwerty", "cqwertyc", "cqwe", "c" };
                string result = str.Aggregate            //Aggregate<TSource,TAccumulate,TResult>
                    (
                       new StringBuilder(str.Capacity),  //buf 
                       (buf, c) => buf.Append(c[0]),
                       buf => buf.ToString()
                    );
                Console.WriteLine(result);
            }

            //example Aggregate Linq
            {
                string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };
                // Determine whether any string in the array is longer than "banana".
                string longestName = fruits.Aggregate(
                                    "banana",
                                    (longest, next) => next.Length > longest.Length ? next : longest,
                                    fruit => fruit.ToUpper());
                Console.WriteLine($"The fruit with the longest name is {longestName}.");
            }

            {
                //LinqBegin30. Дано целое число K (> 0) и целочисленная последовательность A.
                //Найти теоретико-множественную разность двух фрагментов A: первый содержит все четные числа, 
                //а второй — все числа с порядковыми номерами, большими K.
                //В полученной последовательности(не содержащей одинаковых элементов) поменять порядок элементов на обратный.
                int k = 5;
                IEnumerable<int> n = new int[] { 12, 88, 1, 3, 5, 4, 6, 6, 2, 5, 8, 9, 0, 90 };
                var res = n.Where(x => x % 2 == 0).Except(n.Skip(k)).Reverse();
            }

            {
                //LinqBegin22. Дано целое число K (> 0) и строковая последовательность A.
                //Строки последовательности содержат только цифры и заглавные буквы латинского алфавита.
                //Извлечь из A все строки длины K, оканчивающиеся цифрой, отсортировав их по возрастанию.
                //TODO
                int k = 5;
                List<string> str = new List<string> { "ERTE5", "ERT1", "AFGH3", "4CV" };
                var res = str.FindAll(x => (Char.IsDigit(x[x.Length - 1])) && (x.Length == k)).OrderBy(x => x);
                foreach (var n in res)
                {
                    Console.WriteLine(n);
                }

            }

            {
                //LinqBegin29. Даны целые числа D и K (K > 0) и целочисленная последовательность A.
                //Найти теоретико - множественное объединение двух фрагментов A: первый содержит все элементы до первого элемента, 
                //большего D(не включая его), а второй — все элементы, начиная с элемента с порядковым номером K.
                //Полученную последовательность(не содержащую одинаковых элементов) отсортировать по убыванию.
                //TODO
                int K = 3;
                int D = 100;
                IEnumerable<int> A = new int[] { 99, 88, 2, 10, 5, 4, 100 };

                var res = A.Skip(K - 1).Union(A.TakeWhile(g => g < D)).OrderByDescending(x => x);
                //Distinct();
                foreach (var n in res)
                {
                    Console.Write(n + " ");
                }
            }

            {
                //LinqBegin34. Дана последовательность положительных целых чисел.
                //Обрабатывая только нечетные числа, получить последовательность их строковых представлений и отсортировать ее по возрастанию.
                IEnumerable<int> n = new int[] { 12, 88, 1, 3, 5, 4, 6, 6, 2, 5, 8, 9, 0, 90 };
                var res = n.Where(x => x % 2 != 0).Select(x => x.ToString()).OrderBy(x => x);
                Console.Write("\n");
            }

            {
                //LinqBegin46. Даны последовательности положительных целых чисел A и B; все числа в каждой последовательности различны.
                //Найти последовательность всех пар чисел, удовлетворяющих следующим условиям: первый элемент пары принадлежит 
                //последовательности A, второй принадлежит B, и оба элемента оканчиваются одной и той же цифрой. 
                //Результирующая последовательность называется внутренним объединением последовательностей A и B по ключу, 
                //определяемому последними цифрами исходных чисел. 
                //Представить найденное объединение в виде последовательности строк, содержащих первый и второй элементы пары, 
                //разделенные дефисом, например, «49 - 129».
                IEnumerable<int> n1 = new int[] { 12, 88, 11, 3, 55, 679, 222, 845, 9245 };
                IEnumerable<int> n2 = new int[] { 123, 888, 551, 443, 69, 222, 780 };
                var res = n1.Join(n2, x => x % 10, y => y % 10, (x, y) => x.ToString() + " - " + y.ToString() + "  ");  // ключи: x => x % 10, y => y % 10
                foreach (var n in res)
                {
                    Console.Write(n);
                }
            }

            {
                //LinqBegin56. Дана целочисленная последовательность A.
                //Сгруппировать элементы последовательности A, оканчивающиеся одной и той же цифрой, и на основе этой группировки 
                //получить последовательность строк вида «D: S», где D — ключ группировки (т.е.некоторая цифра, которой оканчивается 
                //хотя бы одно из чисел последовательности A), а S — сумма всех чисел из A, которые оканчиваются цифрой D.
                //Полученную последовательность упорядочить по возрастанию ключей.
                //Указание.Использовать метод GroupBy.
                IEnumerable<int> n = new int[] { 12, 88, 11, 3, 55, 679, 222, 845, 9245 };
                List<string> res = new List<string>();

                IEnumerable<IGrouping<int, int>> groups = n.GroupBy(x => x % 10).OrderBy(x => x.Key);

                foreach (IGrouping<int, int> group in groups)
                {
                    string listElement = group.Key.ToString();
                    int summaryValue = 0;
                    foreach (int item in group)
                    {
                        summaryValue += item;
                    }
                    listElement = listElement + ": " + summaryValue.ToString();
                    res.Add(listElement);
                }
                Console.WriteLine();
                foreach (var nn in res)
                {
                    Console.WriteLine(nn);
                }
            }

            {
                //LinqBegin36. Дана последовательность непустых строк. 
                //Получить последовательность символов, которая определяется следующим образом: 
                //если соответствующая строка исходной последовательности имеет нечетную длину, то в качестве
                //символа берется первый символ этой строки; в противном случае берется последний символ строки.
                //Отсортировать полученные символы по убыванию их кодов.
                //TODO

                List<string> str1 = new List<string> { "apple", "mango", "orange", "passionfruit", "grape" };
                var res1 = str1.Select(x => (x.Length % 2 != 0) ? (x[0]) : (x[x.Length - 1])).ToArray();
                Console.WriteLine(res1);
            }

            {
                //LinqBegin44. Даны целые числа K1 и K2 и целочисленные последовательности A и B.
                //Получить последовательность, содержащую все числа из A, большие K1, и все числа из B, меньшие K2. 
                //Отсортировать полученную последовательность по возрастанию.
                //TODO

                int K1 = 60;
                int K2 = 20;
                IEnumerable<int> A = new int[] { 12, 89, 11, 3, 55, 67 };
                IEnumerable<int> B = new int[] { 13, 18, 55, 44, 69 };

                var res4 = A.Where(x => x > K1).Union(B.Where(y => y < K2).OrderBy(x => x));
                Console.WriteLine();
                foreach (var nn in res4)
                {
                    Console.WriteLine(nn);
                }

            }

            {
                //LinqBegin48.Даны строковые последовательности A и B; все строки в каждой последовательности различны, 
                //имеют ненулевую длину и содержат только цифры и заглавные буквы латинского алфавита. 
                //Найти внутреннее объединение A и B, каждая пара которого должна содержать строки одинаковой длины.
                //Представить найденное объединение в виде последовательности строк, содержащих первый и второй элементы пары, 
                //разделенные двоеточием, например, «AB: CD». Порядок следования пар должен определяться порядком 
                //первых элементов пар(по возрастанию), а для равных первых элементов — порядком вторых элементов пар(по убыванию).
                //TODO


                List<string> str1 = new List<string> { "ERTE5", "ERT1", "AFGH3", "4CV" };
                List<string> str2 = new List<string> { "MRTS5", "VRO1", "FFGH2", "7CKK" };

                var req11 = from s1 in str1
                            join s2 in str2
                            on s1.Length equals s2.Length
                            orderby (s1, s2)
                            select new CallPair(s1, s2);

                var req12 = str1.Join(str2,
                            s1 => s1.Length,
                            s2 => s2.Length,
                            (s1, s2) => new CallPair(s1, s2)).
                            OrderBy(s1 => s1.s1).
                            ThenByDescending(s2 => s2.s2);


                foreach (var gr in req11)
                {
                    Console.WriteLine($"{gr.s1} : {gr.s2} ");
                }
                Console.WriteLine();

                foreach (var gr in req12)
                {
                    Console.WriteLine($"{gr.s1} : {gr.s2} ");
                }
            }

            //LinqObj17. Исходная последовательность содержит сведения об абитуриентах. Каждый элемент последовательности
            //включает следующие поля: < Номер школы > < Год поступления > < Фамилия >
            //Для каждого года, присутствующего в исходных данных, вывести число различных школ, которые окончили абитуриенты, 
            //поступившие в этом году (вначале указывать число школ, затем год). 
            //Сведения о каждом годе выводить на новой строке и упорядочивать по возрастанию числа школ, 
            //а для совпадающих чисел — по возрастанию номера года.
            //TODO

            List<Student> Students = new List<Student>() {
               new Student() {SchoolNum = 121, SchoolAdmissionYear = 2007, StudenSurname ="Jones" },
               new Student() {SchoolNum = 125, SchoolAdmissionYear = 2007, StudenSurname ="Jones" },
               new Student() {SchoolNum = 122, SchoolAdmissionYear = 2008, StudenSurname ="Petrov" },
               new Student() {SchoolNum = 123, SchoolAdmissionYear = 2008, StudenSurname ="Petrov" },
               new Student() {SchoolNum = 155, SchoolAdmissionYear = 2008, StudenSurname ="Petrov" },
               new Student() {SchoolNum = 121, SchoolAdmissionYear = 2009, StudenSurname ="Petrov" },
               new Student() {SchoolNum = 155, SchoolAdmissionYear = 2009, StudenSurname ="Petrov" },
               new Student() {SchoolNum = 114, SchoolAdmissionYear = 2009, StudenSurname ="Petrov" },
               new Student() {SchoolNum = 110, SchoolAdmissionYear = 2009, StudenSurname ="Petrov" }

            };

            var req1 = from stud in Students
                       group stud by stud.SchoolAdmissionYear
                       into g
                       select new { Year = g.Key, Count = g.Count() }
                       into v
                       orderby v.Count
                       select v;
            foreach (var gr in req1)
                Console.WriteLine($"{gr.Year} - {gr.Count}");

            Console.ReadKey();

        }

        public class Student
        {
            public int SchoolNum { get; set; }
            public int SchoolAdmissionYear { get; set; }
            public string StudenSurname { get; set; }
        }

        private class CallPair
        {
            public string s1 { get; set; }
            public string s2 { get; set; }

            public CallPair(string s1_, string s2_)
            {
                s1 = s1_;
                s2 = s2_;
            }
        }
    }
}
