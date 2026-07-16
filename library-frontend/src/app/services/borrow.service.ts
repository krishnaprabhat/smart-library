import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BorrowRecord, DashboardStats } from '../models/models';

@Injectable({ providedIn: 'root' })
export class BorrowService {
  private api = `${environment.apiUrl}/borrow`;

  constructor(private http: HttpClient) {}

  borrow(bookId: number) {
    return this.http.post<BorrowRecord>(this.api, { bookId });
  }

  return(borrowId: number) {
    return this.http.post<BorrowRecord>(`${this.api}/return/${borrowId}`, {});
  }

  getMyBorrows() {
    return this.http.get<BorrowRecord[]>(`${this.api}/my`);
  }

  getAllActive() {
    return this.http.get<BorrowRecord[]>(`${this.api}/all`);
  }
}

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private api = `${environment.apiUrl}/dashboard`;

  constructor(private http: HttpClient) {}

  getStats() {
    return this.http.get<DashboardStats>(`${this.api}/stats`);
  }

  getActiveBorrows() {
    return this.http.get<BorrowRecord[]>(`${this.api}/active-borrows`);
  }
}
