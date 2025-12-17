using System;

namespace VotacionKDramas
{
    class Program
    {
         // Vectores y matriz globales
        static string[] kDramas = new string[5];
        static string[] votantes = new string[5];
        static double[] sumaPuntuaciones = new double[5];
        static int[] conteoVotos = new int[5];
        static double[,] votos = new double[5, 5]; // [votante, kdrama]

        static int totalKdramas = 0;
        static int totalVotantes = 0;

        static void Main()
        {
            while (true)
            {
                Console.Clear();

                // Titulo con color
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================");
                Console.WriteLine("    Bienvenido al menu principal");
                Console.WriteLine("====================================");
                Console.ResetColor();

                Console.WriteLine("1. Registro de votantes.");
                Console.WriteLine("2. Registro de k-dramas.");
                Console.WriteLine("3. Votacion.");
                Console.WriteLine("4. Total de votantes.");
                Console.WriteLine("5. Resultados finales.");
                Console.WriteLine("6. Salir.");
                Console.Write("\nElige una opcion: ");

                string opcion = Console.ReadLine()!;

                if (opcion == "1")
                    RegistrarVotantes();
                else if (opcion == "2")
                    RegistrarKDramas();
                else if (opcion == "3")
                    Votar();
                else if (opcion == "4")
                    MostrarTotalVotantes();
                else if (opcion == "5")
                    MostrarResultados();
                else if (opcion == "6")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("¡Gracias por participar!🐻‍❄️");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOpcion no valida.");
                    Console.ResetColor();
                }

                Console.WriteLine("\nPresiona enter para continuar...");
                Console.ReadLine();
            }
        }

        static void RegistrarVotantes()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Registro de Votantes ===");
            Console.ResetColor();

            Console.Write("¿Cuantos votantes habra? (3 a 5): ");
            totalVotantes = Convert.ToInt32(Console.ReadLine());

            if (totalVotantes < 3 || totalVotantes > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: debe ser entre 3 y 5.");
                Console.ResetColor();
                return;
            }

            for (int i = 0; i < totalVotantes; i++)
            {
                Console.Write($"Nombre del votante {i + 1}: ");
                votantes[i] = Console.ReadLine()!;
                // Si no escribe nada, un nombre por defecto
                if (votantes[i] == "")
                    votantes[i] = "Votante " + (i + 1);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVotantes registrados correctamente.");
            Console.ResetColor();
        }

        static void RegistrarKDramas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Registro de K-Dramas ===");
            Console.ResetColor();

            Console.Write("¿Cuantos k-dramas habra? (3 a 5): ");
            totalKdramas = Convert.ToInt32(Console.ReadLine());

            if (totalKdramas < 3 || totalKdramas > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: debe ser entre 3 y 5.");
                Console.ResetColor();
                return;
            }

            for (int i = 0; i < totalKdramas; i++)
            {
                Console.Write($"Nombre del k-drama {i + 1}: ");
                kDramas[i] = Console.ReadLine()!;
                if (kDramas[i] == "")
                    kDramas[i] = "K-Drama " + (i + 1);

                sumaPuntuaciones[i] = 0;
                conteoVotos[i] = 0;
            }

            // Limpiar la matriz de votos
            for (int v = 0; v < 5; v++)
                for (int k = 0; k < 5; k++)
                    votos[v, k] = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nK-Dramas registrados correctamente.");
            Console.ResetColor();
        }

        static void Votar()
        {
            if (totalVotantes == 0 || totalKdramas == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPrimero registra votantes y k-dramas.");
                Console.ResetColor();
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Votacion ===");
            Console.ResetColor();

            for (int v = 0; v < totalVotantes; v++)
            {
                Console.WriteLine($"\nVotando: {votantes[v]}");
                for (int k = 0; k < totalKdramas; k++)
                {
                    Console.Write($"  Calificacion para '{kDramas[k]}' (0-10): ");
                    string calif = Console.ReadLine()!;

                    // Validar que sea un numero
                    if (double.TryParse(calif, out double puntuacion) && puntuacion >= 0 && puntuacion <= 10)
                    {
                        votos[v, k] = puntuacion;
                        sumaPuntuaciones[k] += puntuacion;
                        conteoVotos[k]++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Puntuacion invalida. Se usara 0.");
                        Console.ResetColor();
                        votos[v, k] = 0;
                        sumaPuntuaciones[k] += 0;
                        conteoVotos[k]++;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n¡Votacion completada!");
            Console.ResetColor();
        }

        static void MostrarTotalVotantes()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Total de Votantes ===");
            Console.ResetColor();

            if (totalVotantes == 0)
            {
                Console.WriteLine("No hay votantes registrados.");
            }
            else
            {
                Console.WriteLine($"Total: {totalVotantes} votantes.\n");
                for (int i = 0; i < totalVotantes; i++)
                {
                    Console.WriteLine($"{i + 1}. {votantes[i]}");
                }
            }
        }

        static void MostrarResultados()
        {
            if (totalKdramas == 0 || conteoVotos[0] == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo hay votos aun.");
                Console.ResetColor();
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("================================");
            Console.WriteLine("    RESULTADOS FINALES");
            Console.WriteLine("================================");
            Console.ResetColor();

            // Mostrar tabla
            Console.WriteLine("K-Drama".PadRight(25) + "Puntaje".PadRight(10) + "Votantes");
            Console.WriteLine(new string('-', 45));

            for (int i = 0; i < totalKdramas; i++)
            {
                double promedio = sumaPuntuaciones[i] / conteoVotos[i];
                Console.WriteLine(
                    kDramas[i].PadRight(25) +
                    promedio.ToString("F1").PadRight(10) +
                    conteoVotos[i].ToString()
                );
            }

            // Encontrar el que tiene mas votantes
            int maxVotos = conteoVotos[0];
            int ganador = 0;
            for (int i = 1; i < totalKdramas; i++)
            {
                if (conteoVotos[i] > maxVotos)
                {
                    maxVotos = conteoVotos[i];
                    ganador = i;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n💜Mas votado: {kDramas[ganador]} ({maxVotos} votos)");
            Console.ResetColor();
        }
    }
}
