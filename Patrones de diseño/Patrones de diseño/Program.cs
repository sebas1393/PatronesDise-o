using Patrones_de_diseño.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Patrones_de_diseño
{
    class Program
    {

        static string[] patrones = new string[] { "SINGLETON", "PROTOTYPE", "FACTORY" };
        static async Task Main(string[] args)
        {
            bool flag = true;
            Console.WriteLine("*************************************************************************************************");
            Console.WriteLine("BIENVENIDO AL PROGRAMA DE PATRONES DE SEBAS!!");

            //Menu
            do
            {
                
                // Pinta las opciones
                int cont = 1;
                Console.WriteLine("Elija la opción deseada:");
                foreach (string patron in patrones)
                {
                    Console.WriteLine($"    {cont}- {patron}.");
                    cont++;
                }

                Console.WriteLine("    9- Salir del sistema.") ;

                //Captura la opción del usuario
                int opcion = 0;
                _ = int.TryParse(Console.ReadLine(), out opcion);

                //Valida la opción
                if (opcion == 9) {
                    
                    flag = false;
                    continue;
                }
                if (opcion == 0 || opcion > patrones.Length)
                {
                    Console.WriteLine("Elija una opción valida");
                    continue;
                }
                
                switch (opcion)
                {
                    ///Patron Singleton
                    case 1:
                        await menuSingleton();
                        break;
                    //Patron Prototype
                    case 2:
                        ImplementsPrototype();
                        break;
                    default:
                        Console.WriteLine("Patron no implementado");
                        break;
                }

            } while (flag);
            Console.WriteLine("Hasta luego.");
            Console.ReadKey();
        }

        #region Singleton implementations
        private static async Task menuSingleton()
        {
            int singletonType = 0;

            //Menu singleton
            while (singletonType == 0)
            {

                Console.WriteLine("*******************Elija una implementación del singleton*********************:");
                //Standart para todos los singletons (Sinc Aplications).
                Console.WriteLine(" 1- Singleton con constructor estatico.");
                
                Console.WriteLine(" 2- Problemas del singleton, perdida de control de instanciación.");
                
                Console.WriteLine(" 3- Singleton con inicialización estática.");
                
                //Variante para Multithreading
                Console.WriteLine(" Singleton para multithreading:");
                Console.WriteLine(" Consideraciones: El Lock siempre genera un alto procesamiento, evitar el uso excesivo, es mejor el doublecheck en los singletons");
                Console.WriteLine("     4- Doble validación y hacer lock del objeto de la instancia para evitar generación de varias instancias \n" +
                                  "        En un ambiente multihilos");
                Console.WriteLine("     5- Lock antes de validar el objeto, no se recomienta esta práctica ya que el uso de la \n" +
                                  "        memoria es más costoso");

                Console.WriteLine(" 6- Singleton con delegados");
                //Lee la opción del usuario
                _ = int.TryParse(Console.ReadLine(), out singletonType);

                //Valida opción
                if (singletonType == 0 || singletonType > 10)
                {
                    Console.WriteLine("elija una opción valida");
                    singletonType = 0;
                    continue;
                }
                //Implementa el singleton segun la opción
                switch (singletonType)
                {
                    //Constructor estatico.
                    case 1:
                        ImplementsSingleton();
                        break;
                    //Perdida de manejo en la construcción del objeto
                    case 2:
                        ImplementsSingletonIssue();
                        break;
                    //Singleton del objeto en la variable estatica
                    case 3:
                        SingletonStaticInitialization();
                        break;
                    //Doble checkeo de la variable y lock dentro de la primer validación
                    case 4:
                        for (int i = 0; i < 3; i++)
                            await SingletonDoubleCheck();
                        break;
                    //Lock Directo, no usar para aplicaciones con mucho tráfico, usar opción numero 4.
                    case 5:
                        List<DirectLockSingleton> instances = new List<DirectLockSingleton>();
                        for (int i = 0; i < 3; i++)
                            instances.Add(await SingleLockSingleton());
                        Console.WriteLine(instances[0].Name == instances[1].Name && 
                            instances[1].Name == instances[2].Name ? "Se crearon varios objetos de la misma instancia.": "Hay Varias instacias.") ;                        

                        
                        break;
                }

            }
        }

        private static void ImplementsSingletonIssue()
        {
            Console.WriteLine($"El valor de mi entero es: {Singleton.Singleton.MyInt}");
        }

        private static void SingletonStaticInitialization()
        {
            Singleton.StaticInitializationSingleton inicializacionEstatica = Singleton.StaticInitializationSingleton.GetInstance;

            Singleton.StaticInitializationSingleton instanciados = Singleton.StaticInitializationSingleton.GetInstance;

            if (inicializacionEstatica.Equals(instanciados))
            {
                Console.WriteLine("la instancia uno y dos son la misma instancia ");
            }
        }

        private static async Task SingletonDoubleCheck() {
            // Usa la instanciación con doble validación
            DoubleCheckSingleton instance = DoubleCheckSingleton.GetInstance;
            Console.WriteLine($"Cantidad de instancias iniciadas {DoubleCheckSingleton.totalInstances}");


        }
        private static void ImplementsSingleton()
        {
            Console.WriteLine("***Singleton Pattern demonstration************");

            //No se puede hacer una instanciación new debido a que el constructor es privado.
            //Singleton.Carrito carrito = new Singleton.Carrito(); //Esto fallaria.

            //Tengo que acceder a la instancia, esta propiedad es unica y no puede ser modificada desde afuera, el control lo tiene la clase misma.
            Singleton.Singleton carritoPrimeraInstancia = Singleton.Singleton.GetInstance;

            //Segunda instancia, al acceder a la misma propiedad estatica, me asegura que accedo a la misma instancia que la primera.
            Singleton.Singleton carritoSegundaInstancia = Singleton.Singleton.GetInstance;

            //Comprobación de que los objetos apuntan a la misma dirección de memoria.
            if (carritoPrimeraInstancia.Equals(carritoSegundaInstancia))
            {
                Console.WriteLine("La primera y segunda instancia son la misma.");

            }
            else
            {
                Console.WriteLine("Existen diferente instancias");
            }
        }

        private static async Task<DirectLockSingleton> SingleLockSingleton() {
            DirectLockSingleton instance = DirectLockSingleton.GetInstance;
            Random ram = new Random();
            int number = ram.Next(0, 5);
            instance.Name = $"instance {number}"; 
            return instance;
        }
        #endregion
        private static void ImplementsPrototype()
        {

            //Por implementar.
            Console.WriteLine("Por implementar");

        }


    }
}
