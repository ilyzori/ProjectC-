using Microsoft.AspNetCore.Components;
using BlogApp.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlogApp.Pages
{
    public class CreatePostBase : ComponentBase
    {
        protected Post newPost = new Post();

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IHttpClientFactory HttpClientFactory { get; set; }

        protected async Task HandleValidSubmit()
        {
            var client = HttpClientFactory.CreateClient("BlogAppHttpClient");
            var response = await client.PostAsJsonAsync("api/posts", newPost);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Console.Error.WriteLine($"Error: {response.ReasonPhrase}");
            }
        }
    }
}
