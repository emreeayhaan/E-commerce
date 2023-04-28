using Data.Dto;
using Data.Models;
using MongoDB;

namespace Business.Business
{
    public class CustomerBusiness
    {
        MongoDBService<Customer> _customerService;
        public CustomerBusiness()
        {
            _customerService = new MongoDBService<Customer>();
        }

        public List<Customer> Get()
        {
            return _customerService.GetAll();
        }

        public Customer Get(string id)
        {
            return _customerService.Get(x => x.Id == id);
        }

        public Customer Get(string customerId, bool isCustomerId)
        {
            return _customerService.Get(x => x.CustomerId == customerId);
        }

        public CustomerDto GetDto(string id)
        {
            Customer customer = _customerService.Get(x => x.Id == id);
            return new CustomerDto()
            {
                Address = customer.Address,
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                Name = customer.Name,
                Password = customer.Password,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public CustomerDto GetDto(LoginDto loginDto)
        {
            Customer customer = _customerService.Get(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
            if (customer == null)
                return null;
            return new CustomerDto()
            {
                CustomerId = customer.CustomerId,
                Password = customer.Password,
                Email = customer.Email,
                Address = customer.Address,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public void Add(CustomerDto customerDto)
        {
            Customer customer = new Customer()
            {
                CustomerId = customerDto.CustomerId,
                Address = customerDto.Address,
                Email = customerDto.Email,
                Name = customerDto.Name,
                Password = customerDto.Password,
                PhoneNumber = customerDto.PhoneNumber,
            };
            _customerService.Add(customer);
        }

        public void Update(CustomerDto customerDto)
        {
            Customer customer = Get(customerDto.CustomerId, true);
            customer.Address = customerDto.Address;
            customer.Email = customerDto.Email;
            customer.Name = customerDto.Name;
            customer.Password = customerDto.Password;
            customer.PhoneNumber = customerDto.PhoneNumber;

            _customerService.Update(x => x.Id == customer.Id, customer);
        }

        public void Delete(string id)
        {
            _customerService.Delete(x => x.Id == id);
        }
    }
}
