namespace Chess
{
    class InputClass
    {
        public static T Input<T>(string inputMessage = "")
        {
            PrintClass.Print(inputMessage, '\0');
            return (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
        }
    }
}
