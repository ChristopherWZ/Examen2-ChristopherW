using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSelecciones.CapaDatos;

namespace WebSelecciones.CapaVistas
{
    public partial class Resultados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarResultados();
            }
        }

        protected void MostrarResultados()
        {
            string constr = ConfigurationManager.ConnectionStrings["ELECCIONESConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string query = @"SELECT v.Eleccion, COUNT(v.Eleccion) AS cantidad_votos, c.id_candidato, c.nombre_candidato, p.nombre_partido
                         FROM Votante v
                         INNER JOIN Candidatos c ON v.Eleccion = c.id_candidato
                         INNER JOIN Partido_Politico p ON c.id_partido = p.id_partido
                         GROUP BY v.Eleccion, c.id_candidato, c.nombre_candidato, p.nombre_partido
                         ORDER BY cantidad_votos DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        int maxVotos = 0;
                        int idGanador = 0;
                        StringBuilder sb = new StringBuilder();

                        while (dr.Read())
                        {
                            int idCandidato = Convert.ToInt32(dr["id_candidato"]);
                            int cantidadVotos = Convert.ToInt32(dr["cantidad_votos"]);
                            string nombreCandidato = dr["nombre_candidato"].ToString();
                            string nombrePartido = dr["nombre_partido"].ToString();

                            // Determinar quién tiene más votos
                            if (cantidadVotos > maxVotos)
                            {
                                maxVotos = cantidadVotos;
                                idGanador = idCandidato;
                            }

                            // Aquí puedes acumular o mostrar los resultados como prefieras
                            sb.AppendLine($"{nombreCandidato} ({nombrePartido}): {cantidadVotos} votos <br/><br/>");
                        }

                        // Mostrar resultados en algún lugar de la página
                        resultadoLabel.Text = sb.ToString();

                        // Ahora idGanador contiene el id del candidato con más votos
                        if (idGanador != 0)
                        {
                            // Puedes mostrar el ganador o hacer cualquier otra acción
                            // Por ejemplo:
                            // nombreGanadorLabel.Text = ObtenerNombreCandidato(idGanador);
                        }
                    }
                }
            }
        }

        // Método para obtener el nombre del candidato según su ID (ejemplo)
        private string ObtenerNombreCandidato(int idCandidato)
        {
            string nombreCandidato = string.Empty;
            string nombrePartido = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ELECCIONESConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT c.nombre_candidato, p.nombre_partido
                         FROM Candidatos c
                         INNER JOIN Partido_Politico p ON c.id_partido = p.id_partido
                         WHERE c.id_candidato = @idCandidato";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idCandidato", idCandidato);

                try
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nombreCandidato = dr["nombre_candidato"].ToString();
                            nombrePartido = dr["nombre_partido"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones (puedes implementar según tus necesidades)
                    // Aquí se muestra un ejemplo básico de cómo manejar la excepción
                    Console.WriteLine("Error al obtener nombre del candidato y partido: " + ex.Message);
                }
            }

            return $"{nombreCandidato} ({nombrePartido})";
        }



    }
}