# 💻 Frontend – AI Learning Platform

לקוח React + TypeScript למערכת הלמידה המונעת בינה מלאכותית.

---

## 🚀 הרצה מהירה

```bash
npm install
npm run dev
```

---

## ⚙️ הגדרות סביבת עבודה

- יש להגדיר קובץ `.env` בתיקיית `frontend/` עם:
  ```env
  VITE_API_URL=https://localhost:7289/api
  ```
  (או כתובת ה־API שלך)

---

## 🗂️ מבנה עיקרי

```
src/
├── components/   # קומפוננטות עיקריות (Dashboard, AdminDashboard, Register, Login וכו')
├── redux/        # ניהול state עם Redux Toolkit (slices, thunk)
├── services/     # קריאות ל־API
├── App.tsx       # ניתוב ראשי
└── main.tsx      # כניסה לאפליקציה
```

---

## 🛠️ טכנולוגיות עיקריות

- **React** + **TypeScript**
- **Redux Toolkit** (ניהול state)
- **Material-UI (MUI)** (עיצוב)
- **Vite** (פיתוח והרצה מהירה)
- **React Router** (ניווט)
- **JWT** (אימות משתמשים)

---

## 🧑‍💻 טיפים לפיתוח

- להוספת קומפוננטה חדשה:  
  צרו קובץ חדש ב־`src/components/` וייבאו ל־App.
- להוספת state גלובלי:  
  הוסיפו slice חדש ב־`src/redux/` ועדכנו את ה־store.
- לעיצוב:  
  השתמשו ב־Material-UI (MUI) לכל הרכיבים.

---

## 📝 תיעוד API

- כל הקריאות מתבצעות ל־API שמוגדר ב־VITE_API_URL.
- תיעוד מלא של ה־API נמצא ב־[Swagger](https://localhost:7289/swagger/index.html).

---

## 🐞 טיפים ל־Debug

- בדקו את ה־Network ב־DevTools כדי לוודא שה־API עונה.
- אם יש בעיות CORS – ודאו שה־backend מאזין ל־localhost וש־CORS פתוח.

---

## 📦 Scripts עיקריים

| פקודה           | תיאור                       |
|-----------------|----------------------------|
| `npm run dev`   | הרצת פיתוח (localhost)     |
| `npm run build` | בניית פרויקט להעלאה        |
| `npm run preview` | תצוגה של הבילד המקומי    |

---

## 👩‍💻 תרומות

Pull Requests מתקבלים בברכה!

---

## 📩 יצירת קשר

לשאלות, הערות או שיתופי פעולה:  
sari0583225077@gmail.com