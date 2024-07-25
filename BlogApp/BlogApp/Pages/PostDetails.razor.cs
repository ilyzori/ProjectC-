using Microsoft.AspNetCore.Components;
using BlogApp.Models;
using BlogApp.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlogApp.Pages
{
    public class PostDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        protected Post post;

        [Inject]
        protected IHttpClientFactory HttpClientFactory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var client = HttpClientFactory.CreateClient("BlogAppHttpClient");
            post = await client.GetFromJsonAsync<Post>($"api/posts/{Id}");
        }
    }
}
