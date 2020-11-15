using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IUserService
    {
        string SelectData();
        ResposeModel ValidateLogin(RequestLogin item);
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

    public ResposeModel ValidateLogin(RequestLogin item) {
        ResposeModel response = new ResposeModel();

        var result = _userRepository.SelectDataFromUsernamePassword(item);

        if (result == ""){
            response.Success = false;
        } else {
            response.Success = true;
        }

        return response;
    }
}


}