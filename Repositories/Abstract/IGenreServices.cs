using MovieStore.Models.Domains;

namespace MovieStore.Repositories.Abstract
{
    public interface IGenreServices
    {
        bool Add(Genre genre);
        bool Update(Genre genre);
        bool Delete(int Id);
        bool GetById(int Id);
        IQueryable<Genre> GetAll();
    }
}
