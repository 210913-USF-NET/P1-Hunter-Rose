using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public static class CurrentContext
    {
    public static int CurrentCustomerId{get; set;}

    public static int CurrentStoreId{get; set;}   

    public static int CurrentOrderId{get; set;}

    public static int CurrentLineItemId{get; set;}

    public static int? CurrentLineItemQuantityId {get; set;}

    public static int? CurrentLineItemProductId {get; set;}

    public static int CurrentInventoryId {get; set;}

    public static int CurrentInventoryProduct1 {get; set;}

    public static int CurrentInventoryProduct2 {get; set;}

    public static int CurrentInventoryProduct3 {get; set;}
    }
    
    
}