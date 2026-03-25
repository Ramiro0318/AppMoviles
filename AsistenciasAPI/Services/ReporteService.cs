using AsistenciaAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaAPI.Services
{
    public class ReporteService
    {
        public ReporteService(Repository<Grupo> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public Repository<Grupo> Repository { get; }

        public IMapper Mapper { get; }

        public async Task<byte[]> GenerarReporteAsync(int idGrupo, DateTime fechaInicio, DateTime fechaFin)
        {
            var query = Repository.Query()
                .Where(g => g.Id == idGrupo)
                .Include(g => g.Alumno)
                    .ThenInclude(a => a.Asistencia)
                        .ThenInclude(e => e.IdEstadoNavigation);

            var grupo = await query.FirstOrDefaultAsync();

            if (grupo == null)
                throw new Exception("Grupo no encontrado.");

            // Crear lista de fechas del rango
            var fechas = Enumerable.Range(0, (fechaFin - fechaInicio).Days + 1)
                                   .Select(i => fechaInicio.AddDays(i))
                                   .ToList();

            // Mapeo al DTO
            var dto = Mapper.Map<ReporteDTO>(grupo);
            dto.Fechas = fechas;

            // Alinear cada alumno con todas las fechas
            foreach (var alumno in dto.ListaAlumnos)
            {
                alumno.ListaFecha = fechas
                    .Select(f => alumno.ListaFecha.FirstOrDefault(x => x.Fecha.Date == f.Date)
                        ?? new FechaDTO { Fecha = f, Estado = "" })
                    .ToList();
            }

            // PDF final
            return ReportePDF.GenerarPDF(dto);
        }
    }
}
