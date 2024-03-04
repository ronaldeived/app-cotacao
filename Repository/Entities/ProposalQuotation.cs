using Core.Enums;
using Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository.Entities;

public class ProposalQuotation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    public CodeType Type { get; set; }

    public QuotationBaseDto<object> Quotation { get; set; }
    
    public Status Status { get; set; }
}