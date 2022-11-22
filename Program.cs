using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage;
using JsonSerializer = System.Text.Json.JsonSerializer;

class program
{
    public static string numerProduktu = "";
    public static List<Produkt> produkts = new List<Produkt>();
    public static void Main()
    {
        LoadJson();
        while (true)
        {
            Console.WriteLine("Witamy w automacie! Podaj numer produktu:");
            Console.Write("");
            numerProduktu = Console.ReadLine();

            
            if (numerProduktu.Length > 0 && numerProduktu.Length < 3 && numerProduktu.All(char.IsDigit))
            {
                foreach (var prod in produkts)
                {
                    if (numerProduktu == prod.ProduktNumber)
                    {
                        Console.WriteLine($"Cena wynosi {prod.ProduktCost} dla produktu o nazwie {prod.ProduktName}");
                        Console.WriteLine("Podaj kwote jaka wprowadzasz?");
                        double wprowadzanaKwota = double.Parse(Console.ReadLine());
                        if (wprowadzanaKwota > prod.ProduktCost || wprowadzanaKwota == prod.ProduktCost)
                        {
                            var result = wprowadzanaKwota - prod.ProduktCost;
                            Console.WriteLine($"Kupiles {prod.ProduktName} za {prod.ProduktCost}");
                            if (result > 0)
                            {
                                Console.WriteLine($"Twoja reszta wynosi {result}");
                            }
                            Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Thread.Sleep(3000);
                            Console.Clear();
                            Console.WriteLine("Wprowadzona kwota jest bledna!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wprowadzony numer jest bledny!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }
                }
            }
            else if (numerProduktu == "9999")
            {
                Console.Clear();
                Console.WriteLine("Witamy w panelu administratora!");
                Console.WriteLine("Co chcesz zrobic?");
                Console.WriteLine("1 - Dodac produkt do listy");
                Console.WriteLine("2 - Usunac produkt z listy");
                Console.WriteLine("3 - Edytowac produkt z listy");
                Console.WriteLine("4 - Wyswietlic wszystkie produkty na liscie");
                Console.WriteLine("5 - Wyjscie");
                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        Produkt newProdukt = new Produkt();

                        Console.WriteLine("Podaj nazwe nowego produktu:");
                        newProdukt.ProduktName = Console.ReadLine();
                        Console.WriteLine("Podaj numer nowego produktu:");
                        newProdukt.ProduktNumber = Console.ReadLine();
                        Console.WriteLine("Podaj cene nowego produktu (uzyj kropki a nie przecinka)");
                        newProdukt.ProduktCost = double.Parse(Console.ReadLine());
                        Console.Clear();

                        if (newProdukt.ProduktName.Length > 0 && newProdukt.ProduktNumber.Length > 0 && newProdukt.ProduktName.Length < 10
                            && newProdukt.ProduktNumber.Length < 3 && newProdukt.ProduktCost > 0 && newProdukt.ProduktCost < 10)
                        {
                            SerializedJsonNew(newProdukt);
                            produkts.Add(newProdukt);

                            Console.WriteLine($"Pomyslnie utworzono nowy produkt o nazwie {newProdukt.ProduktName}" +
                                $" o numerze {newProdukt.ProduktNumber} oraz cenie {newProdukt.ProduktCost}");

                            Thread.Sleep(3000);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Wystapil blad z dodaniem produktu sproboj ponownie.");
                            Thread.Sleep(3000);
                            Console.Clear();
                        }

                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Wprowadz numer lub nazwe produktu");
                        var userInputDelete = Console.ReadLine();
                        if (produkts.Any(a => a.ProduktName == userInputDelete) || produkts.Any(a => a.ProduktNumber == userInputDelete))
                        {
                            var ItemDelete = produkts.FirstOrDefault(a => a.ProduktName == userInputDelete || a.ProduktNumber == userInputDelete);

                            Console.WriteLine($"Pomyslnie usunales produkt o nazwie {ItemDelete.ProduktName} i numerze {ItemDelete.ProduktNumber}");
                            produkts.Remove(ItemDelete);
                            Thread.Sleep(3000);
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Podano nieprawidlowa nazwe lub numer!");
                            Thread.Sleep(3000);
                            Console.Clear();
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Wprowadz numer lub nazwe produktu");
                        var userInputEdit = Console.ReadLine();
                        if (produkts.Any(a => a.ProduktName == userInputEdit) || produkts.Any(a => a.ProduktNumber == userInputEdit))
                        {
                            var ItemEdit = produkts.FirstOrDefault(a => a.ProduktName == userInputEdit || a.ProduktNumber == userInputEdit);

                            Console.WriteLine($"Co bys chcial zmienic? W produkcie o nazwie {ItemEdit.ProduktName}");
                            Console.WriteLine("1 - Cene Produktu");
                            Console.WriteLine("2 - Numer Produktu");
                            Console.WriteLine("3 - Nazwe Produktu");

                            var userInputEditEdit = Console.ReadLine();
                            switch (userInputEditEdit)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Podaj nowa cene:");
                                    var newCostEdit = double.Parse(Console.ReadLine());
                                    ItemEdit.ProduktCost = newCostEdit;
                                    SerializedJsonEditCost(ItemEdit, newCostEdit);
                                    Console.WriteLine("Pomyslnie zmieniles cene");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Podaj nowy numer:");
                                    var newNumberEdit = Console.ReadLine();
                                    ItemEdit.ProduktNumber = newNumberEdit;
                                    SerializedJsonEditNumber(ItemEdit, newNumberEdit);
                                    Console.WriteLine("Pomyslnie zmieniles numer");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Podaj nowa nazwe:");
                                    var newNameEdit = Console.ReadLine();
                                    ItemEdit.ProduktName = newNameEdit;
                                    SerializedJsonEditName(ItemEdit, newNameEdit);
                                    Console.WriteLine("Pomyslnie zmieniles nazwe");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Wprowadzono bledny znak!");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    break;
                            }
                            produkts.Add(ItemEdit);
                            produkts.Remove(ItemEdit);

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Podano nieprawidlowa nazwe lub numer!");
                        }
                        break;

                    case "4":
                        Console.Clear();
                        var parseTab = new List<int>();
                        foreach (var produkt in produkts)
                        {
                            parseTab.Add(int.Parse(produkt.ProduktNumber));
                        }
                        for(int i = 0; i < parseTab.Count; i++)
                        {
                            Console.WriteLine($"{produkts[i].ProduktName} {produkts[i].ProduktNumber} {produkts[i].ProduktCost}");
                        }
                        Thread.Sleep(5000);
                        Console.Clear();
                        break;

                    case "5":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Wprowadzono bledny znak");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wprowadzony numer jest bledny!");
                Thread.Sleep(3000);
                Console.Clear();

                
            }
        }
    }
    public static void LoadJson()
    {
        using (StreamReader read = new StreamReader("Produkts.json"))
        {
            string json = read.ReadToEnd();
            produkts = JsonConvert.DeserializeObject<List<Produkt>>(json);
        }
    }
    public static async void SerializedJsonNew(Produkt prod)
    {
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync("Produkts.json");
        var jsonString = await FileIO.ReadTextAsync(file);
        var produktList = JsonConvert.DeserializeObject<List<Produkt>>(jsonString);
        var newProdukt = new Produkt()
        {
            ProduktName = prod.ProduktName,
            ProduktNumber = prod.ProduktNumber,
            ProduktCost = prod.ProduktCost,
        };
        produktList.Add(newProdukt);
        var updatedJsonString = JsonConvert.SerializeObject(produktList);
        await FileIO.WriteTextAsync(file, updatedJsonString);
    }
    public static async void SerializedJsonEditName(Produkt prod, string name)
    {
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync("Produkts.json");
        var jsonString = await FileIO.ReadTextAsync(file);
        var produktList = JsonConvert.DeserializeObject<List<Produkt>>(jsonString);
        var newProdukt = new Produkt()
        {
            ProduktName = name,
            ProduktNumber = prod.ProduktNumber,
            ProduktCost = prod.ProduktCost,
        };
        produktList.Remove(prod);
        produktList.Add(newProdukt);
        var updatedJsonString = JsonConvert.SerializeObject(produktList);
        await FileIO.WriteTextAsync(file, updatedJsonString);
    }
    public static async void SerializedJsonEditNumber(Produkt prod, string number)
    {
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync("Produkts.json");
        var jsonString = await FileIO.ReadTextAsync(file);
        var produktList = JsonConvert.DeserializeObject<List<Produkt>>(jsonString);
        var newProdukt = new Produkt()
        {
            ProduktName = prod.ProduktName,
            ProduktNumber = number,
            ProduktCost = prod.ProduktCost,
        };
        produktList.Remove(prod);
        produktList.Add(newProdukt);
        var updatedJsonString = JsonConvert.SerializeObject(produktList);
        await FileIO.WriteTextAsync(file, updatedJsonString);
    }
    public static async void SerializedJsonEditCost(Produkt prod, double cost)
    {
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync("Produkts.json");
        var jsonString = await FileIO.ReadTextAsync(file);
        var produktList = JsonConvert.DeserializeObject<List<Produkt>>(jsonString);
        var newProdukt = new Produkt()
        {
            ProduktName = prod.ProduktName,
            ProduktNumber = prod.ProduktNumber,
            ProduktCost = cost,
        };
        produktList.Remove(prod);
        produktList.Add(newProdukt);
        var updatedJsonString = JsonConvert.SerializeObject(produktList);
        await FileIO.WriteTextAsync(file, updatedJsonString);
    }
}