import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Book } from '../models/models';

@Injectable({ providedIn: 'root' })
export class BookService {
  private api = `${environment.apiUrl}/books`;

  constructor(private http: HttpClient) {}

  getAll(search?: string, branch?: string) {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (branch) params = params.set('branch', branch);
    return this.http.get<Book[]>(this.api, { params });
  }

  create(book: Partial<Book>) {
    return this.http.post<Book>(this.api, book);
  }

  update(id: number, book: Partial<Book>) {
    return this.http.put(`${this.api}/${id}`, book);
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
