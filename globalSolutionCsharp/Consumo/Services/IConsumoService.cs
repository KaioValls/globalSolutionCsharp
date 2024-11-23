using globalSolutionCsharp.Consumo.Model;

namespace globalSolutionCsharp.Consumo.Services
{
    public interface IConsumoService
    {
        Task RegistrarConsumoAsync(ConsumoRequest request);
        Task<IEnumerable<ConsumoEnergetico>> ObterConsumosAsync();
    }
}
