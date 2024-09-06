import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CreateCustomerRequest } from '../models/create-customer-request.model'; // Asegúrate de que el modelo esté correctamente importado

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'http://localhost:5014';

  constructor(private http: HttpClient) { }

  createCustomer(customer: CreateCustomerRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/customers`, customer)
      .pipe(
        catchError(this.handleError)
      );
  }

  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getCustomerById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/customers/${id}`);
  }

  updateCustomer(id: string, customer: CreateCustomerRequest): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/customers/${id}`, customer);
  }

  deleteCustomer(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/customers/${id}`);
  }

  private handleError(error: any) {
    console.error('An error occurred:', error);
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }

}
