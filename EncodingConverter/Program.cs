using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solution EncodingConverter");
            string[] extensions = new[] { ".cs", ".aspx" };
            var solutionDirectory = @"L:\SourceCodes\Mozart-1.0\code";
            foreach (var f in new DirectoryInfo(solutionDirectory).GetFiles("*", SearchOption.AllDirectories)
                .Where(f => extensions.Contains(f.Extension.ToLower())))
            {
                string s = readFileAsUtf8(f.FullName);
                File.WriteAllText(f.FullName, s, new UTF8Encoding(true));
            }            
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public static String readFileAsUtf8(string fileName)
        {
            Encoding encoding = Encoding.Default;
            String original = String.Empty;

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                original = sr.ReadToEnd();
                encoding = sr.CurrentEncoding;
                sr.Close();
            }

            if (encoding == Encoding.UTF8)
                return original;

            byte[] encBytes = encoding.GetBytes(original);
            byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, encBytes);
            return Encoding.UTF8.GetString(utf8Bytes);
        }
    }
}
