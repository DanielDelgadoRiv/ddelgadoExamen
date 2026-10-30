namespace ddelgadoExamen.Views;

public partial class vLogin : ContentPage
{
    public vLogin()
    {
        InitializeComponent();
    }

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
        string usuario = txtUsuario.Text;
        string contrasena = txtContrasena.Text;

        // Validar que los campos no estén vacíos
        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
        {
            await DisplayAlertAsync("Error", "Por favor, ingrese usuario y contraseña.", "Aceptar");
            return;
        }

        // Validar las credenciales
        // Matriz de usuarios y contraseñas
        string[,] usuarios = new string[,]
        {
            { "recepcion", "gym2026" },
            { "entrenador", "fit_01" },
            { "uisrael", "2026" },
            { "gerente", "admin123" }
        };

        // Verificar si las credenciales coinciden con algún usuario en la matriz
        bool credencialesValidas = false;
        for (int i = 0; i < usuarios.GetLength(0); i++)
        {
            if (usuarios[i, 0] == usuario && usuarios[i, 1] == contrasena)
            {
                credencialesValidas = true;
                break;
            }
        }

        if (!credencialesValidas)
        {
            await DisplayAlertAsync("Error", "Credenciales inválidas.", "Aceptar");
            return;
        }

        // Iniciar sesión
        await Navigation.PushAsync(new vRegistro(usuario));
    }

    private async void btnAcercaDe_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vInfoEstudiante());
    }
}