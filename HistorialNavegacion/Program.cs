class Program
{
    static Stack<string> historial = new Stack<string>();

    static void Main()
    {
        int opcion;
        do
        {
            opcion = MostrarMenu();

            switch (opcion)
            {
                case 1:
                    VisitarPagina();
                    break;
                case 2:
                    Retroceder();
                    break;
                case 3:
                    MostrarPaginaActual();
                    break;
                case 4:
                    MostrarHistorial();
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            if (opcion != 0)
            {
                Console.WriteLine("\nPresiona Enter para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 0);

        Console.WriteLine("Programa finalizado.");
    }

    static int MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("*** SIMULADOR DE HISTORIAL DE NAVEGACIÓN ***");
        Console.WriteLine("=================================================================");
        Console.WriteLine($"Páginas en el historial: {historial.Count}");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("1. Visitar una página");
        Console.WriteLine("2. Retroceder a la página anterior");
        Console.WriteLine("3. Mostrar la página actual");
        Console.WriteLine("4. Mostrar historial completo de navegación");
        Console.WriteLine("0. Salir");

        return LeerEntero("Selecciona una opción: ");
    }

    static void VisitarPagina()
    {
        Console.Write("\nIngresa la URL o nombre de la página a visitar: ");
        string pagina = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(pagina))
        {
            Console.WriteLine("La página no puede estar vacía.");
            return;
        }

        historial.Push(pagina);
        Console.WriteLine($"Visitando: '{pagina}'.");
    }

    static void Retroceder()
    {
        if (historial.Count == 0)
        {
            Console.WriteLine("\nNo hay páginas en el historial para retroceder.");
            return;
        }

        if (historial.Count == 1)
        {
            Console.WriteLine("\nYa estás en la primera página visitada, no hay historial previo.");
            return;
        }

        string paginaAnterior = historial.Pop();
        Console.WriteLine($"\nRetrocediendo desde: '{paginaAnterior}'.");
        Console.WriteLine($"Ahora estás en: '{historial.Peek()}'.");
    }

    static void MostrarPaginaActual()
    {
        if (historial.Count == 0)
        {
            Console.WriteLine("\nNo hay ninguna página visitada todavía.");
            return;
        }

        Console.WriteLine($"\nPágina actual: '{historial.Peek()}'.");
    }

    static void MostrarHistorial()
    {
        if (historial.Count == 0)
        {
            Console.WriteLine("\nNo hay historial de navegación todavía.");
            return;
        }

        Console.WriteLine("\n*** HISTORIAL DE NAVEGACIÓN ***");

        string[] paginas = historial.ToArray();

        for (int i = 0; i < paginas.Length; i++)
        {
            string etiqueta = i == 0 ? " (actual)" : "";
            Console.WriteLine($"{i + 1}. {paginas[i]}{etiqueta}");
        }
    }

    static int LeerEntero(string mensaje)
    {
        int valor;
        Console.Write(mensaje);
        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Write("Entrada inválida, debe ser un número entero. Intenta de nuevo: ");
        }
        return valor;
    }
}