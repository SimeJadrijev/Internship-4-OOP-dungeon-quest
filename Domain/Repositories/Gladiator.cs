using Game.Data.StartingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public class Gladiator : Hero
    {
        public int Rage { get; set; }
        public Gladiator(string name) : base(name)
        {
            Category = "Gladiator";
            Rage = 0;
            HealthPoints = (int)HeroHealthPoints.Gladiator;
            Damage = (int)HeroDamage.Gladiator;
        }

    }
}