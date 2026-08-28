class Program
{
    static Queue<string> colaAtencion = new Queue<string>();

    static void Main()
    {
        int opcion;
        do
        {
            opcion = MostrarMenu();

            switch (opcion)
            {
                case 1:
                    RegistrarPersona();
                    break;
                case 2:
                    AtenderPrimero();
                    break;
                case 3:
                    MostrarPendientes();
                    break;
                case 4:
                    MostrarSiguiente();
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
        Console.WriteLine("*** SISTEMA DE COLA DE ATENCIÓN ***");
        Console.WriteLine("=========================================================");
        Console.WriteLine($"Personas en espera: {colaAtencion.Count}");
        Console.WriteLine("---------------------------------------------------------");
        Console.WriteLine("1. Registrar persona en la cola");
        Console.WriteLine("2. Atender al primero de la cola");
        Console.WriteLine("3. Mostrar cantidad de pendientes");
        Console.WriteLine("4. Mostrar siguiente persona a atender");
        Console.WriteLine("0. Salir");

        return LeerEntero("Selecciona una opción: ");
    }

    static void RegistrarPersona()
    {
        Console.Write("\nIngresa el nombre de la persona: ");
        string nombre = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }

        // Enqueue: agrega el elemento al FINAL de la cola
        colaAtencion.Enqueue(nombre);
        Console.WriteLine($"'{nombre}' fue registrado en la cola.");
    }

    static void AtenderPrimero()
    {
        if (!HayPendientes()) return;

        // Dequeue: extrae y elimina el elemento que está al FRENTE de la cola
        string atendido = colaAtencion.Dequeue();
        Console.WriteLine($"\nAtendiendo a: '{atendido}'.");
        Console.WriteLine($"Quedan {colaAtencion.Count} persona(s) en espera.");
    }

    static void MostrarPendientes()
    {
        Console.WriteLine($"\nCantidad de personas pendientes por atender: {colaAtencion.Count}");
    }

    static void MostrarSiguiente()
    {
        if (!HayPendientes()) return;

        // Peek: consulta el elemento al FRENTE de la cola SIN eliminarlo
        string siguiente = colaAtencion.Peek();
        Console.WriteLine($"\nLa siguiente persona a atender es: '{siguiente}'.");
    }

    static bool HayPendientes()
    {
        if (colaAtencion.Count == 0)
        {
            Console.WriteLine("\nNo hay personas en la cola de atención.");
            return false;
        }
        return true;
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