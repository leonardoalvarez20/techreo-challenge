import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = 'http://localhost:5014';

  constructor(private http: HttpClient) { }

  getTransactions(accountId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/transactions/${accountId}`);
  }
}
