// Clase Producto: define la información de cada artículo del inventario
class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Stock { get; set; }

    public Producto(int id, string nombre, int stock)
    {
        Id = id;
        Nombre = nombre;
        Stock = stock;
    }

    public bool TieneStockBajo => Stock < 5;
}

class Program
{
    static Dictionary<int, Producto> inventario = new Dictionary<int, Producto>();

    static void Main()
    {
        CargarProductosDeEjemplo();

        int opcion;
        do
        {
            opcion = MostrarMenu();

            switch (opcion)
            {
                case 1:
                    AgregarProducto();
                    break;
                case 2:
                    BuscarProducto();
                    break;
                case 3:
                    ActualizarStock();
                    break;
                case 4:
                    EliminarProducto();
                    break;
                case 5:
                    MostrarStockBajo();
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

    static void CargarProductosDeEjemplo()
    {
        inventario.Add(1, new Producto(1, "Teclado mecánico", 12));
        inventario.Add(2, new Producto(2, "Mouse inalámbrico", 3));
        inventario.Add(3, new Producto(3, "Monitor 24 pulgadas", 4));
    }

    static int MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("*** INVENTARIO DE PRODUCTOS ***");
        Console.WriteLine("================================================================");
        Console.WriteLine($"Productos registrados: {inventario.Count}");
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine("1. Agregar producto");
        Console.WriteLine("2. Buscar producto por ID");
        Console.WriteLine("3. Actualizar stock de un producto");
        Console.WriteLine("4. Eliminar producto");
        Console.WriteLine("5. Mostrar productos con stock bajo");
        Console.WriteLine("0. Salir");

        return LeerEntero("Selecciona una opción: ");
    }

    static void AgregarProducto()
    {
        int id = LeerEntero("\nIngresa el ID del producto: ");

        // ContainsKey: verifica si la clave ya existe antes de insertar
        if (inventario.ContainsKey(id))
        {
            Console.WriteLine($"Ya existe un producto con el ID {id}.");
            return;
        }

        Console.Write("Ingresa el nombre del producto: ");
        string nombre = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }

        int stock = LeerEntero("Ingresa el stock inicial: ");

        // Add: inserta en el diccionario
        inventario.Add(id, new Producto(id, nombre, stock));
        Console.WriteLine($"Producto '{nombre}' agregado con ID {id}.");
    }

    static void BuscarProducto()
    {
        if (!HayProductos()) return;

        int id = LeerEntero("\nIngresa el ID del producto a buscar: ");

        if (inventario.TryGetValue(id, out Producto? producto))
        {
            Console.WriteLine("\nProducto encontrado:");
            MostrarProducto(producto);
        }
        else
        {
            Console.WriteLine($"No existe ningún producto con el ID {id}.");
        }
    }

    static void ActualizarStock()
    {
        if (!HayProductos()) return;

        int id = LeerEntero("\nIngresa el ID del producto a actualizar: ");

        if (!inventario.TryGetValue(id, out Producto? producto))
        {
            Console.WriteLine($"No existe ningún producto con el ID {id}.");
            return;
        }

        int nuevoStock = LeerEntero($"Stock actual de '{producto.Nombre}': {producto.Stock}. Ingresa el nuevo stock: ");

        if (nuevoStock < 0)
        {
            Console.WriteLine("El stock no puede ser negativo. No se realizó el cambio.");
            return;
        }

        producto.Stock = nuevoStock;
        Console.WriteLine($"Stock de '{producto.Nombre}' actualizado a {producto.Stock} unidades.");
    }

    static void EliminarProducto()
    {
        if (!HayProductos()) return;

        int id = LeerEntero("\nIngresa el ID del producto a eliminar: ");

        bool eliminado = inventario.Remove(id);

        Console.WriteLine(eliminado
            ? $"Producto con ID {id} eliminado correctamente."
            : $"No existe ningún producto con el ID {id}.");
    }

    static void MostrarStockBajo()
    {
        if (!HayProductos()) return;

        Console.WriteLine("\n*** PRODUCTOS CON STOCK BAJO ***");
        bool hayStockBajo = false;

        foreach (Producto producto in inventario.Values)
        {
            if (producto.TieneStockBajo)
            {
                MostrarProducto(producto);
                hayStockBajo = true;
            }
        }

        if (!hayStockBajo)
            Console.WriteLine("Ningún producto tiene stock bajo actualmente.");
    }

    static void MostrarProducto(Producto producto)
    {
        string alerta = producto.TieneStockBajo ? "STOCK BAJO" : "";
        Console.WriteLine($"ID: {producto.Id} | Nombre: {producto.Nombre} | Stock: {producto.Stock} {alerta}");
    }

    static bool HayProductos()
    {
        if (inventario.Count == 0)
        {
            Console.WriteLine("\nNo hay productos registrados en el inventario.");
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