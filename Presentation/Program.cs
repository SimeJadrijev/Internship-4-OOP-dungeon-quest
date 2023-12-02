using Game.Data;
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

} while (chosenHeroCategory == null|| chosenHeroCategory < 1 || chosenHeroCategory > 3);

ShortTimeOutAndConsoleClear();

if (newHero != null)
    NewHeroInformation(newHero);

ClickToContinueAndConsoleClear();





//Functions
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
static void NewHeroInformation(Hero newHero)
{
    Console.WriteLine("Osnovne informacije o vašem heroju: \n\n" +
                    $"Ime: {newHero.Name} \n" +
                    $"Damage: {newHero.Damage} \n" +
                    $"Health: {newHero.HealthPoints} \n" +
                    $"Kategorija: {newHero.Category} \n");
}
static int? InputInt(string input)
{
     var result = int.TryParse(input, out int number );
    if(result)
        return number;
    else
        return null;
}

