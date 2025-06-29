import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import type { RootState, AppDispatch } from '../redux/store';
import { fetchMyHistoryThunk } from '../redux/thunk';
import CategorySelect from '../components/CategorySelect';
import PromptForm from '../components/PromptForm';

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
    <div style={{ textAlign: 'center', marginTop: 40 }}>
      <h2>שלום {user?.name}!</h2>
      <CategorySelect
        categoryId={categoryId}
        subCategoryId={subCategoryId}
        onCategoryChange={id => {
          setCategoryId(id);
          setSubCategoryId(null);
        }}
        onSubCategoryChange={setSubCategoryId}
      />
      <PromptForm categoryId={categoryId} subCategoryId={subCategoryId} />

      <h3 style={{ marginTop: 40 }}>היסטוריית הלמידה שלך</h3>
      {loading ? (
        <div>טוען היסטוריה...</div>
      ) : error ? (
        <div style={{ color: 'red' }}>שגיאה: {error}</div>
      ) : prompts.length > 0 ? (
        <ul style={{ textAlign: 'right', maxWidth: 600, margin: '0 auto' }}>
          {prompts.map((item, idx) => (
            <li key={item.id ?? idx} style={{ marginBottom: 16, borderBottom: '1px solid #eee', paddingBottom: 8 }}>
              <b>שאלה:</b> {item.prompt} <br />
              <b>תשובה:</b> {item.response}
            </li>
          ))}
        </ul>
      ) : (
        <div style={{ color: '#888' }}>אין היסטוריה</div>
      )}
    </div>
  );
}