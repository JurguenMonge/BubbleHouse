﻿
using Microsoft.Maui.Controls;

namespace FrontEnd
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PrincipalAdministrativa());
        }
    }
}
 