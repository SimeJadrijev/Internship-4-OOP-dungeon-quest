using Game.Data.StartingInformation;
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
        public Hero(string name, int customHealthPoints, int customDamage, string category)
        {
            Name = name;
            HealthPoints = customHealthPoints;
            Damage = customDamage;
            Category = category;
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

        public void ReturnHealth()
        {
            var newHealth = (int)Math.Round(HealthPoints * 0.25);
            HealthPoints += newHealth;
            //if (HealthPoints > GetInitialHealthPoints())  //Ne pise u zadatku da se moze vratit samo do pocetnog healtha?
                //HealthPoints = GetInitialHealthPoints();
        }
        private int GetInitialHealthPoints()
        {
            switch (Category)
            {
                case "Gladiator":
                    return (int)HeroHealthPoints.Gladiator;
                case "Enchanter":
                    return (int)HeroHealthPoints.Enchanter;
                case "Marksman":
                    return (int)HeroHealthPoints.Marksman;
                default:
                    throw new InvalidOperationException("Nepostojeći heroj!");
            }
        }


        public void TradeExperienceForHealth()
        {
            Console.WriteLine($"Ukoliko želite potrošiti {Experience} bodova kako bi vratili puni health, upišite 'da'. Ako ne želite, upišite bilo šta drugo: ");
            var userAnswer = Console.ReadLine();

            if (userAnswer.ToLower() == "da" && Experience >= 2)
            {
                HealthPoints = GetInitialHealthPoints();
                Experience -= (int)Math.Round(Experience / 2.0);
                Console.WriteLine($"\n Sada imate {HealthPoints} health bodova i {Experience} experience bodova. Sretno! \n");

            }
            else if (userAnswer.ToLower() == "da" && Experience < 2)
                Console.WriteLine("Nažalost ne možete obnoviti health jer nemate dovoljno experience bodova! \n");
            else
                Console.WriteLine($"Nastavljate s {HealthPoints} health bodova! Sretno! \n");

        }
    }
}