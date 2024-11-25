using Microsoft.AspNetCore.Mvc;
using Movie_Shop_.Models;
using Movie_Shop_.Repositorey;

namespace Movie_Shop_.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerServices _services;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerServices customerServices, ILogger<CustomerController> logger )
        {
            _logger =  logger;
            _services = customerServices;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCreated = _services.Create(customer);
                    if (isCreated)
                    {
                        TempData["Message"] = "created successfully!";
                        return RedirectToAction("MovieList","Movie");
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating customer. Email address: {customer?.EmailAddress}, Error: {ex.Message}");
                    TempData["Error"] = "An unexpected error occurred. Please try again later.";
                }
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit (string email)
        {
            var customer = _services.GetCustmerByEmail(email);
            return View(customer);
        }

        [HttpPost]

        public IActionResult Edit(Customer customer)
        {
          if (ModelState.IsValid)
          {
                bool isEdited = _services.Update(customer);
                if (isEdited)
                {
                    TempData["Message"] = "Updait successfull!";
                    return RedirectToAction("MovieList","Movie");
                }
                else
                {
                    TempData["Error"] = "Failed to edit . It might already be deleted or does not exist.";
                }
          }

                return View(customer);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _services.DeleteCustomer(id);

                if (isDeleted)
                {
                    TempData["Message"] = " deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Failed to delete . It might already be deleted or does not exist.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting customer with Id: {id}. Error: {ex.Message}");
                TempData["Error"] = "An unexpected error occurred. Please try again later.";
            }
            return RedirectToAction("MovieList","Movie");
        }


    }
}
