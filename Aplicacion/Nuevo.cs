using FluentValidation;
using MediatR;
using Tienda.Microservice.Api.Libro.Modelo;
using Tienda.Microservice.Api.Libro.Persistencia;

namespace Tienda.Microservice.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public int Cantidad { get; set; }
            public Guid AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.Cantidad).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoLibreria _contexto;
            public Manejador(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    Cantidad = request.Cantidad,
                    AutorLibro = request.AutorLibro
                };
                _contexto.LibreriaMaterial.Add(libro);
                var valor = await _contexto.SaveChangesAsync();
                if(valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puede guardar el libro");
            }
            
        }
    }
}
