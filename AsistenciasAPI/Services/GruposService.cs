using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using AutoMapper;
using FluentValidation;

namespace AsistenciasAPI.Services
{
    public class GruposService
    {
        private readonly Repository<Grupo> repository;
        private readonly IMapper mapper;
        private readonly IValidator<AgregarGrupoDTO> validadorAgregar;
        private readonly IValidator<EditarGrupoDTO> validadorEditar;

        public GruposService(Repository<Grupo> repository, IMapper mapper, IValidator<AgregarGrupoDTO> validadorAgregar, IValidator<EditarGrupoDTO> validadorEditar)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validadorAgregar = validadorAgregar;
            this.validadorEditar = validadorEditar;
        }

        public List<GrupoDTO> GetAll()
        {
            var grupos = repository.GetAll().OrderBy(x => x.Nombre)
                .Select(mapper.Map<GrupoDTO>)
                .ToList();
            return grupos;
        }

        public GrupoDTO? Get(int idGrupo)
        {
            var grupo = repository.Get(idGrupo);

            if (grupo != null)
            {
                return mapper.Map<GrupoDTO>(grupo);
            }
            return null;
        }

        public int Agregar(AgregarGrupoDTO dto)
        {
            var result = validadorAgregar.Validate(dto);
            if (result.IsValid)
            {
                var entity = mapper.Map<Grupo>(dto);
                repository.Insert(entity);
                return entity.Id;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void Editar(EditarGrupoDTO dto)
        {
            var result = validadorEditar.Validate(dto);
            if (result.IsValid)
            {
                var entity = mapper.Map<Grupo>(dto);
                repository.Insert(entity);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public bool Eliminar(int idGrupo)
        {
            if (repository.Get(idGrupo) != null)
            {
                repository.Delete(idGrupo);
                return true;
            }
            else 
            {
                return false;
            }
        }

    }
}
