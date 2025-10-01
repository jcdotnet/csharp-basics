namespace ClassLibrary
{
    public ref struct Computer // : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose method called!");
        }

    }
}
