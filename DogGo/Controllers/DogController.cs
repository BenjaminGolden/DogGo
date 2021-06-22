using DogGo.Interfaces;
using DogGo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            List<Dog> dogs = _dogRepository.GetAllDogs();

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
                _dogRepository.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        //GET: dogs/delete
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);

            return View(dog);
        }

        //Post: dogs/delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepository.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        //Get: Dogs/Edit
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepository.GetDogById(id);

            if (dog == null)
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
                _dogRepository.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }
    }
}
