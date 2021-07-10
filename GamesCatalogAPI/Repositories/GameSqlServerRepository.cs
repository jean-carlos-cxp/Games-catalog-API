using GamesCatalogAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection _sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> Find(int page, int quantity)
        {
            var games = new List<Game>();

            var comand = $"select * from Jogos order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, _sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Producer = (string)sqlDataReader["Produtora"],
                    Price = (double)sqlDataReader["Preco"]
                });
            }

            await _sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> Find(Guid id)
        {
            Game game = null;

            var comand = $"select * from Jogos where Id = '{id}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, _sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Producer = (string)sqlDataReader["Produtora"],
                    Price = (double)sqlDataReader["Preco"]
                };
            }

            await _sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Find(string name, string producer)
        {
            var games = new List<Game>();

            var comand = $"select * from Jogos where Nome = '{name}' and Produtora = '{producer}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, _sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Producer = (string)sqlDataReader["Produtora"],
                    Price = (double)sqlDataReader["Preco"]
                });
            }

            await _sqlConnection.CloseAsync();

            return games;
        }

        public async Task Insert(Game game)
        {
            var comand = $"insert Jogos (Id, Nome, Produtora, Preco) values ('{game.Id}', '{game.Name}', '{game.Producer}', {game.Price.ToString().Replace(",", ".")})";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await _sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var comand = $"update Jogos set Nome = '{game.Name}', Produtora = '{game.Producer}', Preco = {game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await _sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var comand = $"delete from Jogos where Id = '{id}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comand, _sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await _sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            _sqlConnection?.Close();
            _sqlConnection?.Dispose();
        }
    }
}
