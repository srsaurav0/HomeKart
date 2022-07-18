namespace HomeKart.Models
{
    public class FilterVM
    {
        public IEnumerable<OwnerVM> PropertyTab { get; set; }
        public int Upper { get; set; }
        public int Lower { get; set; }
    }
}
