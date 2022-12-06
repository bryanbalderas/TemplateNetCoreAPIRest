using Agenda.Data;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            return Ok(await dbContext.contacts.ToListAsync());
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute] Guid id)
        {

            var contact = await dbContext.contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequests)
        {

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequests.Address,
                Email = addContactRequests.Email,
                Phone = addContactRequests.Phone,
                FullName = addContactRequests.FullName,
            };

            await dbContext.contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                contact.Address= updateContactRequest.Address;

                await dbContext.SaveChangesAsync();

                return Ok(contact);

            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {

            var contact = await dbContext.contacts.FindAsync(id);

            if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();

        }
    }
}
