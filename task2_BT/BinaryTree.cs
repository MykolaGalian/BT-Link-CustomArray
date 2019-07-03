using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2_BT
{
    // Объявляем делегат
    public delegate void AddItemToTreeHandler(string message);

    //обобщенный класс - бинарное дерево
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public BinaryTree<T> LeftTree { get; private set; }   //  "ссылка" на левый наследник
        public BinaryTree<T> RightTree { get; private set; }  //  "ссылка" на правый наследник

        public T Node { get; private set; }  // родительский узел

        public BinaryTree(T node)
        {
            this.Node = node;
        }

        // Событие, возникающее при добавлении элемента в дерево 
        public event AddItemToTreeHandler MessageAdd;


        // добавление обьекта в дерево на основе переопределенного метода CompareTo
        public void Insert(T item)
        {
            if (this.Node.CompareTo(item) > 0)   // записываем в левые наследники все оценки меньше родительской 
            {
                if (this.LeftTree == null)
                {
                    this.LeftTree = new BinaryTree<T>(item);
                }
                else
                    this.LeftTree.Insert(item);
            }
            else
            {
                if (this.RightTree == null)   // записываем в правые наследники все оценки больше или равные родительской
                {
                    this.RightTree = new BinaryTree<T>(item);
                }
                else
                    this.RightTree.Insert(item);
            }

            // Событие - сообщение после добавления элемента в дерево
            if (MessageAdd != null)
                MessageAdd($"In tree added element {item.ToString()}");
        }

        // реализация GetEnumerator для двоичного дерева
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            // перебор левых наследников
            if (this.LeftTree != null)
            {
                foreach (T item in this.LeftTree)
                {
                    yield return item;
                }
            }

            //возврат в текущий родительский узел
            yield return this.Node;


            // перебор правых наследников
            if (this.RightTree != null)
            {
                foreach (T item in this.RightTree)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }

    public static class Extensions
    {
        // метод расширения для класса BinaryTree с переменным кол-вом параметров - добавляет элементы в цикле, через метод Insert
        public static void InsertItems<T>(this BinaryTree<T> tree, params T[] items) where T : IComparable<T>
        {
            foreach (T item in items)
            {
                tree.Insert(item);
            }
        }
    }
}
