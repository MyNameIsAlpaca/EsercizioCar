namespace EsercizioCar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {


                Menu menu = new Menu();

                menu.openMenu();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}