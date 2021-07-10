using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeCycleIdController : ControllerBase
    {
        public readonly ISingletonExempla _exemploSingleton1;
        public readonly ISingletonExempla _exemploSingleton2;

        public readonly IScopedExempla _exemploScoped1;
        public readonly IScopedExempla _exemploScoped2;

        public readonly ITransientExempla _exemploTransient1;
        public readonly ITransientExempla _exemploTransient2;

        public LifeCycleIdController(ISingletonExempla exemploSingleton1,
                                       ISingletonExempla exemploSingleton2,
                                       IScopedExempla exemploScoped1,
                                       IScopedExempla exemploScoped2,
                                       ITransientExempla exemploTransient1,
                                       ITransientExempla exemploTransient2)
        {
            _exemploSingleton1 = exemploSingleton1;
            _exemploSingleton2 = exemploSingleton2;
            _exemploScoped1 = exemploScoped1;
            _exemploScoped2 = exemploScoped2;
            _exemploTransient1 = exemploTransient1;
            _exemploTransient2 = exemploTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_exemploSingleton1.Id}");
            stringBuilder.AppendLine($"Singleton 2: {_exemploSingleton2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_exemploScoped1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_exemploScoped2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_exemploTransient1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_exemploTransient2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }

    }

    public interface IGeneralExample
    {
        public Guid Id { get; }
    }

    public interface ISingletonExempla : IGeneralExample
    { }

    public interface IScopedExempla : IGeneralExample
    { }

    public interface ITransientExempla : IGeneralExample
    { }

    public class LifeCycleExample : ISingletonExempla, IScopedExempla, ITransientExempla
    {
        private readonly Guid _guid;

        public LifeCycleExample()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Id => _guid;
    }
}
