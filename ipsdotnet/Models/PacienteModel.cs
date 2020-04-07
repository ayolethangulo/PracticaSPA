using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ipsdotnet.Models
{
    public class PacienteInputModel
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public decimal ValorHospitalizacion { get; set; }
        public decimal Salario { get; set; }
    }
    public class PacienteViewModel : PacienteInputModel
    {
        public PacienteViewModel()
        {

        }
        public PacienteViewModel(Paciente paciente)
        {
            Identificacion = paciente.Identificacion;
            Nombre = paciente.Nombre;
            ValorHospitalizacion = paciente.ValorHospitalizacion;
            Salario = paciente.Salario;
            ValorCopago = paciente.ValorCopago;
        }
        public decimal ValorCopago { get; set; }
    }
}