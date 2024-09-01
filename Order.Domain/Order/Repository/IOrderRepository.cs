

 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Order.Repository;

public interface IOrderRepository
{
   // Task<List<Order>>  All();
    Task<Order>  ById(Guid Id);
   // Task<List<Order>>  ByCustomerId(Guid customerId);
     
    Task Save(Order order);
    Task Update(Order order);
    Task Delete(Order order);
}
