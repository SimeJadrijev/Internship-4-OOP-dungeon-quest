﻿using Game.Data;
using Game.Domain;
using Game.Domain.Repositories;
//Main
Console.WriteLine("Dobrodošli u Dungeon Quest! \n\n");

string chosenHeroName = null;
while (string.IsNullOrWhiteSpace(chosenHeroName))
{
    Console.Write("Odaberite ime svog heroja: ");
    chosenHeroName = Console.ReadLine();
}

var newHero = CreateNewHero(chosenHeroName); //Creating user's hero
ShortTimeOutAndConsoleClear(); //Short pause and clearing of console

PrintHeroInformation(newHero); //Printing some basic information about user's chosen hero
ClickToContinueAndConsoleClear(); //Waiting for user to read the info

var newMonster = CreateNewMonster(); //Creating a new monster
PrintMonsterInformation(newMonster); //Printing some basic information about the monster
ClickToContinueAndConsoleClear(); //Waiting for user to read the info







//Functions
static void PrintMonsterInformation(Monster newMonster)
{
    Console.WriteLine("Osnovne informacije o sljedećem čudovištu: \n\n" +
                    $"Vrsta: {newMonster.Category} \n" +
                    $"Damage: {newMonster.Damage} \n" +
                    $"Health: {newMonster.HealthPoints}");
}
static Monster CreateNewMonster()
{
    var rnd = new Random();
    var randomNumber = rnd.Next(1, 101);
    if (randomNumber < 60)
        return new Goblin();
    else if (randomNumber < 85)
        return new Brute();
    else
        return new Witch();

}
static void ClickToContinueAndConsoleClear()
{
    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak!");
    Console.ReadKey();
    Console.Clear();
}
static void ShortTimeOutAndConsoleClear()
{
    Thread.Sleep(1000);
    Console.Clear();
}
static void PrintHeroInformation(Hero newHero)
{
    Console.WriteLine("Osnovne informacije o vašem heroju: \n\n" +
                    $"Ime: {newHero.Name} \n" +
                    $"Damage: {newHero.Damage} \n" +
                    $"Health: {newHero.HealthPoints} \n" +
                    $"Kategorija: {newHero.Category} \n");
}
static Hero CreateNewHero(string chosenHeroName)
{
    int? chosenHeroCategory;
    Hero newHero = null;
    do
    {
        Console.Write("Odaberite kategoriju heroja! \n" +
                " '1' za Gladiatora \n" +
                " '2' za Enchantera \n" +
                " '3' za Marksmana \n");
        chosenHeroCategory = InputInt(Console.ReadLine());

        switch (chosenHeroCategory)
        {
            case 1:
                Console.WriteLine("Odabrali ste Gladiatora!");
                newHero = new Gladiator(chosenHeroName);
                break;
            case 2:
                Console.WriteLine("Odabrali ste Enchantera!");
                newHero = new Enchanter(chosenHeroName);
                break;
            case 3:
                Console.WriteLine("Odabrali ste Marksmana!");
                newHero = new Marksman(chosenHeroName);
                break;
        }

    } while (chosenHeroCategory == null || chosenHeroCategory < 1 || chosenHeroCategory > 3);
    return newHero;
}
static int? InputInt(string input)
{
     var result = int.TryParse(input, out int number );
    if(result)
        return number;
    else
        return null;
}

