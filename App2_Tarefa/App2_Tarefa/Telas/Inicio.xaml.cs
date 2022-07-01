using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2_Tarefa.Modelos;
using System.Globalization;

namespace App2_Tarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            CultureInfo culture = new CultureInfo("pt-BR");
            String Data = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
            DataHoje.Text = string.Format(Data, "de");
            CarregarTarefas();
        }

        private void ActionGoCadastro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro());
        }

        private void CarregarTarefas()
        {
            
            SLTarefas.Children.Clear();

            List<Tarefa> Lista = new GerenciadorTarefa().Listagem();

            int i = 0;
            foreach(Tarefa tarefa in Lista)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }
        }

        public void LinhaStackLayout(Tarefa tarefa, int index)
        {
            Image Delete = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("Delete.png")
            };
            TapGestureRecognizer DeleteTap = new TapGestureRecognizer();

            DeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };
            Delete.GestureRecognizers.Add(DeleteTap);

            Image Prioridade = new Image() 
            { 
                VerticalOptions = LayoutOptions.Center, 
                Source = ImageSource.FromFile("p" + tarefa.Prioridade + ".png") 
            };


            object StackCentral = null;
            if (tarefa.DataFinalizacao == null)
            {
                StackCentral = new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = tarefa.Nome
                };
            }
            else
            {
                StackCentral = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                ((StackLayout)StackCentral).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });
                ((StackLayout)StackCentral).Children.Add(new Label() 
                { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy HH:mm") + "h",
                    TextColor = Color.Gray, FontSize = 10 });
            }         

            Image Check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOff.png")};
            
            if(tarefa.DataFinalizacao != null)
            {
                Check.Source = ImageSource.FromFile("CheckOn.png");
            }

            TapGestureRecognizer CheckTap = new TapGestureRecognizer();

            CheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            Check.GestureRecognizers.Add(CheckTap);

            StackLayout Linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };

            Linha.Children.Add(Check);
            Linha.Children.Add((View)StackCentral);
            Linha.Children.Add(Prioridade);
            Linha.Children.Add(Delete);

            SLTarefas.Children.Add(Linha);

        }

        
    }
}