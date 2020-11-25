using System;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IUserService
    {
        string SelectData();
        ResposeModel ValidateLogin(RequestLogin item);
        ResposeModel UserProfile(UserProfileModel item);
        ResposeModel UpdateUserProfile(UserProfileModel item);
        UserModelList SelectUsers();
        UserModelList SelectUserList(UserModelRequest requestSerach);
        UserProfileModel SelectIndividual(UserRequestIdModel requestId);
        DropdownDepartmentListModel DepartmentList();

    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string SelectData()
        {
            string result = _userRepository.SelectDataIntoDB();
            return result;
        }
        public UserModelList SelectUsers()
        {
            UserModelList result = _userRepository.SelectUsersAll();
            return result;
        }
          public UserModelList SelectUserList(UserModelRequest requestSerach)
        {
            UserModelList result = _userRepository.QueryUsers(requestSerach);
            return result;
        }
        public ResposeModel ValidateLogin(RequestLogin item)
        {
            ResposeModel response = new ResposeModel();

            if (item.Username != "" && item.Password != "")
            {
                var result = _userRepository.SelectDataFromUsernamePassword(item);

                if (result == "")
                {
                    response.success = false;
                }
                else
                {
                    response.success = true;
                }
            }
            else
            {
                throw new Exception("Invalid Username or Password");
            }

            return response;
        }

        public ResposeModel UserProfile(UserProfileModel item)
        {
            ResposeModel response = new ResposeModel();
            var result = _userRepository.InsertDataForRegister(item);

            if (result == 1)
            {
                response.success = true;
            }
            else
            {
                response.success = false;
            }

            return response;
        }

        public UserProfileModel SelectIndividual(UserRequestIdModel requestId)
        {
            UserProfileModel result = _userRepository.SelectIndividualRepo(requestId);
            return result;
        }

        public DropdownDepartmentListModel DepartmentList()
        {
            DropdownDepartmentListModel result = _userRepository.SelectDepaertmentFromDB();
            return result;
        }

        public ResposeModel UpdateUserProfile(UserProfileModel item)
        {
            ResposeModel response = new ResposeModel();
            var result = _userRepository.UpdateUserProfile(item);

            if (result == 1)
            {
                response.success = true;
            }
            else
            {
                response.success = false;
            }

            return response;
        }
    }  

}