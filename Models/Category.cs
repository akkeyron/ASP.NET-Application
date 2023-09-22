using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        // make this primary key, Id wull auto make it primary key, CategoryId same name as class
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,10,ErrorMessage = "Salah tu")]

        public int DisplayOrder { get; set; }
    }
}
