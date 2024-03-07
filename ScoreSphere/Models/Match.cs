namespace ScoreSphere.Models;
using System.ComponentModel.DataAnnotations;
using ScoreSphere.Migrations;

public class Match
{
  [Key]
  public int Id { get; set; }
  public DateTime? Date { get; set; }
  public int? Team1Id { get; set; }
  public int? Team2Id { get; set; }

  public string? Team1Name { get; set; }
  public string? Team2Name { get; set; }

  public string? Team1Logo { get; set; }
  public string? Team2Logo { get; set; }

  public int? Team1Goals { get; set; }
  public int? Team2Goals { get; set; }

  public int? UserId {get; set;}

  public bool? IsLive {get; set;}
  public Match(int Id, DateTime date, int t1id, int t2id, string t1name, string t2name, string t1logo, string t2logo, int t1goals, int t2goals, int userid, bool isLive)

  {
    this.Id = Id;
    this.Date = date;
    this.Team1Id = t1id;
    this.Team2Id = t2id;
    this.Team1Name = t1name;
    this.Team2Name = t2name;
    this.Team1Logo = t1logo;
    this.Team2Logo = t2logo;
    this.Team1Goals = t1goals;
    this.Team2Goals = t2goals;
    this.UserId = userid;
    this.IsLive = isLive;
  }

  public Match()
  {

  }
}