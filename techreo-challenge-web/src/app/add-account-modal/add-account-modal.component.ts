import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';

import { AccountService } from '../services/account.service'


@Component({
  selector: 'app-add-account-modal',
  templateUrl: './add-account-modal.component.html',
  styleUrls: ['./add-account-modal.component.css'],
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AddAccountModalComponent {
  addAccountForm: FormGroup;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddAccountModalComponent>
  ) {
    this.addAccountForm = this.fb.group({
      Alias: ['', Validators.required],
      InitialBalance: [0, Validators.required]
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    console.log(this.addAccountForm.value)
    if (this.addAccountForm.valid) {
      this.accountService.addAccount(this.addAccountForm.value).subscribe({
        next: (response) => {
          console.log('Account added successfully', response);
          this.dialogRef.close(true);
          // Aquí podrías redirigir a otra página o mostrar una notificación
        },
        error: (error) => {
          console.error('Error adding account', error);
          // Manejo de errores
        }
      });
    }
  }
}
