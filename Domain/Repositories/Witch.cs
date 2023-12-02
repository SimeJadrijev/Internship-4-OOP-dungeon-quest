using Game.Data.StartingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public class Witch : Monster
    {
        public Witch()
        {
            Category = "Witch";
            HealthPoints = (int)MonsterHealthPoints.Witch;
            Experience = (int)MonsterExperience.Witch;
            Damage = (int)MonsterDamage.Witch;
        }
    }
}