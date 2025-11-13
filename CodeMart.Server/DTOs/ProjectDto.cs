using CodeMart.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeMart.Server.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProjectUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string PrimaryLanguage { get; set; }
        public List<string>? SecondaryLanguages { get; set; } = new List<string>();
    }
}
