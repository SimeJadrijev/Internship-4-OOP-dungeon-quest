using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public abstract class Hero
    {
        public string Name { get; set; }
        public int Experience { get; set; }
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
        public int Level { get; set; }
        public string? Category { get; set; }

        public Hero(string name)
        {
            Name = name;
            Experience = 0;
            Level = 1;
            Category = null;
        }

        
    }
}
