using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSelecciones.CapaDatos
{
    public class Cls_VotosResultados
    {
        public int id_voto { get; set; }

        public int id_candidato { get; set; }

        public DateTime fecha { get; set; }

        public int id_votante { get; set; }
    }
}