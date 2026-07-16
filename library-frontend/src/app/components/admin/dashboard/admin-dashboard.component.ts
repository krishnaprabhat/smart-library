import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BorrowService, DashboardService } from '../../../services/borrow.service';
import { AuthService } from '../../../services/auth.service';
import { BorrowRecord, DashboardStats } from '../../../models/models';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  stats: DashboardStats | null = null;
  activeBorrows: BorrowRecord[] = [];
  loading = true;

  constructor(
    private dashboardService: DashboardService,
    private borrowService: BorrowService,
    public auth: AuthService
  ) {}

  ngOnInit() { this.loadData(); }

  loadData() {
    this.loading = true;
    this.dashboardService.getStats().subscribe({
      next: (s) => { this.stats = s; },
      error: () => {}
    });
    this.dashboardService.getActiveBorrows().subscribe({
      next: (b) => { this.activeBorrows = b; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  get overdueCount() { return this.activeBorrows.filter(b => b.isOverdue).length; }
}
