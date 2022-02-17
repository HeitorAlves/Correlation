using SpearmanCorrelation.Models;
using SpearmanCorrelation.ScriptBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SpearmanCorrelation
{
    class Program
    {
        static void Main()
        {
            var diretorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString() + "\\Documents\\tcc\\Bases\\";

            //LeitorCSV.LerCSV<WashingtonBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\hour.csv")
            //    .ComputaMatrixCorrelacaoPearson<WashingtonBaseMapper>();
            //LeitorCSV.LerCSV<SeulBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\SeoulBikeDataTransformed.csv")
            //.ComputaMatrixCorrelacaoPearson<SeulBaseMapper>();

            var dados = LeitorCSV.LerCSV<WashingtonBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\hour.csv");
            //var dados = LeitorCSV.LerCSV<SeulBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\SeoulBikeDataTransformed.csv");

            try
            {
                using (var arquivo = new FileStream(diretorio + "baseTemporalWashington.xlsx", FileMode.Create, FileAccess.Write))
                {
                    var dadosexcel = CriacaoBaseDataHoraQuantidade.GerarBase<WashingtonBaseMapper>(dados);
                    arquivo.Write(dadosexcel);
                }
                Console.WriteLine("\nArquivo Salvo em: " + diretorio + "baseTemporalWashington.xlsx");
                Console.WriteLine("\nClique tecla enter para sair");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("\nNão foi possivel escrever no Diretorio");
            }
            //LeitorCSV.LerCSV<WashingtonBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\hour.csv")
            //    .ComputaMatrixCorrelacaoSpearman<WashingtonBaseMapper>();
            //LeitorCSV.LerCSV<SeulBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\SeoulBikeDataTransformed.csv")
            //    .ComputaMatrixCorrelacaoSpearman<SeulBaseMapper>();

            Console.ReadLine();
        }
    }
}
