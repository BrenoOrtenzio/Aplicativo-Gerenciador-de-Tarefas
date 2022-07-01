using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2_Tarefa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new App2_Tarefa.Telas.Inicio());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
