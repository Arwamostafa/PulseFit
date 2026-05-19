namespace PulseFit.DAL.Entities;

public class MemberShip : BaseEntity
{


    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;


    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;
}

