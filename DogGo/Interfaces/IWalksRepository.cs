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
        List<Walks> GetWalksByWalkerId(int id);
        Walker GetWalkerById(int id);

    }
}
