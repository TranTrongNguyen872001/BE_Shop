using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
    [Route("/api/chat")]
    public class ChatController : BaseController
    {
        /// <summary>
        /// Thêm tin nhắn
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
        [HttpPost]
		[ProducesResponseType(typeof(OutputAddChatline), 200)]
        public async Task<IActionResult> Add([FromBody] AddChatline input)
        {
            input.SendedUser = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
            return await QueryCheck<OutputAddChatline>(input);
        }
        /// <summary>
        /// Lấy danh sách nhóm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("list")]
		[ProducesResponseType(typeof(OutputGetallRoom), 200)]
        public async Task<IActionResult> GetAll([FromBody] GetallRoom input)
        {
            return await QueryCheck<OutputGetallRoom>(input);
        }
        /// <summary>
        /// Lấy danh sách tin nhắn theo nhóm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
        [HttpPost("message")]
		[ProducesResponseType(typeof(OutputGetallChatline), 200)]
        public async Task<IActionResult> GetAllChatline([FromBody] GetallChatline input)
        {
            return await QueryCheck<OutputGetallChatline>(input);
        }
        /// <summary>
        /// Lấy danh sách tin nhắn theo nhóm của bản thân
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Member")]
        [HttpPost("mine")]
		[ProducesResponseType(typeof(OutputGetallChatline), 200)]
        public async Task<IActionResult> GetAllChatlineMine([FromBody] GetallChatline input)
        {
            input.Id = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
            return await QueryCheck<OutputGetallChatline>(input);
        }
    }
}
