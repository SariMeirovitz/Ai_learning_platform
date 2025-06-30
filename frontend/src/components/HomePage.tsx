import { Link } from 'react-router-dom';
import { Box, Button, Typography, Paper, Stack } from '@mui/material';

export default function HomePage() {
  return (
    <Box
      sx={{
        minHeight: '100vh',
        background: 'linear-gradient(135deg, #e3f0ff 0%, #f9f9f9 100%)',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }}
    >
      <Paper
        elevation={6}
        sx={{
          p: 5,
          borderRadius: 4,
          minWidth: 320,
          maxWidth: 400,
          textAlign: 'center',
          boxShadow: '0 8px 32px 0 rgba(31, 38, 135, 0.15)',
        }}
      >
        <Typography variant="h3" gutterBottom sx={{ fontWeight: 700, color: '#1976d2' }}>
          ברוכים הבאים
        </Typography>
        <Typography variant="h6" sx={{ mb: 4, color: '#333' }}>
          לפלטפורמת הלמידה
        </Typography>
        <Stack direction="row" spacing={2} justifyContent="center">
          <Button
            component={Link}
            to="/login"
            variant="contained"
            color="primary"
            size="large"
            sx={{ minWidth: 110, fontWeight: 600, borderRadius: 2 }}
          >
            התחברות
          </Button>
          <Button
            component={Link}
            to="/register"
            variant="outlined"
            color="primary"
            size="large"
            sx={{ minWidth: 110, fontWeight: 600, borderRadius: 2 }}
          >
            הרשמה
          </Button>
        </Stack>
      </Paper>
    </Box>
  );
}