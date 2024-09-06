import { Component, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatToolbar } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../services/customer.service';
import { LoginService } from '../services/login.service'
import { Router } from '@angular/router';



@Component({
  selector: 'app-login-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatToolbar,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    CommonModule
  ],
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})

export class LoginRegisterComponent {
  loginForm: FormGroup;
  registerForm: FormGroup;
  showRegisterForm = false; // Controla si se muestra el formulario de registro

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private loginService: LoginService,
    private cdr: ChangeDetectorRef,
    private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      rfc: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordsMatch });
  }

  toggleForm(): void {
    this.showRegisterForm = !this.showRegisterForm;
    this.cdr.detectChanges();
  }

  onLoginSubmit(): void {
    if (this.loginForm.valid) {
      const loginRequestData = this.loginForm.value;
      this.loginService.login(loginRequestData).subscribe({
        next: (response) => {
          console.log('Customer loged in successfully', response);
          localStorage.setItem('token', response.Token);
          const token = localStorage.getItem('token');
          console.log(token)
          // Redirigir al dashboard
          this.router.navigateByUrl('/dashboard');
        },
        error: (error) => {
          console.error('Error creating customer', error);
          // Manejo de errores
        }
      });
    }
  }

  onRegisterSubmit(): void {
    if (this.registerForm.valid) {
      const customerData = this.registerForm.value;
      this.customerService.createCustomer(customerData).subscribe({
        next: (response) => {
          console.log('Customer created successfully', response);
          // Aquí podrías redirigir a otra página o mostrar una notificación
        },
        error: (error) => {
          console.error('Error creating customer', error);
          // Manejo de errores
        }
      });
    }
  }

  passwordsMatch(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { passwordsDontMatch: true };
  }
}
