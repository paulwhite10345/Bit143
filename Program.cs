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
        // the static int num is the size of the data set determined by the user.
        public static int max;
        //static int max is the range of the data set which is also determined by the user.
        public static LinkedList<int> DataList = new LinkedList<int> ();
        static void Main(string[] args)
        {
            //have the program fill a queue with a set number of random integers determined by the user
            Dataset data = new Dataset();
            Console.WriteLine("Welcome to the duplicate guessing game: \nwhere we ask you to determine a data set size and a range \nAnd ask for you to guess how many duplicate numbers are within that dataset."); 
            Console.Write("\nHow large of a data set would you like: ");
            //intro text
            num = Int32.Parse(Console.ReadLine());
            Console.Write("\nWhat would you like the maximum range of values to be: ");
            max = Int32.Parse(Console.ReadLine());
            data.constructor(num);
            Console.Write("\nHow many integer values do you think are duplicates?\nyou will have 3 tries to answer\nMaximum value is {0} and there is {1} values in the data set\nWhat is your first guess: ",num,max);
            data.Userguess(Int32.Parse(Console.ReadLine()), data.Parse(max), 0);
            Console.WriteLine("");
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
            while (Data.Count > 0)
            {
                Program.DataList.AddLast(Convert.ToInt32(Data.Dequeue()));
            }
        }
        public void Userguess(int guess, int duplicateCount,int numGuesses)
        {
            //the userguess method is a recursive method that checks the user inputted value against the value returned by the Parse method which deletes and counts the number of duplicate integer values.
            int tries = numGuesses;
            if (guess != duplicateCount & tries == 3)
            //1st base case which will print an end statement if true.
            {
                Console.WriteLine("You currently have {0} tries left and the correct answer was {1}", (numGuesses), duplicateCount);
                Console.WriteLine("\n\nAfter removing duplicates from the randomized dataset the Linked List contains {0} values ranging from {1} to {2}\n", Program.DataList.Count(), Program.DataList.Min(), Program.DataList.Max());
            }
            else if (guess != duplicateCount & tries < 3)
            {
                if (guess > duplicateCount)
                    Console.WriteLine("That guess is incorrect you have {0} more tries and the current guess is higher than the actual result: ", (3-tries));
                else if (guess < duplicateCount)
                    Console.WriteLine("That guess is incorrect you have {0} more tries and the current guess is lower than the actual result: ", (3-tries));
                tries++;
                Userguess(Int32.Parse(Console.ReadLine()), duplicateCount, tries);
            }
            else if (guess == duplicateCount)
            {
                Console.WriteLine("{0} that is the correct amount of duplicate values in the randomized data set of {1} values",guess,Program.num);
                Console.WriteLine("\n\nAfter removing duplicates from the randomized dataset the Linked List contains {0} values ranging from {1} to {2}\n", Program.DataList.Count(), Program.DataList.Min(), Program.DataList.Max());
            }
        }
        public int Parse(int num)
        {
            int DuplicateCount = 0;
            if (Program.DataList.Count != 0)
            {
                for (int i = 0; i <= num; i++)
                {
                    if (Program.DataList.Contains(i))
                    {
                        while (Program.DataList.Find(i) != Program.DataList.FindLast(i))
                        {
                            DuplicateCount++;
                            Program.DataList.Remove(Program.DataList.FindLast(i));
                            //removes duplicate ints from the LinkedList, Does not organize them in any way.
                            //organization will be done when the Linked List is sorted to the BST.
                        }
                    }
                }
            }
            return DuplicateCount;
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
