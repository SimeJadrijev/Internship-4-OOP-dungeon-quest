using Game.Data.StartingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public class Goblin : Monster
    {
        public Goblin()
        {
            Category = "Goblin";
            HealthPoints = (int)MonsterHealthPoints.Goblin;
            Experience = (int)MonsterExperience.Goblin;
            Damage = (int)MonsterDamage.Goblin;
        }
  
    }
}