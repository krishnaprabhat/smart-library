import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Reservation } from '../models/models';

@Injectable({ providedIn: 'root' })
export class ReservationService {
  private api = `${environment.apiUrl}/reservations`;

  constructor(private http: HttpClient) {}

  reserve(bookId: number) {
    return this.http.post<Reservation>(this.api, { bookId });
  }

  cancel(reservationId: number) {
    return this.http.delete<{ message: string }>(`${this.api}/${reservationId}`);
  }

  getMyReservations() {
    return this.http.get<Reservation[]>(`${this.api}/my`);
  }

  getAllActive() {
    return this.http.get<Reservation[]>(this.api);
  }
}
