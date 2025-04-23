import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router'; 
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, CommonModule], // เพิ่ม RouterOutlet และ RouterLink ใน imports
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'client';
}
