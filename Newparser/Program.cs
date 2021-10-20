using System;
using System.Collections.Generic;

namespace Newparser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the expression: ");
            string userInputString = Console.ReadLine();
            ParseStringToTokenList tokenList = new ParseStringToTokenList();
            tokenList.GetTokenListFromString(userInputString);

            //Console.WriteLine("List of tokens:");
            //for (int index = 0; index < tokenList._tokenList.Count; index++)
            //{
            //    Console.Write($"{tokenList._tokenList[index]}");
            //}
            //Console.WriteLine("");
            RecursiveDescentParser myparser = new RecursiveDescentParser(tokenList._tokenList);
            try
            {
                double result = myparser.ParseTokenList();
                Console.WriteLine("The result is {0:0.00}", result);
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Error in expression, try again");
                Console.ReadLine();
            }
            

        }
    }
}
