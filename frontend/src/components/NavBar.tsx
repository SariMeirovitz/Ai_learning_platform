import { Link } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import type { RootState } from '../redux/store';
import { clearUser } from '../redux/userSlice';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';

export default function NavBar() {
  const user = useSelector((state: RootState) => state.user.user);
  const dispatch = useDispatch();

  return (
    <AppBar position="static" color="default" elevation={1}>
      <Toolbar sx={{ justifyContent: 'center', gap: 2 }}>
        <Button component={Link} to="/" color="primary">
          בית
        </Button>
        {!user && (
          <Button component={Link} to="/login" color="primary">
            התחברות
          </Button>
        )}
        {!user && (
          <Button component={Link} to="/register" color="primary">
            הרשמה
          </Button>
        )}
        {user && (
          <Button component={Link} to="/dashboard" color="primary">
            אזור אישי
          </Button>
        )}
        {user?.isAdmin === true && (
          <Button component={Link} to="/admin-dashboard" color="primary">
            ניהול
          </Button>
        )}
        {user && (
          <Box sx={{ flexGrow: 0 }}>
            <Button
              color="secondary"
              variant="outlined"
              onClick={() => dispatch(clearUser())}
              sx={{ ml: 2 }}
            >
              התנתק
            </Button>
          </Box>
        )}
      </Toolbar>
    </AppBar>
  );
}