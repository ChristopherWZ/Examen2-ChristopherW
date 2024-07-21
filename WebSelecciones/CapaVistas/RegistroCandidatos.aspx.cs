using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSelecciones.CapaDatos;

namespace WebSelecciones.CapaVistas
{
    public partial class RegistroCandidatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void bcandidato_Click(object sender, EventArgs e)
        {
            int idCandidato = int.Parse(tCandidatoId.Text);
            int idPartido = int.Parse(tPartidoId.Text);
            string plataforma = tPlataforma.Text;
            string nomCandidato = tNombre.Text;

            RegistrarCandidato(idCandidato, idPartido, plataforma, nomCandidato);
        }


        protected void RegistrarCandidato(int idCandidato, int idPartido, string plataforma, string nomCandidato)
        {
            // Utiliza parámetros para prevenir inyección SQL
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ELECCIONESConnectionString"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "INSERT INTO Candidatos (id_candidato, id_partido, plataforma, nombre_candidato) " +
                               "VALUES (@IdCandidato, @IdPartido, @Plataforma, @NomCandidato)";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@IdCandidato", idCandidato);
                    comando.Parameters.AddWithValue("@IdPartido", idPartido);
                    comando.Parameters.AddWithValue("@Plataforma", plataforma);
                    comando.Parameters.AddWithValue("@NomCandidato", nomCandidato);

                    int rowsAffected = comando.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Redirecciona después de la inserción exitosa
                        Response.Redirect("Menu.aspx");
                    }
                    else
                    {
                        // Manejo de error si no se insertaron filas
                        // Puedes mostrar un mensaje de error o loguear el problema
                    }
                }
            }
        }

    }
}