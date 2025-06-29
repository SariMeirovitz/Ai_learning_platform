import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { sendPromptThunk } from '../redux/thunk';
import { clearPromptState } from '../redux/promptSlice';
import type { AppDispatch, RootState } from '../redux/store';

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
    <form onSubmit={handleSubmit} style={{ display: 'flex', gap: 8, marginTop: 32 }}>
      <input
        type="text"
        placeholder="מה תרצה לשאול?"
        value={prompt}
        onChange={e => setPrompt(e.target.value)}
        required
        disabled={loading}
      />
      <button type="submit" disabled={loading || !user || !categoryId || !subCategoryId}>
        שלח
      </button>
      {loading && <span>טוען...</span>}
      {error && <span style={{ color: 'red' }}>{error}</span>}
      {response && (
        <span style={{ color: 'green', marginRight: 8 }}>
          {response}
          <button
            type="button"
            onClick={() => dispatch(clearPromptState())}
            style={{ marginRight: 8 }}
          >
            נקה
          </button>
        </span>
      )}
    </form>
  );
}