using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using bookingApi2BusinessLogic.Dto;
using bookingApi2BusinessLogic.Interfaces;
namespace bookingApi3WebApiservices.Controllers
{
    /*
    Cette class permet gestioner le publication aux services et les communiquer avec le client React
    */
    [ApiController]
    [Route("[Controller]")]
    public class BookinApiController : ControllerBase
    {
        //Le facade pour le access à chaque fontionalite detaille
        private readonly IUnitOfWork _unitOfWork;
        //Le constructor pour permet le depedency injection et le trasient 
        public BookinApiController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        //Validation d'access d'utilisateur
        [HttpPost("login")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> ValidateLogin([FromBody] ClientDto dto)
        {
            try
            {
                //le methode login permet faire la validation d'utilisateur
                var result = await _unitOfWork.Clients.Login(dto);
                //si le resultat etait vrai donc retourner le code 200
                if (result)
                    return Ok(result);
                //si le resultat etait faux donc retourner le code 404
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace);
            }
        }
        [HttpPost("createUser")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> CreateUser([FromBody] ClientDto dto)
        {
            try
            {
                //l'invocation avec le facade vers l'entity client
                var result = await _unitOfWork.Clients.CreateUser(dto);
                //si le resultat etait vrai donc retourner le code 200
                if (result)
                    return Ok(result);
                else
                //si le resultat etait faux donc retourner le code 404
                    return NotFound();
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message+" "+ex.Message+" "+ex.InnerException);
            }
        }
    }
}