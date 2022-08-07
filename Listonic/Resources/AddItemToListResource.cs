using System.ComponentModel.DataAnnotations;

namespace Listonic.Resources
{
    public class AddItemToListResource
    {
        [Required]
        [MaxLength(30)] 
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}