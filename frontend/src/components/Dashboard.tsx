import { useState } from 'react';
import { useSelector } from 'react-redux';
import type { RootState } from '../redux/store';
import CategorySelect from '../components/CategorySelect';
import PromptForm from '../components/PromptForm';

export default function Dashboard() {
  const [categoryId, setCategoryId] = useState<number | null>(null);
  const [subCategoryId, setSubCategoryId] = useState<number | null>(null);
  const user = useSelector((state: RootState) => state.user.user);

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
    </div>
  );
}