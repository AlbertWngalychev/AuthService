using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using AutoMapper;
using DataBaseService.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AuthController(
            ILoggerManager logger,
            IRepository repository,
            IMapper mapper
            )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("guid/{guid}")]
        [Tags("GET")]
        public async Task<ActionResult<AuthReadDto>> GetUser(Guid guid)
        {
            Auth? temp = await _repository.GetUser(guid);

            if (temp == null) return NotFound();
            return Ok(_mapper.Map<AuthReadDto>(temp));
        }

        [HttpGet("{username}")]
        [Tags("GET")]
        public async Task<ActionResult<AuthReadDto>> GetUser(string username)
        {
            _logger.WriteError("test");
            Auth? temp = await _repository.GetUser(username);

            if (temp == null) return NotFound();
            return Ok(_mapper.Map<AuthReadDto>(temp));
        }

        [HttpPost("auth")]
        [Tags("AUTH")]
        public async Task<ActionResult> Auth([FromBody] UsernamePasswordParams upp)
        {
            Auth? temp = await _repository.GetUser(upp.Name);
            if (temp==null)
            {
                return NotFound();
            }
            return upp.Password.CreatePass(temp.Salt).Equals(temp.HashedPass) ? Ok() : BadRequest();
        }

        [HttpPost]
        [Tags("Create")]
        public async Task<ActionResult<AuthReadDto>> Create([FromBody] UsernamePasswordParams upp)
        {
            try
            {
                var temp = await _repository.CreateUser(upp.Name, upp.Password);
                await _repository.SaveChangedAsync();
                return Ok(_mapper.Map<AuthReadDto>(temp));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.WriteError($"{ex.Message}. {ex.Source}. {ex.StackTrace}.");
                return StatusCode(500);
            }

        }

        [HttpDelete("{username}")]
        [Tags("Delete")]
        public async Task<ActionResult> Delete(string username)
        {
            try
            {
                await _repository.DeleteUser(username);
                await _repository.SaveChangedAsync();
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch(Exception ex)
            {
                _logger.WriteError($"{ex.Message}. {ex.Source}. {ex.StackTrace}.");
                return StatusCode(500);
            }

        }

        [HttpDelete("guid/{guid}")]
        [Tags("Delete")]
        public async Task<ActionResult> Delete(Guid guid)
        {
            try
            {
                await _repository.DeleteUser(guid);
                await _repository.SaveChangedAsync();
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.WriteError($"{ex.Message}. {ex.Source}. {ex.StackTrace}.");
                return StatusCode(500);
            }

        }

        [HttpPut("{username}")]
        [Tags("Update")]
        public async Task<ActionResult> UpdatePassword(string username,[FromBody]string newPass)
        {
            try
            {
                await _repository.ChangePassword(username, newPass);
                await _repository.SaveChangedAsync();
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                _logger.WriteError($"{ex.Message}. {ex.Source}. {ex.StackTrace}.");
                return StatusCode(500);
            }
        }

        [HttpPut("guid/{guid}")]
        [Tags("Update")]
        public async Task<ActionResult> UpdatePassword(Guid guid, [FromBody] string newPass)
        {
            try
            {
                await _repository.ChangePassword(guid, newPass);
                await _repository.SaveChangedAsync();
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.WriteError($"{ex.Message}. {ex.Source}. {ex.StackTrace}.");
                return StatusCode(500);
            }
        }
    }
}
