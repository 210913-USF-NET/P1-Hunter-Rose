using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    public class ExampleRepo : IRepo
    {
        public List<StoreFront> GetAllStoreFronts()
        {
            //Put your logic to get data here
            return new List<StoreFront>(){
                new StoreFront(){
                    Name = "Store One"
                }
            };
        }
    }
}