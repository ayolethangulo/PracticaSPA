import { Injectable, Inject } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Paciente } from '../copago/models/paciente';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleHttpErrorService } from '../@base/handle-http-error.service';

@Injectable({
  providedIn: 'root'
})
export class PacienteService {



  baseUrl: string;
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private handleErrorService: HandleHttpErrorService) 
    {
      this.baseUrl = baseUrl;
    }

    get(): Observable<Paciente[]> {
      return this.http.get<Paciente[]>(this.baseUrl + 'api/Paciente')
          .pipe(
              tap(_ => this.handleErrorService.log('datos enviados')),
              catchError(this.handleErrorService.handleError<Paciente[]>('Consulta Paciente', null))
          );
    }

    post(paciente: Paciente): Observable<Paciente> {
      return this.http.post<Paciente>(this.baseUrl + 'api/Paciente', paciente)
          .pipe(
              tap(_ => this.handleErrorService.log('datos enviados')),
              catchError(this.handleErrorService.handleError<Paciente>('Registrar Persona', null))
          );
    }
   /** DELETE: delete the hero from the server */
   delete (paciente: Paciente | string): Observable<Paciente> {
    const id = typeof paciente === 'string' ? paciente : paciente.identificacion;
    const url = `${'api/Paciente'}/${id}`;

    return this.http.delete<Paciente>(url)
    .pipe(
      tap(_ => this.handleErrorService.log('datos eliminados')),
      catchError(this.handleErrorService.handleError<Paciente>('Eliminar Persona', null))
  );
  }
   /** PUT: update the hero on the server */
    update(paciente: Paciente): Observable<any> {
    return this.http.put(this.baseUrl + 'api/Paciente', paciente).pipe(
    tap(_ => this.handleErrorService.log('updated paciente')),
    catchError(this.handleErrorService.handleError<any>('updatePaciente'))
    );
  }
  
}
