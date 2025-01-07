using System.ComponentModel.DataAnnotations;

namespace Estore.Datamodel
{
    public class Lookup
    {
        [Key]
        public int LookupId { get; set; }
        public string? LookupType { get; set; }
        public string? LookupData { get; set; }
        public string? LookupIcon { get; set; }
        public bool IsActive { get; set; }
    }
}
