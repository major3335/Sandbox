public class SingletonService
{
    private static object _lockObj = new object();
    private static SingletonService _Instance;

    private SingletonService()
    { }

    public static SingletonService GetInstance()
    {
        if (_Instance == null)
        {
            // obtain lock so no other threads can access it until the current thread is done
            lock (_lockObj)
            {
                // is it still null? another thread may have initialized _Instance before
                // the current thread obtained the lock
                if (_Instance == null)
                {
                    _Instance = new SingletonService();
                }
            }
        }

        return _Instance;
    }
}
