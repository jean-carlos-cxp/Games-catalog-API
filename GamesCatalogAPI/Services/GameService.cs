using GamesCatalogAPI.Entities;
using GamesCatalogAPI.InputModel;
using GamesCatalogAPI.Repositories;
using GamesCatalogAPI.Services.Exceptions;
using GamesCatalogAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Find(int page, int quantity)
        {
            var games = await _gameRepository.Find(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Find(Guid id)
        {
            var game = await _gameRepository.Find(id);

            if (game == null)
            {
                return null;
            }

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entityGame = await _gameRepository.Find(game.Name, game.Producer);

            if (entityGame.Count > 0)
            {
                throw new GameException("Jogo já cadastrado na base de dados");
            }

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };


            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.Find(id);

            if (entityGame == null)
            {
                throw new GameException("Jogo não cadastrado no sistema");
            }

            entityGame.Name = game.Name;
            entityGame.Producer = game.Producer;
            entityGame.Price = game.Price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Update(Guid id, double price)
        {
            var entityGame = await _gameRepository.Find(id);

            if (entityGame == null)
            {
                throw new GameException("Jogo não cadastrado no sistema");
            }

            entityGame.Price = price;

            await _gameRepository.Update(entityGame);
        }

        public async Task Delete(Guid id)
        {
            var game = await _gameRepository.Find(id);

            if (game == null)
            {
                throw new GameException("Jogo não cadastrado no sistema");
            }

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
