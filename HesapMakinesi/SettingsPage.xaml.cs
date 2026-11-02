namespace HesapMakinesi;

public partial class SettingsPage : ContentPage
{
    private bool isInitializing = true;

    public SettingsPage()
    {
        InitializeComponent();
        LoadCurrentTheme();
        isInitializing = false;
    }

    private void LoadCurrentTheme()
    {
        int savedTheme = Preferences.Get("SelectedTheme", 0); 
        //0 varsayılan 1 açık 2 koyu demwk
        switch (savedTheme)
        {
            case 1:
                LightThemeRadio.IsChecked = true;
                break;
            case 2:
                DarkThemeRadio.IsChecked = true;
                break;
            default:
                SystemThemeRadio.IsChecked = true;
                break;
        }
    }

    private void OnThemeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (isInitializing || !e.Value) return;

        AppTheme selectedTheme = AppTheme.Unspecified;
        int themeIndex = 0;

        if (LightThemeRadio.IsChecked)
        {
            selectedTheme = AppTheme.Light;
            themeIndex = 1;
        }
        else if (DarkThemeRadio.IsChecked)
        {
            selectedTheme = AppTheme.Dark;
            themeIndex = 2;
        }

        Application.Current.UserAppTheme = selectedTheme;
        Preferences.Set("SelectedTheme", themeIndex);
    }
}