# Medicle app

# Medical treatment and patient booking 

It's Medical/health app for fast and affordable filling for medical treatment 
app start up and login trace. 


Apps.cs
			MainPage = new RootPage ();
----
RootPage.cs
		public RootPage ()
		{
			ShowLoginDialog();    

		async void ShowLoginDialog()
		{
			var page = new LoginPage();
            ViewPages.Instance.loginPage = page;
			await Navigation.PushModalAsync(page);
----
LoginPage.cs
		public LoginPage ()
		{
			InitializeComponent ();
			this.BindingContext = new LoginViewModel(this.Navigation);
			NavigationPage.SetHasNavigationBar(this, false);

----
LoginViewModel.cs
        public LoginViewModel( INavigation p_navigation )
        {
            LoginCommand = new Command(async (object o) =>
                {
                    await API.Instance.Login(this.navigation, EmailAddress, Password, ConnectionSuccess, ConnectionFail);
                });

### called whenever response from server is recieved
        private async Task ConnectionSuccess(API.LoginResponse response)
        {
            API.Instance.SaveAccount(response.token, response.user);
            ViewPages.Instance.homePage.Refresh();
            if (response.user != null && response.user.isAccountInfoEmpty())
            {
                ViewPages.Instance.rootPage.goToAccountInfoPage();
                ViewPages.Instance.accountInfoPage.showEditAccountInfoPage();
            }
            try {
            await navigation.PopModalAsync();
            await navigation.PopModalAsync();
            } catch (Exception e) {
                Console.WriteLine("LoginViewModel: Warning cannot pop model");
            }
        }

### called whenever error response from server is recieved
        private async Task ConnectionFail(API.ServerResponseErrorCode errorCode)
        {
            if (errorCode == API.ServerResponseErrorCode.ERROR_CONNECTION_TIMEOUT)
                await MessagePopup.Show(this.navigation, MessagePopup.Type.Error, MessagePopup.Icon.Connection, "Oops!", "Server is unreachable");
            
            else if (errorCode == API.ServerResponseErrorCode.ERROR_INCORRECT_USERNAME_OR_PASSWORD)
                await MessagePopup.Show(this.navigation, MessagePopup.Type.Warning, MessagePopup.Icon.User, "Oops!", "Incorrect email address or password");

            else
                await MessagePopup.Show(this.navigation, MessagePopup.Type.Error, MessagePopup.Icon.User, "Sorry", "An unknown error has appeared");
        }
