namespace ddelgadoExamen.Views;

public partial class vResumen : ContentPage
{
    public vResumen()
    {
        InitializeComponent();
    }

    public vResumen(string usuario, string nombre, string apellido, string edad, DateTime? fechaInicio, string plan, string casillero, double montoInicial, double pagoMensual, double totalPagar)
    {
        InitializeComponent();

        lblUsuarioConectado.Text = $"Usuario conectado: {usuario}";
        lblNombre.Text = nombre;
        lblApellido.Text = apellido;
        lblEdad.Text = edad;
        lblFechaInicio.Text = fechaInicio?.ToString("yyyy-MM-dd") ?? string.Empty;
        lblPlan.Text = plan;
        lblCasillero.Text = casillero;
        lblMontoInicial.Text = "$" + montoInicial.ToString("F2");
        lblPagoMensual.Text = "$" + pagoMensual.ToString("F2");
        lblTotalPagar.Text = "$" + totalPagar.ToString("F2");
    }

    private async void btnNuevoRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vRegistro());
    }

    private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vLogin());
    }
}