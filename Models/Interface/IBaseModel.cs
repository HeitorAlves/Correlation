using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearmanCorrelation.Models.Interface
{
    public interface IBaseModel
    {
        public double Data { get; set; }
        public DateTime DataTexto { get; set; }
        public double QuantidadeAlugueis { get; set; }
        public double Hora { get; set; }
    }
}
