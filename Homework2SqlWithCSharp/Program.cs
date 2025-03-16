namespace Homework2SqlWithCSharp
{
    internal class Program
    {
        static void Main()
        {
            ElectroStore context = new ElectroStore();



            Console.WriteLine("All of clients : ");
            foreach (var client in context.Clients)
            {
                Console.WriteLine($"Client : {client.Name} - {client.Email} - {client.Birthdate}");
            }
        }
    }
}