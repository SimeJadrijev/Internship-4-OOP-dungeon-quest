using Game.Data.StartingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public class Enchanter : Hero
    {
        public Enchanter(string name) : base(name)
        {
            Category = "Enchanter";
            HealthPoints = (int)HeroHealthPoints.Enchanter;
            Damage = (int)HeroDamage.Enchanter;

        }

    }
}
