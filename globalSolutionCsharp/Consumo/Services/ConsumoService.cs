using globalSolutionCsharp.Consumo.Model;
using globalSolutionCsharp.Consumo.Repository;
using globalSolutionCsharp.Redis;
using MongoDB.Bson;

namespace globalSolutionCsharp.Consumo.Services
{
    public class ConsumoService : IConsumoService
    {
        private readonly IConsumoRepository _repository;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "consumos";

        public ConsumoService(IConsumoRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task RegistrarConsumoAsync(ConsumoRequest request)
        {
            var consumo = new ConsumoEnergetico
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Quantidade = request.Quantidade,
                DataHora = DateTime.UtcNow
            };

            await _repository.SalvarAsync(consumo);

            // Remove o cache após inserção de novos dados
            await _cacheService.RemoveAsync(CacheKey);
        }

        public async Task<IEnumerable<ConsumoEnergetico>> ObterConsumosAsync()
        {
            // Tente obter os dados do cache
            var consumosCache = await _cacheService.GetAsync<List<ConsumoEnergetico>>(CacheKey);
            if (consumosCache != null)
            {
                return consumosCache;
            }

            // Caso não exista no cache, busque no banco
            var consumos = await _repository.ObterTodosAsync();
            var response = consumos.Select(c => new ConsumoEnergetico
            {
                Id = c.Id,
                Quantidade = c.Quantidade,
                DataHora = c.DataHora
            }).ToList();

            // Armazene no cache por 5 minutos
            await _cacheService.SetAsync(CacheKey, response, TimeSpan.FromMinutes(5));

            return response;
        }
    }

}
