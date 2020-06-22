using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using setVideo.Model;
using setVideo.Repository;

namespace setVideo.Service
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ILogger<ICustomerServices> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ILogger<ICustomerServices> logger,
                          ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public string Add(Customer customer)
        {
            string ret = "";
            try
            {
               
                Customer user = _customerRepository.getByCpf(customer.cpf);
                if (user == null) {
                    _customerRepository.Add(customer);
                    _customerRepository.Commit();
                    ret = "Usuário inserido com sucesso com id : " + customer.Id;
                } else
                {
                    ret = "Usuário ja existe na base !";
                }
                
                return ret;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adcionar Usuário");
                throw;
            }
        }

        public Customer GetById(int id)
        {
            try
            {
               return _customerRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                Customer customer = _customerRepository.GetById(id);
                customer.active = false;
                _customerRepository.Update(customer);

            }
            catch (Exception ex)
            {

            }
        }
        public IEnumerable<Customer> GetAll()
        {
            try
            {
                return _customerRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro@");
                throw;
            }
        }

    }

    public interface ICustomerServices
    {
        string Add(Customer customer);
        Customer GetById(int id);
        IEnumerable<Customer> GetAll();
        void DeleteUser(int id);
    }
}
