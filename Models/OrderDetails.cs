using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class OrderDetails
    {
        public OrderDetails(){}
        
        public int Id {get; set;}
        public int CustomerId {get; set;}

        public int StoreId {get; set;}

        public DateTime Date {get ; set;}

        public override string ToString()
        {
            return $"CustomerId: {CustomerId}, StoreId: {StoreId}, OrderId: {Id}, Date and Time: {Date}";
        }
    }
}