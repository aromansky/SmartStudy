﻿using Microsoft.Maui.ApplicationModel;

namespace SmartStudy;

public partial class App : Application
{
	public App()
    {
		InitializeComponent();
        MainPage = new AppShell();
        Application.Current.UserAppTheme = AppTheme.Light;
    }

}
