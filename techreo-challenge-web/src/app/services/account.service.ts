import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AccountRequest } from '../models/account-request.model'
import { TransactionRequest } from '../models/transaction-request.model'
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = environment.apiUrl; // Ajusta la URL según tu API

  constructor(private http: HttpClient) { }

  getAccounts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/savings-account`);
  }

  getTransactions(accountId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${accountId}/transactions`);
  }

  addAccount(accountData: AccountRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/savings-account`, accountData);
  }

  deposit(accountId: string, transactionRequest: TransactionRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/savings-account/${accountId}/deposit`, transactionRequest);
  }

  withdraw(accountId: string, transactionRequest: TransactionRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/savings-account/${accountId}/withdraw`, transactionRequest);
  }
}
