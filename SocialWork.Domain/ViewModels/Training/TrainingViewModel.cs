using Microsoft.AspNetCore.Http;

namespace SocialWork.Domain.ViewModels.Training;

public class TrainingViewModel
{
    public  int id { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public decimal? price { get; set; }
    public DateTime? startDate { get; set; }
    public string? imageUrl { get; set; }
    public IFormFile? image { get; set; }
    
}