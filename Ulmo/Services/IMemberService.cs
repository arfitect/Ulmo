using Ulmo.Core.Entities;
using Ulmo.Models;

namespace Ulmo.Services
{
    public interface IMemberService
    {
        Task<List<MemberDto>> GetAll();

        Task<MemberDto> GetById(int id);

        Task<bool> Create(CreateMemberDto memberDto);

        Task<bool> Update(UpdateMemberDto memberDto);
    }
}