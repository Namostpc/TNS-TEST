import { Component, OnInit } from '@angular/core';
import { UserService, User, CreateUpdateUser } from '../user.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  errorMessage: string = '';

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (users) => this.users = users,
      error: (error) => this.errorMessage = error.message || 'Failed to load users.',
    });
  }

  updateUser(user: User): void {
    const userToUpdate: CreateUpdateUser = {
      firstname: user.firstname ? user.firstname : '',
      lastname: user.lastname ? user.lastname : '',
      email: user.email,
      department:  user.departmentname? user.departmentname : ''
    };
    this.router.navigate(['/users/update'], { state: { user: user } });
  }

  deleteUserById(id: number): void {
    this.userService.deleteUserById(id).subscribe({
      next: () => this.loadUsers(),
      error: (error) => this.errorMessage = error.message || 'Failed to delete user.',
    });
  }
}
