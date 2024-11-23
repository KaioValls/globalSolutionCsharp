using globalSolutionCsharp.Consumo.Model;

namespace globalSolutionCsharp.Consumo.Repository
{
    public interface IConsumoRepository
    {
        Task SalvarAsync(ConsumoEnergetico consumo);
        Task<List<ConsumoEnergetico>> ObterTodosAsync();
    }
}
