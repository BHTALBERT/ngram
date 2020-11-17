using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NounDataLibrary;

namespace NGramLibrary
{
    class NGram
    {
        private const string OUTPUTFILE = "debug.txt";
        private int NLevle { get; set; }
        private const int MAXLEVLE = 4;
        private List<string> NGramSeeds { get; set; }

        private string outPutValue = "";

        private const string Path = "./Test/";
        public NGram()
        {
            NLevle = 2;
        }
        public NGram(int level)
        {
            this.NLevle = level;
        }


        // Plan A
        public void InitNGramSedd(string fileName)
        {
            NounData nounDatas = new NounData();
            nounDatas.ReadNounsIndex("NounsIndex.txt");
            Dictionary<string, string> nounIndex = nounDatas.GetDatas();
            nounDatas.ReadNounsData("NounsData.txt");
            Dictionary<string, string> nounData = nounDatas.GetDatas();

            try
            {
                NGramSeeds = new List<string>();
                const Int32 BufferSize = 128;
                string line;
                string[] datas = null;
                string[] lineValues = null;
                int count = 1;
                string seeds = "";
                using var fileStream = File.OpenRead(Path + fileName + ".txt");
                using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);
                while ((line = streamReader.ReadLine()) != null)
                {
                    datas = line.Split(new char[] { '.', '!', '?' }, StringSplitOptions.None);
                    // txt line
                }

                foreach (var item in datas)
                {
                    if (item != "")
                    {
                        Console.WriteLine("\n" + item.Trim());
                        lineValues = item.Trim().Split(' ');//sentences words start point

                        for (int i = 0; i < lineValues.Length; i++)
                        {
                            if (i <= lineValues.Length - this.NLevle)
                            {

                                if (count == this.NLevle)
                                {
                                    seeds += lineValues[i].Trim();
                                    NGramSeeds.Add(seeds);
                                    seeds = "";
                                    count = 1;

                                }
                                else
                                {
                                    seeds += lineValues[i].Trim() + "_";
                                    count++;
                                }

                            }



                        }
                        SearchWordAndPrint(this.NLevle, this.NGramSeeds, nounIndex, nounData);

                    }


                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // Plan B
        public void DefaultInitNGramSedd(string fileName)
        {
            NounData nounDatas = new NounData();
            nounDatas.ReadNounsIndex("NounsIndex.txt");
            Dictionary<string, string> nounIndex = nounDatas.GetDatas();
            nounDatas.ReadNounsData("NounsData.txt");
            Dictionary<string, string> nounData = nounDatas.GetDatas();

            try
            {
                NGramSeeds = new List<string>();
                const Int32 BufferSize = 128;
                string line;
                string[] datas = null;
                string[] lineValues = null;
                int count = 1;
                string seeds = "";
                using var fileStream = File.OpenRead(Path + fileName);
                using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);
                while ((line = streamReader.ReadLine()) != null)
                {
                    datas = line.Split(new char[] { '.', '!', '?' }, StringSplitOptions.None);
                    // txt line
                }

                foreach (var item in datas)
                {
                    if (item != "")
                    {
                        Console.WriteLine("\n" + item.Trim());
                        this.outPutValue += "\n" + item.Trim() + "\n";
                        lineValues = item.Trim().Split(' ');// sentence words
                                                            //sentences words start point
                        while (this.NLevle <= MAXLEVLE)
                        {
                            for (int i = 0; i < lineValues.Length; i++)
                            {
                                if (i <= lineValues.Length - this.NLevle)
                                {
                                    for (int x = i; x < i + this.NLevle; x++)
                                    {
                                        if (count == this.NLevle)
                                        {
                                            seeds += lineValues[x].Trim();
                                            NGramSeeds.Add(seeds);
                                            seeds = "";
                                            count = 1;

                                        }
                                        else
                                        {
                                            seeds += lineValues[x].Trim() + "_";
                                            count++;
                                        }
                                    }
                                }



                            }
                            SearchWordAndPrint(this.NLevle, this.NGramSeeds, nounIndex, nounData);
                            this.NGramSeeds = new List<string>();
                            this.NLevle++;
                        }

                        this.NLevle = 2;
                    }

                }

                OutPutDebug(this.outPutValue);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void SearchWordAndPrint(int nLevel, List<string> sentence, Dictionary<string, string> nounIndex, Dictionary<string, string> nounData)
        {
            try
            {
                /* string[] dataIndexs = null;*/
                /*string nounDataResult = "";*/

                var sb = new StringBuilder();
                string[] resultArrays = null;
                string result = "";
                foreach (var word in sentence)
                {
                    if (nounIndex.ContainsKey(word.ToLower()))
                    {
                        string[] dataIndexs = nounIndex[word.ToLower()].Split(',');

                        foreach (var index in dataIndexs)
                        {
                            if (nounData.ContainsKey(index.Trim()))
                            {
                                string nounDataResult = nounData[index];
                                //Console.WriteLine(nounDataResult);
                                if (nounDataResult.Contains(';'))
                                {
                                    resultArrays = nounDataResult.Split(';');

                                    for (int i = 0; i < resultArrays.Length; i++)
                                    {
                                        if (i != resultArrays.Length - 1)
                                            result += resultArrays[i] + " << and >> ";
                                        else
                                            result += resultArrays[i];
                                    }
                                }
                                else
                                {
                                    result += nounDataResult;
                                }

                            }
                        }

                    }
                    sb.Append(String.Format("{0} {1}\n", word + ",  ", result));
                    result = "";
                }
                Console.WriteLine("\n" + nLevel + " level N-gram\n");
                Console.WriteLine(sb);

                this.outPutValue += "\n" + nLevel + " level N-gram \n";
                this.outPutValue += "\n" + sb + "\n";

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        private static void OutPutDebug(string outPutValue)
        {
            try
            {
                if (File.Exists(OUTPUTFILE))
                {
                    File.Delete(OUTPUTFILE);
                }

                //Create the file.
                using (FileStream fs = File.Create(OUTPUTFILE))
                {
                    //write file
                    AddText(fs, outPutValue);
                    Console.WriteLine("Created Output File: " + OUTPUTFILE);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail to Create Output File: " + OUTPUTFILE);
                throw ex;
            }




        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }


    }
}
