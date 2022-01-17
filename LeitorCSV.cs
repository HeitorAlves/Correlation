using AutoMapper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearmanCorrelation
{
    public static class LeitorCSV 
    {

        public static List<T> LerCSV<T>(string path = null) where T : class
        {
            path = (path ?? @"C:\testes.csv");
            IMapper mapper = AutoMapper.Inicializar<T>();
            List<T> dados = new List<T>();

            using (TextFieldParser csvReader = new TextFieldParser(path))
            {

                ConfiguraLeituraLeitura(csvReader);

                while (!csvReader.EndOfData)
                {
                    string[] valoresCSV = csvReader.ReadFields();
                    dados.Add(mapper.Map<T>(valoresCSV));
                }

            }
            return dados;
        }

        private static void ConfiguraLeituraLeitura(TextFieldParser csvReader)
        {
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });
            csvReader.HasFieldsEnclosedInQuotes = true;
            string[] colunasCSV = csvReader.ReadFields();
        }
    }
}

