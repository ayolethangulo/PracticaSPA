using System;
using Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Datos
{
    public class PacienteRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Paciente> _pacientes = new List<Paciente>();
        public PacienteRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
         public void Guardar(Paciente paciente)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Paciente (Identificacion,Nombre,ValorHospitalizacion,Salario,ValorCopago) 
                                        values (@Identificacion,@Nombre,@ValorHospitalizacion,@Salario,@ValorCopago)";
                command.Parameters.AddWithValue("@Identificacion", paciente.Identificacion);
                command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                command.Parameters.AddWithValue("@ValorHospitalizacion", paciente.ValorHospitalizacion);
                command.Parameters.AddWithValue("@Salario", paciente.Salario);
                command.Parameters.AddWithValue("@ValorCopago", paciente.ValorCopago);
                var filas = command.ExecuteNonQuery();
            }
        }
         public void Eliminar(Paciente paciente)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Delete from paciente where Identificacion=@Identificacion";
                command.Parameters.AddWithValue("@Identificacion", paciente.Identificacion);
                command.ExecuteNonQuery();
            }
        }
          public void Actualizar(Paciente paciente)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Update Paciente set Nombre=@Nombre,ValorHospitalizacion=@ValorHospitalizacion"
                +"Salario=@Salario,ValorCopago=@ValorCopago where Identificacion=@Identificacion";
                command.Parameters.AddWithValue("@Identificacion", paciente.Identificacion);
                command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                command.Parameters.AddWithValue("@ValorHospitalizacion", paciente.ValorHospitalizacion);
                command.Parameters.AddWithValue("@Salario", paciente.Salario);
                command.Parameters.AddWithValue("@ValorCopago", paciente.ValorCopago);
                command.ExecuteNonQuery();
            }
        }
         public List<Paciente> ConsultarTodos()
        {
            SqlDataReader dataReader;
            List<Paciente> pacientes = new List<Paciente>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from paciente ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Paciente paciente = DataReaderMapToPerson(dataReader);
                        pacientes.Add(paciente);
                    }
                }
            }
            return pacientes;
        }
        public Paciente BuscarPorIdentificacion(string identificacion)
        {
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from paciente where Identificacion=@Identificacion";
                command.Parameters.AddWithValue("@Identificacion", identificacion);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                return DataReaderMapToPerson(dataReader);
            }
        }

         private Paciente DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            Paciente paciente = new Paciente();
            paciente.Identificacion = (string)dataReader["Identificacion"];
            paciente.Nombre = (string)dataReader["Nombre"];
            paciente.ValorHospitalizacion = (decimal)dataReader["ValorHospitalizacion"];
            paciente.Salario = (decimal)dataReader["Salario"];
            paciente.ValorCopago = (decimal)dataReader["ValorCopago"];
            return paciente;
        }


    }
}