using DogGo.Interfaces;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IWalkerRepository _walkerRepository;

        public WalksController(IWalksRepository walksRepository, IWalkerRepository walkerRepository)
        {
            _walksRepository = walksRepository;
            _walkerRepository = walkerRepository;
        }
        public ActionResult Index()
        {
            List<Walks> walks = _walksRepository.GetAllWalks();
            return View(walks);
        }

        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepository.GetWalkerById(id);
            List<Walks> walks = _walksRepository.GetAllWalks();

            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            return View(vm);
        }
    }
}
