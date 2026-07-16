import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { AuthResponse } from '../models/models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient, private router: Router) {}

  login(studentId: string, password: string) {
    return this.http.post<AuthResponse>(`${this.api}/login`, { studentId, password });
  }

  register(name: string, studentId: string, password: string) {
    return this.http.post<AuthResponse>(`${this.api}/register`, { name, studentId, password });
  }

  saveAuth(auth: AuthResponse) {
    localStorage.setItem('token', auth.token);
    localStorage.setItem('role', auth.role);
    localStorage.setItem('name', auth.name);
    localStorage.setItem('userId', auth.userId.toString());
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  getName(): string {
    return localStorage.getItem('name') || 'User';
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }
}
