using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearmanCorrelation
{
    public static class Correlacao
    {
        public static double CorrelacaoPearson(List<double> listaA, List<double> listaB)
        {

            var numerador = Covariancia(listaA, listaB);
            var denominador = DesvioPadrao(listaA) * DesvioPadrao(listaB);

            return numerador / denominador;
        }
        public static double CorrelacaoSpearman(List<double> listaA, List<double> listaB, bool BaseAjustaParaRanks = false)
        {
            if (!BaseAjustaParaRanks)
            {
                listaA = GeraListaRankeada(listaA);
                listaB = GeraListaRankeada(listaB);
            }

            var spearman = CorrelacaoPearson(listaA, listaB);
            return spearman;
        }

        public static List<double> GeraListaRankeada(List<double> lista)
        {
            List<double> rank = new List<double>();

            for (int i = 0; i < lista.Count; i++)
            {
                int r = 1, s = 1;

                for (int j = 0; j < i; j++)
                {
                    if (lista[j] < lista[i]) r++;
                }

                for (int j = i + 1; j < lista.Count; j++)
                {
                    if (lista[j] < lista[i]) r++;
                }

                rank.Add(r);
            }

            return rank;
        }

        public static double Covariancia(List<double> listaA, List<double> listaB)
        {
            // media
            var mediaX = Media(listaA);
            var mediaY = Media(listaB);
            // faltando algortimo para buscar o numero de iteracoes... exemplo lista a nao tenha mesmo numero de iteracoes que lista b
            var tam = Math.Min(listaA.Count, listaB.Count);
            double covariancia = 0;
            for (int i = 0; i < tam; i++)
            {
                covariancia += ((listaA[i] - mediaX) * (listaB[i] - mediaY));
            }
            covariancia /= tam;

            return covariancia;

        }
        public static double Media(List<double> valores)
        {
            double media = 0;
            //calcular media
            foreach (var valor in valores)
            {
                media += valor;
            }
            media /= valores.Count;
            return media;
        }
        public static double DesvioPadrao(List<double> valores)
        {

            double desvioPadrao = 0;
            double media = Media(valores);

            //calcular Desvio Padrao
            foreach (var valor in valores)
            {
                desvioPadrao += Math.Pow((valor - media), 2);
            }

            desvioPadrao = Math.Sqrt(desvioPadrao / valores.Count);

            return desvioPadrao;
        }
    }
}
