import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  mode: 'login' | 'register' = 'login';
  studentId = '';
  password = '';
  name = '';
  error = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    this.error = '';
    this.loading = true;

    if (this.mode === 'login') {
      this.auth.login(this.studentId, this.password).subscribe({
        next: (res) => {
          this.auth.saveAuth(res);
          this.loading = false;
          if (res.role === 'Admin' || res.role === 'Librarian') {
            this.router.navigate(['/admin/dashboard']);
          } else {
            this.router.navigate(['/student/catalog']);
          }
        },
        error: () => {
          this.error = 'Invalid credentials. Please try again.';
          this.loading = false;
        }
      });
    } else {
      this.auth.register(this.name, this.studentId, this.password).subscribe({
        next: (res) => {
          this.auth.saveAuth(res);
          this.router.navigate(['/student/catalog']);
        },
        error: () => {
          this.error = 'Registration failed. Email may already be used.';
          this.loading = false;
        }
      });
    }
  }
}
