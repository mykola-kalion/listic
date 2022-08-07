using System.ComponentModel.DataAnnotations;

namespace Listonic.Resources
{
    public class CreateListResource
    {
        [Required]
        [MaxLength(30)] 
        public string Name { get; set; }
    }
}