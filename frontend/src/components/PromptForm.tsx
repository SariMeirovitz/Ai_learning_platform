import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { sendPromptThunk } from '../redux/thunk';
import { clearPromptState } from '../redux/promptSlice';
import type { AppDispatch, RootState } from '../redux/store';
import { Box, TextField, Button, CircularProgress, Alert, IconButton } from '@mui/material';
import ClearIcon from '@mui/icons-material/Clear';

interface Props {
  categoryId: number | null;
  subCategoryId: number | null;
}

export default function PromptForm({ categoryId, subCategoryId }: Props) {
  const dispatch = useDispatch<AppDispatch>();
  const [prompt, setPrompt] = useState('');
  const { user } = useSelector((state: RootState) => state.user);
  const { loading, error, response } = useSelector((state: RootState) => state.prompt);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!user || !categoryId || !subCategoryId) return;
    dispatch(
      sendPromptThunk({
        categoryId,
        subCategoryId,
        prompt1: prompt,
      })
    );
    setPrompt('');
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit}
      sx={{
        display: 'flex',
        gap: 2,
        alignItems: 'center',
        mt: 4,
        flexWrap: 'wrap',
        justifyContent: 'center',
      }}
    >
      <TextField
        label="מה תרצה לשאול?"
        value={prompt}
        onChange={e => setPrompt(e.target.value)}
        required
        disabled={loading}
        variant="outlined"
        size="small"
        sx={{ minWidth: 250 }}
        inputProps={{ dir: 'rtl' }}
      />
      <Button
        type="submit"
        variant="contained"
        color="primary"
        disabled={loading || !user || !categoryId || !subCategoryId}
        sx={{ minWidth: 80 }}
      >
        שלח
      </Button>
      {loading && <CircularProgress size={24} />}
      {error && <Alert severity="error" sx={{ ml: 2 }}>{error}</Alert>}
      {response && (
        <Alert
          severity="success"
          sx={{ ml: 2, display: 'flex', alignItems: 'center' }}
          action={
            <IconButton
              color="inherit"
              size="small"
              onClick={() => dispatch(clearPromptState())}
            >
              <ClearIcon fontSize="small" />
            </IconButton>
          }
        >
          {response}
        </Alert>
      )}
    </Box>
  );
}