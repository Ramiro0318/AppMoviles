using AutoMapper;
using PendientesAPI.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;

namespace PendientesAPI.Services;

public class PendientesService
{
    private readonly Repository<Pendientes> _repo;
    private readonly IMapper _mapper;

    public PendientesService(Repository<Pendientes> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public IEnumerable<PendienteResponseDTO> GetAll()
    {
        var pendientes = _repo.GetAll();
        return _mapper.Map<IEnumerable<PendienteResponseDTO>>(pendientes);
    }

    public IEnumerable<PendienteResponseDTO> GetByUser(int id)
    {
        var pendientes = _repo.GetAll().Where(x => x.IdUsuario == id);
        return _mapper.Map<IEnumerable<PendienteResponseDTO>>(pendientes);
    }

    public PendienteResponseDTO? GetById(int id)
    {
        var pendiente = _repo.GetById(id);
        return pendiente == null ? null : _mapper.Map<PendienteResponseDTO>(pendiente);
    }

    public PendienteResponseDTO? Create(PendienteRequestDTO dto, int user)
    {
        var pendiente = _mapper.Map<Pendientes>(dto);
        if (pendiente == null) return null;
        if (pendiente.IdUsuario != user)
        {
            throw new AccessViolationException();
        }

        _mapper.Map(dto, pendiente);
        pendiente.IdUsuario = user;

        _repo.Add(pendiente);
        _repo.SaveChanges();
        return _mapper.Map<PendienteResponseDTO>(pendiente);
    }

    public PendienteResponseDTO? Update(PendienteRequestDTO dto)
    {
        var pendiente = _repo.GetById(dto.Id);
        if (pendiente == null) return null;

        _mapper.Map(dto, pendiente);
        _repo.Update(pendiente);
        _repo.SaveChanges();
        return _mapper.Map<PendienteResponseDTO>(pendiente);
    }

    public bool Delete(int id)
    {
        var pendiente = _repo.GetById(id);
        if (pendiente == null) return false;

        _repo.Delete(pendiente);
        _repo.SaveChanges();
        return true;
    }
}