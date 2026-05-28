using PulseFit.DAL.Enums;

namespace PulseFit.BLL.ModelViews
{
    public class MemberModelView
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Photo { get; set; }
        public string Email { get; set; } = default!;
        public Gender Gender { get; set; }
    }
}
