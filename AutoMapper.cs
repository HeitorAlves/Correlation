using AutoMapper;
using SpearmanCorrelation.Models;
using System;

namespace SpearmanCorrelation
{
    public static class AutoMapper
    {
        private static IMapper mapper; 
        public static IMapper Inicializar <T> () where T : class
        {
            if (typeof(T) == typeof(WashingtonBaseMapper))
            {
                MappearBaseWashignton();
            }
            else if (typeof(T) == typeof(SeulBaseMapper))
            {
                MappearBaseSeul();
            }

            return mapper;
        }

        private static void MappearBaseWashignton()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<string[], WashingtonBaseMapper>()
                //.ForMember(p => p.Id, opt => opt.MapFrom(s => s[0]))
                .ForMember(p => p.Data, opt => opt.MapFrom(s => DateTime.Parse(s[1]).ToOADate()))
                .ForMember(p => p.Estacao, opt => opt.MapFrom(s => s[2]))
                .ForMember(p => p.Ano, opt => opt.MapFrom(s => s[3]))
                .ForMember(p => p.Mes, opt => opt.MapFrom(s => s[4]))
                .ForMember(p => p.Hora, opt => opt.MapFrom(s => s[5]))
                .ForMember(p => p.Feriado, opt => opt.MapFrom(s => s[6]))
                .ForMember(p => p.DiaSemana, opt => opt.MapFrom(s => s[7]))
                .ForMember(p => p.DiaTrabalho, opt => opt.MapFrom(s => s[8]))
                .ForMember(p => p.Tempo, opt => opt.MapFrom(s => s[9]))
                .ForMember(p => p.Temperatura, opt => opt.MapFrom(s => s[10]))
                .ForMember(p => p.SensaoTermica, opt => opt.MapFrom(s => s[11]))
                .ForMember(p => p.Humidade, opt => opt.MapFrom(s => s[12]))
                .ForMember(p => p.VelocidadeVento, opt => opt.MapFrom(s => s[13]))
                .ForMember(p => p.UsuarioTemporario, opt => opt.MapFrom(s => s[14]))
                .ForMember(p => p.UsuarioRegistradro, opt => opt.MapFrom(s => s[15]))
                .ForMember(p => p.QuantidadeAlugueis, opt => opt.MapFrom(s => s[16]));
            });
            mapper = config.CreateMapper();
        }

        private static void MappearBaseSeul()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<string[], SeulBaseMapper>();
            });
            mapper = config.CreateMapper();
        }

    }
}
