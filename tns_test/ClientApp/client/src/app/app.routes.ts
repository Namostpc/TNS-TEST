import { Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { UserFormComponent } from './user-form/user-form.component';
// ... Imports Components อื่นๆ

export const routes: Routes = [
    { path: 'users', component: UserListComponent },
    { path: 'users/create', component: UserFormComponent },
    { path: 'users/update', component: UserFormComponent },
    { path: '', redirectTo: '/users', pathMatch: 'full' }
];

