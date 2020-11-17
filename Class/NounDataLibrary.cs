using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NounDataLibrary
{
    class NounData
    {
        private Dictionary<string, string> nounDatas;

        private const string Path = "./Data/";

        public NounData()
        {
            nounDatas = new Dictionary<string, string>();
        }

        public void ReadNounsIndex(string fileName)
        {
            try
            {
                const Int32 BufferSize = 128;

                using (var fileStream = File.OpenRead(Path + fileName))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    String[] datas;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        datas = line.Split('|');
                        this.nounDatas.Add(datas[0], datas[1]);
                    }
                    // Process line
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public void ReadNounsData(string fileName)
        {
            try
            {
                const Int32 BufferSize = 128;

                using (var fileStream = File.OpenRead(Path + fileName))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    String[] datas;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        datas = line.Split('|');
                        this.nounDatas.Add(datas[0], datas[1]);
                    }
                    // Process line
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public Dictionary<string, string> GetDatas()
        {
            return this.nounDatas;
        }
    }
}
