using lar_tech.Data.Repositories;
using lar_tech.Domain.Filters;
using lar_tech.Domain.Interfaces;
using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lar_tech.API.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //Using a personalized repository
        private PersonRelationalRepository<Person> _repository;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.PersonRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPeopleAsync([FromQuery] PersonFilterRequest? personFilter)
        {
            //Get all with filters
            //Initiates a result
            var result = new List<Person>();

            //If have filters get the results filterd
            if (personFilter != null) result = await _repository.GetAllFiltered(personFilter);
            else result = await _repository.GetAllAsync();

            //Returns NoContent or Ok
            if (result.Count == 0) return NoContent();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Person>> GetPersonById([FromRoute] string id)
        {
            //Get by the id and if do not find returns NotFound, else return Ok and the object
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Person>> GetPersonByCpf([FromRoute] string cpf)
        {
            //Get by the id and if do not find returns NotFound, else return Ok and the object
            long cpfnumbers = 0;
            var success = long.TryParse(cpf, out cpfnumbers);

            if (!success) return BadRequest("Not a number!");

            var result = await _repository.GetByCpfAsync(cpfnumbers);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        //Uses a custom PaginatedRequest that have the person filter
        [HttpGet("paginated")]
        public async Task<ActionResult<Person>> GetPeoplePaginated([FromQuery] PaginatedPersonRequest paginatedRequest)
        {
            //Get a paginated result
            var result = await _repository.GetPaginatedAndFiltered(paginatedRequest);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddPerson([FromBody] PersonViewModel personVM)
        {
            //Tries to add a new person to the DB, if succedes returns CreatedAtRoute else it returns BadRequest
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Validates the cpf and creates the person object
            if (personVM.PhoneNumbers.Any(pn => !pn.IsValid)) return BadRequest("Incorrect phone formats");
            Person? newPerson = Person.ValidadeAndCreateObject(personVM);

            if (newPerson == null) return BadRequest("Cpf invalid");

            try
            {
                await _repository.PostAsync(newPerson);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }
            return CreatedAtRoute("GetById", new { id = newPerson.Id }, newPerson);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdatePerson([FromBody] Person person)
        {
            //Tries to update a person in the DB, if succedes returns Ok else it returns BadRequest
            try
            {
                await _repository.PutAsync(person);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }
            return Ok();
        }

        [Authorize]
        [HttpPost("toggle/{id}")]
        public async Task<ActionResult> ToggleActiveStatus([FromRoute] string id)
        {
            //Get person by id
            var person = await _repository.GetByIdAsync(id);
            if (person == null) return BadRequest("Incorrect Id!");

            //Toggle active status
            if (person.IsActive) person.IsActive = false;
            else person.IsActive = true;

            //Save changes
            await _repository.PutAsync(person);
            return Ok();

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeletePerson([FromBody] Person person)
        {
            //Tries to delete a person in the DB, if succedes returns Ok else it returns BadRequest
            try
            {
                await _repository.DeleteAsync(person);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonById([FromRoute] string id)
        {
            //Tries to delete a person in the DB by id, if succedes returns Ok else it returns BadRequest
            try
            {
                await _repository.DeleteByIdAsync(id);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }
            return Ok();
        }

    }
}
