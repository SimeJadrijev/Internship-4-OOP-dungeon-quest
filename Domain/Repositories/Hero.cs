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

        public int NormalAttack(Monster newMonster)
        {
            newMonster.HealthPoints -= this.Damage;
            var receivedExperience = newMonster.Experience;
            return receivedExperience;
        }
        public void GainExperience(int receivedExperience)
        {
            if (Experience + receivedExperience >= 100)
            {
                Level++;
                Experience = (Experience + receivedExperience) - 100;
                HealthPoints += 10;
                Damage += 5;
            }
            else
                Experience += receivedExperience;
        }
    }
}