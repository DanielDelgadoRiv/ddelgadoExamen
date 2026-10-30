namespace ddelgadoExamen.Views;

public partial class vRegistro : ContentPage
{
    string usuarioConectado;
    string nombre;
    string apellido;
    string edad;
    DateTime? fechaInicio;
    string plan;
    string casillero;
    double vMontoInicial;
    double vPagoMensual;
    double vTotalPagar = 0;

    public vRegistro()
    {
        InitializeComponent();
    }

    public vRegistro(string usuario)
    {
        InitializeComponent();
        if (!string.IsNullOrEmpty(usuario))
        {
            lblUsuarioConectado.Text = $"Usuario conectado: {usuario}";
            usuarioConectado = usuario;
        } 
        else
        {
            DisplayAlertAsync("Error", "Error al conectar usuario.", "Aceptar");
            Navigation.PushAsync(new vLogin());
        }
    }

    private async void btnCalcularPagoMensual_Clicked(object sender, EventArgs e)
    {
        try
        {
            nombre = txtNombre.Text;
            apellido = txtApellido.Text;
            edad = txtEdad.Text;
            fechaInicio = dpFechaInicio.Date;
            plan = pkPlan.SelectedIndex >= 0
                    ? pkPlan.Items[pkPlan.SelectedIndex].ToString()
                    : string.Empty;
            casillero = pkCasillero.SelectedIndex >= 0
                    ? pkCasillero.Items[pkCasillero.SelectedIndex].ToString()
                    : string.Empty;
            string montoInicial = txtMontoInicial.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(edad) || !fechaInicio.HasValue || string.IsNullOrWhiteSpace(plan) || string.IsNullOrWhiteSpace(casillero) || string.IsNullOrWhiteSpace(montoInicial))
            {
                vTotalPagar = 0;
                txtPagoMensual.Text = "Pago mensual";
                await DisplayAlertAsync("ALTO", "Por favor, complete todos los campos.", "Aceptar");
                return;
            }

            // Validar que monto inicial este entre 120 y 600
            vMontoInicial = double.TryParse(montoInicial, out double monto) ? monto : 0;
            if (vMontoInicial < 120 || vMontoInicial > 600)
            {
                vTotalPagar = 0;
                txtPagoMensual.Text = "Pago mensual";
                await DisplayAlertAsync("ALTO", "El monto inicial debe estar entre 120 y 600.", "Aceptar");
                return;
            }

            // Validación de fecha de inicio: no puede ser menor que fecha actual
            if (fechaInicio.HasValue && fechaInicio.Value < DateTime.Today)
            {
                vTotalPagar = 0;
                txtPagoMensual.Text = "Pago mensual";
                await DisplayAlertAsync("ALTO", "La fecha de inicio no puede ser una fecha pasada.", "Aceptar");
                return;
            }

            // Cálculo de mensualidades
            double membresia = 1200;
            double cuotas = 5;
            vPagoMensual = (membresia - vMontoInicial) / cuotas;
            string recargo = "";

            if (casillero == "NO")
            {
                vPagoMensual = vPagoMensual * 1.02;
                recargo = "2%";
            }
            else if (casillero == "SI")
            {
                vPagoMensual = vPagoMensual * 1.10;
                recargo = "10%";
            }

            txtPagoMensual.Text = "$" + vPagoMensual.ToString("F2");

            // Calcular total a pagar
            vTotalPagar = vMontoInicial + (vPagoMensual * cuotas);

            await DisplayAlertAsync("Calculado", "5 cuotas de " + txtPagoMensual.Text + " (incluido recargo del " + recargo + " a la cuota mensual)", "Aceptar");
        }
        catch (Exception ex)
        {
            vTotalPagar = 0;
            txtPagoMensual.Text = "Pago mensual";
            await DisplayAlertAsync("ERROR", $"Error en la operación: {ex.Message}", "Aceptar");
        }
        
    }

    private async void btnVerResumen_Clicked(object sender, EventArgs e)
    {
        // Validar que vTotalPagar sea mayor a cero antes de mostrar el resumen
        if (vTotalPagar > 0)
        {
            await Navigation.PushAsync(new vResumen(usuarioConectado, nombre, apellido, edad, fechaInicio, plan, casillero, vMontoInicial, vPagoMensual, vTotalPagar));
        }
        else
        {
            await DisplayAlertAsync("ALTO", "Calcule el total a pagar primero.", "Aceptar");
        }
    }
}