using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVideoClub
{
    class Program
    {
        static void Main(string[] args)
        {
        IngresoOpcionMenu:
            DibujarMenu();
            int opcionMenuPrincipal = ProcesarOpcionMenuPrincipal();

            switch (opcionMenuPrincipal)
            {
                case 1:
                    AgregarCliente();
                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;


                case 2:

                    Console.Clear();
                    AgregarPelicula();



                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;


                case 3:

                    Console.Clear();
                    AgregarCopia();


                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;


                case 4:

                    Console.Clear();
                    RegistrarAlquiler();


                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;


                case 5:

                    Console.Clear();
                    MarcarEntrega();

                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;


                case 6:

                    Console.Clear();
                    MarcarDeterioro();


                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;

                case 7:

                    Console.Clear();
                    DibujarMenuConsultas();
                    string opcionMenuConsultas = ProcesarOpcionMenuConsulta().ToUpper();

                    switch (opcionMenuConsultas)
                    {
                        case "A":
                            CopiasEnStock();
                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;


                        case "B":

                            Console.Clear();
                            ListarClientesRegistrados();



                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;


                        case "C":

                            Console.Clear();
                            PeliculasDisponiblesAlquiler();


                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;


                        case "D":

                            Console.Clear();
                            HistorialAlquileres();


                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;


                        case "E":

                            Console.Clear();
                            HistorialAlquileresPorCliente();

                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;


                        case "F":

                            Console.Clear();
                            AlquileresActivos();


                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;

                        case "G":

                            Console.Clear();
                            AlquileresAtrasados();



                            Console.ReadKey();
                            Console.Clear();

                            goto IngresoOpcionMenu;
                        case "0":
                            Console.Clear();
                            goto IngresoOpcionMenu;
                        default:
                            goto IngresoOpcionMenu;

                    }


                    Console.ReadKey();
                    Console.Clear();

                    goto IngresoOpcionMenu;

                default:
                    goto IngresoOpcionMenu;

            }



        }

        private static int ProcesarOpcionMenuPrincipal()
        {
            int opcion = -1;
            string inputUsuario = string.Empty;

            do
            {
                Console.WriteLine("Ingrese la opcion deseada");

                inputUsuario = Console.ReadLine();

                if (int.TryParse(inputUsuario, out opcion) && opcion >= 1 && opcion <= 7)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    DibujarMenu();
                }

            } while (true);

            return opcion;
        }
        private static void DibujarMenu()
        {
            Console.WriteLine("1. Registrar cliente");
            Console.WriteLine("2. Alta de película");
            Console.WriteLine("3. Ingresar copia de película");
            Console.WriteLine("4. Registrar alquiler");
            Console.WriteLine("5. Marcar entrega");
            Console.WriteLine("6. Ingresar deterioro");
            Console.WriteLine("7. Consultas");

            Console.WriteLine("");

        }

        private static void AgregarCliente()
        {
        AgregarCliente:
            Console.WriteLine("Ingrese el nombre.");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido.");
            string apellido = Console.ReadLine();
            Console.WriteLine("Ingrese la dirección.");
            string direccion = Console.ReadLine();
            Console.WriteLine("Ingrese el documento de identidad.");
            string documento = Console.ReadLine();

            using (var context = new PRG3_EF_PR1())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var cliente = context.Clientes.Any(persona => persona.DocumentoIdentidad == documento);
                        if (cliente)
                        {
                            Console.WriteLine("El cliente ya se encuentra registrado. Pulse una tecla para continuar.");
                            Console.ReadKey();
                            goto AgregarCliente;
                        }
                        Console.WriteLine("Ingrese el correo.");
                        string correo = Console.ReadLine();
                        Console.WriteLine("Ingrese el teléfono.");
                        string telefono = Console.ReadLine();
                        Clientes nuevoCliente = new Clientes()
                        {
                            Nombre = nombre,
                            Apellido = apellido,
                            Direccion = direccion,
                            DocumentoIdentidad = documento,
                            Correo = correo,
                            Telefono = telefono
                        };

                        context.Clientes.Add(nuevoCliente);

                        context.SaveChanges();

                        transaction.Commit();

                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }


        }
        private static void AgregarPelicula()
        {
        AgregarPelicula:
            Console.Clear();
            Console.WriteLine("Ingrese el título.");
            string titulo = Console.ReadLine().ToLower();


            using (var context = new PRG3_EF_PR1())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var pelicula = context.Peliculas.Any(peli => peli.Titulo.ToLower() == titulo);
                        if (pelicula)
                        {
                            Console.WriteLine("La película ya se encuentra ingresada. Pulse una tecla para continuar.");
                            Console.ReadKey();
                            goto AgregarPelicula;
                        }
                        Console.WriteLine("Ingrese el año.");
                        int anio = int.Parse(Console.ReadLine());
                    PedirCalificacion:
                        Console.WriteLine("Ingrese la calificación.");
                        int calificacion = int.Parse(Console.ReadLine());
                        if (calificacion < 1 || calificacion > 10)
                        {
                            Console.WriteLine("La calificación debe ser del 1 al 10. Pulse una tecla para continuar.");
                            Console.ReadKey();
                            goto PedirCalificacion;
                        }
                        Peliculas nuevaPelicula = new Peliculas()
                        {
                            Titulo = titulo,
                            Anio = anio,
                            Calificacion = calificacion
                        };

                        context.Peliculas.Add(nuevaPelicula);

                        context.SaveChanges();

                        transaction.Commit();

                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }


        }

        private static void AgregarCopia()
        {
        AgregarCopia:
            Console.WriteLine("Ingrese el ID de la película.");
            long idPelicula = long.Parse(Console.ReadLine());


            using (var context = new PRG3_EF_PR1())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var copia = context.Copias.Any(copy => copy.Id == idPelicula);
                        if (copia)
                        {
                            Console.WriteLine("La copia ya se encuentra ingresada. Pulse una tecla para continuar.");
                            Console.ReadKey();
                            goto AgregarCopia;
                        }
                        Console.WriteLine("Ingrese el formato.");
                        string formato = Console.ReadLine();
                        Console.WriteLine("Ingrese el precio del alquiler.");
                        double precioAlquiler = double.Parse(Console.ReadLine());
                        Copias nuevaCopia = new Copias()
                        {
                            IdPelicula = idPelicula,
                            Formato = formato,
                            PrecioAlquiler = precioAlquiler
                        };

                        context.Copias.Add(nuevaCopia);

                        context.SaveChanges();

                        transaction.Commit();

                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }


        }

        private static void RegistrarAlquiler()
        {
        RegistrarAlquiler:
            Console.WriteLine("Ingrese el ID de la copia.");
            long idCopia = long.Parse(Console.ReadLine());



            using (var context = new PRG3_EF_PR1())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var chequearAlquiler = context.Alquileres.FirstOrDefault(copia => copia.IdCopia == idCopia && copia.FechaEntregada == null);

                        if (chequearAlquiler != null)
                        {
                            Console.WriteLine("La copia ya se encuentra alquilada.");
                            goto RegistrarAlquiler;
                        }

                        Console.WriteLine("Ingrese el ID del cliente.");
                        long idCliente = long.Parse(Console.ReadLine());
                        DateTime fechaAlquiler = DateTime.Now;
                        DateTime fechaTope = fechaAlquiler.AddDays(3);

                        Alquileres nuevoAlquiler = new Alquileres()
                        {
                            IdCopia = idCopia,
                            IdCliente = idCliente,
                            FechaAlquiler = fechaAlquiler,
                            FechaTope = fechaTope
                        };

                        context.Alquileres.Add(nuevoAlquiler);

                        context.SaveChanges();

                        transaction.Commit();

                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }


        }

        private static void MarcarEntrega()
        {
        MarcarEntrega:
            Console.WriteLine("Ingrese el ID de alquiler.");
            long idAlquiler = long.Parse(Console.ReadLine());

            using (var context = new PRG3_EF_PR1())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var chequearEntrega = context.Alquileres.Any(alquiler => alquiler.Id == idAlquiler && alquiler.FechaEntregada != null);
                        if (chequearEntrega)
                        {
                            Console.WriteLine("El alquiler ya fue marcado como entregado.");
                            goto MarcarEntrega;
                        }

                        var alquilerParaMarcar = context.Alquileres.FirstOrDefault(alquiler => alquiler.Id == idAlquiler);

                        alquilerParaMarcar.FechaEntregada = DateTime.Now;
                        context.SaveChanges();

                        transaction.Commit();

                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }


        }
        private static void MarcarDeterioro()
        {

            Console.WriteLine("Ingrese el ID de la copia.");
            long idCopia = long.Parse(Console.ReadLine());

            using (var context = new PRG3_EF_PR1())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var copiaDeteriorada = context.Copias.FirstOrDefault(copia => copia.Id == idCopia);

                        copiaDeteriorada.Deteriorada = true;
                        context.SaveChanges();

                        transaction.Commit();

                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }


        }

        private static void DibujarMenuConsultas()
        {
            Console.WriteLine("A. Ver copias en stock.");
            Console.WriteLine("B. Listar clientes registrados.");
            Console.WriteLine("C. Películas disponibles para alquilar.");
            Console.WriteLine("D. Historial de alquileres.");
            Console.WriteLine("E. Historial de alquileres por cliente.");
            Console.WriteLine("F. Mostrar alquileres activos.");
            Console.WriteLine("G. Ver alquileres atrasados en la entrega.");
            Console.WriteLine("0. Volver al menú principal.");

            Console.WriteLine("");

        }
        private static string ProcesarOpcionMenuConsulta()
        {

            string inputUsuario = string.Empty;
            var menu = new List<string> { "a", "b", "c", "d", "e", "f", "g", "0" };
            do
            {
                Console.WriteLine("Ingrese la opcion deseada");

                inputUsuario = Console.ReadLine().ToLower();
                var opcion = menu.Any(letra => letra == inputUsuario);

                if (opcion)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    DibujarMenuConsultas();
                }

            } while (true);

            return inputUsuario;
        }

        private static void CopiasEnStock()
        {
            using (var context = new PRG3_EF_PR1())
            {


                var stock = context.Copias.ToList();
                Console.WriteLine($"Total de copias: {stock.Count()}");

                foreach (var copia in stock)
                {
                    Console.WriteLine($"ID:{copia.Id} Pelicula: {copia.Peliculas.Titulo} ({copia.Peliculas.Anio}) Calificación: {copia.Peliculas.Calificacion}");
                }


            }
        }

        private static void ListarClientesRegistrados()
        {
            using (var context = new PRG3_EF_PR1())
            {


                var clientes = context.Clientes.ToList();

                Console.WriteLine($"Total de clientes: {clientes.Count()}");
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"ID:{cliente.Id} Nombre: {cliente.Nombre} {cliente.Apellido} Doc: {cliente.DocumentoIdentidad} Dir: {cliente.Direccion} Tel: {cliente.Telefono} Mail: {cliente.Correo}");
                }

            }
        }

        private static void PeliculasDisponiblesAlquiler()
        {
            using (var context = new PRG3_EF_PR1())
            {

                // Lista de todas las copias
                var stock = context.Copias.ToList();

                // Lista de alquileres sin entregar
                var alquileresPendientes = context.Alquileres.Where(copia => copia.FechaEntregada == null).ToList();

                // Recorro stock y pregunto si cada copia está en la lista de alquileres pendientes de entrega.
                foreach (var copia in stock)
                {
                    var alquilada = alquileresPendientes.Any(c => c.IdCopia == copia.Id);
                    if (!alquilada)
                    {
                        Console.WriteLine($"ID:{copia.Id} Pelicula: {copia.Peliculas.Titulo} ({copia.Peliculas.Anio}) Calificación: {copia.Peliculas.Calificacion}");
                    }
                }


            }
        }

        private static void HistorialAlquileres()
        {
            using (var context = new PRG3_EF_PR1())
            {

                // Lista de todos los alquileres
                var alquileres = context.Alquileres.ToList();


                foreach (var alquiler in alquileres)
                {
                    Console.WriteLine($"Película: {alquiler.Copias.Peliculas.Titulo} Formato: {alquiler.Copias.Formato} Cliente: {alquiler.Clientes.Nombre} {alquiler.Clientes.Apellido} Tel: {alquiler.Clientes.Telefono} Mail: {alquiler.Clientes.Correo} ID: {alquiler.Clientes.Id}");
                }


            }
        }

        private static void HistorialAlquileresPorCliente()
        {
            Console.WriteLine("Ingrese el ID del cliente.");
            long idCliente = long.Parse(Console.ReadLine());
            using (var context = new PRG3_EF_PR1())
            {


                // Lista de alquileres sin entregar
                var alquilerPorCliente = context.Alquileres.Where(cliente => cliente.IdCliente == idCliente).OrderBy(copia => copia.FechaEntregada).ToList();

                // Recorro stock y pregunto si cada copia está en la lista de alquileres pendientes de entrega.
                foreach (var alquiler in alquilerPorCliente)
                {

                    Console.WriteLine($"ID:{alquiler.Id} Pelicula: {alquiler.Copias.Peliculas.Titulo} Fecha Alquiler: {alquiler.FechaAlquiler.ToString()} Fecha Tope: {alquiler.FechaTope} Estado: {ActivoOEntregado(alquiler.FechaEntregada)}");

                }


            }
        }

        private static void AlquileresActivos()
        {
            using (var context = new PRG3_EF_PR1())
            {

                // Lista de todos los alquileres activos
                var alquileresActivos = context.Alquileres.Where(fecha => fecha.FechaEntregada == null).ToList();


                foreach (var alquiler in alquileresActivos)
                {
                    Console.WriteLine($"Película: {alquiler.Copias.Peliculas.Titulo} Formato: {alquiler.Copias.Formato} Cliente: {alquiler.Clientes.Nombre} {alquiler.Clientes.Apellido} Tel: {alquiler.Clientes.Telefono} Mail: {alquiler.Clientes.Correo} ID: {alquiler.Clientes.Id}");
                }


            }
        }

        private static void AlquileresAtrasados()
        {
            using (var context = new PRG3_EF_PR1())
            {
                var fechaActual = DateTime.Now;
                // Lista de todos los alquileres activos
                var alquileresAtrasados = context.Alquileres.Where(fecha => fecha.FechaTope < fechaActual && fecha.FechaEntregada == null).ToList();


                foreach (var alquiler in alquileresAtrasados)
                {
                    Console.WriteLine($"Película: {alquiler.Copias.Peliculas.Titulo} Formato: {alquiler.Copias.Formato} Cliente: {alquiler.Clientes.Nombre} {alquiler.Clientes.Apellido} Tel: {alquiler.Clientes.Telefono} Mail: {alquiler.Clientes.Correo} ID: {alquiler.Clientes.Id}");
                }


            }
        }

        private static string ActivoOEntregado(DateTime? fechaEntrega)
        {
            if (fechaEntrega == null)
            {
                return "ACTIVO";
            }

            return "DEVUELTA";
        }

    }
}
