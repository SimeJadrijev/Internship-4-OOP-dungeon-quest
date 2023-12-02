using Game.Data.StartingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public class Marksman : Hero
    {
        public Marksman(string name) : base(name)
        {
            Category = "Marksman";
            HealthPoints = (int)HeroHealthPoints.Marksman;
            Damage = (int)HeroDamage.Marksman;
        }
    }
}