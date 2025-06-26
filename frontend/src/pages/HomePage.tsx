import { Link } from 'react-router-dom';

export default function HomePage() {
  return (
    <div style={{ textAlign: 'center', marginTop: 80 }}>
      <h1>ברוכים הבאים לפלטפורמת הלמידה</h1>
      <Link to="/login">
        <button style={{ margin: 8 }}>התחברות</button>
      </Link>
      <Link to="/register">
        <button style={{ margin: 8 }}>הרשמה</button>
      </Link>
    </div>
  );
}