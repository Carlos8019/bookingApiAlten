using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using bookingApi2BusinessLogic.Dto;
using bookingApi2BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        //Iconfiguration permet obtenir les parameters du fichier appsetting.json
        private readonly IConfiguration _configuration;
        //obtenir les jours maximun pour la disponibilite
        private readonly int _maxDays;        
        //obtenir le maximun des jours pour chaque reservation
        private readonly int _maxReservation;
        
        //logger pour debuguer l'API
        private readonly ILogger _logger;
        //Le constructor pour permet le depedency injection et le trasient 
        public BookinApiController(IUnitOfWork unitOfWork,IConfiguration configuration,ILoggerFactory logger)
        {
            this._configuration=configuration;
            this._unitOfWork = unitOfWork;
            _logger=logger.CreateLogger("ControllerBooking");
            //obtenir les jours du fichier appsetting.json
            try
            {
                int.TryParse(_configuration.GetSection("ParameterDays:MaxDays").Value,out var days);
                this._maxDays=days;
                int.TryParse(_configuration.GetSection("ParameterDays:MaxReservation").Value,out var daysReservation);
                this._maxReservation=daysReservation;
            }
            catch (Exception)//si existe quelque erreur initialiser le tout en 0
            {
                this._maxReservation=0;
                this._maxDays=0;

            }
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
                    return Ok(1);
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
                    return Ok(1);
                else
                    //si le resultat etait faux donc retourner le code 404
                    return NotFound();
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }
        }

        //enregistrer une nouvelle reservation
        [HttpPost("newReservation")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> NewReservation([FromBody] ReservationDto dto)
        {
            try
            {
                //Validation des dates qui sont deja dans les reservations
                if (await _unitOfWork.Reservations.ValidateDatesReservation(dto,this._maxReservation,this._maxDays))
                {
                    //createEntity avec l'flag false pour l'enregistrement
                    var entity = await _unitOfWork.Reservations.CreateEntity(dto, false);
                    if (entity != null)
                    {
                        var insert = await _unitOfWork.Reservations.CreateReservation(entity);
                        if (insert)//retourner 1 si l'enregistrement est correct
                            return Ok(1);
                        else//retourner 0 si l'enregistrement n'est pas correct
                            return Ok(2);
                    }
                    //retourner 3 si le rooms ou le client n'existe pas
                    return Ok(3);
                }
                else //Le code qui es retourne nous dit que l'intervalle deja existe ou que il est plus grand
                    return Ok(_unitOfWork.Reservations.codeValidation);
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }

        }

        //modifier une reservation
        [HttpPost("updateReservation")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> ModifyReservation([FromBody] ReservationDto dto)
        {
            try
            {
                //Validation des dates qui sont deja dans les reservations
                if (await _unitOfWork.Reservations.ValidateDatesReservation(dto,this._maxReservation,this._maxDays))
                {
                    //createEntity avec l'flag true pour le modifier
                    var entity = await _unitOfWork.Reservations.CreateEntity(dto, true);
                    if (entity != null)
                    {
                        var update = await _unitOfWork.Reservations.UpdateReservation(entity);
                        if (update)//retourner 1 si la modification est correcte
                            return Ok(1);
                        else//retourner 0 si si la modification n'est pas correcte
                            return Ok(2);
                    }
                    //retourner 3 si la reservation n'existe pas
                    return Ok(3);

                }
                else //Le code qui es retourne nous dit que l'intervalle deja existe ou que il est plus grand
                    return Ok(_unitOfWork.Reservations.codeValidation);
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }

        }

        //supprimer une reservation
        [HttpPost("deleteReservation")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> DeleteReservation([FromBody] ReservationDto dto)
        {
            try
            {
                    //createEntity avec l'flag true pour le supprimer
                    var entity = await _unitOfWork.Reservations.CreateEntity(dto, true);
                    if (entity != null)
                    {
                        var update = await _unitOfWork.Reservations.DeleteReservation(entity);
                        if (update)//retourner 1 si la suppression est correcte
                            return Ok(1);
                        else//retourner 0 si si la suppression n'est pas correcte
                            return Ok(2);
                    }
                    //retourner 3 si la reservation n'existe pas
                    return Ok(3);
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }

        }
        //obtenir le liste des reservations pour client
        [HttpGet("getReservations/{email}")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> GetReservationsByClient(string email)
        {
            try
            {
                var result=await _unitOfWork.Reservations.GetReservationsByClient(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }
        }
        //Modifier le calendried de disponiblite
        [HttpPost("updateCalendar")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> UpdateCalendar([FromBody] ReservationDto dto)
        {
            try
            {
                //convertir le dto a une Entity
                var entity=await _unitOfWork.Reservations.CreateEntity(dto,false);
                var result=await _unitOfWork.Calendars.UpdateAvailability(entity,0);
                if(result)
                    return Ok(1);
                else
                    return NotFound(0);
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }
        }


        //obtenir le liste des dates disponibles avec la quantite des jours qui est une parameter
        //du fichier appsetting.json
        [HttpGet("getAvailability")]
        //definir le code d'etat de chaque response
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        //definir le services comme asynchrone pour prende en charge la concurrence
        public async Task<IActionResult> GetAvailability()
        {
            try
            {
                _logger.LogInformation("Days from appsetting.json: "+this._maxDays.ToString());
                var result=await _unitOfWork.Calendars.GetAvailability(this._maxDays);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //s'il existe quelque errour, retourner le code 400
                return BadRequest(ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }
        }

        
    }
}