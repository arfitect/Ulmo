using Microsoft.AspNetCore.Mvc;
using Ulmo.Models;
using Ulmo.Services;

namespace Ulmo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemberController : ControllerBase
    {

        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<List<MemberDto>> GetAll()
        {
            return await _memberService.GetAll(); ;
        }

        [HttpGet("{id}")]
        public async Task<MemberDto> GetById(int id)
        {
            return await _memberService.GetById(id); ;
        }

        [HttpPost]
        public async Task Create(CreateMemberDto memberDto)
        {
            await _memberService.Create(memberDto);
        }

        [HttpPost]
        public async Task Update(UpdateMemberDto memberDto)
        {
            await _memberService.Update(memberDto);
        }
    }
}