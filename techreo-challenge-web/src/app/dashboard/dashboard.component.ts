import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator'; // Importa el paginator
import { CommonModule } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table'; // Importa MatTableDataSource
import { MatTabsModule } from '@angular/material/tabs'; // Importa MatTabsModule
import { MatTabGroup } from '@angular/material/tabs';
import { MatToolbar } from '@angular/material/toolbar';

import { AccountsResponse } from '../models/accounts-response.model';
import { TransactionResponse } from '../models/transactions-response.model';
import { AccountService } from '../services/account.service';
import { TransactionService } from '../services/transactions.service';
import { AddAccountModalComponent } from '../add-account-modal/add-account-modal.component';
import { TransactionModalComponent } from '../transaction-modal/transaction-modal.component'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  standalone: true,
  imports: [
    MatToolbar,
    MatTabsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ]
})
export class DashboardComponent implements OnInit {
  accounts: AccountsResponse[] = [];
  transactions: TransactionResponse[] = [];
  displayedColumns: string[] = ['alias', 'balance', 'accountNumber', 'clabe', 'actions'];
  displayedColumnsTransactions: string[] = ['date', 'amount', 'description', 'transactionType'];
  dataSource = new MatTableDataSource<AccountsResponse>([]);
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;


  constructor(
    private accountService: AccountService,
    private transactionsService: TransactionService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts(): void {
    this.accountService.getAccounts().subscribe({
      next: (accounts) => {
        this.accounts = accounts;
        this.dataSource.data = this.accounts; // Asigna los datos a dataSource
        if (this.paginator) {
          this.dataSource.paginator = this.paginator; // Asigna el paginator a la dataSource
        }
      },
      error: (err) => {
        console.error('Error loading accounts', err);
      }
    });
  }

  loadTransactions(accountId: string): void {
    this.transactionsService.getTransactions(accountId).subscribe({
      next: (transactions) => {
        this.transactions = transactions;
        this.tabGroup.selectedIndex = 1; // Cambia al tab de transacciones
      },
      error: (err) => {
        console.error('Error loading transactions', err);
      }
    });
  }

  openAddAccountModal(): void {
    const dialogRef = this.dialog.open(AddAccountModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadAccounts();
      }
    });
  }

  openTransactionModal(accountId: string, transactionType: string): void {
    const dialogRef = this.dialog.open(TransactionModalComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (transactionType === 'deposit') {
          this.accountService.deposit(accountId, result).subscribe({
            next: () => this.loadAccounts(),
            error: (err) => console.error('Error making deposit transaction', err)
          });
        } else {
          this.accountService.withdraw(accountId, result).subscribe({
            next: () => this.loadAccounts(),
            error: (err) => console.error('Error making withdraw transaction', err)
          });
        }
      }
    });
  }

  onTabChange(event: any): void {
    // Acciones a realizar al cambiar de pestaña (opcional)
  }
}
