﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservice.Api.Libro.Modelo;
using Tienda.Microservice.Api.Libro.Persistencia;

namespace Tienda.Microservice.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.Id == request.LibroId).FirstOrDefaultAsync();
                if (libro == null) {
                    throw new Exception("No se encontro el libro");
                }
                var libroDto = _mapper.Map<LibreriaMaterial,  LibroMaterialDto>(libro);
                return libroDto;
            } 
        }
    }
}