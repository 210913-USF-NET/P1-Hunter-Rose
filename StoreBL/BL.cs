using System;
using System.Collections.Generic;
using DL;
using Models;

namespace StoreBL
{
    public class BL : IBL
    {
        private IRepo _repo;
        //dependency injection
        public BL(IRepo repo)
        {
            _repo = repo;
        }
        public List<StoreFront> GetAllStoreFronts()
        {
            return _repo.GetAllStoreFronts();
        }
    }
}