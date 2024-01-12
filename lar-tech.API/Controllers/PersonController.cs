using lar_tech.Data.Database;
using lar_tech.Data.Repositories;
using lar_tech.Domain.Filters;
using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace lar_tech.API.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //Using a personalized repository
        private PersonRelationalRepository<Person> _repository;

        public PersonController(ApplicationDbContext dbContext)
        {
            _repository = new PersonRelationalRepository<Person>(dbContext);
        }


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

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Person>> GetPersonById([FromRoute] string id)
        {
            //Get by the id and if do not find returns NotFound, else return Ok and the object
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        //Uses a custom PaginatedRequest that have the person filter
        [HttpGet("paginated")]
        public async Task<ActionResult<Person>> GetPeoplePaginated([FromQuery] PaginatedPersonRequest paginatedRequest)
        {
            //Get a paginated result
            var result = await _repository.GetPaginatedAndFiltered(paginatedRequest);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddPerson([FromBody] PersonViewModel personVM)
        {
            //Tries to add a new person to the DB, if succedes returns CreatedAtRoute else it returns BadRequest
            Person newPerson = new Person(personVM);
            if (!newPerson.VerifyPhoneNumbers()) return BadRequest("Number provided is not a number");
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
