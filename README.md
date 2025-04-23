# TNS-TEST

สวัสดีครับ ไฟล์นี้จะเป็นการอธิบายโครงสร้างของทั้งทาง Back-end และ Front-end นะครับ

#Back-end
Freamwork ที่ใช้ .NET (ASP.NET Core)
C#: ภาษาโปรแกรมหลักที่ใช้ในการพัฒนา Logic ของส่วนหลังบ้าน
Entity Framework Core (EF Core): Object-Relational Mapper (ORM) ที่ใช้ในการทำงานกับฐานข้อมูล(บางส่วน)
ซึ่ง API ที่ได้ทำการสร้างก็คือ

#Users
Get all User : http://localhost:5207/api/users
Get User By Id : http://localhost:5207/api/users?id=
Create User : http://localhost:5207/api/users/create
Update User : http://localhost:5207/api/users/update
Delete User : http://localhost:5207/api/users/delete?id=4

#Departments
Get all departments: http://localhost:5207/api/department
Get department By Id: http://localhost:5207/api/department?id
Create Department: http://localhost:5207/api/department/create
Update Department: http://localhost:5207/api/department/update

ซึ่ง API ทั้งหมดจะจัดอยู่ในไฟล์ TNS-TEST/tns_test/Controller ครับ
ผมได้ทำการเชื่อมต่อ Database = PostgreSql ครับ

#ก่อนเริ่มรันโปรเจค Back-end
ผู้ทดสอบสามารถรันการทำงานของ Docker ที่มีอยู่ได้ครับ docker-compose-yml
ผู้ทดสอบสามรถกด Ctrl+Shift+P --> Docker compose up และเลือกไฟล์โปรเจคเพื่อทำการใช้ Docker ครับ

#เริ่มรันโปรเจค
พิมพ์ใน Terminal --> dotnet build --> dotnet run

และผู้ทดสอบสามารถทดสอบ API ผ่าน Postman ได้ครับ
โดยการทำสอบ การ Create ทั้งของ User และของ Department ต้องมี Body ครับ
Create User : http://localhost:5207/api/users/create
{
    "firstname": "firstname",
    "lastname": "lastname",
    "email": "email",
    "department": "department"
}

Create Department: http://localhost:5207/api/department/create
{
    "department": "MyDepartment"
}

Update User : http://localhost:5207/api/users/update
{
    "firstname": "firstname",
    "lastname": "lastname",
    "email": "email"
}

Update Department: http://localhost:5207/api/department/update
{
    "department": "old department",
    "newdepartmentname": "new department name"
}



/////////////////////////////////////////////////////////////////////////////////

#ส่วนของ Front-end
Freamwork Angular
Angular CLI
HTML, CSS, TypeScript

ไฟล์ของ Front-end จะถูกจัดอยู่ใน
TNS-TEST/tns_test/ClientApp

เป็นการสร้างหน้าเว็บแบบเรียบง่ายครับ และใช้การเชื่อมต่อ API โดยการใช้ Route ของทางฝั่งหลังบ้านครับ http://localhost:5207/api/users

ตัวหน้า Front-end นี้จะมีแค่การสร้าง User ลบ User และ แก้ไข User เท่านั้นครับ





