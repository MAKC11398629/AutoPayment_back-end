using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Http.Server
{
    internal class AsyncHttpServer : IDisposable
    {

        public AsyncHttpServer()
        {
            listener = new HttpListener();
        }

        public void Start(string prefix)
        {
            Console.WriteLine("Server started at prefix " + prefix);
            lock (listener)
            {
                if (!isRunning)
                {
                    listener.Prefixes.Clear();
                    listener.Prefixes.Add(prefix);
                    listener.Start();

                    listenerThread = new Thread(Listen)
                    {
                        IsBackground = true,
                        Priority = ThreadPriority.Highest
                    };
                    listenerThread.Start();

                    isRunning = true;
                }
            }
        }

        public void Stop()
        {
            lock (listener)
            {
                if (!isRunning)
                    return;

                listener.Stop();

                listenerThread.Abort();
                listenerThread.Join();

                isRunning = false;
            }
        }

        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;

            Stop();

            listener.Close();
        }

        private void Listen()
        {
            while (true)
            {
                try
                {
                    if (listener.IsListening)
                    {
                        var context = listener.GetContext();
                        Task.Run(() => HandleContextAsync(context));
                    }
                    else Thread.Sleep(0);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception error)
                {
                    Console.WriteLine("error: {0}", error.Message.ToString());
                }
            }
        }

        private async Task HandleContextAsync(HttpListenerContext listenerContext)
        {
        // TODO: implement request handling

            Console.WriteLine("has new request!");

            Request request = Request.Get(listenerContext.Request.Url.ToString());

            Response response = Response.GetResponse(request.GetRequestString());

            listenerContext.Response.StatusCode = response.GetStatus();


            response.writeData(listenerContext.Response.OutputStream);
            

        }

        private readonly HttpListener listener;

        private Thread listenerThread;
        private bool disposed;
        private volatile bool isRunning;
    }
}