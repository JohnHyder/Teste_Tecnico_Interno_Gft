using Newtonsoft.Json;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        int totalGoalsTeam1 = await GetTotalGoalForTeam($"https://jsonmock.hackerrank.com/api/football_matches?team1={team}&year={year}");
        int totalGoalsTeam2 = await GetTotalGoalForTeam($"https://jsonmock.hackerrank.com/api/football_matches?team2={team}&year={year}");
        
        return totalGoalsTeam1 + totalGoalsTeam2;
    }

    public static async Task<int> GetTotalGoalForTeam(string url)
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage responseHttpTotalPages = await client.GetAsync(url);
        string responseMessageTotalPages = await responseHttpTotalPages.Content.ReadAsStringAsync();

        ApiResponseEntity responseJsonTotalPages = JsonConvert.DeserializeObject<ApiResponseEntity>(responseMessageTotalPages);
        int totalPages = responseJsonTotalPages.Total_Pages;

        int totalGoals = 0;

        for (int page = 1; page <= totalPages; page++)
        {
            HttpResponseMessage responseHttp = await client.GetAsync($"{url}&page={page}");
            string responseMessage = await responseHttp.Content.ReadAsStringAsync();

            if (responseHttp.IsSuccessStatusCode)
            {
                ApiResponseEntity responseJson = JsonConvert.DeserializeObject<ApiResponseEntity>(responseMessage);

                foreach (var match in responseJson.Data)
                {
                    totalGoals += url.Contains("team1") ? match.Team1Goals : match.Team2Goals;
                }
            }
        }
        return totalGoals;
    }
}