using System;

// Clase Nodo para representar cada número en la lista enlazada
public class Node
{
    public int Value { get; set; } // Valor del nodo
    public Node Next { get; set; } // Puntero al siguiente nodo

    // Constructor para inicializar el nodo con un valor
    public Node(int value)
    {
        Value = value;
        Next = null;
    }
}

// Clase para gestionar la lista enlazada
public class LinkedList
{
    private Node head; // Cabeza de la lista enlazada

    // Método para agregar un nodo al final de la lista
    public void AddNode(int value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    // Método para eliminar nodos fuera de un rango especificado
    public void RemoveOutOfRange(int min, int max)
    {
        while (head != null && (head.Value < min || head.Value > max))
        {
            head = head.Next;
        }

        Node current = head;
        while (current != null && current.Next != null)
        {
            if (current.Next.Value < min || current.Next.Value > max)
            {
                current.Next = current.Next.Next;
            }
            else
            {
                current = current.Next;
            }
        }
    }

    // Método para imprimir los valores de la lista enlazada
    public void PrintList()
    {
        Node current = head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}

// Clase principal del programa
public class Program
{
    public static void Main(string[] args)
    {
        LinkedList list = new LinkedList();
        Random random = new Random();

        // Crear la lista con 50 números aleatorios entre 1 y 999
        for (int i = 0; i < 50; i++)
        {
            int randomNumber = random.Next(1, 1000);
            list.AddNode(randomNumber);
        }

        Console.WriteLine("Lista original:");
        list.PrintList();

        // Leer el rango desde el teclado
        Console.Write("Ingrese el valor mínimo del rango: ");
        int min = int.Parse(Console.ReadLine());
        Console.Write("Ingrese el valor máximo del rango: ");
        int max = int.Parse(Console.ReadLine());

        // Eliminar nodos fuera del rango
        list.RemoveOutOfRange(min, max);

        Console.WriteLine("Lista después de eliminar los nodos fuera del rango:");
        list.PrintList();
    }
}
