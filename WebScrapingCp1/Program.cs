using HtmlAgilityPack;
using System.Text.RegularExpressions;

var isUrlMetacritic = false;

string url = "";
while(isUrlMetacritic == false)
{
    Console.Write("Insira a url do metacritic do jogo que deseja: ");
    url = Console.ReadLine()!;

    var padraoMetacritic = "www.metacritic.com/game/";

    if(Regex.IsMatch(url, padraoMetacritic))
    {
        isUrlMetacritic = true;
    }
    else
    {
        Console.WriteLine("Url invalida!");
    }
}

try
{
    var client = new HttpClient();
    var response = await client.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();

    var doc = new HtmlDocument();
    doc.LoadHtml(content);

    var title = doc.DocumentNode
        .SelectSingleNode("/html/body/div[1]/div/div/div[2]/div[1]/div[1]/div/div/div[2]/div[3]/div[1]/div")
        .InnerText?.Trim();
    var scoreMetacritic = doc.DocumentNode
        .SelectSingleNode("/html/body/div[1]/div/div/div[2]/div[1]/div[1]/div/div/div[2]/div[3]/div[4]/div[1]/div/div[1]/div[2]/div/div/span")
        .InnerText?.Trim();
    var scorePublic = doc.DocumentNode
        .SelectSingleNode("/html/body/div[1]/div/div/div[2]/div[1]/div[1]/div/div/div[2]/div[3]/div[4]/div[2]/div[1]/div[2]/div/div/span")
        .InnerText?.Trim();
    Console.WriteLine("Titulo: " + title + "\nScore Metacritic: " 
        + scoreMetacritic + "\nUser Score: " + scorePublic); ;
}
catch (Exception e)
{
    Console.WriteLine("Erro : " + e);
}


