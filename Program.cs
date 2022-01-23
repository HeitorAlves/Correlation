using SpearmanCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SpearmanCorrelation
{
    class Program
    {
        static void Main()
        {
            var dadosWashigton = LeitorCSV.LerCSV<WashingtonBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\hour.csv");
            //dados.ComputaMatrixCorrelacaoSpearman<WashingtonBaseMapper>();
            dadosWashigton.ComputaMatrixCorrelacaoPearson<WashingtonBaseMapper>();
            Console.WriteLine("--------------------------------------------\n");
            var dadosSeoul = LeitorCSV.LerCSV<SeulBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\SeoulBikeDataTransformed.csv");
            dadosSeoul.ComputaMatrixCorrelacaoPearson<SeulBaseMapper>();

            Console.ReadLine();
        }
    }
}
