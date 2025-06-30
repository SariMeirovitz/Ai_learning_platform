import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { clearStatus } from '../redux/userSlice';
import type { AppDispatch, RootState } from '../redux/store';
import { registerUserThunk } from '../redux/thunk';
import { useNavigate, Link } from 'react-router-dom';
import {
  Paper,
  TextField,
  Button,
  Typography,
  CircularProgress,
  Alert,
  Box,
} from '@mui/material';
import { clearPromptState } from '../redux/promptSlice';

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
        dispatch(clearPromptState());
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
    <Box
      sx={{
        maxWidth: 350,
        mx: 'auto',
        mt: 8,
        direction: 'rtl',
      }}
    >
      <Paper
        elevation={3}
        sx={{
          p: 4,
          display: 'flex',
          flexDirection: 'column',
          gap: 2,
          alignItems: 'center',
        }}
      >
        <form
          onSubmit={handleSubmit}
          style={{
            width: '100%',
            display: 'flex',
            flexDirection: 'column',
            gap: 16,
            alignItems: 'center',
          }}
          dir="rtl"
        >
          <Typography variant="h5" component="h2" gutterBottom>
            הרשמה
          </Typography>
          <TextField
            label="שם"
            value={name}
            onChange={e => setName(e.target.value)}
            required
            fullWidth
            inputProps={{ dir: 'rtl' }}
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
            sx={{ mt: 1 }}
          >
            הירשם
          </Button>
          {loading && <CircularProgress size={24} sx={{ mt: 1 }} />}
          {error && <Alert severity="error">{error}</Alert>}
          {success && (
            <Alert severity="success">
              נרשמת בהצלחה! מעבירים אותך לאזור האישי...
            </Alert>
          )}
          <Typography variant="body2" sx={{ mt: 2 }}>
            כבר יש לך חשבון?{' '}
            <Link to="/login" style={{ color: '#1976d2', textDecoration: 'none' }}>
              התחבר
            </Link>
          </Typography>
        </form>
      </Paper>
    </Box>
  );
}