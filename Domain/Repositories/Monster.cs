using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public abstract class Monster
    {
        public int HealthPoints { get; set; }
        public int Experience { get; set; }
        public int Damage { get; set; }
        public string Category { get; set; }

        public Monster()
        {

        }

    }
}