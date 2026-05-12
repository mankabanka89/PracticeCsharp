using System.ComponentModel.DataAnnotations;

namespace CRMApp.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}