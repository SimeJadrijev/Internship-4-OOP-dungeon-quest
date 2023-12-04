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
        public int Mana { get; set; }
        public int ExtraLife { get; set; }
        public Enchanter(string name) : base(name)
        {
            Category = "Enchanter";
            HealthPoints = (int)HeroHealthPoints.Enchanter;
            Damage = (int)HeroDamage.Enchanter;
            Mana = 30;
            ExtraLife = 1;
        }

        /*
        public void UseMana()
        {
            Console.Write($"Koliko mane želite zamijeniti za health bodove (maksimalno: {Mana})? ");

        }
        public void UseExtraLife()
        {
            if (ExtraLife == 0)
                ExtraLife--;
            else
                Console.WriteLine("Dodatni život je potrošen!");
        }
        */

    }
}