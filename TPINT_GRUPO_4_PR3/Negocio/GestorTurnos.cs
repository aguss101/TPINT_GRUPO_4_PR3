using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class GestorTurnos
    {
        ConsultasTurnos consultas = new ConsultasTurnos();


        public List<Turno> GetTurnos()
        {
            return consultas.GetTurnosAdmin();
        }

        public List<Turno> GetTurnosMedico(string legajo)
        {
            return consultas.GetTurnosMedico(legajo);
        }
    };
}
