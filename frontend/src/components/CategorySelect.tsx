import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCategoriesThunk, fetchSubCategoriesThunk } from '../redux/thunk';
import type { AppDispatch, RootState } from '../redux/store';
import type { Category, SubCategory } from '../redux/categorySlice';

interface Props {
  categoryId: number | null;
  subCategoryId: number | null;
  onCategoryChange: (id: number | null) => void;
  onSubCategoryChange: (id: number | null) => void;
}

export default function CategorySelect({
  categoryId,
  subCategoryId,
  onCategoryChange,
  onSubCategoryChange,
}: Props) {
  const dispatch = useDispatch<AppDispatch>();
  const { categories, subCategories, loading, error } = useSelector(
    (state: RootState) => state.category
  );

  useEffect(() => {
    dispatch(fetchCategoriesThunk());
  }, [dispatch]);

  useEffect(() => {
    if (categoryId !== null) {
      dispatch(fetchSubCategoriesThunk(categoryId));
    }
  }, [categoryId, dispatch]);

  // סינון תתי־קטגוריות לפי הקטגוריה שנבחרה
  const filteredSubCategories = subCategories.filter(
    (sub: SubCategory) => sub.categoryId === categoryId
  );

  return (
    <div style={{ display: 'flex', gap: 12, alignItems: 'center', direction: 'rtl' }}>
      <label>
        קטגוריה:&nbsp;
        <select
          value={categoryId ?? ''}
          onChange={e =>
            onCategoryChange(e.target.value ? Number(e.target.value) : null)
          }
          disabled={loading}
        >
          <option value="">בחר קטגוריה</option>
          {categories.map((cat: Category) => (
            <option key={cat.id} value={cat.id}>
              {cat.name}
            </option>
          ))}
        </select>
      </label>
      <label>
        תת־קטגוריה:&nbsp;
        <select
          value={subCategoryId ?? ''}
          onChange={e =>
            onSubCategoryChange(e.target.value ? Number(e.target.value) : null)
          }
          disabled={!categoryId || loading}
        >
          <option value="">בחר תת־קטגוריה</option>
          {filteredSubCategories.map((sub: SubCategory) => (
            <option key={sub.id} value={sub.id}>
              {sub.name}
            </option>
          ))}
        </select>
      </label>
      {loading && <span style={{ marginRight: 8 }}>טוען...</span>}
      {error && <span style={{ color: 'red', marginRight: 8 }}>{error}</span>}
    </div>
  );
}