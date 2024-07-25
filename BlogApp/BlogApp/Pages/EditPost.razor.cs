using BlogApp.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlogApp.Pages
{
    public partial class EditPost : ComponentBase, IDisposable
    {
        [Parameter]
        public int id { get; set; }

        private Post post = new Post();
        private MudForm form;
        private bool disposed = false;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IHttpClientFactory HttpClientFactory { get; set; }

        private HttpClient Http => HttpClientFactory.CreateClient("BlogAppHttpClient");

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Console.WriteLine($"Fetching post with id: {id}");
                post = await Http.GetFromJsonAsync<Post>($"api/posts/{id}");
                Console.WriteLine($"Fetched post: {post.Title}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching post: {ex.Message}");
            }
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                Console.WriteLine($"Submitting post with id: {post.Id}");
                var response = await Http.PutAsJsonAsync($"api/posts/{post.Id}", post);
                Console.WriteLine($"Response status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post updated successfully.");
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    Console.WriteLine("Failed to update post.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating post: {ex.Message}");
            }
        }

        private void CancelEdit()
        {
            NavigationManager.NavigateTo("/");
        }

        public void Dispose()
        {
            disposed = true;
        }
    }
}
