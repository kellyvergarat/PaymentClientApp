using MongoDB.Bson.Serialization.Attributes;

namespace PaymentAPI.Models
{
    public class PaymentDetail 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? PaymentDetailId { get; set; }
        
        [BsonElement("cardOwnerName")]
        public string CardOwnerName { get; set; } = string.Empty;
        
        [BsonElement("cardNumber")]
        public string CardNumber { get; set; } = string.Empty;
        
        [BsonElement("expirationDate")]
        public string ExpirationDate { get; set; } = string.Empty;
        
        [BsonElement("securityCode")]
        public string SecurityCode { get; set; } = string.Empty;
    }
}
