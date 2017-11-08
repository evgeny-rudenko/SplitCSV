using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace SplitCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() ==0 )
            {
                Console.WriteLine("Пропущено имя файла для деления на строки");
                return;
            }
            int lines = Properties.Settings.Default.Lines;

            String fileName = args[0].ToString();
            int LineCounter = 0;
            int fileCunter = 0;
            String RemainsFileName = Properties.Settings.Default.RemainsFileName;
            string currentFileName;

            currentFileName = RemainsFileName + fileCunter.ToString() + ".sst";
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding(1251) , true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (LineCounter==0)
                    {
                        fileCunter++;
                        currentFileName = RemainsFileName + fileCunter.ToString() + ".sst";
                        if (File.Exists(Path.Combine(Environment.CurrentDirectory, currentFileName) )==true)
                        {
                            File.Delete(Path.Combine(Environment.CurrentDirectory, currentFileName));

                        }

                        AddLineTofile("[HEADER]", currentFileName);
                        AddLineTofile(fileCunter.ToString()+ ";08.11.17;5555.5;ПОСТАВКА;0;0;РУБЛЬ;;", currentFileName);
                        AddLineTofile("[BODY]" , currentFileName);

                    }

                    AddLineTofile(line, currentFileName);
                    LineCounter++;
                    if(LineCounter>= Properties.Settings.Default.Lines)
                    {
                        LineCounter = 0;
                    }
                }
      
            }



        }
        private static void AddLineTofile(String LineToAdd, string fileName)
        {
            DateTime dt = DateTime.Now;

            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\" + fileName, true, Encoding.GetEncoding(1251)))
            
            {

              
                 sw.WriteLine(LineToAdd);

            }

        }

    }
}
