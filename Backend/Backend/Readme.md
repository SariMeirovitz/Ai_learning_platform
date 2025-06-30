# 🖥️ Backend – AI Learning Platform

שרת ה־API של מערכת הלמידה, מבוסס ASP.NET Core, בנוי לפי מודל שכבות:  
**API → BL (Business Logic) → DAL (Data Access Layer)**

---

## 🚀 הרצה מהירה

```bash
dotnet restore
dotnet run
```

---

## 🗂️ מבנה הפרויקט

```
backend/
├── API/    # Controllers – נקודות קצה ל-HTTP (User, Auth, Prompt, Admin, Category)
├── BL/     # Business Logic – שירותים, לוגיקה עסקית, חיבור ל-OpenAI
├── DAL/    # Data Access Layer – גישה למסד נתונים, EF Context, Repositories
├── Models/ # מודלים של הנתונים (DTOs, Entities)
├── Properties/
├── appsettings.json
└── ...
```

---

## ⚙️ הגדרות סביבת עבודה

- ערכי חיבור ל־DB, מפתח OpenAI וטוקן JWT נמצאים ב־`appsettings.json`:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=PATH_TO_MDF_FILE.mdf;Integrated Security=True"
  },
  "OpenAi": {
    "ApiKey": "YOUR_OPENAI_API_KEY"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY"
  }
  ```
- ניתן גם להשתמש ב־User Secrets או בקובץ `appsettings.Development.json`.

---

## 📝 תיעוד API

- תיעוד מלא ב־Swagger:  
  [https://localhost:7289/swagger/index.html](https://localhost:7289/swagger/index.html)
- דרך Swagger אפשר לבדוק את כל ה־endpoints, לשלוח בקשות, לראות דוגמאות ולבחון את מבנה ה־API.

---

## 🧪 בדיקות

אם יש בדיקות:
```bash
dotnet test
```

---

## 🐞 טיפים לפיתוח

- להרצת Migrations:
  ```bash
  dotnet ef migrations add MigrationName
  dotnet ef database update
  ```
- להריץ על פורט אחר:
  ```bash
  dotnet run --urls "https://localhost:5001"
  ```
- להוספת שכבה/שירות חדש:
  - הוסף קובץ מתאים ל־BL או DAL, והזרק אותו ב־API דרך DI (Dependency Injection).

---

## 👩‍💻 תרומות

Pull Requests מתקבלים בברכה!

---

## 📩 יצירת קשר

לשאלות, הערות או שיתופי פעולה:  
sari0583225077@gmail.com