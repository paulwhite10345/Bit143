using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Major_Asignment_3
{
    class Program
    {
        public static Random rnd = new Random();
        public static int num;
        public static int max;
        public static LinkedList<int> DataList = new LinkedList<int> ();
        static void Main(string[] args)
        {
            //have the program fill a queue with a set number of random integers determined by the user
            Dataset data = new Dataset();
            Console.Write("\nHow large of a data set would you like: ");
            num = Int32.Parse(Console.ReadLine());
            Console.Write("\nWhat would you like the maximum range of values to be: ");
            max = Int32.Parse(Console.ReadLine());
            data.constructor(num);
            data.Parse(max);
            BinaryTree tree = new BinaryTree();
            while (DataList.Count != 0)
            {
                tree.push(DataList.First());
                DataList.RemoveFirst();
            }
            BinaryTreeNode node = tree.convertList2Binary(tree.root);
            Console.WriteLine("inorder traversal of the constructed binary tree is\n");
            tree.inorderTraversal(node);
            Console.WriteLine("");
        }
    }
    class Dataset
    {
        static Queue Data = new Queue();
        public void constructor(int num)
        {
            for (int i = 0; i < num; i++ )
            {
                //constructs random data set with integer values ranging from 0-100 the random int data is input into a queue as its created
                int temp = Program.rnd.Next(0,Program.max+1);
                Data.Enqueue(temp);
            }
            Console.WriteLine("\n");
            while (Data.Count > 0)
            {
                Console.Write("{0} ", Data.Peek());
                Program.DataList.AddLast(Convert.ToInt32(Data.Dequeue()));
            }
            Console.WriteLine("\n");
        }
        public void Parse(int num)
        {
            if (Program.DataList.Count != 0)
            {
                Console.WriteLine("\nBefore removing duplicates from the randomized dataset the linked List contains {0} values\n", Program.DataList.Count());
                Console.Write("Present Values within the List:");
                for (int i = 0; i <= num; i++)
                {
                    if (Program.DataList.Contains(i))
                    {
                        while (Program.DataList.Find(i) != Program.DataList.FindLast(i))
                        {
                            Program.DataList.Remove(Program.DataList.FindLast(i));
                            //removes duplicate ints from the LinkedList, Does not organize them in any way.
                            //organization will be done when the Linked List is sorted to the BST.
                        }
                        //if (Program.DataList.Find(i) == Program.DataList.FindLast(i))
                        //{
                        //    Program.DataList.Remove(Program.DataList.Find(i));
                        //    Program.DataList.AddLast(i);
                        //    //reorganizes the list
                        //}
                        Console.Write("{0} ", i);
                    }
                }
                Console.WriteLine("\n\nAfter removing duplicates from the randomized dataset the Linked List contains {0} values ranging from {1} to {2}\n", Program.DataList.Count(),Program.DataList.Min(),Program.DataList.Max());
            }
        }
    }
    public class ListNode
    {
        public int data;
        public ListNode next;
        public ListNode (int d)
        {
            data = d;
            next = null;
        }
    }
    public class BinaryTreeNode
    {
        public int data;
        public BinaryTreeNode left, right = null;
        public BinaryTreeNode(int data)
        {
            this.data = data;
            left = right = null;
        }
    }
    public class BinaryTree
    {
        ListNode head;
        public BinaryTreeNode root;
        public void push(int new_data)
        {
            // allocate node and assign data 
            ListNode new_node = new ListNode(new_data);
            new_node.next = head;
            head = new_node;
        }


        public BinaryTreeNode convertList2Binary(BinaryTreeNode node)
        {
            Queue<BinaryTreeNode>q = new Queue<BinaryTreeNode>();
            //creates a queue to store parent values

            if(head == null)
            {
                node = null;
                return null;
            }
            node = new BinaryTreeNode(head.data);
            q.Enqueue(node);

            head = head.next;
            while (head != null)
            {
                BinaryTreeNode parent = q.Peek();
                BinaryTreeNode pp = q.Dequeue();

                BinaryTreeNode leftChild = null, rightChild = null;

                leftChild = new BinaryTreeNode(head.data);
                q.Enqueue(leftChild);
                head = head.next;

                if (Program.DataList.Count != 0)
                {
                    rightChild = new BinaryTreeNode(head.data);
                    q.Enqueue(rightChild);
                    head = head.next;
                }
                parent.left = leftChild;
                parent.right = rightChild;
            }
            return node;
        }
        public void inorderTraversal(BinaryTreeNode node)
        {
            if (node != null)
            {
                inorderTraversal(node.left);
                Console.Write(node.data + " ");
                inorderTraversal(node.right);
            }
        }
    }
}
