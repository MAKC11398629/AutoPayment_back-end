using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Http.Server
{
    class Response
    {
        private string postData = null;
        private int status;

        private Response(string postData, int status)
        {
            this.postData = postData;
            this.status = status;
        }

        public static Response GetResponse(string data)
        {
                return new Response(data, 200); // 200 - OK

        }
        public int GetStatus()
        {
            return status;
        }

        public void writeData(Stream outputStream)
        {
            Console.WriteLine("Data written to client int thread: {0}, Data Length: {1}",Task.CurrentId, postData.Length);
            using (BinaryWriter bw = new BinaryWriter(outputStream))
            {
                bw.Write(postData);
            }
        }

        private static Response MakeBadRequest()
        {
            return new Response(null, 400); // 400 - Bad Request
        }
    }
}
