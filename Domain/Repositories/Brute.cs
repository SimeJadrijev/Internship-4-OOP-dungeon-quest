using Game.Data.StartingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain.Repositories
{
    public class Brute : Monster
    {
        public Brute()
        {
            Category = "Brute";
            HealthPoints = (int)MonsterHealthPoints.Brute;
            Experience = (int)MonsterExperience.Brute;
            Damage = (int)MonsterDamage.Brute;
        }

        public void StrongAttack(Hero newHero)
        {
            newHero.HealthPoints -= (int)Math.Round(HealthPoints * 0.5);    //Reduce heros's health for 50%
        }
    }
}