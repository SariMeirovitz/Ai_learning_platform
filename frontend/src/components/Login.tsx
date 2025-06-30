import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import type { AppDispatch, RootState } from '../redux/store';
import { loginUserThunk } from '../redux/thunk';
import { clearStatus } from '../redux/userSlice';
import { useNavigate, Link } from 'react-router-dom';
import {
  Paper,
  TextField,
  Button,
  Typography,
  CircularProgress,
  Alert,
} from '@mui/material';

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
    <Paper
      elevation={4}
      component="form"
      onSubmit={handleSubmit}
      sx={{
        maxWidth: 350,
        mx: 'auto',
        mt: 10,
        p: 4,
        display: 'flex',
        flexDirection: 'column',
        gap: 2,
        alignItems: 'center',
        direction: 'rtl',
        borderRadius: 3,
        boxShadow: '0 8px 32px 0 rgba(31, 38, 135, 0.15)',
        background: 'rgba(255,255,255,0.95)',
      }}
    >
      <Typography variant="h5" component="h2" gutterBottom sx={{ fontWeight: 700 }}>
        התחברות
      </Typography>
      <TextField
        label="שם"
        value={name}
        onChange={e => setName(e.target.value)}
        required
        fullWidth
        inputProps={{ dir: 'rtl' }}
        autoFocus
      />
      <TextField
        label="טלפון"
        type="tel"
        value={phone}
        onChange={e => setPhone(e.target.value)}
        required
        fullWidth
        inputProps={{ dir: 'rtl' }}
      />
      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        disabled={loading}
        sx={{
          mt: 1,
          fontWeight: 600,
          letterSpacing: 1,
          borderRadius: 2,
          boxShadow: '0 2px 8px 0 rgba(25, 118, 210, 0.08)',
        }}
      >
        התחבר
      </Button>
      {loading && <CircularProgress size={24} sx={{ mt: 1 }} />}
      {error && <Alert severity="error">{error}</Alert>}
      {success && (
        <Alert severity="success">
          התחברת בהצלחה! מעביר אותך לאזור האישי...
        </Alert>
      )}
      <Typography variant="body2" sx={{ mt: 2 }}>
        אין לך חשבון?{' '}
        <Link to="/register" style={{ color: '#1976d2', textDecoration: 'none', fontWeight: 600 }}>
          להרשמה
        </Link>
      </Typography>
    </Paper>
  );
}