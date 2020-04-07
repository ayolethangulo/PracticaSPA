using System;

namespace Entity
{
    public class Paciente
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public decimal ValorHospitalizacion { get; set; }
        public decimal Salario { get; set; }
        public decimal ValorCopago { get; set; }
        public void CalcularCopago() 
        {
            if (Salario > 2500000)
            {
                ValorCopago = (ValorHospitalizacion * 20)/100;
            }
            else
            {
                ValorCopago = (ValorHospitalizacion * 10)/100;
            }
        }

    }
}
