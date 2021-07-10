using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalogAPI.InputModel;
using GamesCatalogAPI.ViewModel;

namespace GamesCatalogAPI.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Find(int page, int quantity);
        Task<GameViewModel> Find(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);
    }
}
