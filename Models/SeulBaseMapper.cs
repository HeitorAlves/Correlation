using SpearmanCorrelation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearmanCorrelation.Models
{
    public class SeulBaseMapper : IBaseModel
    {
        public DateTime DataTexto { get; set; }
        public double Data { get; set; }
        public double QuantidadeAlugueis { get; set; }
        public double Hora { get; set; }
        public double Temperatura { get; set; }
        public double Humidade { get; set; }
        public double VelocidadeVento { get; set; }
        //nao tem washigton
        public double Visibilidade { get; set; }
        //nao tem washigton
        public double TemperaturaDeOrvalho { get; set; }
        //nao tem washigton
        public double RadiacaoSolar { get; set; }
        //nao tem washigton
        public double Chuva { get; set; }
        //nao tem washigton
        public double Neve { get; set; }
        public double Estacao { get; set; }
        public double Feriado { get; set; }
        public double DiaTrabalho { get; set; }        
    }
}
