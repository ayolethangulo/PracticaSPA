import { Component, OnInit } from '@angular/core';
import { Paciente } from '../models/paciente';
import { PacienteService } from '../../services/paciente.service';


@Component({
  selector: 'app-paciente-consulta',
  templateUrl: './paciente-consulta.component.html',
  styleUrls: ['./paciente-consulta.component.css']
})
export class PacienteConsultaComponent implements OnInit {

  pacientes: Paciente[];
  constructor(private pacienteService: PacienteService) { }

  ngOnInit() {
    this.pacienteService.get().subscribe(result => {
      this.pacientes = result;
    });
  }

  delete(paciente: Paciente): void {
    if(!confirm('Desea elimiar?')){
      this.pacientes = this.pacientes.filter(h => h !== paciente);
      this.pacienteService.delete(paciente).subscribe();
      this.ngOnInit();
    }
  }

  update(paciente: Paciente): void {
    if(!confirm('Desea Modificar?')){
      this.pacientes = this.pacientes.filter(h => h !== paciente);
      this.pacienteService.update(paciente).subscribe();
      this.ngOnInit();
    }
  }
}
