import { PacienteRegistroComponent } from './copago/paciente-registro/paciente-registro.component';
import { PacienteConsultaComponent } from './copago/paciente-consulta/paciente-consulta.component';
import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
  path: 'pacienteRegistro',
  component: PacienteRegistroComponent
  },
  {
    path: 'pacienteConsulta',
    component: PacienteConsultaComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
