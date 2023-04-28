using Data.Dto;
using Data.Models;
using MongoDB;

namespace Business.Business
{
    public class ManagerBusiness
    {
        private MongoDBService<Manager> _managerService;

        public ManagerBusiness()
        {
            _managerService = new MongoDBService<Manager>();
        }

        public List<Manager> Get()
        {
            return _managerService.GetAll();
        }

        public Manager Get(string id)
        {
            return _managerService.Get(x => x.Id == id);
        }

        public Manager Get(string managerId, bool isManager)
        {
            return _managerService.Get(x => x.ManagerId == managerId);
        }

        public ManagerDto GetDto(string id)
        {
            Manager manager = Get(id);
            return new ManagerDto()
            {
                Email = manager.Email,
                Name = manager.Name,
                Password = manager.Password,
                ManagerId = manager.ManagerId,
            };
        }

        public ManagerDto GetDto(LoginDto loginDto)
        {
            Manager manager = _managerService.Get(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
            if (manager == null)
                return null;
            return new ManagerDto()
            {
                Name = manager.Name,
                Email = manager.Email,
                Password = manager.Password,
                ManagerId = manager.ManagerId
            };
        }

        public void Add(ManagerDto managerDto)
        {
            Manager manager = new Manager()
            {
                Name = managerDto.Name,
                Password = managerDto.Password,
                Email = managerDto.Email,
                ManagerId = managerDto.ManagerId,
            };
            _managerService.Add(manager);
        }

        public void Update(ManagerDto managerDto)
        {
            Manager manager = Get(managerDto.ManagerId, true);
            manager.Name = managerDto.Name;
            manager.Email = managerDto.Email;
            manager.Name = managerDto.Name;
            manager.Password = managerDto.Password;

            _managerService.Update(x => x.Id == manager.Id, manager);
        }

        public void Delete(string id)
        {
            _managerService.Delete(x => x.Id == id);
        }
    }
}
