using System;

/// <synaps> Queue class name </synaps>
public class Queue<T>
{

    class Node
    {
        public T value;
        public Node next;

        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
    }

    Node head;
    Node tail;
    
    int count;

    /// <synaps> Returns Queue's type </synaps>
    public Type CheckType()
    {
        return typeof(T);
    }

    /// <synaps> Enqueue Method </synaps>
    public T Enqueue(T value)
    {
        Node node = new Node(value);
        if (head == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            tail.next = node;
            tail = node;
        }
        count++;
        return node.value;
    }
    
    /// <synaps> Dequeue Method </synaps>
    public T Dequeue()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return default(T);
        }
        Node node = head;
        head = node.next;
        count--;
        return node.value;
    }

    ///<synaps> Peek Method </synaps>
    public T Peek()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return default(T);
        }
        return head.value;
    }

    ///<synaps> Print Method </synaps>
    public void Print()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return;
        }
        Node node = head;
        while (node != null)
        {
            Console.WriteLine(node.value);
            node = node.next;
        }
    }


    ///<synaps> Counts # of Nodes in Queue </synaps>
    public int Count()
    {
        int i = 0;
        Node node = head;
        while (node != null)
        {
            i++;
            node = node.next;
        }
        count = i;
        return count;
    }

    ///<synaps> Concatenates all values in the queue only if the queue is of type (string) or (char) </synaps>
    public string Concatenate()
    {
        if (head == null)
        {
            Console.WriteLine("Queue is empty");
            return null;
        }
        if (typeof(T) != typeof(string) && typeof(T) != typeof(char))
        {
            Console.WriteLine("Concatenate() is for a queue of Strings or Chars only.");
            return null;
        }
        Node node = head;
        string result = "";
        while (node != null)
        {
            result += node.value;
            if (typeof(T) == typeof(string) && node.next != null)
                result += " ";
            node = node.next;
        }
        return result;
    }
}
