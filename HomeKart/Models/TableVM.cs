namespace HomeKart.Models
{
    public class TableVM
    {
        public IEnumerable<AdminVM> AdminTab { get; set; }
        public IEnumerable<RegisterVM> UserTab { get; set; }
        public IEnumerable<OwnerVM> PropertyTab { get; set; }
    }
}
