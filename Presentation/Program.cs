using Game.Data;
using Game.Data.StartingInformation;
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

var usersChosenAttack = ChooseAttack(); //User chooses their attack option
var monstersChosenAttack = MonstersChosenAttack(); //Monster's attack option gets randomly chosen


if (usersChosenAttack != null)
{
    var fightResult = RockPaperScissors(usersChosenAttack, monstersChosenAttack); //fight result is determined by RockPaperScissors function
    if (fightResult == true)
    {
        Console.WriteLine("Pobjeda!");
        var receivedExperience = newHero.NormalAttack(newMonster);
        newHero.GainExperience(receivedExperience);
    }
    else if (fightResult == false)
    {
        Console.WriteLine("Poraz!");
        newMonster.NormalAttack(newHero);
    }
    else
        Console.WriteLine("Tie!");
}

if (IsHeroAlive(newHero))
{
    
    //newHero.GainExperience
}





//Functions

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

