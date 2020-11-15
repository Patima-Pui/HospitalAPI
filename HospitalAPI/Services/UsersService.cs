using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IUserService
    {
        string SelectData();
    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
    public string SelectData()
    {
        string result = _userRepository.SelectDataIntoDB();
        return result;
    }
}


}