using SpearmanCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpearmanCorrelation
{
    public static class ExtensionMethods
    {
        public static void ComputaMatrixCorrelacaoSpearman<T>(this List<T> listaBaseMapeada)
        {
            var listaPreparada = AjustaBaseParaOsRanksSpearman<T>(listaBaseMapeada);
            var quantidadePropriedadesClasse = listaBaseMapeada.FirstOrDefault().GetType().GetProperties().Length;

            for (int propriedadeAnalisadaBase = 0; propriedadeAnalisadaBase < quantidadePropriedadesClasse; propriedadeAnalisadaBase++)
            {
                PropertyInfo propriedadeBase = typeof(T).GetProperty(typeof(T).GetProperties()[propriedadeAnalisadaBase].Name);
                var listaBase = listaPreparada.Ranks.Where(x => x.Key == propriedadeAnalisadaBase).FirstOrDefault().Value.ToList();

                for (int propriedadeAnalisadaAlvo = propriedadeAnalisadaBase + 1; propriedadeAnalisadaAlvo < quantidadePropriedadesClasse; propriedadeAnalisadaAlvo++)
                {
                    PropertyInfo propriedadeAlvo = typeof(T).GetProperty(typeof(T).GetProperties()[propriedadeAnalisadaAlvo].Name);
                    var listaAlvo = listaPreparada.Ranks.Where(x => x.Key == propriedadeAnalisadaAlvo).FirstOrDefault().Value.ToList();

                    Console.WriteLine($@"[{propriedadeBase.Name}]X[{propriedadeAlvo.Name}] :" + Correlacao.CorrelacaoSpearman(listaBase, listaAlvo, listaPreparada != null));
                }
            }
        }

        public static void ComputaMatrixCorrelacaoPearson<T>(this List<T> listaBaseMapeada)
        {
            var quantidadePropriedadesClasse = listaBaseMapeada.FirstOrDefault().GetType().GetProperties().Length;
            for (int propriedadeAnalisadaBase = 0; propriedadeAnalisadaBase < quantidadePropriedadesClasse; propriedadeAnalisadaBase++)
            {
                PropertyInfo propriedadeBase = typeof(T).GetProperty(typeof(T).GetProperties()[propriedadeAnalisadaBase].Name);
                var listaBase = listaBaseMapeada.Select(x => (double)propriedadeBase.GetValue(x)).ToList();

                for (int propriedadeAnalisadaAlvo = propriedadeAnalisadaBase + 1; propriedadeAnalisadaAlvo < quantidadePropriedadesClasse; propriedadeAnalisadaAlvo++)
                {
                    PropertyInfo propriedadeAlvo = typeof(T).GetProperty(typeof(T).GetProperties()[propriedadeAnalisadaAlvo].Name);
                    var listaAlvo = listaBaseMapeada.Select(x => (double)propriedadeAlvo.GetValue(x)).ToList();

                    Console.WriteLine($@"[{propriedadeBase.Name}]X[{propriedadeAlvo.Name}] :" + Correlacao.CorrelacaoPearson(listaBase, listaAlvo));
                }
            }
        }

        private static ModelBaseRankPrepared AjustaBaseParaOsRanksSpearman<T>(List<T> dados)
        {
            ModelBaseRankPrepared listaPreparada = new ModelBaseRankPrepared();
            var quantidadePropriedadesClasse = dados.FirstOrDefault().GetType().GetProperties().Length;
            for (int indicePropriedade = 0; indicePropriedade < quantidadePropriedadesClasse; indicePropriedade++)
            {
                PropertyInfo propriedade = typeof(T).GetProperty(typeof(T).GetProperties()[indicePropriedade].Name);
                var lista = dados.Select(x => (double)propriedade.GetValue(x)).ToList();
                var listaRankeada = Correlacao.GeraListaRankeada(lista);


                listaPreparada.Ranks.Add(indicePropriedade, listaRankeada);
            }
            return listaPreparada;
        }
    }
}
