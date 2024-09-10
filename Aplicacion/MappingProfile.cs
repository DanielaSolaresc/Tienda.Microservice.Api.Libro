using AutoMapper;
using Tienda.Microservice.Api.Libro.Modelo;

namespace Tienda.Microservice.Api.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
