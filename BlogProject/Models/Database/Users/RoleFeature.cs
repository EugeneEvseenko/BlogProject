namespace BlogProject.Models.Database.Users;

[Flags]
public enum RoleFeature : int
{
    User = 0b0,
    Moderator = 0b1,
    Admin = 0b10
}