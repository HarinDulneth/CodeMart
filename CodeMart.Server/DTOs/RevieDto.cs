using CodeMart.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeMart.Server.DTOs
{
    public class RevieDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public int Rating { get; set; }
    }
}
