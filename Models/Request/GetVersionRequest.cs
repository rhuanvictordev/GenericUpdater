using System.ComponentModel.DataAnnotations;

namespace AtualizadorGenerico.Models.Request
{
    public class GetVersionRequest
    {
        [Required]
        public string AppKeyName { get; set; }
    }
}
