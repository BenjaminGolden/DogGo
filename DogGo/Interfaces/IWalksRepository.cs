using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Interfaces
{
    public interface IWalksRepository
    {
        List<Walks> GetAllWalks();
        Walker GetWalkerById(int id);

    }
}
