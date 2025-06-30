import * as React from 'react';
import { useEffect, useState } from 'react';
import type { AppDispatch, RootState } from '../redux/store';
import { useDispatch, useSelector } from 'react-redux';
import { fetchAllUsersWithPromptsThunk } from '../redux/thunk';
import {
  Container,
  Typography,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  CircularProgress,
  Alert,
  Box,
  Button,
  Collapse,
  List,
  ListItem,
  Divider,
  TextField,
  MenuItem,
} from '@mui/material';

export default function AdminDashboard() {
  const dispatch = useDispatch<AppDispatch>();
  const { users, loading, error } = useSelector((state: any) => state.admin);
  const categories = useSelector((state: RootState) => state.category.categories || []);
  const [openUserId, setOpenUserId] = useState<number | null>(null);

  // סינונים
  const [filterDate, setFilterDate] = useState<string>('');
  const [filterCategory, setFilterCategory] = useState<string>('');

  useEffect(() => {
    dispatch(fetchAllUsersWithPromptsThunk());
  }, [dispatch]);

  const handleToggle = (userId: number) => {
    setOpenUserId(openUserId === userId ? null : userId);
  };

  if (loading)
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 8 }}>
        <CircularProgress />
      </Box>
    );
  if (error)
    return (
      <Container maxWidth="md" sx={{ mt: 5 }}>
        <Alert severity="error">{error}</Alert>
      </Container>
    );

  return (
    <Container maxWidth="lg" sx={{ mt: 5 }}>
      <Paper elevation={3} sx={{ p: 3 }}>
        <Typography variant="h4" align="center" gutterBottom sx={{ fontWeight: 700 }}>
          ניהול משתמשים
        </Typography>

        {/* סרגל סינון */}
        <Box sx={{ display: 'flex', gap: 2, mb: 3, flexWrap: 'wrap', alignItems: 'center', justifyContent: 'center' }}>
          <TextField
            label="סנן לפי תאריך"
            type="date"
            size="small"
            value={filterDate}
            onChange={e => setFilterDate(e.target.value)}
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            label="סנן לפי קטגוריה"
            select
            size="small"
            value={filterCategory}
            onChange={e => setFilterCategory(e.target.value)}
            sx={{ minWidth: 160 }}
          >
            <MenuItem value="">הכל</MenuItem>
            {categories.map((cat: any) => (
              <MenuItem key={cat.id} value={String(cat.id)}>{cat.name}</MenuItem>
            ))}
          </TextField>
        </Box>

        <TableContainer>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell align="right" sx={{ fontWeight: 600 }}>שם</TableCell>
                <TableCell align="right" sx={{ fontWeight: 600 }}>טלפון</TableCell>
                <TableCell align="right" sx={{ fontWeight: 600 }}>היסטוריה</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {users.map((u: any, userIdx: number) => {
                // סינון פרומפטים של כל משתמש
                const filteredPrompts = (u.prompts || []).filter((p: any) => {
                  let dateOk = true, catOk = true;
                  if (filterDate) {
                    dateOk = !!p.createdAt && p.createdAt.startsWith(filterDate);
                  }
                  if (filterCategory) {
                    catOk =
                      String(p.categoryId ?? p.category_id) === filterCategory;
                  }
                  return dateOk && catOk;
                });

                return (
                  <React.Fragment key={u.id ?? userIdx}>
                    <TableRow>
                      <TableCell align="right">{u.name}</TableCell>
                      <TableCell align="right">{u.phone}</TableCell>
                      <TableCell align="right">
                        <Button
                          variant="outlined"
                          size="small"
                          onClick={() => handleToggle(u.id)}
                        >
                          {openUserId === u.id ? 'הסתר היסטוריה' : 'הצג היסטוריה'}
                        </Button>
                      </TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell colSpan={3} sx={{ p: 0, border: 0 }}>
                        <Collapse in={openUserId === u.id} timeout="auto" unmountOnExit>
                          <Box sx={{ bgcolor: '#f5f5f5', p: 2 }}>
                            <Typography variant="subtitle1" sx={{ mb: 1 }}>
                              היסטוריית למידה של {u.name}:
                            </Typography>
                            {filteredPrompts.length > 0 ? (
                              <List dense>
                                {filteredPrompts.map((p: any, promptIdx: number) => (
                                  <Box key={`user${userIdx}-prompt${promptIdx}`}>
                                    <ListItem alignItems="flex-start" sx={{ px: 0 }}>
                                      <Typography variant="body2">
                                        <b>שאלה:</b> {p.prompt}<br />
                                        <b>תשובה:</b> {p.response}
                                        {p.createdAt && (
                                          <span style={{ color: '#888', fontSize: 13, marginRight: 8 }}>
                                            ({new Date(p.createdAt).toLocaleString('he-IL', {
                                              dateStyle: 'short',
                                              timeStyle: 'short',
                                            })})
                                          </span>
                                        )}
                                      </Typography>
                                    </ListItem>
                                    {promptIdx < filteredPrompts.length - 1 && <Divider />}
                                  </Box>
                                ))}
                              </List>
                            ) : (
                              <Typography color="text.secondary" variant="body2">
                                אין היסטוריה מתאימה לסינון
                              </Typography>
                            )}
                          </Box>
                        </Collapse>
                      </TableCell>
                    </TableRow>
                  </React.Fragment>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>
    </Container>
  );
}