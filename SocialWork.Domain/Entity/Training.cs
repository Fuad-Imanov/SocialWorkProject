namespace SocialWork.Domain.Entity;

public class Training
{
    public  int id { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public decimal? price { get; set; }
    public string? imageUrl { get; set; }
    public DateTime? startDate { get; set; }
}
//video elave olunmalidi