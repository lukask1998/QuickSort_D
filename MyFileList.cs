using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QuickSort_D
{
    class MyFileList : DataList
    {

        int prevNode;
        int currentNode;
        int nextNode;

        public MyFileList(string filename, int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename))
                File.Delete(filename);

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write(rand.NextDouble());
                        writer.Write((j + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public FileStream fs { get; set; }

        public override double Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }

        public override double Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            currentNode = nextNode;
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }

        public override double Value(int Nr)
        {
            int i = 0;
            double result = Head();

            while (i != Nr)
            {
                i++;
                result = Next();

            }

            return result;

        }

        public override void Swap(int a, int b)
        {
            Byte[] data;

            double x = Value(b);
            double y = Value(a);

            Value(a);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(x);
            fs.Write(data, 0, 8);

            Value(b);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(y);
            fs.Write(data, 0, 8);
        }

    }
}
