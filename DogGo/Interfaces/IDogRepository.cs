using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Interfaces
{
    public interface IDogRepository
    {
        void UpdateDog(Dog dog);
        void DeleteDog(int id);
        void AddDog(Dog dog);
        List<Dog> GetAllDogs();
        Dog GetDogById(int id);
        List<Dog> GetDogsByOwnerId(int ownerId);
    }
}
