import { useState } from 'react';
import CategorySelect from '../components/CategorySelect';
import PromptForm from '../components/PromptForm';

export default function Dashboard() {
  const [categoryId, setCategoryId] = useState<number | null>(null);
  const [subCategoryId, setSubCategoryId] = useState<number | null>(null);

  return (
    <div>
      <h2>אזור אישי</h2>
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