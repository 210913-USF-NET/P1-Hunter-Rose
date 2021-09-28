using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace BL
{
    public interface IBL
    {
        List<Stores> SearchStore(string quertStr);
    }
}