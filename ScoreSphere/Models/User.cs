
namespace ScoreSphere.Models;
using System.ComponentModel.DataAnnotations;

using System;
using System.Text.RegularExpressions;

public class User
{

  [Key]

  public int Id { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public string Name { get; set; }
  public string Photo { get; set; }
  public int Points { get; set; }
  public string Achievements { get; set; }
  public int MatchesAttended {get; set;}

  public User(int id, string email, string password, string name, string photo, int points, string achievements, int matchesattended)


  {
    this.Id = id;
    this.Email = email;
    this.Password = password;
    this.Name = name;
    this.Photo = photo;
    this.Points = points;
    this.Achievements = achievements;
    this.MatchesAttended = matchesattended;

  }

  public bool CheckValidEmail() {
    if (this.Email == null) {return false;}
    this.Email = this.Email.ToUpper();      //email not case sensitive
    if (this.Email.Contains("@") == false) //make sure there's an @
    {
      return false;
    }

    return true;
  }

  public bool CheckValidPassword() 
  {
    int MinLength = 8;
    bool HasUppercase = false;
    bool HasNumber = false;
    bool HasSpecialCharacter = false;

    if (this.Password == null) {return false;}
    if (this.Password.Length < MinLength) {return false;}
    foreach (char character in this.Password)
        {
            if (char.IsUpper(character))
            {
                HasUppercase = true;
            }
            else if (char.IsDigit(character))
            {
                HasNumber = true;
            }
            else if (char.IsSymbol(character) || char.IsPunctuation(character))
            {
                HasSpecialCharacter = true;
            }
        }

    return HasUppercase && HasNumber && HasSpecialCharacter;
  }


  public List<String> GetAchievements() 
  {

    return new List<String>();

    public void AwardAchievement(String achievement)
    {
     this.Achievements = this.Achievements + "'" + achievement + "',";
    }

  public List<String> GetAchievements() 
  {
    List<String> Achievements = new();
    Achievements.Add("HOSTED A MATCH🤵");
    Achievements.Add("ADDED A PROFILE PICTURE🖼️");
    Achievements.Add("ATTENDED A MATCH🏟️");


    //checking users achievements
    List<String> UserAchievements = new List<String>();
    foreach (String achievement in Achievements)
    {
        if (this.Achievements.Contains(achievement)) 
        {
            UserAchievements.Add(achievement);
        }
    }
    if (UserAchievements.Count <= 0) {UserAchievements.Add("NO ACHIEVEMENTS ❌");}
    return UserAchievements;

  }

  public User()
  {

  }
}