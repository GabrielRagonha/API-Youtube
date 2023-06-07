using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Channels;


//Doc API: https://developers.google.com/youtube/v3/docs/videos/list?hl=pt-br

class Program
{
    static void Main(string[] args)
    {
        string apiKey = "AIzaSyDGZ65pGb75USP1LRHdflmHzlke2-QSOlY";

        // URL da API do YouTube para obter os vídeos mais populares do dia no Brasil
        string apiUrl = $"https://www.googleapis.com/youtube/v3/videos?part=snippet&chart=mostPopular&maxResults=10&regionCode=BR&key={apiKey}";

        // Criação do cliente HttpClient
        var client = new HttpClient();

        // Envio da solicitação GET à API do YouTube e obtenção da resposta
        var response = client.GetFromJsonAsync<YouTubeVideoListResponse>(apiUrl).Result;

        if (response != null && response.Items.Count > 0)
        {
            List<VideoDTO> videos = new List<VideoDTO>();

            foreach (var item in response.Items)
            {
                // Criação de um objeto VideoDTO e preenchimento com os dados do snippet
                VideoDTO video = new VideoDTO
                {
                    Channel = item.Snippet.ChannelTitle,
                    Title = item.Snippet.Title,
                    Description = item.Snippet.Description
                };

                // Adição do vídeo à lista de vídeos
                videos.Add(video);
            }

            foreach (var video in videos)
            {
                Console.WriteLine("\n\n********************************************************\n");
                Console.WriteLine($"CANAL: {video.Channel}\n");
                Console.WriteLine($"TÍTULO: {video.Title}\n");
                Console.WriteLine($"DESCRIÇÃO: {video.Description}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum vídeo encontrado.");
        }
    }
}

// Classes de modelo para a resposta da API do YouTube
public class YouTubeVideoListResponse
{
    public List<YouTubeVideoItem> Items { get; set; }
}

public class YouTubeVideoItem
{
    public YouTubeVideoSnippet Snippet { get; set; }
}

public class YouTubeVideoSnippet
{
    public string ChannelTitle { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
