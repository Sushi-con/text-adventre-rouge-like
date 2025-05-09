using System.Diagnostics;
using System.Text.Json;
using text_adventer_rouge_like.models;
using static System.Net.Mime.MediaTypeNames;

Directory.SetCurrentDirectory("C:/Users/brade/source/repos/text adventer rouge-like");
string pathToItemJson = "./Items.json";
string Json = await File.ReadAllTextAsync(pathToItemJson);
List<Item> items = new List<Item>();
items = JsonSerializer.Deserialize<List<Item>>(Json);

string pathToEnemesJson = "./Items.json";
//string Json = File.ReadAllText(pathToEnemesJson);
//List<Item> items = new List<Item>();
//items = JsonSerializer.Deserialize<List<Item>>(Json);

Console.Clear();
OverWorld NewGame = new OverWorld();
NewGame.StartGame();