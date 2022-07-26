namespace Chess
{
    class PrintClass
    {
        public static void Print<T>(T message,char end = '\n',int pause=0)
        {
            Console.Write(message + end.ToString());
            Thread.Sleep(pause);
        }
    }
}
