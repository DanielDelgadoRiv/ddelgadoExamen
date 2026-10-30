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

    private async void btnExportarTXT_Clicked(object sender, EventArgs e)
    {
        string datos = lblUsuarioConectado.Text + "\n\n" +
            "Nombre: " + lblNombre.Text + "\n" +
            "Apellido: " + lblApellido.Text + "\n" +
            "Edad: " + lblEdad.Text + "\n" +
            "Fecha de inicio: " + lblFechaInicio.Text + "\n" +
            "Plan: " + lblPlan.Text + "\n" +
            "Casillero: " + lblCasillero.Text + "\n\n" +
            "Monto inicial: " + lblMontoInicial.Text + "\n" +
            "Pago mensual (5 cuotas): " + lblPagoMensual.Text + "\n" +
            "Total a pagar: " + lblTotalPagar.Text + "\n" +
            "------------------------------\n";

        //Guardar datos en archivo de texto
        string ruta = Path.Combine(FileSystem.AppDataDirectory, "ddelgadoExamen.txt");

        await File.WriteAllTextAsync(ruta, datos);

        // Abrir el archivo
        await Launcher.Default.OpenAsync(
            new OpenFileRequest
            {
                Title = "Abrir archivo de texto",
                File = new ReadOnlyFile(ruta)
            });
    }

    private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vLogin());
    }
}