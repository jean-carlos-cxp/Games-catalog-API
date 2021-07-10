using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalogAPI.Entities;

namespace GamesCatalogAPI.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Find(int page, int quantity);
        Task<Game> Find(Guid id);
        Task<List<Game>> Find(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
