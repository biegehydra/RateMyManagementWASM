using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace RateMyManagementWASM.Server.Controllers;
public interface IEntityController<TEntity, TDto> where TEntity : class where TDto : class
{
    public Task<ActionResult<TEntity>> Get(string id, [FromBody] string[] includes);
    public Task<ActionResult> Save([FromBody] TDto dto);
    public Task<ActionResult> CreateMany([FromBody] IEnumerable<TDto> entities);
    public Task<ActionResult<IEnumerable<TEntity>>> GetAll([FromBody] string[] includes);
    public Task<ActionResult> Delete([FromBody] TDto entity);
}
internal static class DataAccessExtensions
{
    internal static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
        params string[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include).AsNoTracking());
        }
        return query;
    }
}