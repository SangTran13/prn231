using Application.Mappings;
using Application.Members.Commands;
using Application.Members.Queries;
using Application.Members.Responses;
using eStoreAPI.Mapping;
using eStoreAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : BaseController<MemberController>
    {
        public MemberController(
           IMediator mediator,
           ILogger<MemberController> logger
       ) : base(mediator, logger) { }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<MemberResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMembers()
        {
            var query = new GetAllMembersQuery();
            return await ExecuteAsync<GetAllMembersQuery, List<MemberResponse>>(query);
        }

        [HttpGet("{memberId}")]
        [ProducesResponseType(typeof(ApiResponse<MemberResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMemberById(int memberId)
        {
            var query = new GetMemberByIdQuery(memberId);
            return await ExecuteAsync<GetMemberByIdQuery, MemberResponse>(query);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = (int)Shared.Constants.StatusCode.ModelInvalid,
                    Message = ResponseMessages.GetMessage(Shared.Constants.StatusCode.ModelInvalid),
                    Errors = ["The request body does not contain required fields"]
                });
            }
            var command = AppMapper<ModelMapping>.Mapper.Map<CreateMemberCommand>(request);
            return await ExecuteAsync<CreateMemberCommand, int>(command);
        }

        [HttpPut("{memberId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMember(int memberId, [FromBody] UpdateMemberRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = (int)Shared.Constants.StatusCode.ModelInvalid,
                    Message = ResponseMessages.GetMessage(Shared.Constants.StatusCode.ModelInvalid),
                    Errors = ["The request body does not contain required fields"]
                });
            }
            var command = AppMapper<ModelMapping>.Mapper.Map<UpdateMemberCommand>(request);
            command.MemberId = memberId;
            return await ExecuteAsync<UpdateMemberCommand, Unit>(command);
        }

        [HttpDelete("{memberId}")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            var command = new DeleteMemberCommand(memberId);
            return await ExecuteAsync<DeleteMemberCommand, Unit>(command);
        }

    }
}
