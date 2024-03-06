namespace ScoreSphere.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
  public string Notifications {get; set;}
  public User(int id, string email, string password, string name, string photo, int points, string achievements, int matchesAttended, string notifications)
  {
    this.Id = id;
    this.Email = email;
    this.Password = password;
    this.Name = name;
    this.Photo = photo;
    this.Points = points;
    this.Achievements = achievements;
    this.MatchesAttended = matchesAttended;
    this.Notifications = notifications;
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

  public void AwardAchievement(string achievement)
  {
    // Check if the achievement has already been achieved
    if (this.Achievements.Contains(achievement))
    {
      // If the achievement is already achieved.
    } else {

      this.Points += 50;
      AddNotification("50",true);
      this.Achievements = this.Achievements + '"' + achievement + "\",\"";
      AddNotification(achievement, false, true);
    }
  }

  public void AddNotification(string notification, bool isPoints=false, bool isAchievement=false)
  {
    // Check if the notification has already been added.
    if (isPoints){
      this.Notifications += $"Points awarded: {notification}🏅,";
    } else if (isAchievement){
      this.Notifications += $"Task completed: {notification},";
    } else {
    this.Notifications += $"{notification},";
    }
  }

  public string GetNumberOfNotifications()
  {
    
    if (this.Notifications == null)
    {
        return "0";
    }

    string[] splitNotis = this.Notifications.Split(',');

    switch (splitNotis.Length - 1)
    {
        case var count when count < 10:
            return $"{count}";
        case var count when count > 9:
            return $"9+";
        default:
            // Any unexpected errors
            return "0";
    }
  }

  public string[] GetNotificationsList()
  {
    string[] splitNotis = this.Notifications.Split(',');

    return splitNotis;
  }

  public List<String> GetAchievements() 
  {
    List<String> Achievements = new();
    Achievements.Add("HOSTED A MATCH🤵");
    Achievements.Add("ADDED A PROFILE PICTURE🖼️");
    Achievements.Add("ATTENDED A MATCH🏟️");
    Achievements.Add("CHANGED NAME📇");


    //checking users achievements
    List<String> UserAchievements = new List<String>();
    foreach (string achievement in Achievements)
    {
        if (this.Achievements.Contains(achievement)) 
        {
          UserAchievements.Add(achievement);
        }
    }
    if (UserAchievements.Count <= 0) {
      UserAchievements.Add("NO ACHIEVEMENTS ❌");
      } else if (UserAchievements.Count >= 2 && UserAchievements.Contains("NO ACHIEVEMENTS ❌")){
        UserAchievements.Remove("NO ACHIEVEMENTS ❌");
      }

    return UserAchievements;
  }

  public User()
  {

  }
}