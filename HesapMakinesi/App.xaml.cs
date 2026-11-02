namespace HesapMakinesi; 

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // temanın ayarları açık koyu vb
        int savedTheme = Preferences.Get("SelectedTheme", 0);
        UserAppTheme = savedTheme switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };

        // uyg başlangıç sayfasını shell yapma
        MainPage = new AppShell();
    }
}