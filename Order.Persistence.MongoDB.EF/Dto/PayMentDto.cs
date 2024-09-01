 
namespace Order.Persistence.MongoDB.EF.Dto;
 
public record PayMentDto(
            string PaymentPrice,
            string PaymentCode,
            string OrderId);
 