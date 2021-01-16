//using DocumentFormat.OpenXml.Bibliography;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace AzureSqlMapper
{
    public class MongoCRUD
    {
        private IMongoDatabase db;
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecords<T>(string table, List<T> records)
        {
            var collection = db.GetCollection<T>(table);
            // collection.InsertOne(record);
            collection.InsertMany(records);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            // collection.InsertOne(record);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {

            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();

        }
        //FullyQualifiedDomainName

        public T FindRecoredByID<T>(string table, string FullyQualifiedDomainName)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("FullyQualifiedDomainName", FullyQualifiedDomainName);
            return collection.Find(filter).First();

        }
    }
}
