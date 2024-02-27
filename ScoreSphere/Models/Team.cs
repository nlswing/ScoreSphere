namespace ScoreSphere.Models;
using System.ComponentModel.DataAnnotations;

public class Team
{
  [Key]
  public int Id {get; set;}
  public string? Name {get; set;}
  public string? Logo {get; set;}
  public Team(int ID, string name, string logo) {
    this.Id = ID;
    this.Name = name;
    this.Logo = logo;
  }

  public Team() {
    
  }
}