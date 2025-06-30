# 🎓 AI Learning Platform – Mini MVP

ברוכים הבאים לפרויקט **פלטפורמת למידה מונעת בינה מלאכותית (AI)**  
המערכת מאפשרת למשתמשים לבחור תחום עניין, לשלוח שאלות (prompts) למנוע AI ולקבל שיעורים מותאמים אישית.  
כוללת לוח משתמשים, היסטוריית למידה ולוח ניהול מלא.

---

## 📁 מבנה הפרויקט

```
AI_learning_platform/
├── backend/      # ASP.NET Core Web API (.NET 7/8)
│   └── README.md
├── frontend/     # React + TypeScript client
│   └── README.md
├── docker-compose.yml
└── README.md     # קובץ תיעוד כללי זה
```

---

## 🎯 מטרות הפרויקט

- רישום משתמשים וניהול חשבונות  
- בחירת קטגוריה ותת-קטגוריה ללמידה  
- שליחת prompts למודל OpenAI וקבלת שיעורים  
- צפייה בהיסטוריית הלמידה של המשתמש  
- לוח ניהול (Admin) לניטור משתמשים ופעילות  

---

## 🛠️ טכנולוגיות בשימוש

- **Backend:** ASP.NET Core (C#)
- **Frontend:** React + TypeScript
- **Database:** SQL Server (MDF file)
- **AI Integration:** OpenAI GPT API
- **Authentication:** JWT
- **ניהול תצורה:** appsettings.json / .env
- **Tools:** Node.js + npm (ל־frontend)

---

## ⚙️ דרישות מוקדמות

- [.NET SDK 7/8](https://dotnet.microsoft.com/en-us/download)
- [Node.js + npm](https://nodejs.org/)
- SQL Server Express / LocalDB (או Docker עם SQL Server)
- OpenAI API Key
- Git

---

## 🚀 התקנה והרצה

### 1. שכפול הריפוזיטורי

```bash
git clone https://github.com/SariMeirovitz/Ai_learning_platform.git
cd Ai_learning_platform
```

### 2. הגדרת קובץ סביבה ל־frontend

בנתיב `frontend/`, צרו קובץ `.env` עם התוכן:
```env
VITE_API_URL=http://localhost:5000/api
```

### 3. הגדרת backend

בתוך `backend/`, ודאו שקובץ `appsettings.json` מוגדר עם:
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
ניתן גם להשתמש ב־User Secrets של .NET או בקובץ `appsettings.Development.json`

### 4. הרצת השרתים

**Backend:**
```bash
cd backend
dotnet restore
dotnet run
```

**Frontend:**
```bash
cd ../frontend
npm install
npm run dev
```

---

## 🧾 מבנה בסיס הנתונים

| Table          | Description                          |
|----------------|--------------------------------------|
| users          | משתמשים (id, name, phone)    |
| categories     | קטגוריות ללמידה                   |
| sub_categories | תתי קטגוריות, מקושרות לקטגוריות   |
| prompts        | היסטוריית למידה (prompt, response, created_at) |

כל קשרי הגומלין מוגדרים באמצעות מפתחות זרים

---

## 🔐 אבטחה והרשאות

- ✔️ משתמשים מאומתים באמצעות JWT
- ✔️ ניתן לקבוע הרשאות משתמש (Admin, User)
- ✔️ מסכים מסוימים (כמו לוח הניהול) מוגבלים לגישה רק למנהלים
- ✔️ שמירת הטוקן ב־localStorage והוספתו לבקשות דרך Header Authorization

---

## 🧠 תהליך השימוש

1. הרשמה למערכת
2. התחברות עם שם וטלפון
3. בחירת קטגוריה ותת-קטגוריה
4. שליחת prompt וקבלת תגובת AI
5. צפייה בהיסטוריית למידה
6. לוח ניהול להצגת כלל המשתמשים והפרומפטים (Admin only)

---

## 🐳 Docker (אופציונלי)

אם ברצונך להריץ את מסד הנתונים עם Docker:

```yaml
version: '3.8'

services:
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      SA_PASSWORD: "YourStrong!Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
```

ודאו שמחרוזת החיבור שלכם תואמת ל־Docker:
```
Server=localhost,1433;User=sa;Password=YourStrong!Passw0rd;
```

---

## ✅ פיצ'רים שבוצעו

- ✔️ JWT-based authentication
- ✔️ סינון בלוח ניהול המשתמשים
- ✔️ חיבור מלא ל־OpenAI
- ✔️ הצגת היסטוריית למידה לכל משתמש
- ✔️ הפרדת תפקידים בין משתמש רגיל ומנהל

---

## 📚 תיעוד API עם Swagger

הפרויקט כולל ממשק תיעוד אינטראקטיבי באמצעות **Swagger**.

- לאחר הרצת ה־backend, ניתן לגשת ל־Swagger בדפדפן בכתובת:
  ```
 [https://localhost:7289/swagger/index.html](https://localhost:7289/swagger/index.html)
  ```
- דרך Swagger אפשר לבדוק את כל ה־endpoints, לשלוח בקשות, לראות דוגמאות ולבחון את מבנה ה־API.

**שימוש ב־Swagger מומלץ במיוחד לפיתוח, בדיקות ולמידה על ה־API של המערכת.**

---

## 📈 הצעות לשדרוג עתידי

- בדיקות יחידה (xUnit) ובדיקות קצה
- פריסה מלאה (Vercel, Netlify, Heroku)
- שימוש ב-Docker מלא כולל frontend + backend

---

## 🤝 תרומות

Pull Requests מתקבלים בברכה 🙌  
לפני שינוי משמעותי — נא לפתוח Issue.

---

## 📝 רישיון

פרויקט זה מופץ תחת רישיון MIT. ניתן להשתמש, להעתיק, לשפר ולהפיץ — תוך מתן קרדיט ראוי.

---

## 📩 יצירת קשר

לשאלות, הערות או שיתופי פעולה:

sari0583225077@gmail.com
