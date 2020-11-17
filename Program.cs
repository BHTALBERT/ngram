using System;
using System.Collections.Generic;
using NGramLibrary;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {

            int MAXLEVEL = 4;

            if (args.Length == 0)
            {
                Console.WriteLine("Default Command Line Interface \n");
                CommandLine();
                bool valid = false;


                while (!valid)
                {
                    Console.Write("\n\nEnter a comman: ");
                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "1":
                            Console.Write("Please Enter Input File Name: ");
                            string fileName = Console.ReadLine();

                            // Plan A
                            /*for (int i = 2; i < MAXLEVEL; i++)
                            {
                                NGram nGram = new NGram(i);
                                nGram.InitNGramSedd(fileName);
                            }*/

                            //Plan B
                            NGram nGram = new NGram();
                            nGram.DefaultInitNGramSedd(fileName);


                            break;
                        case "2":
                            Console.Write("Program End");
                            valid = true;
                            break;
                        case "3":
                            Console.Write("--HELP");
                            valid = false;
                            break;
                        default:
                            Console.Write("\nError: Invalid input");
                            break;
                    }
                }

            }
            else if (args.Length == 1)
            {
                Console.WriteLine("N-gram Extractor – Version 1.0\n\n");
                // Plan A
                /*for (int i = 2; i < MAXLEVEL; i++)
                {
                    NGram nGram = new NGram(i);
                    nGram.InitNGramSedd(fileName);
                }*/

                //Plan B
                NGram nGram = new NGram();
                nGram.DefaultInitNGramSedd(args[0]);
            }


        }

        private static void CommandLine()
        {
            Console.WriteLine("N-gram Extractor – Version 1.0\n\n");
            Console.Write("+---------------------------------------+\n");
            Console.Write("|              Command Lists            |\n");
            Console.Write("|---------------------------------------|\n");
            Console.Write("|    1. Input Text File Name            |\n");
            Console.Write("|    2. End Program                     |\n");
            Console.Write("|    3. Help                            |\n");
            Console.Write("+---------------------------------------+\n");

        }

    }
}
