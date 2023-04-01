using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

// Task defining and performing
namespace center_of_school.data
{
    public class mongo_operation_service
    {
        private readonly IMongoCollection<code_for_mongo_operation> _trainingCenters;

        public mongo_operation_service(IOptions<mongodb_config_setting> options)
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017/centerofschool_database");
            _trainingCenters = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<code_for_mongo_operation>(options.Value.CollectionNameInDb);

        }

        public async Task<List<code_for_mongo_operation>> GetAll() =>
            await _trainingCenters.Find(_ => true).ToListAsync();

        public async Task<code_for_mongo_operation> Get(string id) =>
            await _trainingCenters.Find(m => m.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

        public async Task Create(code_for_mongo_operation newName) =>
            await _trainingCenters.InsertOneAsync(newName);

        public async Task Update(string id, code_for_mongo_operation updateRec) =>
            await _trainingCenters.ReplaceOneAsync(m => m.Id == ObjectId.Parse(id), updateRec);

        public async Task Remove(string id) =>
            await _trainingCenters.DeleteOneAsync(m => m.Id == ObjectId.Parse(id));
    }
}
