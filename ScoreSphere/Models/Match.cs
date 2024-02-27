namespace ScoreSphere.Models;
using System.ComponentModel.DataAnnotations;

public class Match
{
  [Key]
  public int Id {get; set;}
  public DateTime? Date { get; set; }
  public int? Team1ID {get; set;}
  public int? Team2ID {get; set;}
  public int? Team1Goals {get; set;}
  public int? Team2Goals {get; set;}
  public string? Team1Logo { get; set; }
public string? Team2Logo { get; set; }
  public Match(int ID, DateTime date, int t1id, int t2id, int t1goals, int t2goals, string t1logo, string t2logo) {
    this.Id = ID;
    this.Date = date;
    this.Team1ID = t1id;
    this.Team2ID = t2id;
    this.Team1Goals = t1goals;
    this.Team2Goals = t2goals;
    this.Team1Logo = t1logo;
    this.Team2Logo = t2logo;
  }

  public Match() {
    
  }
}