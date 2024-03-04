using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Repository.Entities;
using Repository.Settings;

namespace Repository.Service;

public class MongoDBService
{
    private readonly IMongoCollection<ProposalQuotation> _playlistCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<ProposalQuotation>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<ProposalQuotationDto<QuotationBaseDto<object>>>> GetAsync()
    {
        var result = await _playlistCollection.Find(new BsonDocument()).ToListAsync();

        return result.Select(item => new ProposalQuotationDto<QuotationBaseDto<object>>
        {
            Id = item.Id,
            Cotacao = item.Quotation,
            Status = item.Status,
            Tipo = item.Type
        }).ToList();
    }

    public async Task<ProposalQuotationDto> GetById(int id)
    {
        FilterDefinition<ProposalQuotation> filter = Builders<ProposalQuotation>.Filter.Eq("Id", id);
        var result = await _playlistCollection.Find(filter).FirstOrDefaultAsync();

        return new ProposalQuotationDto
        {
            Id = result.Id,
            Cotacao = result.Quotation,
            Status = result.Status,
            Tipo = result.Type
        };
    }
    
    public async Task<int> CreateAsync(object quotation, Status status, CodeType type)
    {
        var proposal = new ProposalQuotation()
        {
            Quotation = quotation,
            Status = status,
            Type = type
        };
        
        await _playlistCollection.InsertOneAsync(proposal);
        return proposal.Id;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<ProposalQuotation> filter = Builders<ProposalQuotation>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
    }
}