using Game.Data;
using Game.Data.StartingInformation;
using Game.Domain;
using Game.Domain.Repositories;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
//Main
Console.WriteLine("Dobrodošli u Dungeon Quest! \n\n");

string chosenHeroName = null;
while (string.IsNullOrWhiteSpace(chosenHeroName))   //Ask the user to choose their hero's name until it's not blank
{
    Console.Write("Odaberite ime svog heroja: ");
    chosenHeroName = Console.ReadLine();
}


var newHero = CreateNewHero(chosenHeroName); //Creating user's hero
ShortTimeOutAndConsoleClear(); //Short pause and clearing of console

newHero = CreateCustomHero(newHero); //Allowing the user to enter custom values for damage and health points
ClickToContinueAndConsoleClear(); //Waiting for user to read the info

PrintHeroInformation(newHero); //Printing some basic information about user's chosen hero
ClickToContinueAndConsoleClear(); //Waiting for user to read the info


switch (newHero.Category)
{
    case "Gladiator":       
        GladiatorGame(newHero);     //If user chose the gladiator category, start the GladiatorGame function
        break;
    case "Enchanter":
        EnchanterGame(newHero);
        break;
}



//Functions

int TradeManaForHealth(Hero newHero, int manaAmount)
{
    Console.WriteLine($"Ako želite možete zamijeniti mana bodove da bi poboljšali health bodove.");
    Console.Write("Unesite 'da' ako želite to učiniti ili bilo što drugo ako ne želite: ");
    var tradeRequest = Console.ReadLine();

    int? requestedAmount = null;

    if (tradeRequest.ToLower() == "da")
    {
        while (requestedAmount == null || requestedAmount > (manaAmount - 6) || requestedAmount < 1)
        {
            Console.Write($"Unesite prirodni broj 1 - {manaAmount - 6} (jer vam treba minimalno 5 bodova za napad): ");
            requestedAmount = InputInt(Console.ReadLine());
        }

        newHero.HealthPoints += (int)requestedAmount;
        manaAmount -= (int)requestedAmount;
        Console.WriteLine($"Sada imate {newHero.HealthPoints} health bodova i {manaAmount} mana bodova.\n");
        ClickToContinueAndConsoleClear();
    }
    return manaAmount;
}

void EnchanterGame (Hero newHero)
{
    var listOfMonsters = new List<Monster>();
    for (int i = 0; i < 10; i++)
    {
        var newMonster = CreateNewMonster(); //Creating a new monster
        listOfMonsters.Add(newMonster);
    }
    var j = 1;
    var initialHealth = newHero.HealthPoints; //+ (1 + (newHero.Level*10) /100);   //Saving initial health points value (in case we need to return it after using extra life)
    var extraLife = 1;      //Number of extra lives that enchanter is allowed to use
    var initialMana = 30;   //Initial amount of Mana
    foreach (var newMonster in listOfMonsters)
    {
        var roundNumber = 1;
        var receivedExperience = 0;
        var manaAmount = initialMana * newHero.Level;   // each fight, the mana amount is restarted. Higher level gives more mana.

        PrintMonsterInformation(newMonster); //Printing some basic information about the monster
        ClickToContinueAndConsoleClear(); //Waiting for user to read the info

        while (IsHeroAlive(newHero) && IsMonsterAlive(newMonster))  //Run the protocol until someone dies.
        {
            if(manaAmount > 5)
                manaAmount = TradeManaForHealth(newHero, manaAmount); // Checking if user wants to trade Mana for Health points
            else
                Console.WriteLine("Potrebno vam je minimalno 6 mana bodova za zamjenu bodova! \n");

            if (manaAmount < 5)
            {
                Console.WriteLine($"Nemate dovoljno mane za napad! (imate {manaAmount}, a treba vam minimalno 5) \n" +
                                "U ovoj rundi će vam se obnoviti mana, ali nećete imati pravo napada\n" +
                                "pa čudovište automatski pobjeđuje ovu rundu.\n");
                newMonster.NormalAttack(newHero);
                manaAmount = initialMana * newHero.Level;
                ClickToContinueAndConsoleClear();
            }
            else
            {
                var usersChosenAttack = ChooseAttack(); //User chooses their attack option
                var monstersChosenAttack = MonstersChosenAttack(); //Monster's attack option gets randomly chosen

                if (usersChosenAttack != null)
                {
                    Console.WriteLine("\n" + roundNumber + ". runda \n");
                    var fightResult = RockPaperScissors(usersChosenAttack, monstersChosenAttack); //fight result is determined by RockPaperScissors function
                    if (fightResult == true)    //  If user won the round
                    {
                        Console.WriteLine("\nPobjeda! \n\n");
                        receivedExperience = newHero.NormalAttack(newMonster);  // hero attacks the monster (only normal attack for now)
                    }
                    else if (fightResult == false)  //  If user lost the round
                    {
                        Console.WriteLine("\nPoraz! \n\n");
                        newMonster.NormalAttack(newHero);   //  Monster attacks the hero
                    }
                    else                             //  If the round was tied
                        Console.WriteLine("\nIzjednačeno! \n\n");   //  Just print that the result is a tie
                }
                manaAmount -= 5;
            }

            
            //Printing information about the current state of user's and monster's health:
            Console.WriteLine("Vaš health: " + newHero.HealthPoints);
            Console.WriteLine("Čudovištev health: " + newMonster.HealthPoints);
            Console.WriteLine("Mana: " + manaAmount);

            if (newHero.HealthPoints <= 0 && extraLife == 1)    //If we lost, but didn't use an extra life yet
            {
                Console.WriteLine("\nIzgubili ste, ali budući da ste izabrali Enchantera, imate jedan dodatan život! Sretno! \n");
                newHero.HealthPoints = initialHealth;   //Our initial health is returned
                extraLife--;            //The extra life is used
            }

            roundNumber++;  //Incrementing the round number
            ClickToContinueAndConsoleClear();
        }
        if (IsHeroAlive(newHero))   //If hero managed to stay alive
        {
            Console.Clear();

            Console.WriteLine($"\nČestitke! Porazili ste {j}. čudovište! Još samo {10 - j} čudovišta do kraja!\n");
            Console.WriteLine($"Dobili ste {receivedExperience} experience bodova. \n");

            ClickToContinueAndConsoleClear();

            newHero.ReturnHealth(); // Return 25% of user's previous health
            newHero.GainExperience(receivedExperience); //  Receive monster's experience points
            manaAmount = initialMana * newHero.Level;   // Returning the initial mana amount after the fight

            PrintHeroInformation(newHero);  //  Print hero's updated stats (health, experience, and similar information)
            Console.WriteLine("Mana: " + manaAmount);
            ClickToContinueAndConsoleClear();

            newHero.TradeExperienceForHealth(); //In case we want to trade half of our experience for health points
            ClickToContinueAndConsoleClear();
        }
        else    // If hero didn't manage to stay alive
        {
            Console.WriteLine("\n\nIzgubili ste! Više sreće drugi put :)\n\n");
            break;  //Break the for loop
        }
        j++;
    }
    Console.Clear();
    Console.WriteLine("Čestitke!!! Porazili ste svih 10 čudovišta!");
}

void DeleteRage(Hero newHero, int rememberDamage)
{
    newHero.Damage = rememberDamage;
}
void RageAttack(Hero newHero)
{
    var reduceHealth = (int)Math.Round(newHero.HealthPoints * 0.2); //0.2 = Rage coefficient
    newHero.HealthPoints -= reduceHealth;

    newHero.Damage *= 2;
}
void GladiatorGame(Hero newHero)
{
    var listOfMonsters = new List<Monster>();
    for (int i = 0; i < 10; i++)
    {
        var newMonster = CreateNewMonster(); //Creating a new monster
        listOfMonsters.Add(newMonster);
    }
    var j = 1;
    foreach (var newMonster in listOfMonsters)
    {
        var roundNumber = 1;
        var receivedExperience = 0;
        PrintMonsterInformation(newMonster); //Printing some basic information about the monster
        ClickToContinueAndConsoleClear(); //Waiting for user to read the info

        while (IsHeroAlive(newHero) && IsMonsterAlive(newMonster))
        {
            Console.Write("Ako želite kroz ovu borbu koristiti Rage napad, unesite 'da'. \n" +
                        "Ako ne želite, unesite bilo šta drugo.\n");
            var rageAttackQuestion = Console.ReadLine();
            var rememberDamage = newHero.Damage;
            if (rageAttackQuestion == "da")
            {
                RageAttack(newHero);    //Use rage
                Console.WriteLine($"Odabrali ste opciju Rage napad, tako da ćete sad imati: \n" +
                                $" {newHero.Damage} damage bodova \n" +
                                $" {newHero.HealthPoints} health bodova \n");
            }

            var usersChosenAttack = ChooseAttack(); //User chooses their attack option
            var monstersChosenAttack = MonstersChosenAttack(); //Monster's attack option gets randomly chosen

            if (usersChosenAttack != null)
            {
                Console.WriteLine("\n" + roundNumber + ". runda \n");
                var fightResult = RockPaperScissors(usersChosenAttack, monstersChosenAttack); //fight result is determined by RockPaperScissors function
                if (fightResult == true)    //  If user won the round
                {
                    Console.WriteLine("\nPobjeda! \n\n");
                    receivedExperience = newHero.NormalAttack(newMonster);  // hero attacks the monster (only normal attack for now)
                }
                else if (fightResult == false)  //  If user lost the round
                {
                    Console.WriteLine("\nPoraz! \n\n");
                    newMonster.NormalAttack(newHero);   //  Monster attacks the hero
                }
                else                             //  If the round was tied
                    Console.WriteLine("\nIzjednačeno! \n\n");   //  Just print that the result is a tie
            }
            //Printing information about the current state of user's and monster's health:
            Console.WriteLine("Vaš health: " + newHero.HealthPoints);
            Console.WriteLine("Čudovištev health: " + newMonster.HealthPoints);

            roundNumber++;  //Incrementing the round number
            DeleteRage(newHero,rememberDamage); //Return normal damage points
            ClickToContinueAndConsoleClear();
        }
        if (IsHeroAlive(newHero))   //If hero managed to stay alive
        {
            Console.Clear();

            Console.WriteLine($"\nČestitke! Porazili ste {j}. čudovište! Još samo {10 - j} čudovišta do kraja!\n");
            Console.WriteLine($"Dobili ste {receivedExperience} experience bodova. \n");

            ClickToContinueAndConsoleClear();

            newHero.ReturnHealth(); // Return 25% of user's previous health
            newHero.GainExperience(receivedExperience); //  Receive monster's experience points

            PrintHeroInformation(newHero);  //  Print hero's updated stats (health, experience, and similar information)
            ClickToContinueAndConsoleClear();

            newHero.TradeExperienceForHealth();
            ClickToContinueAndConsoleClear();
        }
        else    // If hero didn't manage to stay alive
        {
            Console.WriteLine("Izgubili ste! Više sreće drugi put :)");
            break;  //Break the for loop
        }
        j++;
    }
    Console.Clear();
    Console.WriteLine("Čestitke!!! Porazili ste svih 10 čudovišta!");
}

static Hero CreateCustomHero(Hero newHero)
{
    Console.Write("\nAko želite unijeti svoje 'custom' vrijednosti za health i damage, unesite 'da'. \n" +
            "Ukoliko želite da vam se dodijele normalne vrijednosti, unesite bilo što drugo. \n");
    var requestForCustomHero = Console.ReadLine();

    int? customHealth, customDamage;
    if (requestForCustomHero.ToLower() == "da")
    {
        Console.WriteLine();

        do
        {
            Console.Write("Unesite željenu vrijednost za health (prirodan broj): ");
            customHealth = InputInt(Console.ReadLine());
        }
        while (customHealth == null || customHealth < 1);

        Console.WriteLine();

        do
        {
            Console.Write("Unesite željenu vrijednost za damage (prirodan broj): ");
            customDamage = InputInt(Console.ReadLine());
        }
        while (customDamage == null || customDamage < 1);

        newHero.HealthPoints = customHealth.Value;
        newHero.Damage = customDamage.Value;
        
    }

    return newHero;
} 
static bool IsMonsterAlive(Monster newMonster)
{
    if (newMonster.HealthPoints > 0)
        return true;
    else
        return false;
}
static bool IsHeroAlive (Hero newHero)
{
    if (newHero.HealthPoints > 0)
        return true;
    else
        return false;
}
static bool? RockPaperScissors(int usersChoice, int monstersChoice)
{
    var UserAttackOption = (AttackOptions)usersChoice;
    var MonstersAttackOption = (AttackOptions)monstersChoice;
    Console.WriteLine("Vaš napad: " + UserAttackOption + "\n" + "Njegov napad: " + MonstersAttackOption);
    
    if (UserAttackOption == MonstersAttackOption)
        return null;
    else
    {
        switch (UserAttackOption)
        {
            case AttackOptions.Direct:
                if (MonstersAttackOption == AttackOptions.Side)
                    return true;
                else if (MonstersAttackOption == AttackOptions.Counter)
                    return false;
                break;
            case AttackOptions.Side:
                if (MonstersAttackOption == AttackOptions.Direct)
                    return false;
                else if (MonstersAttackOption == AttackOptions.Counter)
                    return true;
                break;
            case AttackOptions.Counter:
                if (MonstersAttackOption == AttackOptions.Direct)
                    return true;
                else if (MonstersAttackOption == AttackOptions.Side)
                    return false;
                break;
        }
    }
    return null;
}
int MonstersChosenAttack()
{
    var rnd = new Random();
    return rnd.Next(0, 3);
}
 int ChooseAttack()
{
    int chosenAttack;
    do
    {
        Console.Write("Odaberite napad: \n" +
                    "'1' - Direktni napad \n" +
                    "'2' - Napad s boka \n" +
                    "'3' - Protunapad \n");
        chosenAttack = InputInt(Console.ReadLine()) ?? 0;

    } while (chosenAttack > 3 || chosenAttack < 1);

    return chosenAttack - 1; 
}
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
                    $"Kategorija: {newHero.Category} \n" +
                    $"Experience: {newHero.Experience}/100\n" +
                    $"Level: {newHero.Level}");
}
static Hero CreateNewHero(string chosenHeroName)
{
    int? chosenHeroCategory;
    Hero newHero = null;
    Console.WriteLine("\nOsnovne informacije o svakoj vrsti heroja: \n\n" +
                      "Gladiator -> Health: 100 - Damage: 10 - Rage napad \n" +
                      "Enchanter -> Health: 60 - Damage: 30 - Jedan extra život - Mana \n" +
                      "Marksman -> Health: 80 - Damage: 20 - Critical chance - Stun chance \n");
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

/*
  var listOfMonsters = new List<Monster>();
    for (int i = 0; i < 10; i++)
    {
        var newMonster = CreateNewMonster(); //Creating a new monster
        listOfMonsters.Add(newMonster);
    }

    foreach (var newMonster in listOfMonsters)
    {
        var i = 1;
        var roundNumber = 1;
        var receivedExperience = 0;
        PrintMonsterInformation(newMonster); //Printing some basic information about the monster
        ClickToContinueAndConsoleClear(); //Waiting for user to read the info

        while (IsHeroAlive(newHero) && IsMonsterAlive(newMonster))
        {
            var usersChosenAttack = ChooseAttack(); //User chooses their attack option
            var monstersChosenAttack = MonstersChosenAttack(); //Monster's attack option gets randomly chosen

            if (usersChosenAttack != null)
            {
                Console.WriteLine("\n" + roundNumber + ". runda \n");
                var fightResult = RockPaperScissors(usersChosenAttack, monstersChosenAttack); //fight result is determined by RockPaperScissors function
                if (fightResult == true)    //  If user won the round
                {
                    Console.WriteLine("\nPobjeda! \n\n");
                    receivedExperience = newHero.NormalAttack(newMonster);  // hero attacks the monster (only normal attack for now)
                }
                else if (fightResult == false)  //  If user lost the round
                {
                    Console.WriteLine("\nPoraz! \n\n");
                    newMonster.NormalAttack(newHero);   //  Monster attacks the hero
                }
                else                             //  If the round was tied
                    Console.WriteLine("\nIzjednačeno! \n\n");   //  Just print that the result is a tie
            }
            //Printing information about the current state of user's and monster's health:
            Console.WriteLine("Vaš health: " + newHero.HealthPoints);
            Console.WriteLine("Čudovištev health: " + newMonster.HealthPoints);

            roundNumber++;  //Incrementing the round number
            ClickToContinueAndConsoleClear();
        }
        if (IsHeroAlive(newHero))   //If hero managed to stay alive
        {
            Console.Clear();

            Console.WriteLine($"\nČestitke! Porazili ste {i}. čudovište! Još samo {10 - i} čudovišta do kraja!\n");
            Console.WriteLine($"Dobili ste {receivedExperience} experience bodova. \n");

            ClickToContinueAndConsoleClear();

            newHero.ReturnHealth(); // Return 25% of user's previous health
            newHero.GainExperience(receivedExperience); //  Receive monster's experience points

            PrintHeroInformation(newHero);  //  Print hero's updated stats (health, experience, and similar information)
            ClickToContinueAndConsoleClear();

            newHero.TradeExperienceForHealth();
            ClickToContinueAndConsoleClear();
        }
        else    // If hero didn't manage to stay alive
        {
            Console.WriteLine("Izgubili ste! Više sreće drugi put :)");
            break;  //Break the for loop
        }
        i++;
    }
*/