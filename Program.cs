using SpearmanCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpearmanCorrelation
{
    class Program
    {
        
        static void Main()
        {
            var dados = LeitorCSV.LerCSV<WashingtonBaseMapper>(@"C:\Users\DTI Digital\Documents\tcc\bases\hour.csv");
            
            ComputaMatrixCorrelacaoSpearman(dados);
            ComputaMatrixCorrelacaoPearson(dados);

            Console.ReadLine();
        }
        
        private static void ComputaMatrixCorrelacaoSpearman(List<WashingtonBaseMapper> dados)
        {
            var listaPreparada = AjustaBaseParaOsRanksSpearman(dados);
            var quantidadePropriedadesClasse = dados.FirstOrDefault().GetType().GetProperties().Length;
            for (int propriedadeAnalisadaBase = 0; propriedadeAnalisadaBase < quantidadePropriedadesClasse; propriedadeAnalisadaBase++)
            {
                PropertyInfo propriedadeBase = typeof(WashingtonBaseMapper).GetProperty(typeof(WashingtonBaseMapper).GetProperties()[propriedadeAnalisadaBase].Name);
                var listaBase = listaPreparada.Ranks.Where(x => x.Key == propriedadeAnalisadaBase).FirstOrDefault().Value.ToList();

                for (int propriedadeAnalisadaAlvo = propriedadeAnalisadaBase + 1; propriedadeAnalisadaAlvo < quantidadePropriedadesClasse; propriedadeAnalisadaAlvo++)
                {
                    PropertyInfo propriedadeAlvo = typeof(WashingtonBaseMapper).GetProperty(typeof(WashingtonBaseMapper).GetProperties()[propriedadeAnalisadaAlvo].Name);
                    var listaAlvo = listaPreparada.Ranks.Where(x => x.Key == propriedadeAnalisadaAlvo).FirstOrDefault().Value.ToList();

                    Console.WriteLine($@"[{propriedadeBase.Name}]X[{propriedadeAlvo.Name}] :" + Correlacao.CorrelacaoSpearman(listaBase, listaAlvo, listaPreparada != null));
                }
            }
        }

        private static void ComputaMatrixCorrelacaoPearson(List<WashingtonBaseMapper> dados)
        {
            var quantidadePropriedadesClasse = dados.FirstOrDefault().GetType().GetProperties().Length;
            for (int propriedadeAnalisadaBase = 0; propriedadeAnalisadaBase < quantidadePropriedadesClasse; propriedadeAnalisadaBase++)
            {
                PropertyInfo propriedadeBase = typeof(WashingtonBaseMapper).GetProperty(typeof(WashingtonBaseMapper).GetProperties()[propriedadeAnalisadaBase].Name);
                var listaBase = dados.Select(x => (double)propriedadeBase.GetValue(x)).ToList();

                for (int propriedadeAnalisadaAlvo = propriedadeAnalisadaBase + 1; propriedadeAnalisadaAlvo < quantidadePropriedadesClasse; propriedadeAnalisadaAlvo++)
                {
                    PropertyInfo propriedadeAlvo = typeof(WashingtonBaseMapper).GetProperty(typeof(WashingtonBaseMapper).GetProperties()[propriedadeAnalisadaAlvo].Name);
                    var listaAlvo = dados.Select(x => (double)propriedadeAlvo.GetValue(x)).ToList();

                    Console.WriteLine($@"[{propriedadeBase.Name}]X[{propriedadeAlvo.Name}] :" + Correlacao.CorrelacaoPearson(listaBase, listaAlvo));
                }
            }
        }

        #region metodos auxiliares
        private static ModelBaseRankPrepared AjustaBaseParaOsRanksSpearman(List<WashingtonBaseMapper> dados)
        {
            ModelBaseRankPrepared listaPreparada = new ModelBaseRankPrepared();
            var quantidadePropriedadesClasse = dados.FirstOrDefault().GetType().GetProperties().Length;
            for (int indicePropriedade = 0; indicePropriedade < quantidadePropriedadesClasse; indicePropriedade++)
            {
                PropertyInfo propriedade = typeof(WashingtonBaseMapper).GetProperty(typeof(WashingtonBaseMapper).GetProperties()[indicePropriedade].Name);
                var lista = dados.Select(x => (double)propriedade.GetValue(x)).ToList();
                var listaRankeada = Correlacao.GeraListaRankeada(lista);
                //metodo antigo mais lento
                //var listaRankeada = lista.Select(x =>  (double)lista.OrderBy(n => n).ToList().IndexOf(x) + 1).ToList();

                listaPreparada.Ranks.Add(indicePropriedade, listaRankeada);
            }
            return listaPreparada;
        }
        #endregion
    }
}
