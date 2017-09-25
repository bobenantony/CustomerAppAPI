using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagerAPI.Model;
using CustomerManagerAPI.Repository;
using Microsoft.AspNetCore.Cors;

namespace CustomerManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet()]
        [Route("GetCustomers")]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetCustomers().OrderBy(c=>c.Id);
            var customersList = customers.ToList<Customer>();
            var totalRecords = customersList.Count();
            //HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            //return Request.CreateResponse(HttpStatusCode.OK, customers);
            return Ok(customersList);
        }

        [HttpGet]
        [Route("GetStates")]
        public IActionResult GetStates()
        {
            var states = _customerRepository.GetStates();
            //return Request.CreateResponse(HttpStatusCode.OK, states);
            return Ok(states);
        }

        [HttpPost]
        public IActionResult Login([FromBody]UserLogin userLogin)
        {
            //Simulated login
            return Ok(new { status = true });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            //Simulated logout
            return Ok(new { status = true });
        }

        [HttpGet]
        [Route("GetCustomersSummary")]
        public IActionResult GetCustomersSummary()
        {
            int totalRecords;
            var custSummary = _customerRepository.GetCustomersSummary(out totalRecords);
            var custSummaryList = custSummary.ToList().OrderBy(c=>c.Id);
            //HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            //return Request.CreateResponse(HttpStatusCode.OK, custSummary);
            return Ok(custSummaryList);
        }

        //[HttpGet]
        //public IActionResult CheckUnique(int id, string property, string value)
        //{
        //    var opStatus = _customerRepository.CheckUnique(id, property, value);
        //    //return Request.CreateResponse(HttpStatusCode.OK, opStatus);
        //    return Ok(opStatus);
        //}

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult CustomerById(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            // return Request.CreateResponse(HttpStatusCode.OK, customer);
            return Ok(customer);
        }

        //POST api/<controller>
        public IActionResult PostCustomer([FromBody]Customer customer)
        {
            var opStatus = _customerRepository.InsertCustomer(customer);
            if (opStatus.Status)
            {
                //var response = Request.CreateResponse<Customer>(HttpStatusCode.Created, customer);
                //string uri = Url.Link("DefaultApi", new { id = customer.Id });
                //response.Headers.Location = new Uri(uri);
                //return response;
                return Ok(customer);
            }
            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
            return NotFound(opStatus.ExceptionMessage);
        }

        // PUT api/<controller>/5
        public IActionResult PutCustomer(int id, [FromBody]Customer customer)
        {
            var opStatus = _customerRepository.UpdateCustomer(customer);
            if (opStatus.Status)
            {
                return Ok(customer);
            }
            //return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
            return NotFound(opStatus.ExceptionMessage);
        }

        // DELETE api/<controller>/5
        public IActionResult DeleteCustomer(int id)
        {
            var opStatus = _customerRepository.DeleteCustomer(id);

            if (opStatus.Status)
            {
                //return Request.CreateResponse(HttpStatusCode.OK);
                return Ok();
            }
            else
            {
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
                return NotFound(opStatus.ExceptionMessage);
            }
        }
    }
}
