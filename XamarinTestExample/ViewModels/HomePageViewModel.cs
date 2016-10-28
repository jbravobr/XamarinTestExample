using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace XamarinTestExample.ViewModels
{
    public class HomePageViewModel : BindableBase, INavigationAware
    {
        readonly INavigationService _navigationService;
        readonly IPageDialogService _pageDialogService;
        readonly Acr.UserDialogs.IUserDialogs _userDialogs;

        public DelegateCommand SaveUserInfoCmd { get; set; }
        public DelegateCommand ShowDatePickerCmd { get; set; }
        public bool IsMsgInfoCompleted { get; set; }

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

        Acr.UserDialogs.DatePromptConfig DateConfig
        {
            get
            {
                return new Acr.UserDialogs.DatePromptConfig
                {
                    CancelText = "Cancelar",
                    OkText = "Confirmar",
                    OnAction = new Action<Acr.UserDialogs.DatePromptResult>((date) => ConfirmDate(date.SelectedDate))
                };
            }
        }

        Action ConfirmDate(DateTime date)
        {
            return new Action(() => Birthday = date);
        }

        Action ShowDatePicker
        {
            get
            {
                return new Action(() =>
                {
                    _userDialogs.DatePrompt(DateConfig);
                });
            }
        }

        Action SaveUser
        {
            get
            {
                return new Action(() =>
                {
                    if (IsValid)
                        IsMsgInfoCompleted = true;
                    else
                        IsMsgInfoCompleted = false;

                    //await NavigateTo();
                });
            }
        }

        async Task NavigateTo()
        {
            if (!IsValid)
            {
                await _pageDialogService.DisplayAlertAsync(string.Empty,
                                                           "Você precisa informar os seus dados",
                                                           "OK");
                return;
            }

            await _navigationService.NavigateAsync("UserInfoConfirmationPage", AddParameters());
        }

        NavigationParameters AddParameters()
        {
            var parameters = new NavigationParameters();
            parameters.Add("User", GetUser);

            return parameters;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("User"))
                SetUserFromNavigation((User)parameters["User"]);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public HomePageViewModel(INavigationService navigationService
                                , IPageDialogService pageDialogServie
                                , Acr.UserDialogs.IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogServie;
            _userDialogs = userDialogs;

            SaveUserInfoCmd = new DelegateCommand(SaveUser);
            ShowDatePickerCmd = new DelegateCommand(ShowDatePicker);
        }

        bool IsValid
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstName) &&
                    !string.IsNullOrEmpty(LastName) &&
                    ((DateTime)Birthday).Day > 0 &&
                    !string.IsNullOrEmpty(City) &&
                    !string.IsNullOrEmpty(Email))
                {
                    return true;
                }

                return false;
            }
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