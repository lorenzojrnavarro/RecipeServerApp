
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

public static class NonBlockingConsole
{
    private static BlockingCollection<string> m_Queue = new BlockingCollection<string>();
    private static Thread thread;

    public static void Init()
    {
        thread = new Thread(
          () =>
          {
              while (true) Console.WriteLine(m_Queue.Take());
          });
        thread.IsBackground = true;
        thread.Start();
    }

    public static void Destroy()
    {
        thread.Abort();
    }

    public static void WriteLine(string value)
    {
        m_Queue.Add(value);
    }

}