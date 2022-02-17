using OfficeOpenXml;
using SpearmanCorrelation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearmanCorrelation.ScriptBase
{
    public static class CriacaoBaseDataHoraQuantidade
    {
        static public byte[] GerarBase<T>(List<T> Base) where T : IBaseModel
        {
            using (var package = new ExcelPackage())
            {

                int indexInicioDatasFaltosas = 25;
                int offSetValores = 2;

                var worksheet = package.Workbook.Worksheets.Add("Base Formatada");

                worksheet.Cells[1, 1].Value = "Data/Hora";
                worksheet.Cells[1, 1].Style.Font.Bold = true;

                //configura hora
                for (int hora = 0; hora < 24; hora++)
                {
                    worksheet.Cells[hora + 2, 1].Value = hora;
                }

                //ordena data hora
                Base.OrderBy(x => x.Data).ThenBy(x => x.Hora);

                var distinctData = Base.Select(x => x.DataTexto).Distinct().ToList();

                //seleciona e preenche data e preenche quantidade
                for (int quantidadeDatas = offSetValores; quantidadeDatas < distinctData.Count; quantidadeDatas ++)
                {
                    var data = distinctData[quantidadeDatas - offSetValores];
                    worksheet.Cells[1, quantidadeDatas].Value = data.ToString();
                    for (int hora = 0; hora < 24; hora++)
                    {
                        worksheet.Cells[hora + offSetValores, quantidadeDatas].Value = Base.Where(x => x.DataTexto == data && x.Hora == hora)?.FirstOrDefault()?.QuantidadeAlugueis;
                    }
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
    }
}
