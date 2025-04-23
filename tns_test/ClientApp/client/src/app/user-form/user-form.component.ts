import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { UserService, CreateUpdateUser, User } from '../user.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
})
export class UserFormComponent implements OnInit {
  userForm: FormGroup;
  userId: number = 0;
  isEditMode: boolean = false;
  errorMessage: string = '';
  userToEdit: User | null = null;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.userForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      department: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.userToEdit = history.state.user;
    if (this.userToEdit) {
      this.isEditMode = true;
      this.userId = this.userToEdit.userid;
      this.userForm.patchValue(this.userToEdit);
    } else {
      this.isEditMode = false;
      this.userForm.reset();
    }
  }

  loadUser(): void {
    this.userService.getUsers(this.userId).subscribe({
      next: (user) => this.userForm.patchValue(user),
      error: (error) => this.errorMessage = error.message || 'Failed to load user.',
    });
  }

  onSubmit(): void {
    if (this.userForm.valid && this.userToEdit) {
      const updatedUser: CreateUpdateUser = this.userForm.value;
      this.userService.updateUser(this.userToEdit.userid, updatedUser).subscribe({
        next: () => this.router.navigate(['/users']),
        error: (error) => this.errorMessage = error.message || 'Failed to update user.',
      });
    } else if (this.userForm.valid && !this.isEditMode) {
      const newUser: CreateUpdateUser = this.userForm.value;
      this.userService.createUser(newUser).subscribe({
        next: () => this.router.navigate(['/users']),
        error: (error) => this.errorMessage = error.message || 'Failed to create user.',
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/users']);
  }
}