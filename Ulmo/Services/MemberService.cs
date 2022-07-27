using Microsoft.EntityFrameworkCore;
using Ulmo.Core.Entities;
using Ulmo.Core.UoW;
using Ulmo.Models;

namespace Ulmo.Services
{
    public class MemberService : IMemberService
    {

        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MemberDto>> GetAll()
        {
            var memberRepository = _unitOfWork.GetRepository<Member>();
            return await memberRepository.GetAll()
                .Select(p => new MemberDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Surname = p.Surname,
                    FullName = string.Join(" ", p.Name, p.Surname)
                })
                .ToListAsync();
        }

        public async Task<MemberDto> GetById(int id)
        {
            var memberRepository = _unitOfWork.GetRepository<Member>();

            var member = await memberRepository.GetById(id);

            return new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Surname = member.Surname,
                FullName = string.Join(" ", member.Name, member.Surname)
            };
        }

        public async Task<bool> Create(CreateMemberDto memberDto)
        {
            bool result = false;

            try
            {
                if (memberDto != null)
                {
                    var member = new Member { Name = memberDto.Name, Surname = memberDto.Surname };
                    var memberRepository = _unitOfWork.GetRepository<Member>();
                    await memberRepository.Create(member);
                    _unitOfWork.Commit();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<bool> Update(UpdateMemberDto memberDto)
        {
            bool result = false;

            try
            {
                if (memberDto != null)
                {
                    var memberRepository = _unitOfWork.GetRepository<Member>();
                    var member = await memberRepository.GetById(memberDto.Id);
                    member.Name = memberDto.Name;
                    member.Surname = memberDto.Surname;
                    await memberRepository.Update(member);
                    _unitOfWork.Commit();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
