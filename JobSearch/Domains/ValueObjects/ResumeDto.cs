
namespace JobSearch.Domains.ValueObjects;
public class ResumeDto
{
    public int UserId { get; set; }
    public required string Title { get; set; }
    public required string Education { get; set; }
    public required string Experience { get; set; }
}