using Datos;
using Entity;
using System;
using System.Collections.Generic;

namespace Logica
{
    public class PacienteService
    {
        private readonly ConnectionManager _conexion;
        private readonly PacienteRepository _repositorio;
        public PacienteService(string connectionString)
        {
            _conexion = new ConnectionManager(connectionString);
            _repositorio = new PacienteRepository(_conexion);
        }
        public GuardarPacienteResponse Guardar(Paciente paciente)
        {
            try
            {
                paciente.CalcularCopago();
                _conexion.Open();
                _repositorio.Guardar(paciente);
                _conexion.Close();
                return new GuardarPacienteResponse(paciente);
            }
            catch (Exception e)
            {
                return new GuardarPacienteResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }
         public List<Paciente> ConsultarTodos()
        {
            _conexion.Open();
            List<Paciente> pacientes = _repositorio.ConsultarTodos();
            _conexion.Close();
            return pacientes;
        }
        public string Eliminar(string identificacion)
        {
            try
            {
                _conexion.Open();
                var paciente = _repositorio.BuscarPorIdentificacion(identificacion);
                if (paciente != null)
                {
                    _repositorio.Eliminar(paciente);
                    _conexion.Close();
                    return ($"El registro {paciente.Nombre} se ha eliminado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { _conexion.Close(); }

        }
         public string Actualizar(string identificacion)
         {
            try
            {
                _conexion.Open();
                var paciente = _repositorio.BuscarPorIdentificacion(identificacion);
                if (paciente != null)
                {
                    _repositorio.Actualizar(paciente);
                    _conexion.Close();
                    return ($"El registro {paciente.Nombre} se ha actualizado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { _conexion.Close(); }

        }
        public Paciente BuscarxIdentificacion(string identificacion)
        {
            _conexion.Open();
            Paciente paciente = _repositorio.BuscarPorIdentificacion(identificacion);
            _conexion.Close();
            return paciente;
        }
        
    }
       public class GuardarPacienteResponse 
    {
        public GuardarPacienteResponse(Paciente paciente)
        {
            Error = false;
            Paciente = paciente;
        }
        public GuardarPacienteResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Paciente Paciente { get; set; }
    }
}