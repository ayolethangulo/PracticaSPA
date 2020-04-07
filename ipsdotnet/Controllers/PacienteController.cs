using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ipsdotnet.Models;

namespace ipsdotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController: ControllerBase
    {
        private readonly PacienteService _pacienteService;
        public IConfiguration Configuration { get; }
        public PacienteController(IConfiguration configuration)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _pacienteService = new PacienteService(connectionString);
        }
         // GET: api/Paciente
        [HttpGet]
        public IEnumerable<PacienteViewModel> Gets()
        {
            var pacientes = _pacienteService.ConsultarTodos().Select(p=> new PacienteViewModel(p));
            return pacientes;
        }

         // GET: api/Paciente/5
        [HttpGet("{identificacion}")]
        public ActionResult<PacienteViewModel> Get(string identificacion)
        {
            var paciente = _pacienteService.BuscarxIdentificacion(identificacion);
            if (paciente == null) return NotFound();
            var pacienteViewModel = new PacienteViewModel(paciente);
            return pacienteViewModel;
        }
        // POST: api/Paciente
        [HttpPost]
        public ActionResult<PacienteViewModel> Post(PacienteInputModel pacienteInput)
        {
            Paciente paciente = MapearPaciente(pacienteInput);
            var response = _pacienteService.Guardar(paciente);
            if (response.Error) 
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.Paciente);
        }
        // DELETE: api/Paciente/5
        [HttpDelete("{identificacion}")]
        public ActionResult<string> Delete(string identificacion)
        {
            string mensaje = _pacienteService.Eliminar(identificacion);
            return Ok(mensaje);
        }
        //mapear paciente
        private Paciente MapearPaciente(PacienteInputModel pacienteInput)
        {
            var paciente = new Paciente
            {
                Identificacion = pacienteInput.Identificacion,
                Nombre = pacienteInput.Nombre,
                ValorHospitalizacion = pacienteInput.ValorHospitalizacion,
                Salario = pacienteInput.Salario
            };
            return paciente;
        }
        // PUT: api/Paciente/5
        [HttpPut("{identificacion}")]
        public ActionResult<string> Put(string identificacion, Paciente paciente)
        {
            string mensaje = _pacienteService.Actualizar(identificacion);
            return Ok(mensaje);
        }

        
    }
}