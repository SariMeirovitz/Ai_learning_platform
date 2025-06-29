import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import type { AppDispatch, RootState } from '../redux/store';
import { loginUserThunk } from '../redux/thunk';
import { clearStatus } from '../redux/userSlice';
import { useNavigate, Link } from 'react-router-dom';

export default function Login() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { loading, error, success, user } = useSelector((state: RootState) => state.user);
  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(loginUserThunk({ name, phone }));
    setName('');
    setPhone('');
  };

  useEffect(() => {
    if (success && user) {
      const timer = setTimeout(() => {
        dispatch(clearStatus());
        navigate(user.isAdmin ? '/admin-dashboard' : '/dashboard');
      }, 1500);
      return () => clearTimeout(timer);
    }
    if (error) {
      const timer = setTimeout(() => dispatch(clearStatus()), 3000);
      return () => clearTimeout(timer);
    }
  }, [success, error, dispatch, navigate, user]);

  return (
    <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: 12, alignItems: 'center', direction: 'rtl', maxWidth: 320, margin: '0 auto' }}>
      <h2 style={{ margin: 0 }}>התחברות</h2>
      <input
        type="text"
        placeholder="שם"
        value={name}
        onChange={e => setName(e.target.value)}
        required
      />
      <input
        type="tel"
        placeholder="טלפון"
        value={phone}
        onChange={e => setPhone(e.target.value)}
        required
      />
      <button type="submit" disabled={loading}>התחבר</button>
      {loading && <span>טוען...</span>}
      {error && <div style={{ color: 'red' }}>{error}</div>}
      {success && <div style={{ color: 'green' }}>התחברת בהצלחה! מעביר אותך לאזור האישי...</div>}
      <div style={{ marginTop: 8 }}>
        אין לך חשבון? <Link to="/register">להרשמה</Link>
      </div>
    </form>
  );
}