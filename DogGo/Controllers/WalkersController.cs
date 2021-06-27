using DogGo.Interfaces;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepository;
        private readonly IWalksRepository _walksRepository;
        private readonly IOwnerRepository _ownerRepository;

        public WalkersController(
            IWalkerRepository walkerRepository,
            IWalksRepository walksRepository,
            IOwnerRepository ownerRepository
            )
        {
            _walkerRepository = walkerRepository;
            _walksRepository = walksRepository;
            _ownerRepository = ownerRepository;
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        // GET: WalkersController
        public ActionResult Index()
        {
            List<Walker> walkers = _walkerRepository.GetAllWalkers();
            try
            {
            int ownerId = GetCurrentUserId();
                Owner owner = _ownerRepository.GetOwnerById(ownerId);
                walkers = _walkerRepository.GetWalkersInNeighborhood(owner.NeighborhoodId);
            }
            catch (Exception ex)
            {
                walkers = _walkerRepository.GetAllWalkers();
            }

            return View(walkers);
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepository.GetWalkerById(id);
            List<Walks> walks = _walksRepository.GetWalksByWalkerId(id);

            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


      
    }
}
