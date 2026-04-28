using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PendientesAPI.Models.Entities;

namespace PendientesAPI.Repositories;

public class Repository<T> where T : class
{
    private readonly PendientesContext _context;
    private readonly DbSet<T> tabla;

    public Repository(PendientesContext context)
    {
        _context = context;
        tabla = context.Set<T>();
    }

    public IEnumerable<T> GetAll() =>
        tabla;

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) =>
        tabla.Where(predicate);

    public T? GetById(int id) =>
        tabla.Find(id);

    public void Add(T entity) =>
        tabla.Add(entity);

    public void Update(T entity) =>
        tabla.Update(entity);

    public void Delete(T entity) =>
        tabla.Remove(entity);

    public void SaveChanges() =>
        _context.SaveChanges();
}