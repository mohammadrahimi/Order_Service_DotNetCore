
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Order.Persistence.MongoDB.EF.Model;

public class OrderModel
{

    [BsonId]
    [Key]
    public ObjectId Id { get; set; }

    [Required]
    [BsonElement("customerId")]
    public string CustomerId { get; set; }

    //[Required]
    [BsonElement("customeName")]
    public string CustomerName { get; set; }

    //[Required]
    [BsonElement("customerMobile")]
    public string CustomerMobile { get; set; }

    [BsonElement("customerAddress")]
    public string CustomerAddress { get; set; }

    [Required]
    [BsonElement("orderId")]
    public string OrderId { get; set; }

    [BsonElement("orderItems")]
    public string OrderItems { get; set; }

    [BsonElement("orderDateTime")]
    public string OrderDateTime { get; set; }

    [BsonElement("paymentCode")]
    public string PaymentCode { get; set; }

    [BsonElement("paymentPrice")]
    public string PaymentPrice { get; set; }

    [BsonElement("orderState")]
    public string OrderState { get; set; }
}
