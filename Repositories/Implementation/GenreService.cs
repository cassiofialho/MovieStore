using MovieStore.Models.Domains;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Repositories.Implementation
{
    public class GenreService : IGenreServices
    {
        private readonly DatabaseContext databaseContext;
        public GenreService(DatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext;
        }

        public bool Add(Genre genre)
        {
            try
            {
                databaseContext.Genre.Add(genre);
                databaseContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Genre> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}
