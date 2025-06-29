import { Link } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import type { RootState } from '../redux/store';
import { clearUser } from '../redux/userSlice';

export default function NavBar() {
  const user = useSelector((state: RootState) => state.user.user);
  const dispatch = useDispatch();

  return (
    <nav style={{
      display: 'flex',
      gap: 16,
      padding: 16,
      background: '#f0f0f0',
      alignItems: 'center',
      justifyContent: 'center'
    }}>
      <Link to="/">בית</Link>
      {!user && <Link to="/login">התחברות</Link>}
      {!user && <Link to="/register">הרשמה</Link>}
      {user && <Link to="/dashboard">אזור אישי</Link>}
      {user?.isAdmin === true && <Link to="/admin-dashboard">ניהול</Link>}
      {user && (
        <button
          onClick={() => dispatch(clearUser())}
          style={{ marginRight: 8 }}
        >
          התנתק
        </button>
      )}
    </nav>
  );
}