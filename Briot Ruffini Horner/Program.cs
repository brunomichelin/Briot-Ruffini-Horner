using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briot_Ruffini_Horner
{
    class Program
    {
        static double[] function;
        static double xK;
        static double precisao;
        static int casas;

        static void Main(string[] args)
        {
            BuildFunction();

            Console.Write("Quer arredondar para quantas casas depois da vírgula? (Se não quer arredondar escreva '-1'): ");
            casas = Convert.ToInt32(Console.ReadLine());

            Console.Write("Quer que o Briot Ruffini procure o primeiro xK? (Y/N): ");

            if (Console.ReadLine().ToUpper() == "Y")
                BriotRuffini();
            else
            {
                Console.Write("Escreva o primeiro xK: ");
                xK = Convert.ToDouble(Console.ReadLine());
            }

            BriotRuffiniHorner();
            Console.Write("\n\nResposta: " + xK);
            Console.ReadLine();
        }

        private static void BriotRuffini()
        {
            Console.Write("Diferença de xK para o Briot Ruffini (exemplo: 0,5): ");
            double bR = Convert.ToDouble(Console.ReadLine());
            
            double iAntigo = 0;

            if (function[function.Length - 1] < 0)
                for (double i = 0; ; i += bR)
                {
                    double value = function[0];

                    for (int coefSoma = 1; coefSoma < function.Length; coefSoma++)
                        value = (value * i) + function[coefSoma];
                    if (value > 0)
                    {
                        if (casas != -1)
                            xK = Math.Round(((double)(i + (i - 1)) / 2), casas, MidpointRounding.AwayFromZero);
                        else
                            xK = ((double)(i + iAntigo) / 2);
                        break;
                    }

                    iAntigo = i;
                }
        }

        private static void BriotRuffiniHorner()
        {
            double value = function[0], value2;
            
            for (int i = 1; ((value < 0) ? value * -1 : value) > precisao; i++)
            {
                value = function[0];
                value2 = function[0];

                Console.WriteLine("\nXK = " + xK + "\n");

                for (int coefSoma = 1; coefSoma < function.Length; coefSoma++)
                {
                    if (casas != -1)
                        value = Math.Round((value * xK) + function[coefSoma], casas, MidpointRounding.AwayFromZero);
                    else
                        value = (value * xK) + function[coefSoma];

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(value.ToString() + "|");

                    if (coefSoma + 1 < function.Length)
                    {
                        if (casas != -1)
                            value2 = Math.Round((value2 * xK) + value, casas, MidpointRounding.AwayFromZero);
                        else
                            value2 = (value2 * xK) + value;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(value2.ToString() + "|");
                    }
                }

                Console.WriteLine();

                if (casas != -1)
                    xK = Math.Round((xK - (value / value2)), casas, MidpointRounding.AwayFromZero);
                else
                    xK -= (value / value2);
            }
        }

        private static void BuildFunction()
        {
            Console.WriteLine("Escreva os coeficientes do polinômio separados por ';' (exemplo: 1,5;4;6,7...): ");
            string leitura = Console.ReadLine();

            function = new double[leitura.Split(';').Length];

            for (int i = 0; i < leitura.Split(';').Length; i++)
                function[i] = Convert.ToDouble(leitura.Split(';')[i]);

            Console.WriteLine("Escreva a Precisão (Exemplo: 0,1):");
            precisao = Convert.ToDouble(Console.ReadLine());
        }
    }
}
