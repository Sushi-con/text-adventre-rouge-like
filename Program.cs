using System.Diagnostics;
using System.Text.Json;
using text_adventer_rouge_like.models;
using static System.Net.Mime.MediaTypeNames;

//Directory.SetCurrentDirectory("../../../");
string pathToItemJson = "./Items.json";
string Json = await File.ReadAllTextAsync(pathToItemJson);
List<Item> items = new List<Item>();
items = JsonSerializer.Deserialize<List<Item>>(Json);

string pathToEnemesJson = "./enemes.json";
string EnemiesJson = File.ReadAllText(pathToEnemesJson);
List<Enemies> enemies = new List<Enemies>();
enemies = JsonSerializer.Deserialize<List<Enemies>>(EnemiesJson);
foreach (Enemies enemie in enemies)
{
    enemie.setBase();
}

string pathToSpellsJson = "./spells.json";
string SpellsJson = File.ReadAllText(pathToSpellsJson);
List<Spell> spells = new List<Spell>();
spells = JsonSerializer.Deserialize<List<Spell>>(SpellsJson);

Console.Clear();
OverWorld NewGame = new OverWorld();
NewGame.StartGame(enemies, spells, items);

