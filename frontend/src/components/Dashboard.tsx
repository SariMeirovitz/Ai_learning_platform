import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import type { RootState, AppDispatch } from '../redux/store';
import { fetchMyHistoryThunk } from '../redux/thunk';
import CategorySelect from './CategorySelect';
import PromptForm from './PromptForm';
import {
  Container,
  Typography,
  Paper,
  CircularProgress,
  Alert,
  List,
  ListItem,
  ListItemText,
  Divider,
  Box,
} from '@mui/material';

export default function Dashboard() {
  const dispatch = useDispatch<AppDispatch>();
  const [categoryId, setCategoryId] = useState<number | null>(null);
  const [subCategoryId, setSubCategoryId] = useState<number | null>(null);

  const user = useSelector((state: RootState) => state.user.user);
  const prompts = useSelector((state: RootState) => state.prompt.prompts);
  const loading = useSelector((state: RootState) => state.prompt.loading);
  const error = useSelector((state: RootState) => state.prompt.error);

  useEffect(() => {
    dispatch(fetchMyHistoryThunk());
  }, [dispatch]);

  return (
    <Container maxWidth="md" sx={{ mt: 5, fontFamily: "'Assistant', 'Varela Round', Arial, sans-serif" }}>
      <Paper
        elevation={3}
        sx={{
          p: 4,
          background: 'linear-gradient(135deg, #e3f0ff 0%, #f9f9f9 100%)',
          borderRadius: 4,
        }}
      >
        <Typography variant="h4" align="center" gutterBottom sx={{ fontWeight: 700, color: '#1976d2' }}>
          שלום {user?.name}!
        </Typography>

        <Box sx={{ my: 3 }}>
          <CategorySelect
            categoryId={categoryId}
            subCategoryId={subCategoryId}
            onCategoryChange={id => {
              setCategoryId(id);
              setSubCategoryId(null);
            }}
            onSubCategoryChange={setSubCategoryId}
          />
        </Box>

        <PromptForm categoryId={categoryId} subCategoryId={subCategoryId} />

        <Typography variant="h5" sx={{ mt: 5, mb: 2, color: '#1565c0', fontWeight: 600 }}>
          היסטוריית הלמידה שלך
        </Typography>

        {loading ? (
          <Box sx={{ display: 'flex', justifyContent: 'center', my: 3 }}>
            <CircularProgress />
          </Box>
        ) : error ? (
          <Alert severity="error">{error}</Alert>
        ) : prompts.length > 0 ? (
          <Paper
            variant="outlined"
            sx={{
              maxHeight: 350,
              overflow: 'auto',
              p: 2,
              background: 'linear-gradient(90deg, #f5fafd 60%, #e3f0ff 100%)',
              borderRadius: 3,
              boxShadow: '0 2px 8px 0 rgba(25, 118, 210, 0.06)',
            }}
          >
            <List>
              {prompts.map((item, idx) => (
                <Box key={item.id ?? idx}>
                  <ListItem alignItems="flex-start">
                    <ListItemText
                      primary={
                        <>
                          <b style={{ color: '#1976d2' }}>שאלה:</b> {item.prompt}
                          {item.createdAt && (
                            <span style={{ color: '#888', fontSize: 13, marginRight: 8 }}>
                              ({new Date(item.createdAt).toLocaleString('he-IL', {
                                dateStyle: 'short',
                                timeStyle: 'short',
                              })})
                            </span>
                          )}
                        </>
                      }
                      secondary={
                        <>
                          <b style={{ color: '#388e3c' }}>תשובה:</b> {item.response}
                        </>
                      }
                    />
                  </ListItem>
                  {idx < prompts.length - 1 && <Divider />}
                </Box>
              ))}
            </List>
          </Paper>
        ) : (
          <Typography color="text.secondary" align="center">
            אין היסטוריה
          </Typography>
        )}
      </Paper>
    </Container>
  );
}