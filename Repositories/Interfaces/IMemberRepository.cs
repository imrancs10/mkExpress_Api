using MKExpress.API.Models;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IMemberRepository:ICrudRepository<Member>
    {
        Task<bool> ActivateDeactivateMember(int memberId);
        Task<bool> ChangePassword(int memberId);
        Task<bool> ChnageRole(int memberId);
    }
}
