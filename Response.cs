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
            Console.WriteLine("The image has been written to client");
            String path = @"C:\1\Leh_projs\AutoPayment_back-end\image.jpg";
            FileStream ss = new FileStream(path, FileMode.Open);
            using (BinaryWriter bw = new BinaryWriter(outputStream))
            {
                byte[] array;
                using (BinaryReader br = new BinaryReader(ss))
                {
                    array = br.ReadBytes( (int)ss.Length);
                }
                    bw.Write(array);
            }
        }

        private static Response MakeBadRequest()
        {
            return new Response(null, 400); // 400 - Bad Request
        }
    }
}
