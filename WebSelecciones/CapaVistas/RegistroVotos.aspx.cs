using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSelecciones.CapaVistas
{
    public partial class RegistroVotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void bvoto_Click(object sender, EventArgs e)
        {
            int idVotante = int.Parse(tIdVotante.Text);
            string nombre = tNombre.Text;
            string apellido = tApellido.Text;
            int edad = int.Parse(tEdad.Text);
            int eleccion = int.Parse(tEleccion.Text); // asegúrate de que este campo sea numérico según tu diseño de base de datos

            RegistrarVotante(idVotante, nombre, apellido, edad, eleccion);
        }



        protected void RegistrarVotante(int idVotante, string nombre, string apellido, int edad, int eleccion)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ELECCIONESConnectionString"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                // Primero inserta en la tabla Votante
                string queryVotante = "INSERT INTO Votante (id_votante, nombre, apellido, edad, Eleccion) " +
                                      "VALUES (@IdVotante, @Nombre, @Apellido, @Edad, @Eleccion)";

                using (SqlCommand comandoVotante = new SqlCommand(queryVotante, conexion))
                {
                    comandoVotante.Parameters.AddWithValue("@IdVotante", idVotante);
                    comandoVotante.Parameters.AddWithValue("@Nombre", nombre);
                    comandoVotante.Parameters.AddWithValue("@Apellido", apellido);
                    comandoVotante.Parameters.AddWithValue("@Edad", edad);
                    comandoVotante.Parameters.AddWithValue("@Eleccion", eleccion);

                    comandoVotante.ExecuteNonQuery();
                }

                // Luego inserta en la tabla VotosResultados
                string queryVotos = "INSERT INTO VotosResultados (id_candidato, fecha, id_votante) " +
                                    "VALUES (@IdCandidato, @Fecha, @IdVotante)";

                DateTime fechaActual = DateTime.Now;

                using (SqlCommand comandoVotos = new SqlCommand(queryVotos, conexion))
                {
                    comandoVotos.Parameters.AddWithValue("@IdCandidato", eleccion);  // Aquí debes ajustar según tu lógica
                    comandoVotos.Parameters.AddWithValue("@Fecha", fechaActual);
                    comandoVotos.Parameters.AddWithValue("@IdVotante", idVotante);

                    comandoVotos.ExecuteNonQuery();
                }
            }

            // Redirecciona después de la inserción exitosa
            Response.Redirect("Menu.aspx");
        }



    }
}