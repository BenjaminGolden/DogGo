using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public Walks Walk { get; set; } = new Walks();
        public List<Walks> Walks { get; set; }
        public Totaltime TotalTime { get; set; }
    }
}
