namespace MenuCreation
{
    public delegate void MyDelegate();


    class Program
    {
    public static bool MenuGenerator(string[] menuOptions, MyDelegate[] menuOperations)
        {
            int optionChosen=0;
            bool menuIsCreated=true;
            if (menuOptions.Length != menuOperations.Length)
            {
                Console.WriteLine("The menu has to have the same amount of options and operations, thus it cannot be created");
                return false;
            }
            else if (menuOptions == null || menuOperations == null)
            {
                Console.WriteLine("Either the options or the operations are null! Menu cannot be created");
                return false;
            }
            else { 
            try
            {
                    
                    do
                    {
                        Console.WriteLine("Choose one of the following options:\n");
                        for (int i = 0; i < menuOptions.Length; i++)
                        {
                            Console.WriteLine("{0}) {1}\n", i + 1, menuOptions[i]);
                        }
                        Console.WriteLine($"{menuOptions.Length + 1}) Exit");
                        menuIsCreated = int.TryParse(Console.ReadLine(), out optionChosen);
                        if (optionChosen > menuOptions.Length + 1)
                        {
                            Console.WriteLine("Error, the option must be one of the previously mentioned!");
                        }
                        else if(optionChosen<=menuOptions.Length &&optionChosen>0)
                        {
                            //como es un _vector_ de delegados, accedemos a la posicion de cada delegado
                            //y LUEGO lo invocamos
                            menuOperations[optionChosen-1].Invoke();// = new MyDelegate[optionChosen];
                        }
                        
                    } while (optionChosen != menuOptions.Length + 1);
                    return menuIsCreated;
            }
            catch (Exception)
            {
                Console.WriteLine("There was some problem creating the menu");
                return false;
                //menuIsCreated = false;
            }
            }
            

        }
        //static void f1()
        //{
        //    Console.WriteLine("A");
        //}
        //static void f2()
        //{
        //    Console.WriteLine("B");
        //}
        //static void f3()
        //{
        //    Console.WriteLine("C");
        //}
        static void Main(string[] args)
        {
            MenuGenerator(new string[] { "Op1", "Op2", "Op3" },
            new MyDelegate[] { () => Console.WriteLine("Brooklyn99"), () => Console.WriteLine("BrakingBad"), () => Console.WriteLine("TheGoodPlace") });
            Console.WriteLine("Bye!");
            Console.ReadKey();
        }

    }
}