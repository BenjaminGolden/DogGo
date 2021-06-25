using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;

namespace DogGo.Interfaces
{
    public interface IOwnerRepository
    {
         void AddOwner(Owner owner);

         void UpdateOwner(Owner owner);

         void DeleteOwner(int id);
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);

        Owner GetOwnerByEmail(string email);
    }
}
