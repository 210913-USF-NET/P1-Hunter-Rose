using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class BBBBL : IBL
    {
        private IRepo _repo;
        public BBBBL(IRepo irepo){
             _repo = irepo;

        } 
        public List<Stores> SearchStore(string quertStr){
            return new List<Stores>();
        }
    }
}