using System;

// Clase Nodo para representar a cada estudiante en la lista enlazada
public class Student
{
    public string Cedula { get; set; } // Cédula del estudiante
    public string Nombre { get; set; } // Nombre del estudiante
    public string Apellido { get; set; } // Apellido del estudiante
    public string Correo { get; set; } // Correo electrónico del estudiante
    public double NotaDefinitiva { get; set; } // Nota definitiva del estudiante
    public Student Next { get; set; } // Puntero al siguiente estudiante

    // Constructor para inicializar los datos del estudiante
    public Student(string cedula, string nombre, string apellido, string correo, double nota)
    {
        Cedula = cedula;
        Nombre = nombre;
        Apellido = apellido;
        Correo = correo;
        NotaDefinitiva = nota;
        Next = null;
    }
}

// Clase de lista enlazada para gestionar a los estudiantes
public class StudentList
{
    private Student headApproved = null; // Cabeza de la lista de estudiantes aprobados
    private Student headDisapproved = null; // Cabeza de la lista de estudiantes reprobados

    // Método para agregar un estudiante a la lista correspondiente
    public void AddStudent(string cedula, string nombre, string apellido, string correo, double nota)
    {
        Student newStudent = new Student(cedula, nombre, apellido, correo, nota);

        if (nota >= 7)
        {
            // Insertar al inicio de la lista de aprobados
            newStudent.Next = headApproved;
            headApproved = newStudent;
        }
        else
        {
            // Insertar al final de la lista de reprobados
            if (headDisapproved == null)
            {
                headDisapproved = newStudent;
            }
            else
            {
                Student current = headDisapproved;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newStudent;
            }
        }
    }

    // Método para buscar un estudiante por cédula
    public Student SearchStudent(string cedula)
    {
        // Buscar en la lista de aprobados
        Student current = headApproved;
        while (current != null)
        {
            if (current.Cedula == cedula)
                return current;
            current = current.Next;
        }

        // Buscar en la lista de reprobados
        current = headDisapproved;
        while (current != null)
        {
            if (current.Cedula == cedula)
                return current;
            current = current.Next;
        }

        return null; // Estudiante no encontrado
    }

    // Método para eliminar un estudiante por cédula
    public bool DeleteStudent(string cedula)
    {
        // Intentar eliminar de la lista de aprobados
        if (DeleteFromList(ref headApproved, cedula))
            return true;

        // Intentar eliminar de la lista de reprobados
        return DeleteFromList(ref headDisapproved, cedula);
    }

    // Método auxiliar para eliminar de una lista específica
    private bool DeleteFromList(ref Student head, string cedula)
    {
        Student current = head;
        Student previous = null;

        while (current != null)
        {
            if (current.Cedula == cedula)
            {
                if (previous == null)
                {
                    head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false; // Estudiante no encontrado
    }

    // Método para contar estudiantes aprobados
    public int CountApproved()
    {
        return CountList(headApproved);
    }

    // Método para contar estudiantes reprobados
    public int CountDisapproved()
    {
        return CountList(headDisapproved);
    }

    // Método auxiliar para contar nodos en una lista
    private int CountList(Student head)
    {
        int count = 0;
        Student current = head;
        while (current != null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }

    // Método para imprimir todos los estudiantes
    public void PrintAllStudents()
    {
        Console.WriteLine("Estudiantes Aprobados:");
        PrintList(headApproved);
        Console.WriteLine("Estudiantes Reprobados:");
        PrintList(headDisapproved);
    }

    // Método auxiliar para imprimir una lista de estudiantes
    private void PrintList(Student head)
    {
        Student current = head;
        while (current != null)
        {
            Console.WriteLine($"Cédula: {current.Cedula}, Nombre: {current.Nombre}, Apellido: {current.Apellido}, Correo: {current.Correo}, Nota: {current.NotaDefinitiva}");
            current = current.Next;
        }
    }
}

// Clase principal del programa
public class Program
{
    public static void Main(string[] args)
    {
        StudentList studentList = new StudentList();

        while (true)
        {
            Console.WriteLine("\n--- Gestión de Estudiantes ---");
            Console.WriteLine("1. Agregar Estudiante");
            Console.WriteLine("2. Buscar Estudiante por Cédula");
            Console.WriteLine("3. Eliminar Estudiante");
            Console.WriteLine("4. Contar Estudiantes Aprobados");
            Console.WriteLine("5. Contar Estudiantes Reprobados");
            Console.WriteLine("6. Imprimir Todos los Estudiantes");
            Console.WriteLine("7. Salir");

            Console.Write("Seleccione una opción: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    // Capturar datos del estudiante
                    Console.Write("Ingrese la Cédula: ");
                    string cedula = Console.ReadLine();
                    Console.Write("Ingrese el Nombre: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingrese el Apellido: ");
                    string apellido = Console.ReadLine();
                    Console.Write("Ingrese el Correo: ");
                    string correo = Console.ReadLine();
                    Console.Write("Ingrese la Nota Definitiva: ");
                    double nota = double.Parse(Console.ReadLine());
                    studentList.AddStudent(cedula, nombre, apellido, correo, nota);
                    break;
                case 2:
                    // Buscar estudiante por cédula
                    Console.Write("Ingrese la Cédula a buscar: ");
                    cedula = Console.ReadLine();
                    Student found = studentList.SearchStudent(cedula);
                    if (found != null)
                    {
                        Console.WriteLine($"Encontrado - Cédula: {found.Cedula}, Nombre: {found.Nombre}, Apellido: {found.Apellido}, Correo: {found.Correo}, Nota: {found.NotaDefinitiva}");
                    }
                    else
                    {
                        Console.WriteLine("Estudiante no encontrado.");
                    }
                    break;
                case 3:
                    // Eliminar estudiante por cédula
                    Console.Write("Ingrese la Cédula a eliminar: ");
                    cedula = Console.ReadLine();
                    if (studentList.DeleteStudent(cedula))
                    {
                        Console.WriteLine("Estudiante eliminado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("Estudiante no encontrado.");
                    }
                    break;
                case 4:
                    // Contar estudiantes aprobados
                    Console.WriteLine($"Total de Estudiantes Aprobados: {studentList.CountApproved()}");
                    break;
                case 5:
                    // Contar estudiantes reprobados
                    Console.WriteLine($"Total de Estudiantes Reprobados: {studentList.CountDisapproved()}");
                    break;
                case 6:
                    // Imprimir todos los estudiantes
                    studentList.PrintAllStudents();
                    break;
                case 7:
                    // Salir del programa
                    return;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }
        }
    }
}
