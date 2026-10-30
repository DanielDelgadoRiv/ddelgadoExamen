namespace ddelgadoExamen.Views;

public partial class vRegistro : ContentPage
{
    public vRegistro()
    {
        InitializeComponent();
    }

    public vRegistro(string usuario)
    {
        InitializeComponent();
    }

    private async void btnCalcularPagoMensual_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Pago Mensual", "Calcular.", "Aceptar");
    }

    private async void btnVerResumen_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Ver Resumen", "Ver resumen.", "Aceptar");
    }
}