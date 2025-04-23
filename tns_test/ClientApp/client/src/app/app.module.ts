import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router'; // Import 2  RouterModule และ Routes
import { AppComponent } from './app.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserFormComponent } from './user-form/user-form.component'; 


const routes: Routes = [
    { path: 'users', component: UserListComponent },
    { path: 'users/create', component: UserFormComponent },
    { path: 'users/update', component: UserFormComponent },
    { path: '', redirectTo: '/users', pathMatch: 'full' }
  ];

  @NgModule({
    declarations: [
      AppComponent,
      UserListComponent,
      UserFormComponent
    ],
    imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(routes)
    ],
    providers: [],
    bootstrap: [AppComponent]
  })
  export class AppModule { }