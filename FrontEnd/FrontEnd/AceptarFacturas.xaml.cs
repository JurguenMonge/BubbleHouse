namespace FrontEnd;

public partial class AceptarFacturas : ContentPage
{
	public AceptarFacturas()
	{
		InitializeComponent();
	}

    int selecionado = 0;


    private void btnTarjeta_Clicked(object sender, EventArgs e)
    {
        if(selecionado != 0)
        {
            btnTarjeta.BackgroundColor = Colors.Black;
            btnTarjeta.TextColor = Colors.White;
            btnEfectivo.BackgroundColor = Colors.White;
            btnEfectivo.TextColor = Colors.Black;
            selecionado = 0;
        }
    }

    private void btnEfectivo_Clicked(object sender, EventArgs e)
    {
        if (selecionado == 0)
        {
            btnTarjeta.BackgroundColor = Colors.White;
            btnTarjeta.TextColor = Colors.Black;
            btnEfectivo.BackgroundColor = Colors.Black;
            btnEfectivo.TextColor = Colors.White;
            selecionado = 1;
        }
    }
}