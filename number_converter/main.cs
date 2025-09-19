using System;
using System.Linq;
 
namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pick a operation 1=bin-> 2oct-> 3dec-> 4hex->");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1) 
            {
                Console.WriteLine("Convert to what 1-octal 2-dec 3-hex");
                int choice2 = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a binary number");
                string binary = Console.ReadLine();
                if (choice2 == 1) 
                {
                    Console.WriteLine(BintoOct(binary));
                }
                else if (choice2 == 2) 
                {
 
                    //Console.WriteLine(BintoDec(binary));
                }
                else if (choice2 == 3) 
                {
 
                    //Console.WriteLine(BinToHex(binary));
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Convert to what 1-binary 2-octal 3-hex");
                int choice2 = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a decimal number");
                int number = int.Parse(Console.ReadLine());
                if (choice2 == 1)
                {
                    Console.WriteLine(DecToBinary(number));
                }
                else if (choice2 == 2)
                {
                    Console.WriteLine(DecToOctal(number));
                }
                else if (choice2 == 3)
                {
                    Console.WriteLine(DecToHex(number));
                }
 
 
            }
 
 
            }
        static string DecToBinary(int number)
        {
            string binary = "";
            while (number > 0)
            {
                binary = (number % 2) + binary;
                number = number / 2;
            }
            return binary;
        }
        static string DecToOctal(int number)
        {
            string octal = "";
            while (number > 0)
            {
                octal = (number % 8) + octal;
                number = number / 8;
            }
            return octal;
        }
        static string DecToHex(int number) 
        {
            string hex = "";
            while (number > 0)
            {
                if (number % 16 > 9) 
                { 
                    switch (number % 16) 
                    {
                        case 10:
                            hex = "A" + hex;
                            break;
                        case 11:
                            hex = "B" + hex;
                            break;
                        case 12:
                            hex = "C" + hex;
                            break;
                        case 13:
                            hex = "D" + hex;
                            break;
                        case 14:
                            hex = "E" + hex;
                            break;
                        case 15:
                            hex = "F" + hex;
                            break;
                    }
                    number = number / 16;
                    continue;
                }
                else
                {
                    hex = (number % 16) + hex;
                    number = number / 16;
                }
 
 
            }
            return hex;
 
        }
 
        static dynamic BintoOct(string binary) 
        {
            int dec_num = 0;
            double placeholder = 0;
 
            foreach (char bit in binary.Reverse()) 
            {  
                if (bit == '1')
                {
                    dec_num = dec_num + (int)Math.Pow(2, placeholder);
                }
 
                placeholder++;
            }
 
            //return dec_num;
            string octal_num = "";
            while (dec_num > 0)
            {
                octal_num = octal_num + (dec_num % 8);
                dec_num = dec_num / 8;
 
            }
            return octal_num;
 
        }
    }
}
