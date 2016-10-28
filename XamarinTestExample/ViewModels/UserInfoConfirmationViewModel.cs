using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace XamarinTestExample.ViewModels
{
    public class UserInfoConfirmationViewModel : BindableBase, INavigationAware
    {
        readonly INavigationService _navigationService;
        readonly Acr.UserDialogs.IUserDialogs _userDialogs;

        public DelegateCommand SaveUserInfoCmd { get; set; }
        public DelegateCommand BackToUserInfoCmd { get; set; }

        string _firstname;
        public string FirstName
        {
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }

        string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }

        string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        object _birthday;
        public object Birthday
        {
            get { return _birthday; }
            set { SetProperty(ref _birthday, value); }
        }

        Action SaveUserInfo
        {
            get
            {
                return new Action(async () =>
                {
                    var confirm = await _userDialogs.ConfirmAsync(PrepareConfirmDialog);

                    if (!confirm)
                    {
                        await _userDialogs.AlertAsync(string.Empty,
                                                       "Dados não confirmados",
                                                       "OK");
                        return;
                    }

                    await _userDialogs.AlertAsync(string.Empty,
                                                       "Dados confirmados!",
                                                       "OK");
                    return;
                });
            }
        }

        Acr.UserDialogs.ConfirmConfig PrepareConfirmDialog
        {
            get
            {
                return new Acr.UserDialogs.ConfirmConfig
                {
                    CancelText = "Cancelar",
                    OkText = "Confirmar",
                    Title = "Você confirma os dados do usuário",
                    Message = string.Empty
                };
            }
        }

        Action BackToUserInfo
        {
            get
            {
                return new Action(async () => await _navigationService.GoBackAsync(AddParameters()));
            }
        }

        public UserInfoConfirmationViewModel(Acr.UserDialogs.IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        NavigationParameters AddParameters()
        {
            var parameters = new NavigationParameters();
            parameters.Add("User", GetUser);

            return parameters;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("User"))
                SetUserFromNavigation((User)parameters["User"]);
        }

        User GetUser
        {
            get
            {
                return new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Birthday = Birthday
                };
            }
        }

        void SetUserFromNavigation(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Birthday = user.Birthday;
        }
    }
}
