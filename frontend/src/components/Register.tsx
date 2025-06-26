import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { clearStatus } from '../redux/userSlice';
import type { AppDispatch, RootState } from '../redux/store';
import { registerUserThunk } from '../redux/thunk';
import { useNavigate, Link } from 'react-router-dom';

export default function Register() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { loading, error, success } = useSelector((state: RootState) => state.user);
  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(registerUserThunk({ name, phone }));
    setName('');
    setPhone('');
  };

  useEffect(() => {
    if (success) {
      const timer = setTimeout(() => {
        dispatch(clearStatus());
        navigate('/dashboard');
      }, 2000);
      return () => clearTimeout(timer);
    }
    if (error) {
      const timer = setTimeout(() => dispatch(clearStatus()), 3000);
      return () => clearTimeout(timer);
    }
  }, [success, error, dispatch, navigate]);

  return (
    <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: 12, alignItems: 'center', direction: 'rtl', maxWidth: 320, margin: '0 auto' }}>
      <h2 style={{ margin: 0 }}>הרשמה</h2>
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
      <button type="submit" disabled={loading}>הירשם</button>
      {loading && <span>טוען...</span>}
      {error && <div style={{ color: 'red' }}>{error}</div>}
      {success && <div style={{ color: 'green' }}>נרשמת בהצלחה! מעבירים אותך לאזור האישי...</div>}
      <div style={{ marginTop: 8 }}>
        כבר יש לך חשבון? <Link to="/login">התחבר</Link>
      </div>
    </form>
  );
}