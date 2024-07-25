using Microsoft.AspNetCore.Components;
using BlogApp.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Routing;

namespace BlogApp.Pages
{
    public class IndexBase : ComponentBase
    {
        protected List<Post> posts;

        [Inject]
        protected IHttpClientFactory HttpClientFactory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPostsAsync();
        }

        protected async Task LoadPostsAsync()
        {
            var client = HttpClientFactory.CreateClient("BlogAppHttpClient");
            try
            {
                posts = await client.GetFromJsonAsync<List<Post>>("api/posts");
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Request error: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Console.Error.WriteLine($"The content type is not supported: {ex.Message}");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.Error.WriteLine($"Invalid JSON: {ex.Message}");
            }
        }

        protected async Task DeletePost(int id)
        {
            var client = HttpClientFactory.CreateClient("BlogAppHttpClient");
            await client.DeleteAsync($"api/posts/{id}");
            await LoadPostsAsync();
        }
    }
}
