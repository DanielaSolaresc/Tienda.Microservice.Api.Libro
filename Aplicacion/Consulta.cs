﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservice.Api.Libro.Modelo;
using Tienda.Microservice.Api.Libro.Persistencia;

namespace Tienda.Microservice.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDto>>
        {
            public Ejecuta() { }
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();
                var libreriaDTO = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libros);
                return libreriaDTO;
            }
        }
    }
}
