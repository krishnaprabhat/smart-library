import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../models/models';

@Component({
  selector: 'app-manage-users',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.css']
})
export class ManageUsersComponent implements OnInit {
  users: User[] = [];
  filteredUsers: User[] = [];
  search = '';
  loading = true;
  toast: { message: string; type: 'success' | 'error' } | null = null;

  roles = ['Student', 'Librarian', 'Admin'];

  constructor(private userService: UserService, public auth: AuthService) {}

  ngOnInit() { this.loadUsers(); }

  loadUsers() {
    this.loading = true;
    this.userService.getAll().subscribe({
      next: (u) => { this.users = u; this.applyFilter(); this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  applyFilter() {
    this.filteredUsers = this.users.filter(u =>
      !this.search ||
      u.name.toLowerCase().includes(this.search.toLowerCase()) ||
      u.studentId.toLowerCase().includes(this.search.toLowerCase())
    );
  }

  changeRole(user: User, role: string) {
    this.userService.updateRole(user.id, role).subscribe({
      next: () => { user.role = role; this.showToast(`${user.name}'s role updated to ${role}.`, 'success'); },
      error: () => { this.showToast('Failed to update role.', 'error'); }
    });
  }

  deleteUser(user: User) {
    if (!confirm(`Delete user "${user.name}"? This cannot be undone.`)) return;
    this.userService.delete(user.id).subscribe({
      next: () => { this.users = this.users.filter(u => u.id !== user.id); this.applyFilter(); this.showToast(`User "${user.name}" deleted.`, 'success'); },
      error: () => { this.showToast('Failed to delete user.', 'error'); }
    });
  }

  showToast(message: string, type: 'success' | 'error') {
    this.toast = { message, type };
    setTimeout(() => (this.toast = null), 3500);
  }
}
