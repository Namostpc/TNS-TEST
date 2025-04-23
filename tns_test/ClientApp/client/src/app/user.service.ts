import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

// กำหนด Interface สำหรับ User (ปรับตามโครงสร้าง JSON ที่ API ของคุณส่งกลับมา)
export interface User {
  userid: number;
  firstname: string;
  lastname: string;
  email: string;
  departmentid: number;
  departmentname: string;
}

// กำหนด Interface สำหรับข้อมูลที่ใช้ในการสร้าง/แก้ไข User (ปรับตาม API ของคุณ)
export interface CreateUpdateUser {
  firstname: string;
  lastname: string;
  email: string;
  department: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5207/api/users'; // แทนที่ YOUR_BACKEND_PORT ด้วย Port ของ Back-end
  constructor(private http: HttpClient) { }

  // ดึงข้อมูล Users ทั้งหมด
  getUsers(userId?: number): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  // ดึงข้อมูล User ตาม ID
  getUseById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/?${id}`);
  }

  // สร้าง User ใหม่
  createUser(user: CreateUpdateUser): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/create`, user);
  }

  // แก้ไข User ตาม ID
  updateUser(userid: number, user: CreateUpdateUser): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/update`, user);
  }

  // ลบ User ตาม ID
  deleteUserById(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete/?id=${id}`);
  }
}
