namespace DentistApp.DAL.Repository;

public interface IUserQueries
{
    Task<bool> IsEmailTakenAsync(string email);
}