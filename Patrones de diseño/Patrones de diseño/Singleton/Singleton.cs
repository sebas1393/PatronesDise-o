using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Clases que implementan el patron singleton.
/// </summary>
namespace Patrones_de_diseño.Singleton
{
    /// <summary>
    /// Implementación standart del patron singleton usando un constructor estatico para instanciación de un solo objeto de clase.
    /// Esta implementación asegura el uso de una sola instancia de la clase en todo el sistema, si se crean referencias desde variables, apuntaran
    /// a la misma dirección de memoria.
    /// </summary>
    public sealed class Singleton
    {
        #region Singleton implementation using static constructor
        private static readonly Singleton Instance;
        private static int TotalInstances;
        /*
         * Private constructor is used to prevent
         * creation of instances with the 'new' keyword
         * outside this class.
         */
        private Singleton()
        {
            Console.WriteLine("--Private constructor is called.");
            Console.WriteLine("--Exit now from private constructor.");
        }
        /*
         * A static constructor is used for the following purposes:
         * 1. To initialize any static data
         * 2. To perform a specific action only once
         *
         * The static constructor will be called automatically before:
         * i. You create the first instance; or
         * ii.You refer to any static members in your code.
         *
         */
        // Here is the static constructor
        static Singleton()
        {
            // Printing some messages before you create the instance
            Console.WriteLine("Static constructor is called");
            Instance = new Singleton();
            TotalInstances++;
            Console.WriteLine($"- Singleton instance is created. Number of instances: {TotalInstances}");
            Console.WriteLine("- Exit from static constructor.");
        }
        /*
      * If you like to use expression-bodied read-only
      * property, you can use the following line (C# v6.0 onwards).
      */
        // public static Singleton GetInstance => Instance;
        #endregion
        public static Singleton GetInstance => Instance;
        /* The following line is used to discuss
        the drawback of the approach. */

        public static int MyInt = 25;
    }
    /// <summary>
    /// No declara el constructor estatico, usa directamente la variable para instanciar el objeto.
    /// Buena practica si no se requiere que el constructor haga alguna acción.
    /// </summary>
    public sealed class StaticInitializationSingleton
    {

        private static readonly StaticInitializationSingleton Instance = new StaticInitializationSingleton();
        private StaticInitializationSingleton()
        {

        }
        public static StaticInitializationSingleton GetInstance => Instance;

    }
    /// <summary>
    /// Checkea dos veces si el objeto esta siendo instanciado multiples veces, asegura que solo un hilo pueda instanciarlo y los demas no.
    /// </summary>
    public sealed class DoubleCheckSingleton
    {
        //Se usa volatile para asegurar que la asignación a la variable instancia
        //Halla Finalizado antes de ser accesada.
        private static volatile DoubleCheckSingleton Instance;
        public static object lockObject = new object();
        public static int totalInstances;
        private DoubleCheckSingleton() {
            Console.WriteLine("Se inicio una nueva instancia de el objeto");
            totalInstances++;
        }
        /// <summary>
        /// Variable estatica instanciadora
        /// </summary>
        public static DoubleCheckSingleton GetInstance
        {
            get
            {
                //Si la instancia es nula
                if (Instance == null)
                {
                    //Bloquea el objeto cuando la instancia esta nula.
                    lock (lockObject)
                    {
                        // Doble validación para evitar la multiple instanciación
                        if (Instance == null)
                        {
                            Instance = new DoubleCheckSingleton();
                        }
                    }
                }
                return Instance;
            }
        }


    }
    /// <summary>
    /// Realiza Lock de todos los hilos que ingresan al metodo, si la instancia es null genera una nueva instancia, de lo contrario genera una nueva. 
    /// Muy costoso, prevenir su uso si es una aplicación de uso costoso.
    /// </summary>
    public sealed class DirectLockSingleton {
        private static volatile DirectLockSingleton Instance;
        private static object lockObject = new object();
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DirectLockSingleton() { }

        public static DirectLockSingleton GetInstance {
            get {
                lock (lockObject) {
                    if (Instance == null) {
                        Instance = new DirectLockSingleton();
                    }
                }
                return Instance;
            }
        }
            
    }

    public sealed class delegateSingletonLazyload {
        //Custom Delegate
        delegate delegateSingletonLazyload SingletonDelegateWithNoParameter();
        static SingletonDelegateWithNoParameter Del = MakeSingletonInstance;
        //Built-in Func<out-result> Delegate.
        // Very important for lazy loading: lazy loading can be used with delegates built-in functions.
        static Func<delegateSingletonLazyload> myFuncDelegate = MakeSingletonInstance;
        private static readonly Lazy<delegateSingletonLazyload> Instance = new Lazy<delegateSingletonLazyload>( 
            //Del() // Using Custom Delegate.
            myFuncDelegate() // Built-in Delegate.
            //()=> new delegateSingletonLazyload() //Lamda.
            )
        ;

        private static delegateSingletonLazyload MakeSingletonInstance() {
            return new delegateSingletonLazyload();
        }
        private delegateSingletonLazyload() { }

        public static delegateSingletonLazyload GetInstance => Instance.Value;
    }
}
