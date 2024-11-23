using globalSolutionCsharp.Consumo.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using static globalSolutionCsharp.Consumo.Repository.ConsumoRepository;

namespace globalSolutionCsharp.Consumo.Repository
{
    public class ConsumoRepository : IConsumoRepository
    {
        private readonly IMongoCollection<ConsumoEnergetico> _consumoCollection;

        public ConsumoRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("ConsumoEnergeticoDB");

            // Cria a collection "Consumos" se não existir
            if (!CollectionExists(database, "Consumos"))
            {
                database.CreateCollection("Consumos");
            }

            _consumoCollection = database.GetCollection<ConsumoEnergetico>("Consumos");
        }

        private bool CollectionExists(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            return database.ListCollections(new ListCollectionsOptions { Filter = filter }).Any();
        }

        public async Task SalvarAsync(ConsumoEnergetico consumo)
        {
            await _consumoCollection.InsertOneAsync(consumo);
        }

        public async Task<List<ConsumoEnergetico>> ObterTodosAsync()
        {
            return await _consumoCollection.Find(new BsonDocument()).ToListAsync();
        }
    }

}
