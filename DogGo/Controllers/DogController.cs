using DogGo.Interfaces;
using DogGo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class DogController : Controller

    {
        private readonly IDogRepository _dogRepository;
        public DogController(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepository.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        public ActionResult Details(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }
            return View(dog);
        }

        //get: /dogs/create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //Post: dogs/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id
                dog.OwnerId = GetCurrentUserId();

                _dogRepository.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        //GET: dogs/delete
        [Authorize]
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);

            if (dog.OwnerId != GetCurrentUserId())
            {
                return NotFound();
            }

            return View(dog);
        }

        //Post: dogs/delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                dog.OwnerId = GetCurrentUserId();

                _dogRepository.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        //Get: Dogs/Edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);

            if (dog.OwnerId != GetCurrentUserId())
            {
                return NotFound();
            }

            return View(dog);
        }

        //Post: Dogs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {

                dog.OwnerId = GetCurrentUserId();
                _dogRepository.UpdateDog(dog);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return View(dog);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
