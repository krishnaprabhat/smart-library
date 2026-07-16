import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../models/models';

@Injectable({ providedIn: 'root' })
export class UserService {
  private api = `${environment.apiUrl}/users`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<User[]>(this.api);
  }

  updateRole(id: number, role: string) {
    return this.http.put(`${this.api}/${id}/role`, { role });
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }

  updateProfile(name: string, phone: string, department: string) {
    return this.http.put(`${this.api}/profile`, { name, phone, department });
  }
}
