using Game.Data;
using Game.Domain;
//Main
Console.WriteLine("Dobrodošli u Dungeon Quest! \n\n");

string chosenHeroName = null;
while (string.IsNullOrWhiteSpace(chosenHeroName))
{
    Console.Write("Odaberite ime svog heroja: ");
    chosenHeroName = Console.ReadLine();
}

int? chosenHeroCategory;
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
            break;
        case 2:
            Console.WriteLine("Odabrali ste Enchantera!");
            break;
        case 3:
            Console.WriteLine("Odabrali ste Marksmana!");
            break;
    }

} while (chosenHeroCategory == null|| chosenHeroCategory < 1 || chosenHeroCategory > 3);


//Functions
static int? InputInt(string input)
{
     var result = int.TryParse(input, out int number );
    if(result)
        return number;
    else
        return null;
}
