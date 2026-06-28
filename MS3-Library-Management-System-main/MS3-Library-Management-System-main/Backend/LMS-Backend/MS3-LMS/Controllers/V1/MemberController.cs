using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MS3_LMS.Enity.Core;
using MS3_LMS.Enity.User;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;
using MS3_LMS.Service;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberservice;

        public MemberController(IMemberService memberservice)
        {
            _memberservice = memberservice;
        }

        //[HttpPost("NewMember")]
        //public async Task<ActionResult> CreateNewMember(MemberRequestModel memberRequestModel)
        //{
        //    await _memberservice.CreateNewUser(memberRequestModel);

            
        //    return CreatedAtAction(nameof(CreateNewMember), new { id = memberRequestModel.Nic }, memberRequestModel);
        //}
        
        [HttpPost]
        [Route("new-member")]
        public async Task<ActionResult> CreateNewMember(MemberRequestModel memberRequestModel)
        {
             var data= await _memberservice.NewMemeber(memberRequestModel);
            return Ok(data);

          
        }


        [HttpGet]
        [Route("Get Member by id")]
        public async Task<IActionResult>getAllMembers(Guid memberid)
        {
            try
            {
                var member = await _memberservice.GetMemberByNic(memberid);
                if(member == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("Get all members")]
        public async Task<IActionResult> RetriveAllmembers()
        {
            try
            {
                var data = await _memberservice.GetAllMembers();
                if(data == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get member by id based state")]
        public async Task<IActionResult>GetRecordsByid(Guid id,BookLend.State state)
        {
            var data=await _memberservice.GetRecordsById(id,state);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateMember(Guid id,MemberResponse model) 
        {
            var data=await _memberservice.EditMember(id,model);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteMember(Guid id)
        {
            await _memberservice.DeleteMember(id);
            return true;
        }

        [HttpPut("update isverify")]
        public async Task<IActionResult>UpdateIsverifyState(Guid MemberId,bool isverify)
        {
            try
            {
              var data=  await _memberservice.UpdateIsverify(MemberId, isverify);
                return Ok(new { message = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("userpassword/{userid}")]
        public async Task<IActionResult>changePassword(Guid userid,[FromBody]string password)
        {
            try
            {
                var data=await _memberservice.updatePAssword(userid,password);
                if (!data)
                {
                    return BadRequest("password did not change");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
