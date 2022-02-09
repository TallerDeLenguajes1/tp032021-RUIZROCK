using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using CadeteriaWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb
{
    public class PerfilDeMapeo : Profile
    {
        public PerfilDeMapeo()
        {
            // --------------------MAPEO DE USUARIOS--------------------
            CreateMap<Usuario, UsuarioLoginViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioCreateViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateViewModel>().ReverseMap();

            // --------------------MAPEO DE CADETES--------------------
            CreateMap<Cadete, CadeteCreateViewModel>().ReverseMap();
            CreateMap<Cadete, CadeteUpdateViewModel>().ReverseMap();

            // --------------------MAPEO DE PEDIDOS--------------------
            CreateMap<Pedido, PedidoCreateViewModel>().ReverseMap()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src));
            CreateMap<Pedido, PedidoUpdateViewModel>().ReverseMap()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Cadete, opt => opt.MapFrom(src => src));

            CreateMap<Pedido, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Cliente.Id))
                .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
                .ForMember(dst => dst.Direccion, opt => opt.MapFrom(src => src.Cliente.Direccion))
                .ForMember(dst => dst.Telefono, opt => opt.MapFrom(src => src.Cliente.Telefono));

            CreateMap<Pedido, Cadete>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Cadete.Id));
        }
    }
}
