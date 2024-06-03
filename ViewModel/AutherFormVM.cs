using System.ComponentModel.DataAnnotations;

namespace bookStore.ViewModel
{
    public class AutherFormVM
    {
        public int? Id { get; set; }
        [MaxLength(20 , ErrorMessage ="cannot exeed 20 chars")]
        public string Name { get; set; }
    }
}
